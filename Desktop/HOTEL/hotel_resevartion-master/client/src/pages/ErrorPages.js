import React from "react";
import Header from "../components/header";
import Footer from "./../components/Footer";
import { Link } from "react-router-dom";

const Errorpages = () => {
  return (
    <div>
      <Header />
      <section
        className="site-hero inner-page overlay"
        data-stellar-background-ratio="0.5"
      >
        <div className="container">
          <div className="row site-hero-inner justify-content-center align-items-center">
            <div className="col-md-10 text-center" data-aos="fade">
              <h1 className="heading mb-3">Oops!!! Are you Lost?</h1>
              <ul className="custom-breadcrumbs mb-4">
                <li>
                  <Link to="/" className="Home-a">
                    Go back
                  </Link>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </div>
  );
};

export default Errorpages;
