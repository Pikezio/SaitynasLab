import React from "react";
import notes from "../../images/notes.jpg";

const Home = () => {
  return (
    <div className="h-screen bg-gray-200">
      <img
        className="z-10 object-cover h-full w-full bg-fixed"
        src={notes}
        alt="Music Notes"
      />
      <h1 className="lg:text-8xl text-6xl absolute top-96 pt-10 mt-10 w-full text-gray-200 text-center">
        Welcome to NotesApp!
      </h1>
    </div>
  );
};

export default Home;
