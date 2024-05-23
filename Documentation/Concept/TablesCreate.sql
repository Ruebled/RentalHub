CREATE TABLE Users (
	user_id INT PRIMARY KEY,
	username VARCHAR(20) UNIQUE,
	password VARCHAR(100),
	email VARCHAR(35) UNIQUE,
	phone_number VARCHAR(15),
	role VARCHAR(10)
);

CREATE TABLE Locations (
	location_id INT PRIMARY KEY,
	apartment_number INT,
	zipcode VARCHAR(10),
	street_address VARCHAR(30),
	city VARCHAR(20),
	province VARCHAR(20),
	country VARCHAR(15)
);

CREATE TABLE Apartments (
	apartment_id INT PRIMARY KEY,
	landlord_id INT,
	location_id INT,
	nr_bedrooms INT,
	nr_bathrooms INT,
	floor_area INT,
	rent_amount INT,
	available_status VARCHAR(10),
	description CLOB
	FOREIGN KEY(landlord_id) REFERENCES Users(user_id),
	FOREIGN KEY(location_id) REFERENCES Locations(location_id)
);

CREATE TABLE Booking (
	booking_id INT PRIMARY KEY,
	apartment_id INT,
	user_id INT,
	check_in_date DATE,
	check_out_date DATE,
	booking_status VARCHAR(10),
	total_price INT,
	payment_status VARCHAR(10),
	booking_date DATE,
	additional_notes CLOB,
	FOREIGN KEY(apartment_id) REFERENCES Apartments(apartment_id),
	FOREIGN KEY(user_id) REFERENCES Users(user_id)
);

CREATE TABLE Payment (
	payment_id INT PRIMARY_KEY,
	booking_id INT,
	user_id INT,
	payment_amount INT,
	payment_method VARCHAR(15),
	payment_status VARCHAR(10),
	transaction_id INT UNIQUE,
	currency VARCHAR(10),
	payment_notes CLOB,
	FOREIGN KEY(booking_id) REFERENCES Booking(booking_id),
	FOREIGN KEY(user_id) REFERENCES Users(user_id)
);

