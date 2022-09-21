import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

const Admineventlist = ({
  id,
  name,
  event_date,
  event_checkout,
  event_checkin,
  event_adults,
  event_children,
  fk_account_id,
  event_approval,
  event_note,
  setEventData,
}) => {
  const dateCheckin = event_checkin.split("T16:00:00.000Z");
  const dateCheckOut = event_checkout.split("T16:00:00.000Z");

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

  const handleEventApproved = async (e) => {
    e.preventDefault();

    if (window.confirm("Confirm?")) {
      await axios
        .put(`http://localhost:4000/events/${id}`, {
          event_checkout: event_checkout,
          event_checkin: event_checkin,
          event_adults: event_adults,
          event_children: event_children,
          fk_account_id: fk_account_id,
          event_approval: "Approved",
          event_note: event_note,
        })
        .then((response) => {
          setEventData([]);
        });
    }
  };
  if (event_approval === "PENDING" || event_approval === "") {
    return (
      <div className="rowreservation">
        <div className="column">
          <div className="cardreservation">
            <h3>Event</h3>
            <p>Name: {name}</p>
            <p>Event Date Checkin: {dateCheckin}</p>
            <p>Event Date Checkout: {dateCheckOut}</p>
            <p>Event Status: {event_approval}</p>
            <p>Note: {event_note}</p>
            <button onClick={handleCancel}>Cancel</button>
            <button className="button-approve" onClick={handleEventApproved}>
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

export default Admineventlist;
