import React from "react";
import { Carousel } from "react-bootstrap";
import slider_1 from "../../assets/slider-1.jpg";
import slider_2 from "../../assets/slider-2.jpg";
import slider_3 from "../../assets/slider-3.jpg";
import slider_4 from "../../assets/slider-4.jpg";
import slider_5 from "../../assets/slider-5.jpg";
import slider_6 from "../../assets/slider-6.jpg";
import slider_7 from "../../assets/slider-7.jpg";

export default function CarouselContainer() {
  return (
    <div data-aos="fade-up">
      <Carousel fade>
        <Carousel.Item interval={1000}>
          <img
            width={500}
            height={450}
            className="d-block w-100 carousel"
            src={slider_1}
            alt="First slide"
          />
        </Carousel.Item>
        <Carousel.Item interval={1000}>
          <img
            width={500}
            height={450}
            className="d-block w-100 carousel"
            src={slider_2}
            alt="Second slide"
          />
        </Carousel.Item>
        <Carousel.Item interval={1000}>
          <img
            width={500}
            height={450}
            className="d-block w-100 carousel"
            src={slider_3}
            alt="Third slide"
          />
        </Carousel.Item>
      </Carousel>
    </div>
  );
}
