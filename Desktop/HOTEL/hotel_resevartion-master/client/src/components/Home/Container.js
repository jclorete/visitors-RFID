import React, { useEffect, useState } from "react";
import img_1 from "../../assets/img_1.jpg";
import food_1 from "../../assets/food-1.jpg";

import CarouselContainer from "./CarouselContainer";
import axios from "axios";

const Container = () => {
  const [filePath, setFilepath] = useState([]);
  useEffect(() => {
    axios.get("http://localhost:4000/rooms").then((res) => {
      const data = res.data.data;
      setFilepath(data);
    });
  }, [setFilepath]);
  return (
    <>
      <section className="py-5 bg-light" id="welcome">
        <div className="container">
          <div className="row align-items-center">
            <div
              className="col-md-12 col-lg-7 ml-auto order-lg-2 position-relative mb-5"
              data-aos="fade-up"
            >
              <figure className="img-absolute">
                <img src={food_1} alt="foood" className="img-fluid" />
              </figure>
              <img src={img_1} alt="round-img" className="img-fluid rounded" />
            </div>
            <div className="col-md-12 col-lg-4 order-lg-1" data-aos="fade-up">
              <h2 className="heading">Welcome!</h2>
              <p className="mb-4">
                Lorem ipsum dolor sit amet consectetur adipisicing elit. Ad
                harum beatae aliquid! Ipsa nemo minima culpa aperiam iure velit
                fugiat porro, fuga illum nostrum doloremque. Magnam saepe
                numquam ratione ad?
              </p>
            </div>
          </div>
        </div>
      </section>

      <section className="section">
        <div className="container">
          <div className="row justify-content-center text-center mb-5">
            <div className="col-md-7">
              <h2 className="heading" data-aos="fade-up">
                Rooms &amp; Suites
              </h2>
              <p data-aos="fade-up" data-aos-delay="100">
                Lorem ipsum dolor sit amet consectetur adipisicing elit. Ad
                harum beatae aliquid! Ipsa nemo minima culpa aperiam iure velit
                fugiat porro, fuga illum nostrum doloremque. Magnam saepe
                numquam ratione ad?
              </p>
            </div>
          </div>
          <div className="row">
            {filePath &&
              filePath.map((values) => (
                <div
                  className="col-md-6 col-lg-4"
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
                  </div>
                  <span className="text-uppercase letter-spacing-1">
                    Room Capacity: {values.adult_capacity} Adult{" "}
                    {values.children_capacity} Chilren only
                  </span>
                  <hr />
                  <span className="text-uppercase letter-spacing-1">
                    Room Description: {values.room_description}
                  </span>
                </div>
              ))}
          </div>
        </div>
      </section>

      <section className="section slider-section bg-light">
        <div className="container">
          <div className="row justify-content-center text-center mb-5">
            <div className="col-md-7">
              <h2 className="heading" data-aos="fade-up">
                Photos
              </h2>
              <p data-aos="fade-up" data-aos-delay="100">
                Lorem ipsum dolor sit amet consectetur adipisicing elit. Aut
                laborum ut ea harum voluptatem ducimus repellendus blanditiis!
                Consectetur reiciendis, eaque tenetur saepe, odit perferendis,
                iusto repellendus voluptatibus suscipit veniam ratione?
              </p>
            </div>
          </div>
          <CarouselContainer />
        </div>
      </section>
    </>
  );
};

export default Container;
