import React from "react";
import { useNavigate } from "react-router-dom";
import useAxios from "axios-hooks";

const Login = ({ setToken, setLoggedIn }) => {
  const url = "http://localhost:1234/api/login";

  const [{ data, error }, loginUser] = useAxios(
    {
      method: "POST",
      url: url,
    },
    { manual: true }
  );

  const navigate = useNavigate();
  const handleFormSubmit = (e) => {
    e.preventDefault();

    loginUser({
      data: {
        username: e.target.elements.username?.value,
        password: e.target.elements.password?.value,
      },
    });
  };

  let badLogin = false;
  if (error) {
    badLogin = true;
  }

  if (data) {
    setToken(data.token);
    setLoggedIn(true);
    navigate("/");
  }

  // Styles
  const classes = {
    pageBody: "h-screen flex bg-gray-200",
    formContainer:
      "w-full max-w-md m-auto bg-white rounded-lg border border-primaryBorder shadow-default py-10 px-16",
    btnContainer: "flex justify-center items-center mt-6",
    btn: "bg-gray-900 py-2 px-4 text-sm text-white rounded border border-green focus:outline-none focus:border-green-dark",
    input:
      "w-full p-2 text-primary border rounded-md outline-none text-sm transition duration-150 ease-in-out mb-4",
  };

  return (
    <div className={classes.pageBody}>
      <div className={classes.formContainer}>
        <h1 className="text-2xl font-medium text-primary mt-4 mb-12 text-center">
          Log in
        </h1>

        <form onSubmit={(e) => handleFormSubmit(e)}>
          <div>
            <label htmlFor="username">Username</label>
            <input
              className={classes.input}
              type="text"
              id="username"
              placeholder="Username"
            />
          </div>
          <div>
            <label htmlFor="password">Password</label>
            <input
              className={classes.input}
              type="password"
              id="password"
              placeholder="Your Password"
            />
          </div>
          <div>{badLogin && "Wrong login information."}</div>

          <div className={classes.btnContainer}>
            <button className={classes.btn}>Login</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
