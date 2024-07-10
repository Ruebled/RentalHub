-- Truncate tables in the correct order
TRUNCATE TABLE APARTMENTIMAGES;
TRUNCATE TABLE Apartment_Amenities;
TRUNCATE TABLE Reviews;
TRUNCATE TABLE Payments;
TRUNCATE TABLE Bookings;
TRUNCATE TABLE Apartments;
TRUNCATE TABLE Users;
TRUNCATE TABLE Amenities;
TRUNCATE TABLE Cities;
TRUNCATE TABLE States;
TRUNCATE TABLE Countries;
TRUNCATE TABLE Apartment_Main_Image;

-- Reset sequences if needed (already provided earlier)
ALTER SEQUENCE user_id_seq RESTART START WITH 1;
ALTER SEQUENCE apartment_id_seq RESTART START WITH 1;
ALTER SEQUENCE booking_id_seq RESTART START WITH 1;
ALTER SEQUENCE review_id_seq RESTART START WITH 1;
ALTER SEQUENCE payment_id_seq RESTART START WITH 1;
ALTER SEQUENCE amenity_id_seq RESTART START WITH 1;
ALTER SEQUENCE userimage_id_seq RESTART START WITH 1;
ALTER SEQUENCE apartment_image_id_seq RESTART START WITH 1;
ALTER SEQUENCE city_id_seq RESTART START WITH 1;
ALTER SEQUENCE state_id_seq RESTART START WITH 1;
ALTER SEQUENCE country_id_seq RESTART START WITH 1;
ALTER SEQUENCE apartment_main_image_id_seq RESTART START WITH 1;

-- Commit the changes
COMMIT;


-- Insert sample data into Countries table
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'United States');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Canada');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'United Kingdom');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Australia');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Germany');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'France');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Japan');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Brazil');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'India');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'China');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Mexico');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Italy');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Spain');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Netherlands');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'South Korea');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Russia');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Argentina');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Sweden');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Switzerland');
INSERT INTO Countries (CountryID, Name) VALUES (country_id_seq.NEXTVAL, 'Norway');

-- Insert sample data into States table
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'California', 1);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'New York', 1);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Ontario', 2);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Quebec', 2);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'England', 3);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Victoria', 4);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Bavaria', 5);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Île-de-France', 6);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Tokyo', 7);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'São Paulo', 8);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Jalisco', 9);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Lazio', 10);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Madrid', 11);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'North Holland', 12);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Seoul', 15);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Moscow Oblast', 16);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Buenos Aires', 17);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Stockholm', 18);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Zurich', 19);
INSERT INTO States (StateID, Name, CountryID) VALUES (state_id_seq.NEXTVAL, 'Oslo', 20);

-- Insert sample data into Cities table
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Los Angeles', 1);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'New York City', 2);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Toronto', 3);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Montreal', 4);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'London', 5);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Melbourne', 6);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Munich', 7);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Paris', 8);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Tokyo', 9);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'São Paulo', 10);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Guadalajara', 11);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Rome', 12);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Madrid', 13);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Amsterdam', 14);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Seoul', 15);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Moscow', 16);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Buenos Aires', 17);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Stockholm', 18);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Zurich', 19);
INSERT INTO Cities (CityID, Name, StateID) VALUES (city_id_seq.NEXTVAL, 'Oslo', 20);

