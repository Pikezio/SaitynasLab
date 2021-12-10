import React from "react";
import Burger from "./Burger";
import Button from "./Button";
import Logo from "./Logo";
import NavItem from "./NavItem";
import { useState } from "react";
import { useNavigate } from "react-router";

const Navbar = ({ loggedIn, setLoggedIn }) => {
  const navigate = useNavigate();
  const [isActive, setActive] = useState(false);

  const toggleHide = () => {
    setActive(!isActive);
  };

  const handleLogout = () => {
    window.localStorage.clear();
    setLoggedIn(false);
    navigate("login", { replace: true });
  };

  const renderAuthButtons = () => {
    if (loggedIn) {
      return <Button text="Log out" href="/" onClick={handleLogout} />;
    } else {
      return (
        <>
          <Button text="Log in" href="/login" />
          <Button text="Register" href="/register" />
        </>
      );
    }
  };

  return (
    <>
      <nav className="flex items-center justify-between flex-wrap bg-gray-900 p-6">
        <Logo />
        <Burger onClick={toggleHide} />

        <div
          className={`${
            isActive ? "" : "hidden"
          } w-full block flex-grow lg:flex lg:items-center lg:w-auto`}
        >
          <div className="text-sm lg:flex-grow">
            <NavItem text="Home" href="/" />
            {loggedIn && <NavItem text="Concerts" href="/concerts" />}
          </div>
          <div className="flex lg:items-center lg:w-auto">
            {renderAuthButtons()}
          </div>
        </div>
      </nav>
    </>
  );
};

export default Navbar;
