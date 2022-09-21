import React from "react";
import Sidebar from "../components/sidebar";
import "../css/Profile.css";
import { Switch, Route } from "react-router-dom";
import Profiledata from "../components/ProfileLinks/ProfileData";

import Reservationdata from "../components/ProfileLinks/ReservationData";
import Eventdata from "../components/ProfileLinks/EventData";

const Profile = () => {
  return (
    <>
      <Sidebar />
      <Switch>
        <Route path="/profile/user" component={Profiledata} />
        <Route path="/profile/reservation" component={Reservationdata} />
        <Route path="/profile/event" component={Eventdata} />
      </Switch>
    </>
  );
};

export default Profile;
