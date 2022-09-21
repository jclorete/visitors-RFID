const pool = require("../../config/database.js");

module.exports = {
  /* READ */
  read_reservation: async (id, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM reservations as re INNER JOIN rooms as ro on  re.fk_room_id = ro.room_id INNER JOIN accounts as a on re.fk_account_id = a.account_id WHERE reservation_id = $1`,
        [id],
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  read_reservation_user: async (id, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM reservations as re INNER JOIN rooms as ro on  re.fk_room_id = ro.room_id INNER JOIN accounts as a on re.fk_account_id = a.account_id WHERE a.account_id = $1`,
        [id],
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  /* READ ALL */
  read_all_reservation: async (callBack) => {
    try {
      await pool.query(
        `SELECT * FROM reservations as re INNER JOIN rooms as ro on  re.fk_room_id = ro.room_id INNER JOIN accounts as a on re.fk_account_id = a.account_id`,
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  read_all_FilteredReservation: async (callBack) => {
    try {
      await pool.query(
        `SELECT re.*,ro.room_title,ro.room_description,a.account_firstname,a.account_lastname FROM reservations as re INNER JOIN rooms as ro on  re.fk_room_id = ro.room_id INNER JOIN accounts as a on re.fk_account_id = a.account_id`,
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  /* CREATE */
  create_reservation: async (data, callBack) => {
    try {
      await pool.query(
        `INSERT INTO reservations(reservation_checkin ,reservation_checkout ,reservation_room_type,reservation_Total_Pay,reservation_payment_method,reservation_room_number_checkin,fk_room_id,fk_account_id) VALUES ($1,$2,$3,$4,$5,$6,$7,$8)`,
        [
          data.reservation_checkin,
          data.reservation_checkout,
          data.reservation_room_type,
          data.reservation_Total_Pay,
          data.reservation_payment_method,
          data.reservation_room_number_checkin,
          data.fk_room_id,
          data.fk_account_id,
        ],
        (error, results, fields) => {
          if (error) {
            return callBack(error);
          }
          return callBack(null, results.rows);
        }
      );
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  /* UPDATE */
  update_reservation: async (id, data, callBack) => {
    try {
      await pool.query(
        `UPDATE reservations SET reservation_checkin = $1,reservation_checkout = $2,reservation_payment_method = $3,reservation_payment_status =$4,reseravtion_status= $5,reservation_room_number_checkin=$6,fk_room_id = $7,fk_account_id= $8 WHERE reservation_id = $9`,
        [
          data.reservation_checkin,
          data.reservation_checkout,
          data.reservation_payment_method,
          data.reservation_payment_status,
          data.reseravtion_status,
          data.reservation_room_number_checkin,
          data.fk_room_id,
          data.fk_account_id,
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
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  /* DELETE */
  delete_reservation: async (id, callBack) => {
    try {
      await pool.query(
        `DELETE FROM reservations WHERE reservation_id = $1`,
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
