import React, { useEffect, useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";
import Multiselect from "multiselect-react-dropdown";

const Container = () => {
  const id = localStorage.getItem("id");
  const [isToggled, setIsToggled] = useState(false);
  const [amenities, setAmenities] = useState([]);
  const [eventData, setEventData] = useState({
    fk_account_id: id,
    event_checkin: "mm/dd/yyyy",
    event_checkout: "mm/dd/yyyy",
    event_adults: 1,
    event_children: 0,
    event_note: "",
    event_amenities: [],
  });

  const handleChange = (event) => {
    const name = event.target.name;
    if (name === "event_checkin") {
      setEventData((values) => ({
        ...values,
        [name]: event.target.value,
      }));
    } else if (name === "event_checkout") {
      setEventData((values) => ({
        ...values,
        [name]: event.target.value,
      }));
    } else if (name === "event_adults") {
      setEventData((values) => ({
        ...values,
        [name]: event.target.value,
      }));
    } else if (name === "event_children") {
      setEventData((values) => ({
        ...values,
        [name]: event.target.value,
      }));
    } else if (name === "event_note") {
      setEventData((values) => ({
        ...values,
        [name]: event.target.value,
      }));
    }

    console.log(eventData);
  };

  const disablePastDate = () => {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, "0");
    const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
    const yyyy = today.getFullYear();
    return yyyy + "-" + mm + "-" + dd;
  };

  const disableCheckoutDate = () => {
    if (eventData.event_checkin === "mm/dd/yyyy") {
      const today = new Date();
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      const yyyy = today.getFullYear();
      return yyyy + "-" + mm + "-" + dd;
    } else {
      const today = new Date(eventData.event_checkin);
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      const yyyy = today.getFullYear();
      return yyyy + "-" + mm + "-" + dd;
    }
  };

  const handleMultiselect = (event) => {
    const amenities = event;
    setEventData((values) => ({
      ...values,
      event_amenities: amenities,
    }));

    console.log(eventData);
  };

  const handleMultiselectRemove = (event) => {
    var array = [...eventData.event_amenities]; // make a separate copy of the array
    var index = array.indexOf(event);
    if (index !== -1) {
      array.splice(index, 1);
      setEventData(array);
    }

    console.log(eventData);
  };

  const handleIsToggled = () => {
    setIsToggled(!isToggled);
    console.log(amenities);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    await axios
      .post("http://localhost:4000/events/create", eventData)
      .then((res) => {
        toast.success("Success", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
      });
  };

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      async function fetchData() {
        await axios
          .get("http://localhost:4000/amenities/title")
          .then((res) => {
            const data = res.data.data;
            if (data !== null) {
              const title = data?.map((values) => values.amenities_title);

              setAmenities(title);
            }
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
  }, []);

  return (
    <section className="section contact-section" id="event">
      <div className="container">
        <div className="row">
          <div className="col-md-7" data-aos="fade-up" data-aos-delay="100">
            <form
              onSubmit={handleSubmit}
              className="bg-white p-md-5 p-4 mb-5 border"
            >
              <div className="row">
                <div className="col-md-6 form-group">
                  <label
                    className="text-black font-weight-bold"
                    htmlFor="checkin_date"
                  >
                    Date Check In
                  </label>
                  <input
                    type="date"
                    name="event_checkin"
                    value={eventData.event_checkin}
                    onChange={handleChange}
                    id="checkin_date"
                    className="form-control"
                    min={disablePastDate()}
                  />
                </div>
                <div className="col-md-6 form-group">
                  <label
                    className="text-black font-weight-bold"
                    htmlFor="checkout_date"
                  >
                    Date Check Out
                  </label>
                  <input
                    type="date"
                    name="event_checkout"
                    value={eventData.event_checkout}
                    onChange={handleChange}
                    id="checkout_date"
                    className="form-control"
                    min={disableCheckoutDate()}
                  />
                </div>
              </div>

              <div className="row">
                <div className="col-md-6 form-group">
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
                      id="adults"
                      className="form-control"
                      name="event_adults"
                      value={eventData.event_adults}
                      onChange={handleChange}
                    >
                      <option value="">0</option>
                      <option value="1">1</option>
                      <option value="2">2</option>
                      <option value="3">3</option>
                      <option value="4">4+</option>
                    </select>
                  </div>
                </div>
                <div className="col-md-6 form-group">
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
                      id="children"
                      className="form-control"
                      name="event_children"
                      value={eventData.event_children}
                      onChange={handleChange}
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
              <div className="">
                <div className=" form-group">
                  <label
                    htmlFor="amenities"
                    className="font-weight-bold text-black"
                  >
                    Amenities
                  </label>
                  <div className="field-icon-wrap">
                    <label className="switch">
                      <input
                        type="checkbox"
                        checked={isToggled}
                        onChange={handleIsToggled}
                      />
                      <span className="slider round" />
                    </label>
                    {isToggled ? (
                      <Multiselect
                        id="children"
                        name="event_children"
                        isObject={false}
                        onSelect={handleMultiselect}
                        onRemove={handleMultiselectRemove}
                        options={amenities}
                      ></Multiselect>
                    ) : (
                      <></>
                    )}
                  </div>
                </div>
              </div>
              <div className="row mb-4">
                <div className="col-md-12 form-group">
                  <label
                    className="text-black font-weight-bold"
                    htmlFor="message"
                  >
                    Event Details
                  </label>
                  <textarea
                    id="message"
                    className="form-control "
                    cols="30"
                    rows="8"
                    name="event_note"
                    value={eventData.event_note}
                    onChange={handleChange}
                  ></textarea>
                </div>
              </div>
              <div className="row">
                <div className="col-md-6 form-group">
                  <input
                    type="submit"
                    value="Request"
                    className="btn btn-primary text-white py-3 px-5 font-weight-bold"
                  />
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Container;