-- Insert sample data into Users table with hashed passwords
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Jane',    'Smith',   'jane_smith',    '6cf615d5bcaac778352a8f1f3360d23f02f34ec182e259897fd6ce485d7870d4', 'jane.smith@example.com',  	'+1987654321', 'Host',  2,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Michael', 'Brown',   'michael_brown', '5906ac361a137e2d286465cd6588ebb5ac3f5ae955001100bc41577c3d751764', 'michael.brown@example.com',  	'+1654321897', 'Admin', 3,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'John',    'Doe',     'john_doe',      '0b14d501a594442a01c6859541bcb3e8164d183d32937b851835442f69d5c94e', 'john.doe@example.com',  		'+1234567890', 'Guest', 1,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Emily',   'Jones',   'emily_jones',   'b97873a40f73abedd8d685a7cd5e5f85e4a9cfb83eac26886640a0813850122b', 'emily.jones@example.com',  	'+1765432981', 'Guest', 4,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Chris',   'Evans',   'chris_evans',   '8b2c86ea9cf2ea4eb517fd1e06b74f399e7fec0fef92e3b482a6cf2e2b092023', 'chris.evans@example.com',  	'+1876543210', 'Host',  5,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Sophia',  'Adams',   'sophia_adams',  '598a1a400c1dfdf36974e69d7e1bc98593f2e15015eed8e9b7e47a83b31693d5', 'sophia.adams@example.com',  	'+1543219876', 'Guest', 6,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Matthew', 'Clark',   'matthew_clark', '5860836e8f13fc9837539a597d4086bfc0299e54ad92148d54538b5c3feefb7c', 'matthew.clark@example.com',  	'+1987123456', 'Host',  7,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Olivia',  'Wilson',  'olivia_wilson', '57f3ebab63f156fd8f776ba645a55d96360a15eeffc8b0e4afe4c05fa88219aa', 'olivia.wilson@example.com',  	'+1623456789', 'Guest', 8,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Samuel',  'Jackson', 'samuel_jackson','9323dd6786ebcbf3ac87357cc78ba1abfda6cf5e55cd01097b90d4a286cac90e', 'samuel.jackson@example.com',  '+1765432109', 'Host',  9,  SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Sarah',   'Adams',   'sarah_adams',   'aa4a9ea03fcac15b5fc63c949ac34e7b0fd17906716ac3b8e58c599cdc5a52f0', 'sarah.adams@example.com',  	'+1876543219', 'Guest', 10, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Daniel',  'Smith',   'daniel_smith',  '53d453b0c08b6b38ae91515dc88d25fbecdd1d6001f022419629df844f8ba433', 'daniel.smith@example.com',  	'+1543218765', 'Host',  11, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Hannah',  'Wilson',  'hannah_wilson', 'b3d17ebbe4f2b75d27b6309cfaae1487b667301a73951e7d523a039cd2dfe110', 'hannah.wilson@example.com',  	'+1987123344', 'Guest', 12, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'James',   'Johnson', 'james_johnson', '48caafb68583936afd0d78a7bfd7046d2492fad94f3c485915f74bb60128620d', 'james.johnson@example.com',  	'+1765432112', 'Host',  13, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Lily',    'Martin',  'lily_martin',   'c6863e1db9b396ed31a36988639513a1c73a065fab83681f4b77adb648fac3d6', 'lily.martin@example.com',  	'+1876543212', 'Guest', 14, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'William', 'Taylor',  'william_taylor','c63c2d34ebe84032ad47b87af194fedd17dacf8222b2ea7f4ebfee3dd6db2dfb', 'william.taylor@example.com', 	'+1543218912', 'Host',  15, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Mia',     'Thompson','mia_thompson',  '17a3379984b560dc311bb921b7a46b28aa5cb495667382f887a44a7fdbca7a7a', 'mia.thompson@example.com',  	'+1987123678', 'Guest', 16, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Jacob',   'Mitchell','jacob_mitchell','69bfb918de05145fba9dcee9688dfb23f6115845885e48fa39945eebb99d8527', 'jacob.mitchell@example.com', 	'+1765432789', 'Host',  17, SYSTIMESTAMP);   
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Ava',     'Baker',   'ava_baker',     'd2042d75a67922194c045da2600e1c92ff6d87e8fb6e0208606665f2d1dfa892', 'ava.baker@example.com',  		'+1876543321', 'Guest', 18, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Noah',    'Robinson','noah_robinson', '91a9ef3563010ea1af916083f9fb03a117d4d0d2a697f82368da1f737629f717', 'noah.robinson@example.com',  	'+1765432789', 'Host',  21, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Ethan',   'Garcia',  'ethan_garcia',  '5790ac3d0b8ae8afc72c2c6fb97654f2b73651c328de0a3b74854ade562dd17a', 'ethan.garcia@example.com', 	'+1543218765', 'Host',  19, SYSTIMESTAMP);
INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) VALUES (user_id_seq.NEXTVAL, 'Mia',     'Morris',  'mia_morris',    '7535d8f2d8c35d958995610f971287288ab5e8c82a3c4fdc2b6fb5d757a5b9f8', 'mia.morris@example.com',  	'+1987123678', 'Guest', 20, SYSTIMESTAMP);

