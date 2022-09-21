const {
  readaccount,
  readallaccount,
  createaccount,
  updateaccount,
  deleteaccount,
  login,
} = require("./account.controller");

const router = require("express").Router();

router.get("/", readallaccount);
router.get("/:id", readaccount);
router.post("/login", login);
router.post("/create", createaccount);
router.put("/:id", updateaccount);
router.delete("/:id", deleteaccount);

module.exports = router;
