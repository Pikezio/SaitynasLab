import { React, useState, useEffect } from "react";
import { Routes, Route } from "react-router-dom";
import Login from "./components/Pages/Login";
import Home from "./components/Pages/Home";
import Footer from "./components/Footer";
import Register from "./components/Pages/Register";
import Concerts from "./components/Pages/Concert/Concerts";
import Navbar from "./components/Navbar";
import useToken from "./components/useToken";
import ConcertPage from "./components/Pages/Concert/ConcertPage";
import SongPage from "./components/Pages/Song/SongPage";

const App = () => {
  const { token, setToken, getToken } = useToken();
  const [LoggedIn, setLoggedIn] = useState(false);

  useEffect(() => {
    const userToken = getToken();
    if (userToken) {
      setToken(userToken);
      setLoggedIn(true);
    }
  }, [getToken, setToken]);

  if (LoggedIn) {
    return (
      <>
        <Navbar loggedIn={LoggedIn} setLoggedIn={setLoggedIn} />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/concerts" element={<Concerts token={token} />} />
          <Route path="/concerts/:id" element={<ConcertPage token={token} />} />

          {/* SONGS */}
          <Route
            path="/concerts/:concertId/songs/:songId"
            element={<SongPage token={token} />}
          />
          <Route
            path="*"
            element={
              <main style={{ padding: "1rem" }}>
                <p>There's nothing here!</p>
              </main>
            }
          />
        </Routes>
        <Footer />
      </>
    );
  }

  if (!LoggedIn) {
    return (
      <>
        <Navbar loggedIn={LoggedIn} setLoggedIn={setLoggedIn} />
        <Routes>
          <Route
            path="/login"
            element={<Login setToken={setToken} setLoggedIn={setLoggedIn} />}
          />
          <Route
            path="/register"
            element={<Register setToken={setToken} setLoggedIn={setLoggedIn} />}
          />
        </Routes>
        <Footer />
      </>
    );
  }
};

export default App;