-- Insert sample data into Apartments table
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 1, 'Cozy Downtown Loft', 					'Modern loft in the heart of downtown', 			'123 Main Street',			1, '90001',   	250.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 2, 'Luxury Loft in Downtown', 			'Modern loft with stunning views', 					'123 Main St', 				2, '10001', 	250.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 3, 'Cozy Apartment near Park', 			'Comfortable apartment with park view', 			'456 Oak Ave',				3, 'M5V 3L9',  	150.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 4, 'Charming Studio in London', 			'Bright studio in central London', 					'789 Park Lane', 		 	8, 'H2Z 1J5',	180.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 5, 'Sunny Beach House', 					'Beachfront house with private access', 			'321 Ocean Blvd',			6, 'W1G 9DQ',	300.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 6, 'City Center Penthouse', 				'Luxurious penthouse with panoramic views', 		'101 High St', 				7, '3000',   	400.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 7, 'Elegant Apartment in Paris', 			'Classic Parisian apartment near Louvre', 			'456 Rue de Rivoli', 		8, '80331',   	280.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 8, 'Traditional Japanese House', 			'Authentic Japanese house with garden', 			'789 Sakura Lane', 			9, '75001', 	200.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 9, 'Modern Loft in São Paulo', 			'Spacious loft in the heart of São Paulo', 	    	'101 Paulista Ave', 		10,'100-0005',  220.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 10, 'Rustic Cabin in Bavaria', 			'Cozy cabin in the Bavarian countryside', 			'123 Black Forest Rd', 		11,'04578-000',	180.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 11, 'Luxury Apartment in Amsterdam', 		'Stylish apartment with canal view', 				'456 Prinsengracht', 		14,'90002',   	320.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 12, 'Seaside Villa in Seoul', 			'Villa with ocean view in Gangnam district', 		'789 Haeundae Rd',			15,'10002',    	450.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 13, 'Moscow Kremlin View Apartment', 		'Apartment overlooking the Kremlin', 				'101 Red Square', 			16,'M5V 3L8', 	280.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 14, 'Modern Apartment in Buenos Aires', 	'Chic apartment in Palermo district', 				'456 Avenida Santa Fe', 	17,'H2Z 1J6', 	200.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 15, 'Scandinavian Loft in Stockholm', 	'Minimalist loft in Stockholm city center', 		'789 Drottninggatan', 		18,'W1G 9DZ', 	300.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 16, 'Lakefront Chalet in Zurich', 		'Chalet with stunning views of Lake Zurich', 		'101 Bahnhofstrasse', 		19,'3001',   	350.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 17, 'Modern Apartment in Oslo', 			'Sleek apartment in Oslo city center', 				'456 Karl Johans gate', 	20,'80332',   	280.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 18, 'Beachfront Condo in Cancun', 		'Condo with direct beach access in Cancun', 		'123 Cancun Beach Blvd', 	11,'75002',  	400.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 19, 'Venetian Canal House', 				'Historic house along the canals of Venice', 		'456 Canal Grande', 		12,'100-0006',  300.00, SYSTIMESTAMP);
INSERT INTO Apartments (ApartmentID, HostID, Name, Description, AddressLine, CityID, ZipCode, PricePerNight, CreatedAt) VALUES (apartment_id_seq.NEXTVAL, 20, 'Sydney Harbour View Apartment', 		'Apartment with panoramic views of Sydney Harbour', '789 Circular Quay', 		6, '04578-001', 380.00, SYSTIMESTAMP);

-- Insert Dummy Data into ApartmentImages 
-- write outside of this file
INSERT INTO Apartment_Images (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartment_image_id_seq.NEXTVAL, 1,  EMPTY_BLOB(), SYSTIMESTAMP);

-- Insert data into ApartmentMainImage
INSERT INTO Apartment_Main_Image (ApartmentMainImageID, ApartmentID, MainImageID, SetDate) VALUES (apartment_main_image_id_seq.NEXTVAL, 1, 1, SYSTIMESTAMP);

