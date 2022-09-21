import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import "../css/Profile.css";
const Admindashboard = () => {
  const history = useHistory();
  const [active, setActive] = useState("Profile");

  const handleClick = (event) => {
    const name = event.target.name;
    setActive(name);
    console.log(active);
  };

  const handleLogout = (event) => {
    localStorage.removeItem("id");
    history.push("/");
  };
  return (
    <div className="profile-container">
      <div className="sidebar">
        <header>Dashboard</header>
        <Link
          to="/admin/dashboard"
          className={active === "Profile" ? "active a-hover" : "a-hover"}
          name="Profile"
          onClick={handleClick}
        >
          <span>Dashboard</span>
        </Link>
        <Link
          to="/admin/room"
          className={active === "Room" ? "active a-hover" : "a-hover"}
          name="Room"
          onClick={handleClick}
        >
          <span>Room</span>
        </Link>
        <Link
          to="/admin/amenities"
          className={active === "Amenities" ? "active a-hover" : "a-hover"}
          name="Amenities"
          onClick={handleClick}
        >
          <span>Amenities</span>
        </Link>
        <Link
          to="/admin/history"
          className={active === "History" ? "active a-hover" : "a-hover"}
          name="History"
          onClick={handleClick}
        >
          <span>History</span>
        </Link>

        <span onClick={handleLogout} className="a-hover span">
          <span>Logout</span>
        </span>
      </div>
    </div>
  );
};

export default Admindashboard;
