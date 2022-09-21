import React, { useState } from "react";
import img_avatar from "../assets/img_avatar2.png";
import "../css/admin.css";
import axios from "axios";
import { toast } from "react-toastify";
import { useHistory } from "react-router-dom";

const Adminlogin = () => {
  let history = useHistory();
  const [adminInfo, setAdminInfo] = useState({
    account_email: "",
    account_password: "",
    account_type: "admin",
  });

  const handleChange = (event) => {
    const name = event.target.name;

    setAdminInfo((values) => ({
      ...values,
      [name]: event.target.value,
    }));

    console.log(adminInfo);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await axios
      .post("http://localhost:4000/accounts/login", adminInfo)
      .then((res) => {
        const id = res.data?.data;
        const account_type = res.data.data[0].account_type;
        console.log(id[0]?.account_id);
        console.log(account_type);
        if (account_type === "admin") {
          toast.success("Success", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
          localStorage.setItem("AdminID", id[0]?.account_id);

          history.push("/admin/dashboard");
        }
      });
  };

  return (
    <form className="admin-login-form" onSubmit={handleSubmit}>
      <div className="imgcontainer">
        <img src={img_avatar} alt="Avatar" className="avatar" />
      </div>

      <div className="admin-container">
        <label htmlFor="uname">
          <b>Admin Email</b>
        </label>
        <input
          className="admin-input"
          type="text"
          placeholder="Enter Username"
          name="account_email"
          value={adminInfo.account_email}
          onChange={handleChange}
          required
        />

        <label htmlFor="psw">
          <b>Password</b>
        </label>
        <input
          className="admin-input"
          type="password"
          placeholder="Enter Password"
          name="account_password"
          value={adminInfo.account_password}
          onChange={handleChange}
          required
        />
        <div className="admin-button">
          <button type="submit">Login</button>
        </div>
      </div>
    </form>
  );
};

export default Adminlogin;
