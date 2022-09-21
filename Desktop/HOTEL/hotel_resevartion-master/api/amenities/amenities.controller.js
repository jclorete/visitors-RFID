const {
  read_all_amenities,
  create_amenities,
  delete_amenities,
  title_amenities,
} = require("./amenities.service");

module.exports = {
  readallamenities: async (req, res) => {
    try {
      await read_all_amenities((err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: false,
            message: "error",
          });
        }
        return res.json({
          success: true,
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },

  amenitiestitle: async (req, res) => {
    try {
      await title_amenities((err, results) => {
        if (err) {
          console.log(err);
          return;
        }
        if (!results) {
          return res.json({
            success: false,
            message: "error",
          });
        }
        return res.json({
          success: true,
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  createamenities: async (req, res) => {
    try {
      const data = req.body;
      await create_amenities(data, (err, results) => {
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
          message: "AMENITIES SUCCESSFULLY CREATED",
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },

  deleteamenities: async (req, res) => {
    const id = req.params.id;
    await delete_amenities(id, (err, results) => {
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
        message: "AMENITIES SUCCESSFULLY DELETED!",
        data: results,
      });
    });
  },
};
