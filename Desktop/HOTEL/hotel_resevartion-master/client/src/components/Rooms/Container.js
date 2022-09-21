import React, { useEffect, useState } from "react";

import { Link } from "react-router-dom";
import axios from "axios";

export default function Container() {
  const [query, setQuery] = useState(0);
  const [filePath, setFilepath] = useState([]);
  const [dateCheckin, setDateCheckin] = useState("");
  const [isLoading, setIsloading] = useState(false);

  const handleChange = (e) => {
    setDateCheckin(e.target.value);
  };

  const handleChildrenFilter = (e) => {
    const value = e.target.value;
    setQuery(value);
    // console.log(value);
    // const filteredRoom = filePath
    //   .filter((obj) => value <= obj.children_capacity)
    //   .map((obj) => ({
    //     room_id: obj.room_id,
    //     room_description: obj.room_description,
    //     room_status: obj.room_status,
    //     room_price: obj.room_price,
    //     room_path: obj.room_path,
    //     children_capacity: obj.children_capacity,
    //     adult_capacity: obj.adult_capacity,
    //   }));

    // setFilterRooms(filteredRoom);
  };

  const handleAdultFilter = (e) => {
    const value = e.target.value;
    setQuery(value);

    console.log(query);
    // console.log(value);
    // const filteredRoom = filePath
    //   .filter((obj) => value <= obj.adult_capacity)
    //   .map((obj) => ({
    //     room_id: obj.room_id,
    //     room_description: obj.room_description,
    //     room_status: obj.room_status,
    //     room_price: obj.room_price,
    //     room_path: obj.room_path,
    //     children_capacity: obj.children_capacity,
    //     adult_capacity: obj.adult_capacity,
    //   }));

    // setFilterRooms(filteredRoom);
  };

  const disablePastDate = () => {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, "0");
    const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
    const yyyy = today.getFullYear();
    return yyyy + "-" + mm + "-" + dd;
  };

  const disableCheckoutDate = () => {
    if (dateCheckin === "") {
      const today = new Date();
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      const yyyy = today.getFullYear();
      return yyyy + "-" + mm + "-" + dd;
    } else {
      const today = new Date(dateCheckin);
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      const yyyy = today.getFullYear();
      return yyyy + "-" + mm + "-" + dd;
    }
  };

  const handleFilter = (e) => {
    e.preventDefault();

    const value = "available";
    const filteredRoom = filePath
      .filter((obj) => obj.room_status === value)
      .map((obj) => ({
        room_id: obj.room_id,
        room_description: obj.room_description,
        room_status: obj.room_status,
        room_price: obj.room_price,
        room_path: obj.room_path,
        children_capacity: obj.children_capacity,
        adult_capacity: obj.adult_capacity,
      }));

    setFilepath(filteredRoom);
  };

  const getRooms = async () => {
    await axios.get("http://localhost:4000/rooms").then((res) => {
      const data = res.data.data;
      setFilepath(data);
      setIsloading(true);
    });
  };

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      setIsloading(false);
      getRooms();
    }

    return () => {
      isMounted = false;
    };
  }, [setFilepath]);
  return (
    <div>
      <section
        className="site-hero inner-page overlay"
        data-stellar-background-ratio="0.5"
      >
        <div className="container">
          <div className="row site-hero-inner justify-content-center align-items-center">
            <div className="col-md-10 text-center" data-aos="fade">
              <h1 className="heading mb-3">Rooms</h1>
              <ul className="custom-breadcrumbs mb-4">
                <li>
                  <Link to="/" className="Home-a">
                    Home
                  </Link>
                </li>
                <li>&bull;</li>
                <li>Rooms</li>
              </ul>
            </div>
          </div>
        </div>

        <a className="mouse smoothscroll" href="#next">
          <div className="mouse-icon">
            <span className="mouse-wheel"></span>
          </div>
        </a>
      </section>
      <section className="section pb-4">
        <div className="container">
          <div className="row check-availabilty" id="next">
            <div className="block-32" data-aos="fade-up" data-aos-offset="-200">
              <form action="#">
                <div className="row">
                  <div className="col-md-6 mb-3 mb-lg-0 col-lg-3">
                    <label
                      htmlFor="checkin_date"
                      className="font-weight-bold text-black"
                    >
                      Check In
                    </label>
                    <div className="field-icon-wrap">
                      <div className="icon">
                        <span className="icon-calendar"></span>
                      </div>
                      <input
                        type="date"
                        value={dateCheckin}
                        onChange={handleChange}
                        id="checkin_date"
                        className="form-control"
                        required
                        min={disablePastDate()}
                      />
                    </div>
                  </div>
                  <div className="col-md-6 mb-3 mb-lg-0 col-lg-3">
                    <label
                      htmlFor="checkout_date"
                      className="font-weight-bold text-black"
                    >
                      Check Out
                    </label>
                    <div className="field-icon-wrap">
                      <div className="icon">
                        <span className="icon-calendar"></span>
                      </div>
                      <input
                        type="date"
                        id="checkout_date"
                        className="form-control"
                        required
                        min={disableCheckoutDate()}
                      />
                    </div>
                  </div>
                  <div className="col-md-6 mb-3 mb-md-0 col-lg-3">
                    <div className="row">
                      <div className="col-md-6 mb-3 mb-md-0">
                        <label
                          htmlFor="adults"
                          className="font-weight-bold text-black"
                        >
                          Adults
                        </label>
                        <div className="field-icon-wrap">
                          <div className="icon">
                            <span className="ion-ios-arrow-down"></span>
                          </div>
                          <select
                            name=""
                            id="adults"
                            className="form-control"
                            onChange={handleAdultFilter}
                          >
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4+</option>
                          </select>
                        </div>
                      </div>
                      <div className="col-md-6 mb-3 mb-md-0">
                        <label
                          htmlFor="children"
                          className="font-weight-bold text-black"
                        >
                          Children
                        </label>
                        <div className="field-icon-wrap">
                          <div className="icon">
                            <span className="ion-ios-arrow-down"></span>
                          </div>
                          <select
                            name=""
                            id="children"
                            className="form-control"
                            onChange={handleChildrenFilter}
                          >
                            <option value="0">0</option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4+</option>
                          </select>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="col-md-6 col-lg-3 align-self-end">
                    <button
                      className="btn btn-primary btn-block text-white"
                      onClick={handleFilter}
                    >
                      Check Availabilty
                    </button>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </section>
      {isLoading ? (
        <section className="section">
          <div className="container">
            <div className="row">
              {filePath &&
                filePath
                  .filter((value) => {
                    return (
                      query <= value.adult_capacity ||
                      query <= value.adult_capacity
                    );
                  })
                  .map((values) => (
                    <div
                      className="col-md-6 col-lg-4 mb-5"
                      data-aos="fade-up"
                      key={values.room_id}
                    >
                      <figure className="img-wrap">
                        <img
                          src={values.room_path}
                          alt="template"
                          className="img-fluid mb-3"
                        />
                      </figure>
                      <div className="p-3 text-center room-info">
                        <h2>{values.room_title}</h2>
                        <span className="text-uppercase letter-spacing-1">
                          <b> Php {values.room_price} / per night</b>
                        </span>
                        <br />
                        <span className="text-uppercase letter-spacing-1">
                          Room Capacity: {values.adult_capacity} Adult{" "}
                          {values.children_capacity} Chilren only
                        </span>
                        <br />
                        <hr />
                        <span className="text-uppercase letter-spacing-1">
                          Room Description: {values.room_description}
                        </span>
                      </div>
                    </div>
                  ))}
            </div>
          </div>
        </section>
      ) : (
        <section className="section">
          <div className="container">
            <div className="row">
              <h1>Loading...</h1>
            </div>
          </div>
        </section>
      )}
    </div>
  );
}
