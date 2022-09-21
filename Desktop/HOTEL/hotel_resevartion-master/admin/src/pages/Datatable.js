import React from "react";

export default function Datatable({ data }) {
  const columns = data[0] && Object.keys(data[0]);

  return (
    <table cellPadding={0} cellSpacing={0}>
      <thead>
        <tr>
          <th>Reservation ID</th>
          <th>Reservation Checkin</th>
          <th>Reservation Checkout</th>
          <th>Reservation Book Date</th>
          <th>Reservation Payment Method</th>
          <th>Reservation Payment Status</th>
          <th>Reservation Status</th>
          <th>Reservation Room Type</th>
          <th>Room Number Checked in</th>
          <th>Room ID</th>
          <th>Account ID</th>
          <th>Room Title</th>
          <th>Room Description</th>
          <th>Firstname of Client</th>
          <th>Lastname of CLient</th>
        </tr>
      </thead>
      <tbody>
        {data.map((row) => (
          <tr>
            {columns.map((column) => (
              <td>{row[column]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
