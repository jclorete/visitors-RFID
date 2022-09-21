import React from "react";
import { Route, Switch } from "react-router-dom";
import Admindashboard from "../components/AdminLinks/AdminDashboard";

import AdminSidebar from "./AdminSidebar";
import "../css/reservationData.css";
import Room from "../components/Room/Room";
import History from "./History";
import Amenities from "./Amenities";

const Sidebar = () => {
  return (
    <>
      <AdminSidebar />
      <Switch>
        <Route exact path="/admin/dashboard" component={Admindashboard} />
        <Route exact path="/admin/room" component={Room} />
        <Route exact path="/admin/history" component={History} />
        <Route path="/admin/amenities" component={Amenities} />
        {/* <Route path="/admin/reservation" component={Adminreservation} /> */}
      </Switch>
    </>
  );
};

export default Sidebar;
