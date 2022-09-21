import React, { useLayoutEffect } from "react";
import "./css/App.css";
import "./css/bootstrap.css";
import "./css/bootstrap.min.css";
import Home from "./pages/Home";
import AOS from "aos";
import "aos/dist/aos.css";
import { Switch, Route, useLocation } from "react-router-dom";
import Room from "./pages/Room";
import ReservationRoom from "./pages/ReservationRoom";
import ReservationEvent from "./pages/ReservationEvent";
import Login from "./pages/Login";
import Profile from "./pages/Profile";
import Errorpages from "./pages/ErrorPages";
import Register from "./pages/Register";
import Protectedroutes from "./components/ProtectedRoutes";

export const DataContext = React.createContext();

function App() {
  const location = useLocation();
  // Scroll to top if path changes
  AOS.init();
  useLayoutEffect(() => {
    window.scrollTo(0, 0);
  }, [location.pathname]);

  return (
    <>
      <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/rooms" component={Room} />
        <Protectedroutes
          exact
          path="/reservation"
          component={ReservationRoom}
        />
        <Protectedroutes exact path="/event" component={ReservationEvent} />
        <Route exact path="/login" component={Login} />
        <Protectedroutes path="/profile" component={Profile} />
        <Route exact path="/info" component={Register} />
        <Route path="*" component={Errorpages} />
      </Switch>
    </>
  );
}

export default App;
