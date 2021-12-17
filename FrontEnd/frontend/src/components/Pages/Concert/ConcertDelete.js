import { React, useState, useEffect } from "react";
import useAxios from "axios-hooks";

const DeleteModal = ({ token, manualGet, id }) => {
  const [open, setOpen] = useState(true);
  useEffect(() => {
    setOpen(false);
  }, []);

  const [showModal, setShowModal] = useState(false);

  const [{ response }, executeDelete] = useAxios(
    {
      url: "http://localhost:1234/api/concerts/" + id,
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    {
      manual: true,
    }
  );

  if (response) {
    manualGet();
  }

  return (
    <div>
      <button
        className="mr-5 hover:text-red-600"
        onClick={() => {
          setShowModal(true);
        }}
      >
        Delete
      </button>

      {showModal && (
        <div
          className={`fixed z-10 inset-0 overflow-y-auto ${
            open ? "opacity-0" : "opacity-100"
          } transition-all duration-1000  backdrop-filter backdrop-blur-lg`}
        >
          <div className={`flex justify-center h-screen items-center`}>
            <div className="flex flex-col w-11/12 sm:w-5/6 lg:w-1/2 max-w-2xl mx-auto rounded-lg border border-gray-300 shadow-xl">
              <div className="flex flex-row justify-between p-6 bg-white border-b border-gray-200 rounded-tl-lg rounded-tr-lg">
                <p className="font-semibold text-gray-800">Are you sure?</p>
              </div>
              <div className="flex flex-row items-center justify-between p-5 bg-white border-t border-gray-200 rounded-bl-lg rounded-br-lg">
                <button
                  className="font-semibold text-gray-600"
                  onClick={() => {
                    setShowModal(false);
                  }}
                >
                  Cancel
                </button>
                <button
                  onClick={(() => setShowModal(false), executeDelete)}
                  className={`rounded text-white bg-red-700 hover:bg-red-500 mt-4 lg:mt-0 mr-3 px-4 py-2`}
                >
                  Delete
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default DeleteModal;
