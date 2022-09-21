import axios from "axios";
import React, { useEffect, useState } from "react";
import Datatable from "./Datatable";
import Datatableevent from "./DatatableEvent";
export default function History() {
  const [historyReservationData, setHistoryReservationData] = useState([]);
  const [historyEventData, setHistoryEventData] = useState([]);

  async function fetchReservationData() {
    await axios
      .get("http://localhost:4000/reservations/admin")
      .then((res) => {
        const data = res.data?.data;

        if (data !== null) {
          setHistoryReservationData(data);
        }
      })
      .then((data) => {});
  }

  async function fetchEventData() {
    await axios.get("http://localhost:4000/events/history").then((res) => {
      const data = res.data?.data;

      if (data !== null) {
        setHistoryEventData(data);
      }
    });
  }

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      fetchReservationData();
      fetchEventData();
    }
    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <div className="profileuser-container">
      <h2>Reservation History</h2>
      <Datatable data={historyReservationData} />
      <h2>Event History</h2>
      <Datatableevent data={historyEventData} />
    </div>
  );
}
