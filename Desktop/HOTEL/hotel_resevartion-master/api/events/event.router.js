const {
  readallevent,
  readevent,
  createevent,
  updateevent,
  readeventuser,
  deleteevent,
  readFilteredevent,
} = require("./event.controller");

const router = require("express").Router();

router.get("/", readallevent);
router.get("/history", readFilteredevent);
router.get("/:id", readevent);

router.get("/user/:id", readeventuser);
router.post("/create", createevent);
router.put("/:id", updateevent);
router.delete("/:id", deleteevent);

module.exports = router;
