import { React, useState } from "react";
import useAxios from "axios-hooks";
import { Link } from "react-router-dom";
import jwt_decode from "jwt-decode";
import ConcertForm from "./ConcertForm";

const Concerts = ({ token }) => {
  const [showModal, setShowModal] = useState(false);

  // Pagal userius pakeisti matomumą.

  const toggleModal = () => {
    setShowModal(!showModal);
  };

  var decoded = jwt_decode(token);
  const userId =
    decoded[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    ];
  const roles =
    decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  let admin = roles.includes("Admin");
  let onlyMusician = !roles.includes("Admin") && !roles.includes("Creator");

  const [{ data, loading, error }] = useAxios(
    {
      url: "http://localhost:1234/api/concerts",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  if (loading) {
    return "Loading";
  }

  if (error) {
    return "Error getting Concerts";
  }

  if (data) {
    var userData = data;
    if (admin) {
      userData = data.sort((a, b) => {
        if (new Date(a.date) > new Date(b.date)) return 1;
        else return -1;
      });
    } else {
      userData = data
        .filter((i) => i.userId === userId)
        .sort((a, b) => {
          if (new Date(a.date) > new Date(b.date)) return 1;
          else return -1;
        });
    }

    return (
      <div className="bg-gray-200 grid justify-items-center">
        <div className="w-3/4 flex justify-content-between">
          <h1 className="flex-1 font-bold text-6xl mt-5 text-left w-3/4">
            Concerts
          </h1>
          {!onlyMusician && (
            <button
              onClick={() => toggleModal()}
              className="hover:text-green-700 mt-11 mr-2 font-bold text-xl"
            >
              Create
            </button>
          )}
        </div>

        <ul className="my-5 border-t-2 border-gray-300 w-3/4  h-screen">
          {userData.map((concert) => (
            <li
              className="p-3 hover:bg-gray-100 bg-gray-300 my-3 rounded-lg"
              key={concert.id}
            >
              <Link to={`${concert.id}`}>
                <div className="flex justify-between">
                  <p className="font-bold">{concert.title}</p>
                  <div className="flex">
                    <p className="font-bold mr-2">
                      {new Date(concert.date).toLocaleDateString("lt-LT")}
                    </p>
                    <p className="font-bold">
                      {new Date(concert.date).toLocaleTimeString("lt-LT")}
                    </p>
                  </div>
                </div>
              </Link>
            </li>
          ))}
        </ul>
        {showModal && <ConcertForm token={token} toggleModal={toggleModal} />}
      </div>
    );
  }
};

export default Concerts;