-- Insert sample data into Bookings table
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 2, 1, TO_DATE('2024-07-01', 'YYYY-MM-DD'), TO_DATE('2024-07-05', 'YYYY-MM-DD'), 1000.00, 'Active', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 3, 2, TO_DATE('2024-07-10', 'YYYY-MM-DD'), TO_DATE('2024-07-15', 'YYYY-MM-DD'), 750.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 4, 3, TO_DATE('2024-07-03', 'YYYY-MM-DD'), TO_DATE('2024-07-08', 'YYYY-MM-DD'), 900.00, 'Active', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 5, 4, TO_DATE('2024-07-20', 'YYYY-MM-DD'), TO_DATE('2024-07-25', 'YYYY-MM-DD'), 1500.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 6, 5, TO_DATE('2024-08-01', 'YYYY-MM-DD'), TO_DATE('2024-08-05', 'YYYY-MM-DD'), 1600.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 7, 6, TO_DATE('2024-08-10', 'YYYY-MM-DD'), TO_DATE('2024-08-15', 'YYYY-MM-DD'), 1200.00, 'Cancelled', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 8, 7, TO_DATE('2024-08-03', 'YYYY-MM-DD'), TO_DATE('2024-08-08', 'YYYY-MM-DD'), 1100.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 9, 8, TO_DATE('2024-09-01', 'YYYY-MM-DD'), TO_DATE('2024-09-05', 'YYYY-MM-DD'), 1400.00, 'Cancelled', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 10, 9, TO_DATE('2024-09-10', 'YYYY-MM-DD'), TO_DATE('2024-09-15', 'YYYY-MM-DD'), 1150.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 11, 10, TO_DATE('2024-09-03', 'YYYY-MM-DD'), TO_DATE('2024-09-08', 'YYYY-MM-DD'), 950.00, 'Active', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 12, 11, TO_DATE('2024-10-01', 'YYYY-MM-DD'), TO_DATE('2024-10-05', 'YYYY-MM-DD'), 1700.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 13, 12, TO_DATE('2024-10-10', 'YYYY-MM-DD'), TO_DATE('2024-10-15', 'YYYY-MM-DD'), 1800.00, 'CheckedIn', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 14, 13, TO_DATE('2024-10-03', 'YYYY-MM-DD'), TO_DATE('2024-10-08', 'YYYY-MM-DD'), 1250.00, 'CheckedIn', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 15, 14, TO_DATE('2024-11-01', 'YYYY-MM-DD'), TO_DATE('2024-11-05', 'YYYY-MM-DD'), 1350.00, 'CheckedIn', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 16, 15, TO_DATE('2024-11-10', 'YYYY-MM-DD'), TO_DATE('2024-11-15', 'YYYY-MM-DD'), 1450.00, 'Active', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 17, 16, TO_DATE('2024-11-03', 'YYYY-MM-DD'), TO_DATE('2024-11-08', 'YYYY-MM-DD'), 950.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 18, 17, TO_DATE('2024-12-01', 'YYYY-MM-DD'), TO_DATE('2024-12-05', 'YYYY-MM-DD'), 1550.00, 'Cancelled', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 19, 18, TO_DATE('2024-12-10', 'YYYY-MM-DD'), TO_DATE('2024-12-15', 'YYYY-MM-DD'), 1650.00, 'NoShow', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 20, 19, TO_DATE('2024-12-03', 'YYYY-MM-DD'), TO_DATE('2024-12-08', 'YYYY-MM-DD'), 1050.00, 'Cancelled', SYSTIMESTAMP);
INSERT INTO Bookings (BookingID, GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt) VALUES (booking_id_seq.NEXTVAL, 2, 20, TO_DATE('2025-01-01', 'YYYY-MM-DD'), TO_DATE('2025-01-05', 'YYYY-MM-DD'), 1750.00, 'CheckedIn', SYSTIMESTAMP);

-- Insert sample data into Reviews table
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 1, 5, 'Great experience, would definitely come back!', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 3, 4, 'Nice place, good location.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 5, 5, 'Amazing view from the apartment!', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 7, 4, 'Comfortable stay, friendly host.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 9, 5, 'Absolutely loved the apartment.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 11, 4, 'Clean and spacious, would recommend.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 13, 5, 'Perfect location, great amenities.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 15, 4, 'Good value for money.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 17, 5, 'Exceeded expectations, will book again.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 19, 4, 'Nice place to relax.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 2, 5, 'Wonderful stay, very accommodating host.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 4, 4, 'Pleasant experience overall.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 6, 5, 'Fantastic location, clean and comfortable.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 8, 4, 'Enjoyed the stay, would recommend.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 10, 5, 'Lovely apartment, great service.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 12, 4, 'Good amenities, friendly staff.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 14, 5, 'Excellent place, exceeded expectations.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 16, 4, 'Nice view, comfortable stay.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 18, 5, 'Well-maintained apartment, good location.', SYSTIMESTAMP);
INSERT INTO Reviews (ReviewID, BookingID, Rating, Review, CreatedAt) VALUES (review_id_seq.NEXTVAL, 20, 4, 'Clean and spacious, good value.', SYSTIMESTAMP);

