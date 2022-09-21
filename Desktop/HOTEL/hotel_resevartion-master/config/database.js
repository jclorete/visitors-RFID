const Pool = require(`pg`).Pool;

const pool = new Pool({
  host: "localhost",
  user: "postgres",
  password: "psql",
  port: 5432,
  database: "pcmh_database",
});

module.exports = pool;
