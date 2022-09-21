CREATE DATABASE pcmh_database;

CREATE TABLE rooms(
    room_id SERIAL,
    room_title VARCHAR(150) NOT NULL,
    room_price INTEGER DEFAULT 0,
    room_status VARCHAR(150) NOT NULL,
    room_path VARCHAR(250),
    children_capacity INTEGER DEFAULT 0,
    adult_capacity INTEGER DEFAULT 0,
    room_capacity INTEGER DEFAULT 0,
    room_description VARCHAR(250),
    PRIMARY KEY(room_id)
);

CREATE TABLE amenities(
    amenities_id SERIAL,
    amenities_title VARCHAR(150) NOT NULL,
    PRIMARY KEY(amenities_id)
);

CREATE TABLE accounts(
    account_id serial,
    account_firstname VARCHAR(150),
    account_lastname VARCHAR(150),
    account_phone VARCHAR(150),
    account_email VARCHAR(150)NOT NULL,
    account_password VARCHAR(150) NOT NULL,
    account_type VARCHAR(150) NOT NULL,
    account_sex VARCHAR(150),
    account_address VARCHAR(250),
    PRIMARY KEY(account_id),
    UNIQUE(account_email)
);

CREATE TABLE reservations(
    reservation_id SERIAL,
    reservation_checkin DATE NOT NULL,
    reservation_checkout DATE NOT NULL,
    reservation_date DATE DEFAULT NOW(),
    reservation_payment_method VARCHAR(150),
    reservation_payment_status VARCHAR(150) DEFAULT 'PENDING',
    reseravtion_status VARCHAR(150) DEFAULT 'WAITING',
    reservation_room_type VARCHAR(150),
    reservation_Total_Pay INTEGER DEFAULT 0,
    reservation_room_number_checkin INTEGER DEFAULT 0,
    fk_room_id INTEGER REFERENCES rooms(room_id),
    fk_account_id INTEGER REFERENCES accounts(account_id),
    PRIMARY KEY(reservation_id)
);

CREATE TABLE events(
    event_id SERIAL,
    event_checkin DATE NOT NULL,
    event_checkout DATE NOT NULL,
    event_adults VARCHAR(150),
    event_children VARCHAR(150),
    event_note VARCHAR(1000),
    event_date DATE DEFAULT NOW(),
    event_Amenities TEXT [], 
    event_approval VARCHAR(255) DEFAULT 'PENDING',
    fk_account_id INTEGER REFERENCES accounts(account_id),
    PRIMARY KEY(event_id)
);

/*-----------------------------------------------------------------------END OF DATABASE--------------------*/

/*SAMPLE QUERY

SELECT * FROM reservations as re INNER JOIN rooms as ro on  re.fk_room_id = ro.room_id INNER JOIN accounts as a on re.fk_account_id = a.account_id;
  {
            "reservation_id": 3,
            "reservation_checkin": "2022-02-20T16:00:00.000Z",
            "reservation_checkout": "2022-02-22T16:00:00.000Z",
            "reservation_adults": 3,
            "reservation_children": 2,
            "reservation_datae": "2022-02-21T16:00:00.000Z",
            "fk_room_id": 1,
            "fk_account_id": 3,
            "room_id": 1,
            "room_description": "family room",
            "room_price": 15500,
            "room_status": "available",
            "account_id": 3,
            "account_firstname": "zeth",
            "account_lastname": "kenneth",
            "account_email": "qwe3@gmail.com",
            "account_phone": "123456789",
            "account_password": "zxcvzxcv",
            "account_type": "ADMIN"
        }
*/