-- Insert sample data into Payments table
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 1, 1000.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 3, 750.00, 'PayPal', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 5, 1500.00, 'Bank Transfer', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 7, 1100.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 9, 1400.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 11, 950.00, 'PayPal', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 13, 1250.00, 'Bank Transfer', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 15, 1350.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 17, 1550.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 19, 1050.00, 'PayPal', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 2, 750.00, 'Bank Transfer', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 4, 900.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 6, 1600.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 8, 1200.00, 'PayPal', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 10, 1150.00, 'Bank Transfer', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 12, 1800.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 14, 1450.00, 'Credit Card', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 16, 950.00, 'PayPal', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 18, 1650.00, 'Bank Transfer', SYSTIMESTAMP);
INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate) VALUES (payment_id_seq.NEXTVAL, 20, 1600.00, 'Credit Card', SYSTIMESTAMP);

-- Insert sample data into Amenities table
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'WiFi', 'High-speed internet access');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Parking', 'On-site parking available');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Pool', 'Outdoor swimming pool');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Gym', 'Fitness center with modern equipment');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Kitchen', 'Fully equipped kitchen with appliances');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Air Conditioning', 'Central air conditioning');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Heating', 'Central heating system');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'TV', 'Flat-screen TV with cable channels');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Laundry', 'Washer and dryer available');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Pet Friendly', 'Pets allowed on premises');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Smoking Allowed', 'Designated smoking areas');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Balcony', 'Private balcony with views');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Security', '24/7 security surveillance');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Concierge', 'Concierge service available');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Spa', 'On-site spa and wellness facilities');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Barbecue', 'Outdoor barbecue grills');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Elevator', 'Elevator access to all floors');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Wheelchair Accessible', 'Accessible rooms and facilities');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Bicycle Parking', 'Secure bicycle parking');
INSERT INTO Amenities (AmenityID, Name, Description) VALUES (amenity_id_seq.NEXTVAL, 'Children Playground', 'Play area for children');

-- Insert sample data into Apartment_Amenities table
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (1, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (1, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (1, 4);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (1, 5);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (1, 6);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (2, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (2, 3);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (2, 4);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (2, 5);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (2, 8);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (3, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (3, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (3, 3);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (3, 5);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (3, 9);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (4, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (4, 3);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (4, 4);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (4, 6);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (4, 7);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (5, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (5, 4);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (5, 5);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (5, 6);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (5, 10);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (6, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (6, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (6, 4);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (6, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (6, 11);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (7, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (7, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (7, 3);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (7, 5);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (7, 12);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (8, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (8, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (8, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (8, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (8, 13);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (9, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (9, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (9, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (9, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (9, 14);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (10, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (10, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (10, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (10, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (10, 15);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (11, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (11, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (11, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (11, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (11, 16);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (12, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (12, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (12, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (12, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (12, 17);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (13, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (13, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (13, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (13, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (13, 18);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (14, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (14, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (14, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (14, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (14, 19);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (15, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (15, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (15, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (15, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (15, 20);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (16, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (16, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (16, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (16, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (17, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (17, 2);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (17, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (17, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (18, 1);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (18, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (18, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (18, 5); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (19, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (19, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (19, 3); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (19, 4);
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (20, 1); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (20, 2); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (20, 4); 
INSERT INTO Apartment_Amenities (ApartmentID, AmenityID) VALUES (20, 5);

-- Insert sample data into Photos table
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 1,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 2,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 3,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 4,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 5,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 6,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 7,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 8,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 9,  EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 10, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 11, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 12, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 13, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 14, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 15, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 16, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 17, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 18, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 19, EMPTY_BLOB(), SYSTIMESTAMP);
-- INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, Image, UploadedAt) VALUES (apartmentimage_id_seq.NEXTVAL, 20, EMPTY_BLOB(), SYSTIMESTAMP);



COMMIT;


-- Original passwords (before hashing):
-- john_doe: password1
-- jane_smith: password2
-- michael_brown: password3
-- emily_jones: password4
-- chris_evans: password5
-- sophia_adams: password6
-- matthew_clark: password7
-- olivia_wilson: password8
-- samuel_jackson: password9
-- sarah_adams: password10
-- daniel_smith: password11
-- hannah_wilson: password12
-- james_johnson: password13
-- lily_martin: password14
-- william_taylor: password15
-- mia_thompson: password16
-- jacob_mitchell: password17
-- ava_baker: password18
-- ethan_garcia: password19
-- mia_morris: password20
-- noah_robinson: password21

