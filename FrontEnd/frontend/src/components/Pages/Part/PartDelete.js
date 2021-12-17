import React, { useState } from "react";
import DeleteModal from "../DeleteModal";

const PartDelete = ({ deletePart }) => {
  const [showDeleteModal, setShowDeleteModal] = useState(false);

  return (
    <>
      <button
        className="px-5 text-red-800 hover:text-red-600 font-bold"
        onClick={() => setShowDeleteModal(true)}
      >
        X
      </button>
      {showDeleteModal && (
        <DeleteModal
          setShowDeleteModal={setShowDeleteModal}
          executeDelete={deletePart}
        />
      )}
    </>
  );
};

export default PartDelete;
