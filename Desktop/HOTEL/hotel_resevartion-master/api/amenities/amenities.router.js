const {
  readallamenities,
  createamenities,
  deleteamenities,
  amenitiestitle,
} = require("./amenities.controller");

const router = require("express").Router();

router.get("/title", amenitiestitle);
router.get("/", readallamenities);
router.post("/create", createamenities);
router.delete("/:id", deleteamenities);

module.exports = router;
