import React from "react";
import { Link } from "react-router-dom";

const NavItem = ({ text, href }) => {
  return (
    <Link
      to={href}
      className="font-montserrat block mt-4 lg:inline-block 
      lg:mt-0 text-white hover:text-gray-500 mr-4"
    >
      {text}
    </Link>
  );
};

export default NavItem;
