const {
  read_reservation,
  read_reservation_user,
  read_all_reservation,
  create_reservation,
  update_reservation,
  delete_reservation,
  read_all_FilteredReservation,
} = require("./reservation.service");

module.exports = {
  readFilteredreservation: async (req, res) => {
    try {
      await read_all_FilteredReservation((err, results) => {
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

  readreservation: async (req, res) => {
    try {
      const id = req.params.id;
      await read_reservation(id, (err, results) => {
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
  readreservationuser: async (req, res) => {
    try {
      const id = req.params.id;
      await read_reservation_user(id, (err, results) => {
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
  readallreservation: async (req, res) => {
    try {
      await read_all_reservation((err, results) => {
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
  createreservation: async (req, res) => {
    try {
      const data = req.body;
      await create_reservation(data, (err, results) => {
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
          message: "RESERVATION SUCCESSFULLY CREATED",
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  updatereservation: async (req, res) => {
    const id = req.params.id;
    const data = req.body;
    await update_reservation(id, data, (err, results) => {
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
        message: "RESERVATION SUCCESSFULLY UPDATE!",
        data: results,
      });
    });
  },
  deletereservation: async (req, res) => {
    const id = req.params.id;
    await delete_reservation(id, (err, results) => {
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
        message: "RESERVATION SUCCESSFULLY DELETED!",
        data: results,
      });
    });
  },
};
