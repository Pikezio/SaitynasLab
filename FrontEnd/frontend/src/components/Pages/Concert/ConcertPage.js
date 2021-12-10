import { React, useState } from "react";
import useAxios from "axios-hooks";
import { useParams } from "react-router";
import { Link, useNavigate } from "react-router-dom";
import ConcertUpdate from "./ConcertUpdate";
import DeleteModal from "../DeleteModal";
import SongForm from "../Song/SongForm";

const ConcertPage = ({ token }) => {
  const navigate = useNavigate();
  const [songs, setsongs] = useState(null);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showAddModal, setShowAddModal] = useState(false);
  const { id } = useParams();

  const toggleModal = () => {
    setShowUpdateModal(!showUpdateModal);
  };

  const [
    { loading: deleteLoading, error: deleteError, response },
    executeDelete,
  ] = useAxios(
    {
      method: "DELETE",
      url: "http://localhost:1234/api/concerts/" + id,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    {
      manual: true,
    }
  );

  const [{ data, loading, error }] = useAxios(
    {
      url: "http://localhost:1234/api/concerts/" + id,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    {
      useCache: false,
    }
  );

  const [{ data: songsData, loading: songsLoading, error: songsError }] =
    useAxios(
      {
        url: "http://localhost:1234/api/concerts/" + id + "/songs",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
      {
        useCache: false,
      }
    );

  if (songsError) {
    return "Error getting songs.";
  }
  if (songsLoading) {
    return "Loading songs...";
  }
  if (songsData && songs == null) {
    setsongs(songsData);
  }
  if (loading) {
    return "Loading...";
  }
  if (error) {
    return "Error";
  }
  if (deleteError) {
    return "Could not delete.";
  }
  if (response) {
    navigate("/concerts");
  }
  if (data) {
    const date = new Date(data.date);
    return (
      <div className="text-center bg-gray-200 font-semibold grid justify-items-center">
        <div className="p-3">
          <h1 className="font-bold text-4xl mt-5 text-left inline-block">
            {data.title}
          </h1>
          <h1 className="font-bold inline-block ml-10">
            {date.toLocaleDateString("lt-LT")}{" "}
            {date.toLocaleTimeString("lt-LT")}
          </h1>
        </div>
        <div className="">
          <button
            onClick={() => toggleModal()}
            className={`${
              loading && "disabled"
            } font-semibold hover:text-green-800`}
          >
            Update
          </button>
          {" | "}
          <button
            onClick={() => {
              setShowDeleteModal(true);
            }}
            className={`${
              deleteLoading ? "disabled" : ""
            }  font-semibold hover:text-red-800`}
          >
            Delete
          </button>
        </div>

        <ul className="my-5 border-t-2 border-gray-300 w-3/4 h-screen">
          {songs &&
            songs.map((s, i) => (
              <Link to={`songs/${s.id}`}>
                <div className="flex">
                  <div className="flex bg-gray-300 rounded-full h-11 w-11 font-bold text-xl pl-3 pt-2 my-3 mr-2">
                    {i + 1}.
                  </div>
                  <li
                    className="flex-1 hover:bg-gray-100 bg-gray-300 my-2 rounded-lg text-left pl-5 pt-3 pb-3 text-lg ml-2"
                    key={s.id}
                  >
                    {s.title}
                  </li>
                </div>
              </Link>
            ))}
          <button
            onClick={() => setShowAddModal(true)}
            className={`${
              deleteLoading ? "disabled" : ""
            } font-semibold mb-2 hover:text-yellow-600 hover:font-bold`}
          >
            Add
          </button>
        </ul>

        {showUpdateModal && (
          <ConcertUpdate token={token} toggleModal={toggleModal} />
        )}
        {showDeleteModal && (
          <DeleteModal
            setShowDeleteModal={setShowDeleteModal}
            executeDelete={executeDelete}
          />
        )}
        {showAddModal && (
          <SongForm
            token={token}
            setShowAddModal={setShowAddModal}
            concertId={id}
          />
        )}
      </div>
    );
  }
};

export default ConcertPage;
