import { React, useState, useEffect } from "react";
import useAxios from "axios-hooks";
import PartForm from "./PartForm";
import PartDelete from "./PartDelete";
import PartUpdate from "./PartUpdate";
import axios from "axios";

const PartList = ({ token, concertId, songId }) => {
  const [showForm, setShowForm] = useState(false);
  const [parts, setParts] = useState([]);
  const [error, setError] = useState(null);
  const songUrl =
    "http://localhost:1234/api/concerts/" + concertId + "/songs/" + songId;
  const header = { Authorization: `Bearer ${token}` };

  const getParts = () => {
    axios
      .get(songUrl + "/parts", { headers: header })
      .then((res) => {
        console.log(res.data);
        setParts(res.data);
      })
      .catch((err) => {
        setError(err);
      });
  };

  const [, executeDelete] = useAxios(
    {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    {
      manual: true,
    }
  );

  const [, executePut] = useAxios(
    {
      method: "PUT",
      headers: header,
    },
    { manual: true }
  );

  const updatePart = (id, data) => {
    console.log("Updating: " + id);
    executePut({
      url: songUrl + "/parts/" + id,
      data: data,
    });
    setParts(
      parts.map((part) => {
        if (part.id === id) {
          return { ...part, instrument: data.instrument };
        }
        return part;
      })
    );
  };

  const deletePart = (id) => {
    console.log("Deleteing: " + id);
    executeDelete({
      url: songUrl + "/parts/" + id,
    });
    setParts(parts.filter((part) => part.id !== id));
  };

  useEffect(() => {
    getParts();
  }, []);

  if (error) {
    return "Error getting parts.";
  }

  if (parts) {
    return (
      <div className="grid place-items-center">
        <h1 className="mt-5 mb-2 border-t-2 border-gray-300 text-xl pt-3 w-full">
          PARTS
        </h1>
        <ul className="bg-gray-300 rounded-xl shadow-lg w-max px-5 py-2">
          {parts.length !== 0 ? (
            parts.map((p) => (
              <li className="flex justify-between items-baseline" key={p.id}>
                <div className="text-gray-800 pt-1">{p.instrument}</div>
                <div>
                  <PartDelete deletePart={() => deletePart(p.id)} />
                  <PartUpdate
                    updatePart={updatePart}
                    partId={p.id}
                    oldInfo={p}
                  />
                </div>
              </li>
            ))
          ) : (
            <p className="text-red-800">No parts...</p>
          )}
        </ul>
        {showForm ? (
          <PartForm
            token={token}
            songId={songId}
            concertId={concertId}
            refreshList={getParts}
            setShowForm={setShowForm}
          />
        ) : (
          <button className="mt-3" onClick={() => setShowForm(true)}>
            Add part
          </button>
        )}
      </div>
    );
  } else {
    return <>Loading</>;
  }
};

export default PartList;
