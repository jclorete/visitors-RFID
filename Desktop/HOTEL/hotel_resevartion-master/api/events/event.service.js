const pool = require("../../config/database.js");

module.exports = {
  /* READ */
  read_event: async (id, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM events as re INNER JOIN accounts as a on re.fk_account_id = a.account_id WHERE event_id = $1`,
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
  read_event_user: async (id, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM events as re INNER JOIN accounts as a on re.fk_account_id = a.account_id WHERE a.account_id = $1`,
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
  read_all_event: async (callBack) => {
    try {
      await pool.query(
        `SELECT * FROM events as re INNER JOIN accounts as a on re.fk_account_id = a.account_id`,
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

  read_all_FilteredEvent: async (callBack) => {
    try {
      await pool.query(
        `SELECT re.event_id,re.event_checkin,re.event_checkout,re.event_date,re.event_amenities,re.event_approval,a.account_firstname,a.account_lastname,a.account_phone,a.account_email,a.account_address FROM events as re INNER JOIN accounts as a on re.fk_account_id = a.account_id`,
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
  create_event: async (data, callBack) => {
    try {
      await pool.query(
        `INSERT INTO events(event_checkin ,event_checkout ,event_adults,event_children,event_note,event_amenities,fk_account_id) VALUES ($1,$2,$3,$4,$5,$6,$7)`,
        [
          data.event_checkin,
          data.event_checkout,
          data.event_adults,
          data.event_children,
          data.event_note,
          data.event_amenities,
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
  update_event: async (id, data, callBack) => {
    try {
      await pool.query(
        `UPDATE events SET event_checkin = $1,event_checkout = $2 ,event_adults = $3,event_children = $4, event_note = $5,event_amenities=$6,event_approval = $7, fk_account_id = $8 WHERE event_id = $9`,
        [
          data.event_checkin,
          data.event_checkout,
          data.event_adults,
          data.event_children,
          data.event_note,
          data.event_amenities,
          data.event_approval,
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
  delete_event: async (id, callBack) => {
    try {
      await pool.query(
        `DELETE FROM events WHERE event_id = $1`,
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
