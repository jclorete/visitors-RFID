const express = require(`express`);
const cors = require(`cors`);
const fileUpload = require("express-fileupload");

const app = express();

// middleware
app.use(cors());
app.use(express.json());
app.use(fileUpload());

//getting routes
const accountsRouter = require("./api/accounts/account.router");
const roomsRouter = require("./api/rooms/room.router");
const reservationsRouter = require("./api/reservations/reservation.router");
const eventsRouter = require("./api/events/event.router");
const amenitiesRouter = require("./api/amenities/amenities.router");

//applying Routes
app.use("/accounts", accountsRouter);
app.use("/rooms", roomsRouter);
app.use("/reservations", reservationsRouter);
app.use("/events", eventsRouter);
app.use("/amenities", amenitiesRouter);
// Upload Endpoint
app.post("/upload", (req, res) => {
  if (req.files === null) {
    return res.status(400).json({ msg: "No file uploaded" });
  }

  const file = req.files.file;

  file.mv(`${__dirname}/client/public/images/${file.name}`, (err) => {
    if (err) {
      console.error(err);
      return res.status(500).send(err);
    }

    res.json({ filePath: `/images/${file.name}` });
  });
});

app.listen(4000, () => {
  console.log("SERVER RUNNING IN PORT 4000");
});
