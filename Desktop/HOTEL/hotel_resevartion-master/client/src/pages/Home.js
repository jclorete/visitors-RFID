import React from "react";
import Container from "../components/Home/Container";
import Header from "../components/header";
import Hero from "../components/Home/Hero";
import Footer from "../components/Footer";
import { Link } from "react-router-dom";
const Home = () => {
  return (
    <>
      <Header />
      <Hero />
      <Container />
      <section className="section bg-image overlay">
        <div className="container">
          <div className="row align-items-center">
            <div
              className="col-12 col-md-6 text-center mb-4 mb-md-0 text-md-left"
              data-aos="fade-up"
            >
              <h2 className="text-white font-weight-bold">
                A Best Place To Stay. Reserve Now!
              </h2>
            </div>
            <div
              className="col-12 col-md-6 text-center text-md-right"
              data-aos="fade-up"
              data-aos-delay="200"
            >
              <Link
                to="/reservation"
                className="btn btn-outline-white-primary py-3 text-white px-5"
              >
                Reserve Now
              </Link>
            </div>
          </div>
        </div>
      </section>
      <Footer />
    </>
  );
};

export default Home;
