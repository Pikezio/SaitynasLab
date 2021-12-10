import React from "react";
import { Link } from "react-router-dom";

const Button = ({ text, href, onClick }) => {
  return (
    <div>
      <Link
        to={href}
        onClick={onClick}
        className="inline-block text-sm px-4 py-2 
        leading-none border rounded text-white 
        border-white hover:border-transparent 
        hover:text-teal-500 hover:bg-gray-300 mt-4 lg:mt-0 mr-3"
      >
        {text}
      </Link>
    </div>
  );
};

export default Button;
