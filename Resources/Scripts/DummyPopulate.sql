-- Populate Countries table
DECLARE
    v_country_name VARCHAR2(100);
BEGIN
    FOR i IN 1..100 LOOP
        v_country_name := 'Country ' || i;
        INSERT INTO Countries (Name) VALUES (v_country_name);
    END LOOP;
    COMMIT;
END;
/

-- Populate States table
DECLARE
    v_state_name VARCHAR2(100);
BEGIN
    FOR i IN 1..100 LOOP
        v_state_name := 'State ' || i;
        INSERT INTO States (Name, CountryID)
        VALUES (v_state_name, MOD(i, 100) + 1); -- Modulo to assign countries
    END LOOP;
    COMMIT;
END;
/

-- Populate Cities table
DECLARE
    v_city_name VARCHAR2(100);
BEGIN
    FOR i IN 1..100 LOOP
        v_city_name := 'City ' || i;
        INSERT INTO Cities (Name, StateID)
        VALUES (v_city_name, MOD(i, 100) + 1); -- Modulo to assign states
    END LOOP;
    COMMIT;
END;
/

-- Function to hash passwords using SHA-256
CREATE OR REPLACE FUNCTION hash_password(p_password IN VARCHAR2) RETURN VARCHAR2 IS
    v_hash VARCHAR2(64);
BEGIN
    WITH
        FUNCTION to_hex_string(p_bytes IN RAW) RETURN VARCHAR2 IS
            v_hex VARCHAR2(64);
        BEGIN
            SELECT LOWER(TO_CHAR(p_bytes, 'FMX')) INTO v_hex FROM DUAL;
            RETURN v_hex;
        END to_hex_string;
    BEGIN
        SELECT to_hex_string(SYS.DBMS_CRYPTO.HASH(SYS.UTL_RAW.CAST_TO_RAW(p_password), SYS.DBMS_CRYPTO.HASH_SH256)) INTO v_hash FROM DUAL;
        RETURN v_hash;
    END;
END hash_password;
/

-- Populate Users table
DECLARE
    v_username VARCHAR2(50);
    v_email VARCHAR2(100);
    v_password VARCHAR2(255);
BEGIN
    FOR i IN 1..100 LOOP
        v_username := 'user' || i;
        v_email := 'user' || i || '@example.com';
        v_password := hash_password('password' || i); -- Hashing password
        INSERT INTO Users (Username, PasswordHash, Email, FirstName, LastName, UserType)
        VALUES (v_username, v_password, v_email, 'User' || i, 'Lastname' || i, CASE WHEN MOD(i, 3) = 0 THEN 'Admin' ELSE 'Guest' END);
    END LOOP;
    COMMIT;
END;
/

-- Populate Apartments table
DECLARE
    v_host_id INT;
    v_name VARCHAR2(100);
    v_description CLOB;
    v_address_line1 VARCHAR2(255);
    v_city_id INT;
    v_zip_code INT;
    v_price_per_night NUMBER(10, 2);
BEGIN
    FOR i IN 1..100 LOOP
        v_host_id := MOD(i, 100) + 1; -- Modulo to assign host IDs
        v_name := 'Apartment ' || i;
        v_description := 'Description of apartment ' || i;
        v_address_line1 := 'Address Line 1, Apartment ' || i;
        v_city_id := MOD(i, 100) + 1; -- Modulo to assign city IDs
        v_zip_code := LPAD(i, 5, '0'); -- Padding with zeros to simulate zip codes
        v_price_per_night := 50 + (i * 5); -- Adjust price per night for variation
        INSERT INTO Apartments (HostID, Name, Description, AddressLine1, CityID, ZipCode, PricePerNight)
        VALUES (v_host_id, v_name, v_description, v_address_line1, v_city_id, v_zip_code, v_price_per_night);
    END LOOP;
    COMMIT;
END;
/

-- Populate Bookings table
DECLARE
    v_guest_id INT;
    v_apartment_id INT;
    v_check_in_date DATE;
    v_check_out_date DATE;
    v_total_price NUMBER(10, 2);
    v_status VARCHAR2(10);
