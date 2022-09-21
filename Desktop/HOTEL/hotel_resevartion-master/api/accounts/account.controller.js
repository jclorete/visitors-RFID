const {
  read_account,
  read_all_account,
  create_account,
  update_account,
  delete_account,
  verify_existing_accounts,
  login_account,
} = require("./account.service");

module.exports = {
  readaccount: async (req, res) => {
    try {
      const id = req.params.id;
      await read_account(id, (err, results) => {
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
  readallaccount: async (req, res) => {
    try {
      await read_all_account((err, results) => {
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
  createaccount: async (req, res) => {
    try {
      const data = req.body;

      await verify_existing_accounts(data.account_email, (err, results) => {
        if (err) {
          return res.status(500).json({ err });
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        console.log(results);
        if (results.length === 1) {
          return res.json({
            success: 0,
            message: "ACCOUNT IS ALREADY EXIST",
          });
        }
        return;
      });

      await create_account(data, (err, results) => {
        if (err) {
          return res.status(500).json({ err });
        }
        if (!results) {
          return res.json({
            success: 0,
            message: "error",
          });
        }
        return res.json({
          success: 1,
          message: "ACCOUNT SUCCESSFULLY CREATED",
          data: results,
        });
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  updateaccount: async (req, res) => {
    const id = req.params.id;
    const data = req.body;
    await update_account(id, data, (err, results) => {
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
        message: "ACCOUNT SUCCESSFULLY UPDATE!",
        data: results,
      });
    });
  },
  deleteaccount: async (req, res) => {
    const id = req.params.id;
    await delete_account(id, (err, results) => {
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
        message: "ACCOUNT SUCCESSFULLY delete!",
        data: results,
      });
    });
  },
  login: async (req, res) => {
    const data = req.body;

    await login_account(data, (err, results) => {
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
      if (results.length === 1) {
        return res.json({
          success: 1,
          message: "ACCOUNT SUCCESSFULLY LOGIN!",
          data: results,
        });
      } else {
        return res.json({
          success: 1,
          message: "INVALID EMAIL OR PASSWORD!",
        });
      }
    });
  },
};
