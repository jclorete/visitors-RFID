import axios from "axios";
import React, { useEffect, useState } from "react";
import AdminDashboardList from "./AdminDashboardList";
import AdminEventList from "./AdminEventList";
const Admindashboard = () => {
  const [reservationData, setReservationData] = useState([]);
  const [eventData, setEventData] = useState([]);

  function getReservationData(e) {
    return axios.get("http://localhost:4000/reservations");
  }

  function getEventData(e) {
    return axios.get("http://localhost:4000/events");
  }

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      async function fetchData() {
        await axios.all([getReservationData(), getEventData()]).then((res) => {
          const data = res[0].data?.data;
          const data2 = res[1].data?.data;
          if (data !== null && data2 !== null) {
            setReservationData(data);
            setEventData(data2);
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
    <>
      <div className="profileuser-container">
        <h2>Reservation</h2>
        {reservationData?.map((values) => (
          <AdminDashboardList
            key={values.reservation_id}
            id={values.reservation_id}
            name={values.account_firstname + " " + values.account_lastname}
            room={values.room_title}
            reservation_date={values.reservation_date}
            room_price={values.room_price}
            room_status={values.reservation_payment_status}
            reseravtion_status={values.reseravtion_status}
            reservation_checkin={values.reservation_checkin}
            reservation_checkout={values.reservation_checkout}
            reservation_payment_method={values.reservation_payment_method}
            reservation_payment_status={values.reservation_payment_status}
            fk_room_id={values.fk_room_id}
            fk_account_id={values.fk_account_id}
            setReservationData={setReservationData}
          />
        ))}
      </div>
      <div className="profileuser-container">
        <h2>Event</h2>
        {eventData?.map((values) => (
          <AdminEventList
            key={values.event_id}
            event_checkin={values.event_checkin}
            event_checkout={values.event_checkout}
            reservation_room_type={values.reservation_room_type}
            event_adults={values.event_adults}
            event_children={values.event_children}
            fk_account_id={values.fk_account_id}
            id={values.event_id}
            name={values.account_firstname + " " + values.account_lastname}
            event_date={values.event_date}
            event_approval={values.event_approval}
            event_note={values.event_note}
            setEventData={setEventData}
          />
        ))}
      </div>
    </>
  );
};

export default Admindashboard;
