-- Drop tables if they exist
BEGIN
    FOR rec IN (SELECT table_name
                FROM user_tables
                WHERE table_name IN (
                    'APARTMENTIMAGES', 'PAYMENTS', 'REVIEWS', 
                    'BOOKINGS', 'APARTMENTS', 'USERS', 'CITIES', 'STATES', 
                    'COUNTRIES', 'USERIMAGES'
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
                    'PAYMENT_ID_SEQ', 'USERIMAGE_ID_SEQ', 'APARTMENT_IMAGE_ID_SEQ', 
                    'CITY_ID_SEQ', 'STATE_ID_SEQ', 'COUNTRY_ID_SEQ', 'ZIPCODE_ID_SEQ', 'IMAGE_ID_SEQ'
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