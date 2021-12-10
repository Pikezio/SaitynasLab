import { React, useState } from "react";
import useAxios from "axios-hooks";
import { useParams } from "react-router";
import { useNavigate } from "react-router-dom";
import DeleteModal from "../DeleteModal";
import SongUpdate from "./SongUpdate";

const SongPage = ({ token }) => {
  const navigate = useNavigate();
  const { concertId, songId } = useParams();
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showUpdateModal, setShowUpdateModal] = useState(false);

  const songUrl =
    "http://localhost:1234/api/concerts/" + concertId + "/songs/" + songId;
  const header = { Authorization: `Bearer ${token}` };

  const [
    { loading: deleteLoading, error: deleteError, response },
    executeDelete,
  ] = useAxios(
    {
      method: "DELETE",
      url: songUrl,
      headers: header,
    },
    {
      manual: true,
    }
  );

  const [{ data: parts }] = useAxios({
    url: songUrl + "/parts",
    headers: header,
  });

  const [{ data, loading, error }] = useAxios(
    {
      url: songUrl,
      headers: header,
    },
    {
      useCache: false,
    }
  );

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
    navigate("/concerts/" + concertId);
  }

  if (data) {
    return (
      <div className="text-center bg-gray-200 font-semibold pb-5">
        <div className="p-3 h-screen">
          <h1 className="font-bold text-4xl mt-5 text-left inline-block mb-3">
            {data.title}
          </h1>
          <br />
          <button
            onClick={() => setShowUpdateModal(true)}
            className={`${
              deleteLoading ? "disabled" : ""
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
            } font-semibold hover:text-red-800`}
          >
            Delete
          </button>
        </div>
        <ul>{parts && parts.map((p) => <li key={p.id}>{p.instrument}</li>)}</ul>
        {showDeleteModal && (
          <DeleteModal
            setShowDeleteModal={setShowDeleteModal}
            executeDelete={executeDelete}
          />
        )}
        {showUpdateModal && (
          <SongUpdate token={token} setShowUpdateModal={setShowUpdateModal} />
        )}
      </div>
    );
  }
};

export default SongPage;
