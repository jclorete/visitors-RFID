import React from "react";
import { Link } from "react-router-dom";
export default function Footer() {
  return (
    <>
      <footer className="section footer-section">
        <div className="container">
          <div className="row">
            <div className="col-md-5 mb-5">
              <ul className="list-unstyled link">
                <li>
                  <Link to="/rooms">Rooms</Link>
                </li>
                <li>
                  <Link to="/reservations">Reservation</Link>
                </li>
                <li>
                  <a href="#">About Us</a>
                </li>
              </ul>
            </div>
            <div className="col-md-4 mb-5"></div>
            <div className="col-md-3 mb-5 pr-md-5 contact-info">
              <p>
                <span className="d-block">
                  <span className="ion-ios-location h5 mr-3 text-primary"></span>
                  Address
                </span>{" "}
                <span>
                  {" "}
                  Zamboanga City <br /> Philippines
                </span>
              </p>
              <p>
                <span className="d-block">
                  <span className="ion-ios-telephone h5 mr-3 text-primary"></span>
                  Phone
                </span>{" "}
                <span> (+63) 999-9129-3369</span>
              </p>
              <p>
                <span className="d-block">
                  <span className="ion-ios-email h5 mr-3 text-primary"></span>
                  Email
                </span>{" "}
                <span> info@domain.com</span>
              </p>
            </div>
          </div>
          <div className="">
            <p className="text-left">
              Copyright &copy; {new Date().getFullYear()} All Rights Reserved
            </p>

            {/* <p className="col-md-6 text-right social">
            <a href="#">
              <span className="fa fa-tripadvisor"></span>
            </a>
            <a href="#">
              <span className="fa fa-facebook"></span>
            </a>
            <a href="#">
              <span className="fa fa-twitter"></span>
            </a>
            <a href="#">
              <span className="fa fa-linkedin"></span>
            </a>
            <a href="#">
              <span className="fa fa-vimeo"></span>
            </a>
          </p> */}
          </div>
        </div>
      </footer>
    </>
  );
}
