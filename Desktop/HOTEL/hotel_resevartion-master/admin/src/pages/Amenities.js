import React, { useState, useEffect } from "react";
import axios from "axios";
import { toast } from "react-toastify";
import Amenitieslist from "../components/AmenitiesList";

const Amenities = () => {
  const [amenitiesData, setAmenitiesData] = useState([]);
  const [isFetching, setIsFetching] = useState(false);
  const [amenities, setAmenities] = useState({
    amenities_title: "",
  });

  const handleChange = (event) => {
    const data = event.target.value;
    setAmenities(() => ({
      amenities_title: data,
    }));

    console.log(amenities);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    await axios
      .post("http://localhost:4000/amenities/create", amenities)
      .then((res) => {
        setIsFetching(true);
        toast.success("Success", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        setAmenities("");
      });
  };

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      async function fetchData() {
        await axios
          .get("http://localhost:4000/amenities")
          .then((res) => {
            const data = res.data?.data;
            if (data !== null) {
              setAmenitiesData(data);
            }
            setIsFetching(false);
          })
          .catch(function (error) {
            if (error.response) {
              console.log(error.response.data);
              console.log(error.response.status);
              console.log(error.response.headers);
            }
          });
      }
      fetchData();
    }
    return () => {
      isMounted = false;
    };
  }, [isFetching]);

  return (
    <div className="profileuser-container">
      <h2>Amenities</h2>
      {amenitiesData?.map((values) => (
        <Amenitieslist
          key={values.amenities_id}
          amenities_id={values.amenities_id}
          amenities_title={values.amenities_title}
        />
      ))}
      <div className="main-register-container">
        <div className="register-container">
          <div className="title">Add Amenities</div>
          <div className="content">
            <form onSubmit={handleSubmit}>
              <div className="user-details">
                <div className="input-box">
                  <span className="details">Amenities Name</span>
                  <input
                    className="input-room"
                    type="text"
                    id="room_description"
                    name="room_description"
                    value={amenities.amenities_title}
                    onChange={handleChange}
                  />
                </div>
                <div className="button">
                  <input type="submit" value="Save" />
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Amenities;
