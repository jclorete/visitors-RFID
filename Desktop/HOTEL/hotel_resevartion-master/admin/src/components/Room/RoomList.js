import axios from "axios";
import React, { useState } from "react";

const Roomlist = ({
  id,
  room_title,
  room_price,
  room_status,
  room_path,
  children_capacity,
  adult_capacity,
  room_description,
  room_capacity,
}) => {
  const [available, setAvailable] = useState(room_status);

  const handleCancel = async () => {
    // console.log(reservationData);
    if (window.confirm("Are you sure you wish to remove?")) {
      await axios
        .delete(`http://localhost:4000/rooms/${id}`)
        .then((response) => {
          window.location.reload();
        });
    } else {
      return;
    }
  };

  const handleEventApproved = async (e) => {
    e.preventDefault();
    if (window.confirm("Confirm?")) {
      await axios
        .put(`http://localhost:4000/rooms/${id}`, {
          room_title: room_title,
          room_price: room_price,
          room_status: available,
          room_path: room_path,
          children_capacity: children_capacity,
          adult_capacity: adult_capacity,
          room_description: room_description,
          room_capacity: room_capacity,
        })
        .then((response) => {
          window.location.reload();
        });
    }
  };
  const handleChange = (e) => {
    setAvailable(() => e.target.value);
    console.log(available);
  };
  return (
    <div className="rowreservation">
      <div className="column">
        <div className="cardreservation">
          <h3>{room_title}</h3>
          <p>Room Price: Php {room_price}</p>
          <p>Adult Capacity: {adult_capacity}</p>
          <p>Children Capacity: {children_capacity}</p>
          <p>Total Rooms: {room_capacity}</p>
          <p>Room Description: {room_description}</p>
          <select
            className="form-control "
            value={available}
            onChange={handleChange}
          >
            <option value="available">available</option>
            <option value="not available">not available</option>
          </select>

          <button onClick={handleCancel}>Remove</button>
          <button className="button-approve" onClick={handleEventApproved}>
            Ok
          </button>
        </div>
      </div>
    </div>
  );
};

export default Roomlist;
