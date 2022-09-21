import React from "react";
import { Link } from "react-router-dom";

export default function Hero() {
  return (
    <>
      <section
        className="site-hero inner-page overlay"
        data-stellar-background-ratio="0.5"
      >
        <div className="container">
          <div className="row site-hero-inner justify-content-center align-items-center">
            <div className="col-md-10 text-center" data-aos="fade">
              <h1 className="heading mb-3">Room Reservation</h1>
              <ul className="custom-breadcrumbs mb-4">
                <li>
                  <Link to="/" className="Home-a">
                    Home
                  </Link>
                </li>
                <li>&bull;</li>
                <li>Reservation</li>
              </ul>
            </div>
          </div>
        </div>

        <a className="mouse smoothscroll" href="#reservation">
          <div className="mouse-icon">
            <span className="mouse-wheel"></span>
          </div>
        </a>
      </section>
    </>
  );
}
