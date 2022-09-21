const pool = require("../../config/database.js");

module.exports = {
  /* READ */

  read_account: async (id, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM accounts WHERE account_id = $1`,
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
  read_all_account: async (callBack) => {
    try {
      await pool.query(`SELECT * FROM accounts`, (error, results, fields) => {
        if (error) {
          return callBack(error);
        }
        return callBack(null, results.rows);
      });
    } catch (error) {
      console.log(error);
      res.status(500).send("SERVER ERROR!");
    }
  },
  /* CREATE */
  create_account: async (data, callBack) => {
    try {
      await pool.query(
        `INSERT INTO accounts(account_firstname,account_lastname,account_phone,account_email,account_password,account_type,account_sex,account_address) VALUES ($1,$2,$3,$4,$5,$6,$7,$8) RETURNING *`,
        [
          data.account_firstname,
          data.account_lastname,
          data.account_phone,
          data.account_email,
          data.account_password,
          data.account_type,
          data.account_sex,
          data.account_address,
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
  update_account: async (id, data, callBack) => {
    try {
      await pool.query(
        `UPDATE accounts SET account_firstname = $1, account_lastname = $2,account_phone = $3, account_email = $4, account_password = $5, account_type = $6, account_sex = $7, account_address = $8 WHERE account_id = $9`,
        [
          data.account_firstname,
          data.account_lastname,
          data.account_phone,
          data.account_email,
          data.account_password,
          data.account_type,
          data.account_sex,
          data.account_address,
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
  delete_account: async (id, callBack) => {
    try {
      await pool.query(
        `DELETE FROM accounts WHERE account_id = $1`,
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
  /* Verify existing accounts */
  verify_existing_accounts: async (email, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM accounts WHERE account_email = $1`,
        [email],
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
  login_account: async (data, callBack) => {
    try {
      await pool.query(
        `SELECT * FROM accounts WHERE account_email = $1 AND account_password = $2`,
        [data.account_email, data.account_password],
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
};
