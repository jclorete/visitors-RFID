import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";

const Header = () => {
  const id = localStorage.getItem("id");
  const [isToggle, setIsToggle] = useState(false);
  const [colorChange, setColorChange] = useState(false);

  const handleClick = () => {
    if (isToggle) {
      setIsToggle(false);
    } else {
      setIsToggle(true);
    }
  };
  const changeNavbarColor = () => {
    if (window.scrollY >= 350) {
      setColorChange(true);
    } else {
      setColorChange(false);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("id");
    window.location.reload();
  };

  useEffect(() => {
    window.addEventListener("scroll", changeNavbarColor);
    return () => {
      window.removeEventListener("scroll", changeNavbarColor);
    };
  }, []);

  return (
    <div className={isToggle ? "menu-open" : ""}>
      <header
        className={
          colorChange
            ? "site-header js-site-header scrolled"
            : "site-header js-site-header"
        }
      >
        <div className="container-fluid">
          <div className="row align-items-center">
            <div className="col-6 col-lg-4 site-logo" data-aos="fade">
              <Link to="/">Pilar College Mini Hotel</Link>
            </div>

            <div className="col-6 col-lg-8">
              <div
                className={
                  isToggle
                    ? "site-menu-toggle js-site-menu-toggle open aos-animate"
                    : " aos-animate site-menu-toggle js-site-menu-toggle"
                }
                data-aos="fade"
                onClick={handleClick}
              >
                <span></span>
                <span></span>
                <span></span>
              </div>

              <div
                style={isToggle ? { display: "block" } : { display: "none" }}
                className={
                  isToggle
                    ? "site-navbar js-site-navbar aos-animate"
                    : "site-navbar js-site-navbar aos-animate"
                }
              >
                <nav role="navigation">
                  <div className="container">
                    <div className="row full-height align-items-center">
                      <div className="col-md-6 mx-auto">
                        <ul className="list-unstyled menu">
                          <li className="active">
                            <Link to="/">Home</Link>
                          </li>
                          <li>
                            <Link to="/rooms">Rooms</Link>
                          </li>

                          {id !== null ? (
                            <li>
                              <Link to="/profile/user">Profile</Link>
                            </li>
                          ) : (
                            ""
                          )}

                          <li>
                            <Link to="/event">Events</Link>
                          </li>
                          <li>
                            <Link to="/reservation">Reservation</Link>
                          </li>
                          <li className="login-li">
                            {id === null ? (
                              <Link to="/login" className="btn-login">
                                Login
                              </Link>
                            ) : (
                              <span
                                className="btn-login"
                                onClick={handleLogout}
                              >
                                Logout
                              </span>
                            )}
                          </li>
                        </ul>
                      </div>
                    </div>
                  </div>
                </nav>
              </div>
            </div>
          </div>
        </div>
      </header>
    </div>
  );
};

export default Header;
