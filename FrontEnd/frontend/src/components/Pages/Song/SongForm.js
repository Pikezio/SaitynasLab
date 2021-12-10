import useAxios from "axios-hooks";
import { React, useState, useEffect } from "react";
import { useForm } from "react-hook-form";

const SongForm = ({ token, setShowAddModal, concertId }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [open, setOpen] = useState(true);

  useEffect(() => {
    setOpen(false);
  }, []);

  const url = "http://localhost:1234/api/concerts/" + concertId + "/songs";
  const header = {
    Authorization: `Bearer ${token}`,
  };

  const [{ data, loading, error }, executePost] = useAxios(
    {
      method: "POST",
      url: url,
      headers: header,
    },
    { manual: true }
  );

  const submitForm = (data) => {
    executePost({
      data: {
        title: data.titleField,
        date: data.dateField,
      },
    });
  };

  if (data) {
    setShowAddModal(false);
    window.location.reload();
  }

  if (error) {
    return <h1>Error happened!</h1>;
  } else {
    return (
      <form onSubmit={handleSubmit(submitForm)}>
        <div
          className={`fixed z-10 inset-0 overflow-y-auto ${
            open ? "opacity-0" : "opacity-100"
          } transition-all duration-1000  backdrop-filter backdrop-blur-lg`}
        >
          <div className="flex justify-center h-screen items-center">
            <div className="flex flex-col w-11/12 sm:w-5/6 lg:w-1/2 max-w-2xl mx-auto rounded-lg border border-gray-300 shadow-xl">
              <div className="flex flex-row justify-between p-6 bg-white border-b border-gray-200 rounded-tl-lg rounded-tr-lg">
                <p className="font-semibold text-gray-800">Create a new Song</p>
              </div>
              <div className="flex flex-col px-6 py-5 bg-gray-50">
                <p className="mb-2 font-semibold text-gray-700">Title</p>
                <input
                  type="text"
                  {...register("titleField", { required: true })}
                  placeholder="Song title..."
                  className="p-5 mb-5 bg-white border border-gray-200 rounded shadow-sm"
                ></input>
                {errors.titleField && (
                  <span className="text-red-500">This field is required</span>
                )}
              </div>
              <div className="flex flex-row items-center justify-between p-5 bg-white border-t border-gray-200 rounded-bl-lg rounded-br-lg">
                <button
                  className="font-semibold text-gray-600"
                  type="button"
                  onClick={() => setShowAddModal(false)}
                >
                  Cancel
                </button>
                <button
                  className={`${
                    loading ? "disabled" : ""
                  }  border rounded text-white bg-gray-900
    border-white hover:border-transparent 
    hover:text-teal-500 hover:bg-green-200 mt-4 lg:mt-0 mr-3 px-4 py-2`}
                >
                  {loading ? "Creating" : "Save"}
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
    );
  }
};

export default SongForm;
