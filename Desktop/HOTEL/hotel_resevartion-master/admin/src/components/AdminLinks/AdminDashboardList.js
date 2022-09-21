import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

const Admindashboardlist = ({
  id,
  name,
  room,
  reservation_date,
  room_price,
  room_status,
  reseravtion_status,
  reservation_checkin,
  reservation_checkout,
  reservation_room_type,
  reservation_adults,
  reservation_children,
  reservation_payment_method,
  reservation_payment_status,
  fk_room_id,
  fk_account_id,
  setReservationData,
}) => {
  var reservDate = reservation_date.split("T16:00:00.000Z");
  console.log(reservation_date);
  const [paymentStatus, setPaymentStatus] = useState({
    reservation_payment_status: reservation_payment_status,
  });
  const [roomNumber, setRoomNumber] = useState(0);

  const handleInputChange = (event) => {
    const room = event.target.value;

    setRoomNumber(() => room);
    console.log(roomNumber);
  };

  const handleCancel = async () => {
    // console.log(reservationData);
    if (window.confirm("Are you sure you wish to cancel?")) {
      await axios
        .put(`http://localhost:4000/reservations/${id}`, {
          reservation_checkin: reservation_checkin,
          reservation_checkout: reservation_checkout,
          reservation_room_type: reservation_room_type,
          reservation_payment_method: reservation_payment_method,
          reservation_payment_status: reservation_payment_status,
          reseravtion_status: "Disapproved",
          reservation_room_number_checkin: 0,
          fk_room_id: fk_room_id,
          fk_account_id: fk_account_id,
        })
        .then((response) => {
          window.location.reload();
        });
    } else {
      return;
    }
  };

  const handleApproved = async (e) => {
    e.preventDefault();

    if (window.confirm("Confirm?")) {
      await axios
        .put(`http://localhost:4000/reservations/${id}`, {
          reservation_checkin: reservation_checkin,
          reservation_checkout: reservation_checkout,
          reservation_room_type: reservation_room_type,
          reservation_adults: reservation_adults,
          reservation_children: reservation_children,
          reservation_payment_method: reservation_payment_method,
          reservation_payment_status: reservation_payment_status,
          reservation_room_number_checkin: roomNumber,
          reseravtion_status: "Approved",
          fk_room_id: fk_room_id,
          fk_account_id: fk_account_id,
        })
        .then((response) => {
          window.location.reload();
        });
    }
  };

  const handleChange = (e) => {
    setPaymentStatus(() => ({ reservation_payment_status: e.target.value }));
    console.log(paymentStatus);
  };
  const SubmitPayment = async (event) => {
    event.preventDefault();
    if (window.confirm("Confirm?")) {
      await axios
        .put(`http://localhost:4000/reservations/${id}`, {
          reservation_checkin: reservation_checkin,
          reservation_checkout: reservation_checkout,
          reservation_room_type: reservation_room_type,
          reservation_adults: reservation_adults,
          reservation_children: reservation_children,
          reservation_payment_method: reservation_payment_method,
          reservation_payment_status: paymentStatus.reservation_payment_status,
          reservation_room_number_checkin: roomNumber,
          reseravtion_status: reseravtion_status,
          fk_room_id: fk_room_id,
          fk_account_id: fk_account_id,
        })
        .then((response) => {
          window.location.reload();
        });
    }
  };

  if (
    reservation_payment_status === "PENDING" ||
    reservation_payment_status === ""
  ) {
    return (
      <div className="rowreservation">
        <div className="column">
          <div className="cardreservation">
            <h3>Reservation</h3>
            <p>Name: {name}</p>
            <p>Room: {room}</p>
            <p>Room Price: Php {room_price}</p>
            <p>Reservation Date: {reservDate}</p>
            <p>Payment Status: </p>
            <div className="select-payment">
              <form className="payment" onSubmit={SubmitPayment}>
                <select
                  className="form-control-1"
                  value={paymentStatus.reservation_payment_status}
                  onChange={handleChange}
                >
                  <option value="PENDING">Pending</option>
                  <option value="Paid">Paid</option>
                  <option value="Cancelled">Cancelled</option>
                </select>
                <div className="buttonPayment">
                  <input type="submit" value="Save" />
                </div>
              </form>
            </div>
            <p>
              Status:{" "}
              <span style={{ fontWeight: "bold" }}> {reseravtion_status}</span>
            </p>

            <div className="input-box">
              <span className="details">Set Room Occupation:</span>
              <input
                className="inputRoom"
                type="text"
                name="roomNumber"
                value={roomNumber}
                onChange={handleInputChange}
              />
            </div>

            <button onClick={handleCancel}>Cancel</button>
            <button className="button-approve" onClick={handleApproved}>
              Aprrove
            </button>
          </div>
        </div>
      </div>
    );
  } else {
    return <div></div>;
  }
};

export default Admindashboardlist;
