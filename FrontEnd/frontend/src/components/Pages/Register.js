import React from "react";

const Register = () => {
  const handleFormSubmit = (e) => {
    e.preventDefault();

    let username = e.target.elements.username?.value;
    let email = e.target.elements.email?.value;
    let password = e.target.elements.password?.value;

    console.log(username, email, password);
  };

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
          Registration
        </h1>

        <form onSubmit={handleFormSubmit}>
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
            <label htmlFor="email">Email</label>
            <input
              className={classes.input}
              type="email"
              id="email"
              placeholder="Your Email"
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

          <div className={classes.btnContainer}>
            <button className={classes.btn}>Register</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
