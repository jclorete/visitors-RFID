const pool = require("../../config/database.js");

module.exports = {
  /* READ */
  read_all_amenities: async (callBack) => {
    try {
      await pool.query(`SELECT * FROM amenities`, (error, results, fields) => {
        if (error) {
          return callBack(error);
        }
        return callBack(null, results.rows);
      });
    } catch (error) {
      console.error(err.message);
      res.status(500).send("Server error");
    }
  },

  /* CREATE */
  create_amenities: async (data, callBack) => {
    try {
      await pool.query(
        `INSERT INTO amenities(amenities_title) VALUES ($1) RETURNING *`,
        [data.amenities_title],
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.error(err.message);
      res.status(500).send("Server error");
    }
  },

  /* get only title */
  title_amenities: async (callBack) => {
    try {
      await pool.query(
        `SELECT amenities_title FROM amenities`,
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.error(err.message);
      res.status(500).send("Server error");
    }
  },
  /* DELETE */
  delete_amenities: async (id, callBack) => {
    try {
      await pool.query(
        `DELETE FROM amenities WHERE amenities_id = $1`,
        [id],
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.error(err.message);
      res.status(500).send("Server error");
    }
  },
};
