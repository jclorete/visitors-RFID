import "./css/admin.css";
import { Route, Switch } from "react-router-dom";
import Adminlogin from "./pages/AdminLogin";
import "./App.css";
import Admin from "./pages/Admin";

function App() {
  return (
    <>
      <Switch>
        <Route exact path="/" component={Adminlogin} />
        <Route path="/admin" component={Admin} />
      </Switch>
    </>
  );
}

export default App;
