import { React, useState, useEffect } from "react";
import { useForm } from "react-hook-form";

const SongUpdate = ({ updatePart, partId, oldInfo }) => {
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm();
  const [open, setOpen] = useState(true);
  const [show, setShow] = useState(false);

  useEffect(() => {
    setOpen(false);
  }, []);

  const submitForm = (data) => {
    updatePart(partId, {
      instrument: data.instrument,
    });
    setShow(false);
  };

  if (oldInfo) {
    setValue("instrument", oldInfo.instrument);
  }

  if (show) {
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
                <p className="font-semibold text-gray-800">Update part</p>
              </div>
              <div className="flex flex-col px-6 py-5 bg-gray-50">
                <p className="mb-2 font-semibold text-gray-700">Instrument</p>
                <input
                  type="text"
                  {...register("instrument", { required: true })}
                  placeholder="Instrument..."
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
                  onClick={() => setShow(false)}
                >
                  Cancel
                </button>
                <button
                  className={` border rounded text-white bg-gray-900
    border-white hover:border-transparent 
    hover:text-teal-500 hover:bg-green-200 mt-4 lg:mt-0 mr-3 px-4 py-2`}
                >
                  Save
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
    );
  } else {
    return (
      <button
        className="text-green-800 hover:text-green-600 font-bold"
        onClick={() => setShow(true)}
      >
        Edit
      </button>
    );
  }
};

export default SongUpdate;
