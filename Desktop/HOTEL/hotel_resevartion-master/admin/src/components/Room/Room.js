import axios from "axios";
import React, { useEffect, useState } from "react";
import Roomlist from "./RoomList";
import { toast } from "react-toastify";

const Room = () => {
  const [rooms, setRooms] = useState([]);
  const [file, setFile] = useState("");
  const [isFetching, setIsFetching] = useState(false);
  const [filename, setFilename] = useState("Choose File");
  const [message, setMessage] = useState("");
  const [newRoom, setNewRoom] = useState({
    room_title: "",
    room_price: 0,
    room_status: "available",
    room_path: "",
    children_capacity: 0,
    adult_capacity: 0,
    room_capacity: 0,
    room_description: "",
  });
  const onChange = (e) => {
    setFile(e.target.files[0]);
    setFilename(e.target.files[0].name);
  };

  const handleInputChange = (e) => {
    const name = e.target.name;

    setNewRoom((values) => ({
      ...values,
      [name]: e.target.value,
    }));
    console.log(newRoom);
  };

  const handleSubmitPicture = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("file", file);

    try {
      const res = await axios.post("http://localhost:4000/upload", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      console.log(res.data.filePath);
      setNewRoom((values) => ({
        ...values,
        room_path: res.data.filePath,
      }));

      setMessage("File Uploaded");
    } catch (err) {
      if (err.response.status === 500) {
        setMessage("There was a problem with the server");
      } else {
        setMessage(err.response.data.msg);
      }
    }
  };

  const handleSubmitRoom = async (e) => {
    e.preventDefault();
    await axios
      .post("http://localhost:4000/rooms/create", newRoom)
      .then((res) => {
        setIsFetching(true);
        toast.success("Success", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        setMessage("");
      });
  };

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      async function fetchData() {
        await axios.get("http://localhost:4000/rooms").then((res) => {
          const data = res.data?.data;
          if (data !== null) {
            setRooms(data);
          }
          setIsFetching(false);
        });
      }
      fetchData();
    }
    return () => {
      isMounted = false;
    };
  }, [rooms]);

  return (
    <div className="profileuser-container">
      <h2>Room</h2>
      {rooms?.map((values) => (
        <Roomlist
          key={values.room_id}
          room_title={values.room_title}
          room_price={values.room_price}
          room_status={values.room_status}
          room_path={values.room_path}
          id={values.room_id}
          children_capacity={values.children_capacity}
          adult_capacity={values.adult_capacity}
          room_capacity={values.room_capacity}
          room_description={values.room_description}
        />
      ))}

      <div className="main-register-container">
        <div className="register-container">
          <div className="title">Add Room</div>
          <div className="content">
            <form onSubmit={handleSubmitPicture}>
              <div className="user-details">
                <div className="input-box">
                  <span className="details">Upload Room Image</span>
                  <input
                    type="file"
                    className="custom-file-input"
                    id="customFile"
                    onChange={onChange}
                  />
                  {message ? <h3>{message}</h3> : null}
                </div>
                <div className="button">
                  <input type="submit" value="Add Picture" />
                </div>
              </div>
            </form>

            <form onSubmit={handleSubmitRoom}>
              <div className="user-details">
                <div className="input-box">
                  <span className="details">Room Name</span>
                  <input
                    className="input-room"
                    type="text"
                    id="room_title"
                    name="room_title"
                    value={newRoom.room_title}
                    onChange={handleInputChange}
                  />
                </div>

                <div className="input-box">
                  <span className="details">Room Price</span>
                  <input
                    className="input-room"
                    type="text"
                    name="room_price"
                    placeholder="Room Price"
                    value={newRoom.room_price}
                    onChange={handleInputChange}
                  />
                </div>

                <div className="input-box">
                  <span className="details">Adult Capacity</span>
                  <input
                    className="input-room"
                    type="text"
                    name="adult_capacity"
                    placeholder="Adult Capacity"
                    value={newRoom.adult_capacity}
                    onChange={handleInputChange}
                  />
                </div>
                <div className="input-box">
                  <span className="details">Children Capacity</span>
                  <input
                    className="input-room"
                    type="text"
                    name="children_capacity"
                    placeholder="Chilren Capacity"
                    value={newRoom.children_capacity}
                    onChange={handleInputChange}
                  />
                </div>
                <div className="input-box">
                  <span className="details">Room Capacity</span>
                  <input
                    className="input-room"
                    type="text"
                    name="room_capacity"
                    value={newRoom.room_capacity}
                    onChange={handleInputChange}
                  />
                </div>
                <div className="row mb-4">
                  <div className="col-md-12 textarea">
                    <span
                      className="text-black font-weight-bold"
                      htmlFor="message"
                    >
                      Room Description
                    </span>
                    <textarea
                      id="message"
                      className="form-control  "
                      cols="35"
                      rows="10"
                      name="room_description"
                      value={newRoom.room_description}
                      onChange={handleInputChange}
                    ></textarea>
                  </div>
                </div>
                <div className="button">
                  <input type="submit" value="Save" />
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Room;
