const pool = require("../../config/database.js");

module.exports = {
  /* READ */
  read_all_room: async (callBack) => {
    try {
      await pool.query(`SELECT * FROM rooms`, (error, results, fields) => {
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
  /* READ ALL */
  read_room: async (id, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM rooms WHERE room_id = $1`,
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
  /* CREATE */
  create_room: async (data, callBack) => {
    try {
      await pool.query(
        `INSERT INTO rooms(room_title,room_price,room_status,room_path,children_capacity,
          adult_capacity,room_capacity,room_description) VALUES ($1, $2, $3, $4,$5,$6,$7,$8) RETURNING *`,
        [
          data.room_title,
          data.room_price,
          data.room_status,
          data.room_path,
          data.children_capacity,
          data.adult_capacity,
          data.room_capacity,
          data.room_description,
        ],
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
  /* UPDATE */
  update_room: async (id, data, callBack) => {
    try {
      await pool.query(
        `UPDATE rooms SET room_title = $1, room_price = $2 , room_status = $3 ,  room_path = $4 children_capacity = $5,
        adult_capacity=$6,room_capacity= $7, room_description = $8 WHERE room_id = $9 `,
        [
          data.room_title,
          data.room_price,
          data.room_status,
          data.room_path,
          data.children_capacity,
          data.adult_capacity,
          data.room_capacity,
          data.room_description,
          id,
        ],
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
  delete_room: async (id, callBack) => {
    try {
      await pool.query(
        `DELETE FROM rooms WHERE room_id = $1`,
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
