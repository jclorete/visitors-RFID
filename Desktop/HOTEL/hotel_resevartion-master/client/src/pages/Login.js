import React, { useState } from "react";
import "../css/login.css";
import { toast } from "react-toastify";
import axios from "axios";
import { useHistory } from "react-router-dom";

const Login = () => {
  let history = useHistory();

  const [isToggle, setIsToggle] = useState(false);
  const [loginInfo, setLoginInfo] = useState({
    account_email: "",
    account_password: "",
    account_type: "user",
  });

  const handleClick = () => {
    if (isToggle) {
      setIsToggle(false);
    } else {
      setIsToggle(true);
    }
  };

  const handleChange = (event) => {
    const name = event.target.name;
    if (name === "username") {
      setLoginInfo((values) => ({
        ...values,
        account_email: event.target.value,
      }));
    } else if (name === "pass") {
      setLoginInfo((values) => ({
        ...values,
        account_password: event.target.value,
      }));
    }
  };

  const handleRegister = async (event) => {
    event.preventDefault();
    history.push({
      pathname: "/info",
      state: {
        account_email: loginInfo.account_email,
        account_password: loginInfo.account_password,
        account_type: "user",
      },
    });
  };

  const handleLogin = async (event) => {
    event.preventDefault();
    await axios
      .post("http://localhost:4000/accounts/login", loginInfo)
      .then((res) => {
        const id = res.data?.data;
        const account_type = res.data.data[0].account_type;
        console.log(id[0]?.account_id);
        console.log(account_type);
        if (account_type === "user") {
          toast.success("Success", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
          localStorage.setItem("id", id[0]?.account_id);

          history.push("/rooms");
        }
      })
      .catch((error) => {
        // Error
        if (error.response) {
          toast.error("Invalid Email or Password", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
        } else if (error.request) {
          toast.error("Something happened try again", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
          console.log(error.request);
        } else {
          toast.error("Invalid Email or Password", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
        }
      });
  };

  return (
    <div className="main-container">
      <div
        className={
          isToggle ? "login-container right-panel-active " : "login-container"
        }
      >
        <div className="form-container sign-in-container">
          <form onSubmit={handleLogin} className="form">
            <h1 className="form__title">Login</h1>
            <div className="form__input-group">
              <input
                type="text"
                className="form__input"
                name="username"
                required
                placeholder="Email"
                value={loginInfo.account_email}
                onChange={handleChange}
              />
            </div>
            <div className="form__input-group">
              <input
                type="password"
                className="form__input"
                name="pass"
                maxLength="20"
                required
                placeholder="Password"
                value={loginInfo.account_password}
                onChange={handleChange}
              />
            </div>
            <div className="form__input-group">
              <button type="submit" className="form__button">
                Login
              </button>
            </div>
          </form>
        </div>

        <div className="form-container sign-up-container">
          <form onSubmit={handleRegister} className="form">
            <h1 className="form__title">Register</h1>
            <div className="inputs-group">
              <div className="form__input-group">
                <input
                  type="text"
                  className="form__input"
                  name="username"
                  required
                  placeholder="Email"
                  value={loginInfo.account_email}
                  onChange={handleChange}
                />
              </div>
              <div className="form__input-group">
                <input
                  type="password"
                  className="form__input"
                  name="pass"
                  maxLength="20"
                  required
                  placeholder="Password"
                  value={loginInfo.account_password}
                  onChange={handleChange}
                />
              </div>
            </div>

            <button className="form__button" type="submit">
              Register
            </button>
          </form>
        </div>

        <div className="overlay-container">
          <div className="overlay-content">
            <div className="overlay-panel overlay-left">
              <h1>Welcome Back!</h1>
              <p>Please login with your personal info</p>
              <button className="ghost" onClick={handleClick}>
                Sign In
              </button>
            </div>
            <div className="overlay-panel overlay-right">
              <h1>Hello, Friend!</h1>
              <p className="p-tag">
                Enter your personal details and start journey with us
              </p>
              <button className="ghost" onClick={handleClick}>
                Sign Up
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
