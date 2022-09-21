import axios from "axios";
import React, { useEffect, useState } from "react";
import "../../css/reservationData.css";
import Reservationlist from "./ReservationList";
const Reservationdata = () => {
  const id = localStorage.getItem("id");
  const [reservationData, setReservationData] = useState([]);

  useEffect(() => {
    async function fetchData() {
      await axios
        .get(`http://localhost:4000/reservations/user/${id}`)
        .then((res) => {
          const data = res.data?.data;
          if (data !== null) setReservationData(data);
        });
    }
    fetchData();
  }, [id, reservationData]);

  return (
    <div className="profileuser-container">
      {reservationData?.map((values) => (
        <Reservationlist
          key={values.reservation_id}
          id={values.reservation_id}
          name={values.account_firstname + " " + values.account_lastname}
          room={values.room_description}
          reservation_date={values.reservation_date}
          room_price={values.room_price}
          room_status={values.reservation_payment_status}
          reservation_status={values.reseravtion_status}
          setReservationData={setReservationData}
        />
      ))}
    </div>
  );
};

export default Reservationdata;
