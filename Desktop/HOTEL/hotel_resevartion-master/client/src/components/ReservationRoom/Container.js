import React, { useEffect, useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";
const Container = () => {
  const id = parseInt(localStorage.getItem("id"));
  const [roomType, setRoomType] = useState([]);
  const [checkinDate, setCheckinDate] = useState("");
  const [checkinOut, setCheckinOut] = useState("");
  const [total, setTotal] = useState({
    totalPayment: "",
  });
  const [reservationData, setReservationData] = useState({
    fk_account_id: id,
    fk_room_id: 0,
    reservation_checkin: "",
    reservation_checkout: "",
    reservation_room_type: "",
    reservation_total_pay: "",
    reservation_payment_method: "Pay Upon Check-in",
  });

  const [checkReservation, setCheckReservation] = useState(0);

  const handleChange = (event) => {
    const name = event.target.name;
    if (name === "reservation_room_type") {
      const value = event.target.value;
      const roomID = roomType
        .filter((obj) => obj.room_title.includes(value))
        .map((obj) => ({ room_id: obj.room_id, room_price: obj.room_price }));

      const room_id = roomID[0].room_id;
      const room_price = roomID[0].room_price;
      setReservationData((values) => ({
        ...values,
        [name]: event.target.value,
        fk_room_id: room_id,
      }));

      setTotal((values) => ({
        totalPayment: parseInt(room_price),
      }));
      console.log(total);
      console.log(room_price);
    } else if (name === "reservation_payment_method") {
      setReservationData((values) => ({
        ...values,
        [name]: event.target.value,
      }));
    } else if (name === "reservation_checkin") {
      let month = new Date(event.target.value);
      setCheckinDate(parseInt(month.getDate()));
      setReservationData((values) => ({
        ...values,
        [name]: event.target.value,
      }));

      console.log(checkinDate);
    } else if (name === "reservation_checkout") {
      let month = new Date(event.target.value);
      setReservationData((values) => ({
        ...values,
        [name]: event.target.value,
      }));

      setCheckinOut(parseInt(month.getDate()));
    }

    console.log(reservationData);
  };

  const ClickRoomType = (event) => {
    const value = event.target.value;
    const roomID = roomType
      .filter((obj) => obj.room_title.includes(value))
      .map((obj) => ({ room_id: obj.room_id }));
    const room_id = roomID[0].room_id;

    setReservationData((values) => ({
      ...values,
      reservation_room_type: event.target.value,
      fk_room_id: room_id,
    }));
  };

  const disablePastDate = () => {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, "0");
    const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
    const yyyy = today.getFullYear();
    return yyyy + "-" + mm + "-" + dd;
  };

  const disableCheckoutDate = () => {
    if (reservationData.reservation_checkin === "") {
      const today = new Date();
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      const yyyy = today.getFullYear();
      return yyyy + "-" + mm + "-" + dd;
    } else {
      const today = new Date(reservationData.reservation_checkin);
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      const yyyy = today.getFullYear();
      return yyyy + "-" + mm + "-" + dd;
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    await setReservationData((values) => ({
      ...values,
      reservation_room_type: total.totalPayment * (checkinOut - checkinDate),
    }));

    if (checkReservation.includes(reservationData.fk_room_id)) {
      toast.info(
        "You already Booked this type of room please wait for the admin to approved your reservation Thank you !",
        {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        }
      );
    } else {
      await axios
        .post("http://localhost:4000/reservations/create", reservationData)
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
      setReservationData({
        fk_account_id: id,
        reservation_checkin: "",
        reservation_checkout: "",
        reservation_adults: 0,
        reservation_children: 0,
        reservation_payment_method: "Pay Upon Check-in",
      });
    }
  };
  async function fetchRoom() {
    axios.get("http://localhost:4000/rooms").then((res) => {
      const data = res.data?.data;
      if (data !== null) {
        setRoomType(data);
        setReservationData((values) => ({
          ...values,
          reservation_room_type: data[0].room_title,
          fk_room_id: data[0].room_id,
        }));
        setTotal(() => ({
          totalPayment: parseInt(data[0].room_price),
        }));
      }
    });
  }

  async function AccountReservation() {
    await axios
      .get(`http://localhost:4000/reservations/user/${id}`)
      .then((res) => {
        const data = res.data?.data;
        const room_id = data.map((values) => values.fk_room_id);
        setCheckReservation(room_id);
      });
  }
  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      fetchRoom();
      AccountReservation();
    }
    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <section className="section contact-section" id="reservation">
      <div className="container">
        <div className="row">
          <div className="col-md-7" data-aos="fade-up" data-aos-delay="100">
            <form
              className="bg-white p-md-5 p-4 mb-5 border"
              onSubmit={handleSubmit}
            >
              <div className="row">
                <div className="col-md-6 form-group">
                  <label className="text-black font-weight-bold" htmlFor="room">
                    Room Type
                  </label>
                  <select
                    id={reservationData.fk_room_id}
                    className="form-control "
                    name="reservation_room_type"
                    value={reservationData.reservation_room_type}
                    onChange={handleChange}
                    onClick={ClickRoomType}
                  >
                    {roomType ? (
                      roomType &&
                      roomType.map((value) => (
                        <option
                          id={value.room_id}
                          key={value.room_id}
                          value={value.room_title}
                        >
                          {value.room_title}
                        </option>
                      ))
                    ) : (
                      <option value="none">None</option>
                    )}
                  </select>
                </div>
                <div className="col-md-6 form-group">
                  <label
                    className="text-black font-weight-bold "
                    htmlFor="payment"
                  >
                    Payment Method
                  </label>
                  <select
                    className="form-control "
                    name="reservation_payment_method"
                    value={reservationData.reservation_payment_method}
                    onChange={handleChange}
                  >
                    <option value="chekin">Pay Upon Check-in</option>
                  </select>
                </div>
              </div>
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
                    id="checkin_date"
                    name="reservation_checkin"
                    className="form-control"
                    value={reservationData.reservation_checkin}
                    onChange={handleChange}
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
                    id="checkout_date"
                    name="reservation_checkout"
                    className="form-control"
                    value={reservationData.reservation_checkout}
                    onChange={handleChange}
                    min={disableCheckoutDate()}
                  />
                </div>
              </div>
              <hr />
              <div className="total-payment">
                <p>
                  {reservationData.reservation_room_type} x {total.totalPayment}{" "}
                  {checkinOut - checkinDate > 0
                    ? "x " + (checkinOut - checkinDate)
                    : ""}
                </p>
                <p>
                  <b>Total</b>{" "}
                  {total.totalPayment * (checkinOut - checkinDate) > 0
                    ? total.totalPayment * (checkinOut - checkinDate)
                    : ""}
                </p>
              </div>
              <div className="row">
                <div className="col-md-6 form-group">
                  <input
                    type="submit"
                    value="Reserve Now"
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
