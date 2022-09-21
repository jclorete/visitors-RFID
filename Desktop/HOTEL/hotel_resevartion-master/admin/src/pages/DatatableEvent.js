import React from "react";

const Datatableevent = ({ data }) => {
  const columns = data[0] && Object.keys(data[0]);
  return (
    <table cellPadding={0} cellSpacing={0}>
      <thead>
        <tr>
          <th>Event ID</th>
          <th>Event Checkin</th>
          <th>Event Checkout</th>
          <th>Event Data</th>
          <th>Event Amenities</th>
          <th>Event Approval</th>
          <th>Firstname of Client</th>
          <th>Lastname of CLient</th>
          <th>Client's Phone Number</th>
          <th>Client's Email</th>
          <th>Client's Address</th>
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
};

export default Datatableevent;
