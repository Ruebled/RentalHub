
-- Create sequences
CREATE SEQUENCE user_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE apartment_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE booking_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE review_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE payment_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE userimage_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE apartment_image_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE city_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE state_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE country_id_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE support_id_seq START WITH 1 INCREMENT BY 1;

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
    SizeInSquareFeet INT,
    NumberOfRooms INT,
    PricePerNight NUMBER(10, 2) NOT NULL,
    MainPhotoURL VARCHAR2(255),
    AverageRating NUMBER(3, 2),
    NumberOfReviews INT,
    HouseRules CLOB,
    CancellationPolicy VARCHAR2(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (HostID) REFERENCES Users(UserID),
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);

CREATE TABLE ApartmentImages (
    ApartmentImageID INT DEFAULT apartmentimage_id_seq.NEXTVAL PRIMARY KEY,
    ApartmentID INT NOT NULL,
    PhotoUrl VARCHAR2(255) DEFAULT 'https://picsum.photos/200',
    UploadedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID)
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

CREATE TABLE Support(
	TicketId INT DEFAULT support_id_seq.NEXTVAL PRIMARY KEY,
	UserId INT NOT NULL,
	TicketTitle VARCHAR2(50) NOT NULL, 
	TicketMessage VARCHAR2(600) NOT NULL,
	CreateDate DATE NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

COMMIT;
