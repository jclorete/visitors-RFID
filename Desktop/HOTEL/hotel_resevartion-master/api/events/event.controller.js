const {
  read_event,
  read_event_user,
  read_all_event,
  create_event,
  update_event,
  delete_event,
  read_all_FilteredEvent,
} = require("./event.service");

module.exports = {
  readFilteredevent: async (req, res) => {
    try {
      await read_all_FilteredEvent((err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        return res.json({
          success: 1,
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  readevent: async (req, res) => {
    const id = req.params.id;
    try {
      await read_event(id, (err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        return res.json({
          success: 1,
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  readeventuser: async (req, res) => {
    const id = req.params.id;
    try {
      await read_event_user(id, (err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        return res.json({
          success: 1,
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  readallevent: async (req, res) => {
    try {
      await read_all_event((err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        return res.json({
          success: 1,
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  createevent: async (req, res) => {
    try {
      const data = req.body;
      await create_event(data, (err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        return res.json({
          success: 1,
          message: "event SUCCESSFULLY CREATED",
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  updateevent: async (req, res) => {
    const id = req.params.id;
    const data = req.body;
    await update_event(id, data, (err, results) => {
      if (err) {
        console.log(err);
        return;
      }
      if (!results) {
        return res.json({
          success: 0,
          message: "error",
        });
      }
      return res.json({
        success: 1,
        message: "event SUCCESSFULLY UPDATE!",
        data: results,
      });
    });
  },
  deleteevent: async (req, res) => {
    const id = req.params.id;
    await delete_event(id, (err, results) => {
      if (err) {
        console.log(err);
        return;
      }
      if (!results) {
        return res.json({
          success: 0,
          message: "error",
        });
      }
      return res.json({
        success: 1,
        message: "event SUCCESSFULLY DELETED!",
        data: results,
      });
    });
  },
};
