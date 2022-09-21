import React from "react";
import axios from "axios";

const Amenitieslist = ({ amenities_title, amenities_id }) => {
  const handleCancel = async () => {
    if (window.confirm("Are you sure you wish to remove?")) {
      await axios
        .delete(`http://localhost:4000/amenities/${amenities_id}`)
        .then((response) => {
          window.location.reload();
        });
    } else {
      return;
    }
  };
  return (
    <div className="rowreservation">
      <div className="column">
        <div className="cardreservation">
          <h3>{amenities_title}</h3>

          <button onClick={handleCancel}>Remove</button>
        </div>
      </div>
    </div>
  );
};

export default Amenitieslist;
