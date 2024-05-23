SELECT apartment_id FROM Apartments 
WHERE apartment_id NOT IN (
	SELECT apartment_id FROM Booking
	WHERE booking_status in ("Pending", "Confirmed")
);

SELECT apartment_id FROM Apartments 
WHERE apartment_id NOT IN (
	SELECT apartment_id FROM Booking
	WHERE booking_status in ("Pending", "Confirmed")
)
AND location_id in (
	SELECT location_id FROM Locations
	WHERE coutry = coutry_req
	AND city = city_req
);

