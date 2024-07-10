-- Drop tables if they exist
BEGIN
    FOR rec IN (SELECT table_name
                FROM user_tables
                WHERE table_name IN (
                    'APARTMENT_AMENITIES', 'APARTMENT_IMAGES', 'PAYMENTS', 'REVIEWS', 
                    'BOOKINGS', 'APARTMENTS', 'AMENITIES', 'USERS', 'CITIES', 'STATES', 
                    'COUNTRIES', 'USERIMAGES', 'APARTMENT_MAIN_IMAGE'
                ))
    LOOP
        BEGIN
            EXECUTE IMMEDIATE 'DROP TABLE ' || rec.table_name || ' CASCADE CONSTRAINTS';
        EXCEPTION
            WHEN OTHERS THEN
                DBMS_OUTPUT.PUT_LINE('Error dropping table ' || rec.table_name);
        END;
    END LOOP;
END;
/

-- Drop sequences if they exist
BEGIN
    FOR rec IN (SELECT sequence_name
                FROM user_sequences
                WHERE sequence_name IN (
                    'USER_ID_SEQ', 'APARTMENT_ID_SEQ', 'BOOKING_ID_SEQ', 'REVIEW_ID_SEQ', 
                    'PAYMENT_ID_SEQ', 'AMENITY_ID_SEQ', 'USERIMAGE_ID_SEQ', 'APARTMENT_IMAGE_ID_SEQ', 
                    'CITY_ID_SEQ', 'STATE_ID_SEQ', 'COUNTRY_ID_SEQ', 'ZIPCODE_ID_SEQ', 'IMAGE_ID_SEQ',
					'APARTMENT_MAIN_IMAGE_ID_SEQ'
                ))
    LOOP
        BEGIN
            EXECUTE IMMEDIATE 'DROP SEQUENCE ' || rec.sequence_name;
        EXCEPTION
            WHEN OTHERS THEN
                DBMS_OUTPUT.PUT_LINE('Error dropping sequence ' || rec.sequence_name);
        END;
    END LOOP;
END;
/

-- Create sequences
CREATE SEQUENCE user_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE apartment_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE booking_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE review_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE payment_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE amenity_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE userimage_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE apartment_image_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE city_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE state_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE country_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE apartment_main_image_id_seq START WITH 1 INCREMENT BY 1;

-- Create tables
CREATE TABLE Countries (
    CountryID INT DEFAULT country_id_seq.NEXTVAL PRIMARY KEY,
    Name VARCHAR2(100) NOT NULL UNIQUE
);

CREATE TABLE States (
    StateID INT DEFAULT state_id_seq.NEXTVAL PRIMARY KEY,
    Name VARCHAR2(100) NOT NULL UNIQUE,
    CountryID INT NOT NULL,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);

CREATE TABLE Cities (
    CityID INT DEFAULT city_id_seq.NEXTVAL PRIMARY KEY,
    Name VARCHAR2(100) NOT NULL UNIQUE,
    StateID INT NOT NULL,
    FOREIGN KEY (StateID) REFERENCES States(StateID)
);

CREATE TABLE UserImages (
    UserImageID INT DEFAULT userimage_id_seq.NEXTVAL PRIMARY KEY,
    Image BLOB NOT NULL
);

CREATE TABLE Users (
    UserID INT DEFAULT user_id_seq.NEXTVAL PRIMARY KEY,
    FirstName VARCHAR2(30),
    LastName VARCHAR2(30),
    Username VARCHAR2(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR2(255) NOT NULL,
    Email VARCHAR2(100) NOT NULL UNIQUE,
    PhoneNumber VARCHAR2(20),
    UserType VARCHAR2(10) CHECK (UserType IN ('Guest', 'Host', 'Admin')),
    ProfileImageId INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProfileImageId) REFERENCES UserImages(UserImageID)
);

CREATE TABLE Apartments (
    ApartmentID INT DEFAULT apartment_id_seq.NEXTVAL PRIMARY KEY,
    HostID INT NOT NULL,
    Name VARCHAR2(100) NOT NULL,
    Description CLOB,
    AddressLine VARCHAR2(255),
    CityID INT NOT NULL,
    ZipCode VARCHAR2(20) NOT NULL,
    PricePerNight NUMBER(10, 2) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (HostID) REFERENCES Users(UserID),
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);

CREATE TABLE Apartment_Images (
    ApartmentImageID INT DEFAULT apartmentimage_id_seq.NEXTVAL PRIMARY KEY,
    ApartmentID INT NOT NULL,
    Image BLOB NOT NULL,
    UploadedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID)
);

CREATE TABLE Apartment_Main_Image (
	ApartmentMainImageID INT DEFAULT apartment_main_image_id_seq.NEXTVAL PRIMARY KEY,
	ApartmentID INT,
	MainImageID INT,
	SetDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID),
	FOREIGN KEY (MainImageID) REFERENCES Apartment_Images(ApartmentImageID)
);

CREATE TABLE Bookings (
    BookingID INT DEFAULT booking_id_seq.NEXTVAL PRIMARY KEY,
    GuestID INT NOT NULL,
    ApartmentID INT NOT NULL,
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    TotalPrice NUMBER(10, 2) NOT NULL,
    Status VARCHAR2(10) DEFAULT 'Active' CHECK (Status IN ('Active', 'Cancelled', 'CheckedIn', 'CheckedOut', 'NoShow')) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (GuestID) REFERENCES Users(UserID),
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID)
);

CREATE TABLE Reviews (
    ReviewID INT DEFAULT review_id_seq.NEXTVAL PRIMARY KEY,
    BookingID INT NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Review VARCHAR2(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);

CREATE TABLE Payments (
    PaymentID INT DEFAULT payment_id_seq.NEXTVAL PRIMARY KEY,
    BookingID INT NOT NULL,
    Amount NUMBER(10, 2) NOT NULL,
    PaymentMethod VARCHAR2(20) CHECK (PaymentMethod IN ('Credit Card', 'PayPal', 'Bank Transfer', 'Cash')) NOT NULL,
    PaymentDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);

CREATE TABLE Amenities (
    AmenityID INT DEFAULT amenity_id_seq.NEXTVAL PRIMARY KEY,
    Name VARCHAR2(100) NOT NULL UNIQUE,
    Description CLOB
);

CREATE TABLE Apartment_Amenities (
    ApartmentID INT NOT NULL,
    AmenityID INT NOT NULL,
    PRIMARY KEY (ApartmentID, AmenityID),
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID),
    FOREIGN KEY (AmenityID) REFERENCES Amenities(AmenityID)
);

COMMIT;
