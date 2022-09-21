const {
  readallreservation,
  readreservation,
  createreservation,
  updatereservation,
  deletereservation,
  readreservationuser,
  readFilteredreservation,
} = require("./reservation.controller");

const router = require("express").Router();

router.get("/", readallreservation);
router.get("/admin", readFilteredreservation);
router.get("/:id", readreservation);
router.get("/user/:id", readreservationuser);
router.post("/create", createreservation);
router.put("/:id", updatereservation);
router.delete("/:id", deletereservation);

module.exports = router;
