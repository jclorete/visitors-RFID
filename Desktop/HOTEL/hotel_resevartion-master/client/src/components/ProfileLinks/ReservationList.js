import axios from "axios";
import React from "react";
import { toast } from "react-toastify";

const Reservationlist = ({
  id,
  name,
  room,
  room_price,
  room_status,
  reservation_status,
  reservation_date,
  setReservationData,
}) => {
  var reservDate = reservation_date.split("T16:00:00.000Z");

  const handleCancel = async () => {
    // console.log(reservationData);
    if (window.confirm("Are you sure you wish to cancel?")) {
      await axios
        .delete(`http://localhost:4000/reservations/${id}`)
        .then((response) => {
          toast.success("Success", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
          setReservationData([]);
        });
    } else {
      return;
    }
  };
  return (
    <div className="rowreservation">
      <div className="column">
        <div className="cardreservation">
          <h3>Reservation</h3>
          <p>Name: {name}</p>
          <p>Room: {room}</p>
          <p>Room Price: Php {room_price}</p>
          <p>Reservation Date: {reservDate}</p>
          <p>Payment Status: {room_status}</p>
          <p>
            Status:{" "}
            <span style={{ fontWeight: "bold" }}> {reservation_status}</span>
          </p>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      </div>
    </div>
  );
};

export default Reservationlist;
