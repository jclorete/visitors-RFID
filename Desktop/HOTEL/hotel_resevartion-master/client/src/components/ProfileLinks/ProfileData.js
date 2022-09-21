import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

const Profiledata = () => {
  const id = localStorage.getItem("id");
  const [showPassword, setShowPassword] = useState(false);
  const [accountInfo, setAccountInfo] = useState({
    account_firstname: "Hello",
    account_lastname: "",
    account_phone: "",
    account_email: "account_email",
    account_password: "account_password",
    account_type: "account_type",
    account_sex: "",
    account_address: "",
  });
  const handleSubmit = async (event) => {
    event.preventDefault();
    await axios
      .put(`http://localhost:4000/accounts/${id}`, accountInfo)
      .then((res) => {
        toast.success("Success", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
      });
  };

  const handleClickPassword = (event) => {
    const checked = event.target.checked;
    if (checked) {
      setShowPassword(true);
    } else {
      setShowPassword(false);
    }

    console.log(checked);
  };
  const handleChange = (event) => {
    const name = event.target.name;

    setAccountInfo((values) => ({
      ...values,
      [name]: event.target.value,
    }));
    console.log({ accountInfo });
  };

  useEffect(() => {
    async function fetchData() {
      await axios.get(`http://localhost:4000/accounts/${id}`).then((res) => {
        const data = res.data?.data[0];

        setAccountInfo({
          account_firstname: data?.account_firstname,
          account_lastname: data?.account_lastname,
          account_phone: data?.account_phone,
          account_email: data?.account_email,
          account_password: data?.account_password,
          account_type: data?.account_type,
          account_sex: data?.account_sex,
          account_address: data?.account_address,
        });
        console.log(data);
      });
    }
    fetchData();
  }, [setAccountInfo, id]);

  return (
    <div className="profileuser-container">
      <div className="profileuser-container2">
        <div className="title">Account Information</div>
        <div className="content">
          <form onSubmit={handleSubmit}>
            <div className="profileuser-details">
              <div className="input-box">
                <span className="details">Last Name</span>
                <input
                  type="text"
                  placeholder="Enter your last name"
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
                  placeholder="Enter your first name"
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
                  placeholder="Enter your number"
                  name="account_phone"
                  value={accountInfo.account_phone}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="input-box">
                <span className="details">Email</span>
                <input
                  type="text"
                  placeholder="Enter your Email"
                  name="account_email"
                  value={accountInfo.account_email}
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
              <div className="input-box">
                <span className="details">Password</span>
                <input
                  type={showPassword ? "text" : "Password"}
                  placeholder="Enter your Password"
                  name="account_password"
                  value={accountInfo.account_password}
                  onChange={handleChange}
                  required
                />
                <span className="details show">
                  Show password
                  <input
                    className="checkbox-password"
                    type="checkbox"
                    value="Show Password"
                    onClick={handleClickPassword}
                  />
                </span>
              </div>
            </div>

            <div className="gender-details">
              <select
                className="shadow border  rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
                name="status"
                value={accountInfo.account_sex}
                onChange={handleChange}
              >
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Prefer not to say">Prefer not to say</option>
              </select>
            </div>
            <div className="button">
              <input type="submit" value="Update" />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Profiledata;
