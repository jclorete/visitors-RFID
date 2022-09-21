import axios from "axios";
import React from "react";
import { toast } from "react-toastify";

const Eventlist = ({
  id,
  name,
  event_date,
  event_approval,
  event_note,
  setEventData,
}) => {
  const date = event_date.split("T16:00:00.000Z");
  const handleCancel = async () => {
    // console.log(reservationData);
    if (window.confirm("Are you sure you wish to cancel?")) {
      await axios
        .delete(`http://localhost:4000/events/${id}`)
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
          setEventData([]);
        });
    } else {
      return;
    }
  };
  return (
    <div className="rowreservation">
      <div className="column">
        <div className="cardreservation">
          <h3>Event</h3>
          <p>Name: {name}</p>
          <p>Event Date: {date}</p>
          <p>Event Status: {event_approval}</p>
          <p>Note: {event_note}</p>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      </div>
    </div>
  );
};
export default Eventlist;
