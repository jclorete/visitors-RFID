import React, { useState } from "react";
import "../css/register.css";
import { useHistory, useLocation } from "react-router-dom";
import axios from "axios";
import { toast } from "react-toastify";
const Register = () => {
  const history = useHistory();
  const location = useLocation();
  const { account_email, account_password, account_type } = location.state;
  const [accountInfo, setAccountInfo] = useState({
    account_firstname: "",
    account_lastname: "",
    account_phone: "",
    account_email: account_email,
    account_password: account_password,
    account_type: account_type,
    account_sex: "",
    account_address: "",
  });
  const handleSubmit = async (event) => {
    event.preventDefault();
    await axios
      .post("http://localhost:4000/accounts/create", accountInfo)
      .then((res) => {
        const id = res.data.data;
        console.log(id[0].account_id);
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
      });
  };

  const handleClick = (event) => {
    const sex = event.target.value;
    setAccountInfo((values) => ({
      ...values,
      account_sex: sex,
    }));
    console.log({ accountInfo });
  };

  const handleChange = (event) => {
    const name = event.target.name;

    setAccountInfo((values) => ({
      ...values,
      [name]: event.target.value,
    }));
    console.log({ accountInfo });
  };
  return (
    <div className="main-register-container">
      <div className="register-container">
        <div className="title">Account Information</div>
        <div className="content">
          <form onSubmit={handleSubmit}>
            <div className="user-details">
              <div className="input-box">
                <span className="details">Last Name</span>
                <input
                  type="text"
                  placeholder="e.g Dela Cruz"
                  name="account_lastname"
                  value={accountInfo.account_lastname}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box">
                <span className="details">First Name</span>
                <input
                  type="text"
                  placeholder="e.g Juan "
                  name="account_firstname"
                  value={accountInfo.account_firstname}
                  onChange={handleChange}
                  required
                />
              </div>

              <div className="input-box">
                <span className="details">Phone Number</span>
                <input
                  type="text"
                  placeholder="e.g 09xxxxxxxxx"
                  name="account_phone"
                  value={accountInfo.account_phone}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box">
                <span className="details">Address</span>
                <input
                  type="text"
                  placeholder="House No./Street/Village/Town/City"
                  name="account_address"
                  value={accountInfo.account_address}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="gender-details">
              <input
                type="radio"
                name="account_sex"
                id="dot-1"
                value="Male"
                onClick={handleClick}
              />
              <input
                type="radio"
                name="account_sex"
                id="dot-2"
                value="Female"
                onClick={handleClick}
              />
              <input
                type="radio"
                name="account_sex"
                id="dot-3"
                value="Prefer not to say"
                onClick={handleClick}
              />
              <span className="gender-title">Sex</span>
              <div className="category">
                <label htmlFor="dot-1">
                  <span className="dot one"></span>
                  <span className="gender">Male</span>
                </label>
                <label htmlFor="dot-2">
                  <span className="dot two"></span>
                  <span className="gender">Female</span>
                </label>
                <label htmlFor="dot-3">
                  <span className="dot three"></span>
                  <span className="gender">Prefer not to say</span>
                </label>
              </div>
            </div>
            <div className="button">
              <input type="submit" value="Save" />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Register;
