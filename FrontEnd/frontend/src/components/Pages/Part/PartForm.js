import { React } from "react";
import useAxios from "axios-hooks";
import { useForm } from "react-hook-form";

const PartForm = ({ token, concertId, songId, refreshList, setShowForm }) => {
  const songUrl =
    "http://localhost:1234/api/concerts/" + concertId + "/songs/" + songId;
  const header = { Authorization: `Bearer ${token}` };

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const [{ data, loading, error }, executePost] = useAxios(
    {
      method: "POST",
      url: songUrl + "/parts",
      headers: header,
    },
    { manual: true }
  );

  const submitForm = (data) => {
    executePost({
      data: {
        instrument: data.instrument,
      },
    });
  };

  if (error) {
    return "Error adding part.";
  }

  if (loading) {
    return "Loading...";
  }

  if (data) {
    refreshList();
    setShowForm(false);
  }

  return (
    <form onSubmit={handleSubmit(submitForm)}>
      <input
        className="m-3 mt-5 ml-7 p-1 rounded-xl shadow"
        type="text"
        placeholder="new instrument"
        {...register("instrument", { required: true })}
      ></input>
      {errors.instrument && (
        <span className="text-red-500">This field is required</span>
      )}
      <button className="text-green-800 hover:text-green-600">Add</button>
    </form>
  );
};

export default PartForm;
