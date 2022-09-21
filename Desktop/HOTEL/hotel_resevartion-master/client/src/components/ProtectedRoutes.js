import React from "react";
import { Redirect, Route } from "react-router-dom";
const Protectedroutes = (props) => {
  const id = localStorage.getItem("id");

  if (id !== null) {
    return <Route {...props} />;
  } else {
    return <Redirect to="/login" />;
  }
};

export default Protectedroutes;
