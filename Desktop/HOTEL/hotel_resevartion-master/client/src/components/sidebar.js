import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";

const Sidebar = () => {
  const history = useHistory();
  const [active, setActive] = useState("Profile");

  const handleClick = (event) => {
    const name = event.target.name;
    setActive(name);
    console.log(name);
  };
  const handleLogout = () => {
    localStorage.removeItem("id");
    history.push("/");
  };
  return (
    <div className="profile-container">
      <div className="sidebar">
        <header>Profile</header>
        <Link to="/" className="a-hover">
          <span>Home</span>
        </Link>
        <Link
          to="/profile/user"
          className={active === "Profile" ? "active a-hover" : "a-hover"}
          name="Profile"
          onClick={handleClick}
        >
          <span>Profile</span>
        </Link>
        <Link
          to="/profile/reservation"
          className={active === "Reservation" ? "active a-hover" : "a-hover"}
          name="Reservation"
          onClick={handleClick}
        >
          <span>Reservation</span>
        </Link>
        <Link
          to="/profile/event"
          className={active === "Events" ? "active a-hover" : "a-hover"}
          name="Events"
          onClick={handleClick}
        >
          <span>Events</span>
        </Link>
        <span onClick={handleLogout} className="a-hover span">
          <span>Logout</span>
        </span>
      </div>
    </div>
  );
};

export default Sidebar;
