import axios from "axios";
import React, { useEffect, useState } from "react";
import EventList from "./EventList";
import "../../css/reservationData.css";
const Eventdata = () => {
  const [eventData, setEventData] = useState([]);
  const id = localStorage.getItem("id");

  useEffect(() => {
    async function fetchData() {
      await axios.get(`http://localhost:4000/events/user/${id}`).then((res) => {
        const data = res.data?.data;

        if (data !== null) setEventData(data);
      });
    }
    fetchData();
  }, [id, eventData]);

  return (
    <div className="profileuser-container">
      {eventData?.map((values) => (
        <EventList
          key={values.event_id}
          id={values.event_id}
          name={values.account_firstname + " " + values.account_lastname}
          event_date={values.event_date}
          event_approval={values.event_approval}
          event_note={values.event_note}
          setEventData={setEventData}
        />
      ))}
    </div>
  );
};

export default Eventdata;
