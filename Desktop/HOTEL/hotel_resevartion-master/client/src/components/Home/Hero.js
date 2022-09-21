import React from "react";

export default function Hero() {
  return (
    <section className="site-hero overlay" data-stellar-background-ratio="0.5">
      <div className="container">
        <div className="row site-hero-inner justify-content-center align-items-center">
          <div className="col-md-10 text-center" data-aos="fade-up">
            <span className="custom-caption text-uppercase text-white d-block  mb-3">
              Pilar College Mini Hotel
            </span>
            <h1 className="heading">A Nice Place To Stay</h1>
          </div>
        </div>
      </div>

      <a className="mouse" href="#welcome">
        <div className="mouse-icon">
          <span className="mouse-wheel"></span>
        </div>
      </a>
    </section>
  );
}