BEGIN
    FOR i IN 1..100 LOOP
        v_guest_id := MOD(i, 100) + 1; -- Modulo to assign guest IDs
        v_apartment_id := MOD(i, 100) + 1; -- Modulo to assign apartment IDs
        v_check_in_date := TRUNC(SYSDATE) + i; -- Start from today + i days
        v_check_out_date := TRUNC(SYSDATE) + i + 3; -- Check out after 3 days
        v_total_price := 100 + (i * 10); -- Adjust total price for variation
        v_status := CASE WHEN i <= 50 THEN 'Confirmed' ELSE 'Pending' END; -- Half bookings confirmed, half pending
        INSERT INTO Bookings (GuestID, ApartmentID, CheckInDate, CheckOutDate, TotalPrice, Status)
        VALUES (v_guest_id, v_apartment_id, v_check_in_date, v_check_out_date, v_total_price, v_status);
    END LOOP;
    COMMIT;
END;
/

-- Populate Reviews table
DECLARE
    v_booking_id INT;
    v_rating INT;
    v_review VARCHAR2(255);
BEGIN
    FOR i IN 1..100 LOOP
        v_booking_id := MOD(i, 100) + 1; -- Modulo to assign booking IDs
        v_rating := TRUNC(DBMS_RANDOM.VALUE(1, 6)); -- Random rating between 1 and 5
        v_review := 'Review for booking ' || v_booking_id;
        INSERT INTO Reviews (BookingID, Rating, Review)
        VALUES (v_booking_id, v_rating, v_review);
    END LOOP;
    COMMIT;
END;
/

-- Populate Payments table
DECLARE
    v_booking_id INT;
    v_amount NUMBER(10, 2);
    v_payment_method VARCHAR2(20);
    v_payment_date TIMESTAMP;
BEGIN
    FOR i IN 1..100 LOOP
        v_booking_id := MOD(i, 100) + 1; -- Modulo to assign booking IDs
        v_amount := 50 + (i * 2); -- Adjust amount for variation
        v_payment_method := CASE MOD(i, 3)
                             WHEN 0 THEN 'Credit Card'
                             WHEN 1 THEN 'PayPal'
                             ELSE 'Bank Transfer'
                             END;
        v_payment_date := SYSTIMESTAMP - INTERVAL '10' * i DAY; -- Payment date in the past
        INSERT INTO Payments (BookingID, Amount, PaymentMethod, PaymentDate)
        VALUES (v_booking_id, v_amount, v_payment_method, v_payment_date);
    END LOOP;
    COMMIT;
END;
/

-- Populate Amenities table
DECLARE
    v_name VARCHAR2(100);
    v_description CLOB;
BEGIN
    FOR i IN 1..20 LOOP -- Inserting 20 amenities
        v_name := 'Amenity ' || i;
        v_description := 'Description of amenity ' || i;
        INSERT INTO Amenities (Name, Description)
        VALUES (v_name, v_description);
    END LOOP;
    COMMIT;
END;
/

-- Populate Apartment_Amenities table
DECLARE
    v_apartment_id INT;
    v_amenity_id INT;
BEGIN
    FOR i IN 1..100 LOOP
        v_apartment_id := MOD(i, 100) + 1; -- Modulo to assign apartment IDs
        v_amenity_id := MOD(i, 20) + 1; -- Modulo to assign amenity IDs
        INSERT INTO Apartment_Amenities (ApartmentID, AmenityID)
        VALUES (v_apartment_id, v_amenity_id);
    END LOOP;
    COMMIT;
END;
/

-- Populate UserImages table with dummy data
DECLARE
    v_image BLOB;
BEGIN
    FOR i IN 1..100 LOOP
        -- Dummy BLOB data for the sake of example
        v_image := UTL_RAW.CAST_TO_RAW('Dummy Image Data ' || i);
        INSERT INTO UserImages (Image) VALUES (v_image);
    END LOOP;
    COMMIT;
END;
/

-- Populate ApartmentImages table with dummy data
DECLARE
    v_apartment_id INT;
    v_image BLOB;
BEGIN
    FOR i IN 1..100 LOOP
        v_apartment_id := MOD(i, 100) + 1; -- Modulo to assign apartment IDs
        -- Dummy BLOB data for the sake of example
        v_image := UTL_RAW.CAST_TO_RAW('Dummy Image Data ' || i);
        INSERT INTO ApartmentImages (ApartmentID, Image) VALUES (v_apartment_id, v_image);
    END LOOP;
    COMMIT;
END;
/

-- Populate Wishlists table
DECLARE
    v_user_id INT;
    v_apartment_id INT;
BEGIN
    FOR i IN 1..100 LOOP
        v_user_id := MOD(i, 100) + 1; -- Modulo to assign user IDs
        v_apartment_id := MOD(i, 100) + 1; -- Modulo to assign apartment IDs
        INSERT INTO Wishlists (UserID, ApartmentID)
        VALUES (v_user_id, v_apartment_id);
    END LOOP;
    COMMIT;
END;
/
