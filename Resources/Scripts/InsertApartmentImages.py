import oracledb as cx_Oracle

# Function to read URLs from a file and group them by apartment
def read_urls_from_file(file_path):
    apartments = []
    with open(file_path, 'r') as file:
        apartment_urls = []
        for line in file:
            line = line.strip()
            if line == "":
                if apartment_urls:
                    apartments.append(apartment_urls)
                    apartment_urls = []
            else:
                apartment_urls.append(line)
        if apartment_urls:
            apartments.append(apartment_urls)
    return apartments

# Function to update Apartments table and insert into ApartmentImages table
def process_apartment_images(apartments):
    dsn = cx_Oracle.makedsn('localhost', '1521', service_name='ORCL')
    connection = cx_Oracle.connect(user='test', password='test', dsn=dsn)
    
    cursor = connection.cursor()

    try:
        for apartment_id, urls in enumerate(apartments, start=1):
            if urls:
                # Update main image URL in Apartments table
                main_image_url = urls[0]
                cursor.execute("""
                    UPDATE Apartments
                    SET MainPhotoURL = :main_image_url
                    WHERE ApartmentID = :apartment_id
                """, {'main_image_url': main_image_url, 'apartment_id': apartment_id})
                print(f"Updated main image for ApartmentID {apartment_id}")

                # Insert remaining URLs into ApartmentImages table
                for url in urls[1:]:
                    cursor.execute("""
                        INSERT INTO ApartmentImages (ApartmentImageID, ApartmentID, PhotoUrl)
                        VALUES (apartment_image_id_seq.NEXTVAL, :apartment_id, :photo_url)
                    """, {'apartment_id': apartment_id, 'photo_url': url})
                    print(f"Inserted image URL {url} for ApartmentID {apartment_id}")

        connection.commit()
        print("All URLs processed successfully.")

    except cx_Oracle.Error as error:
        print(f"Error processing URLs: {error}")
        connection.rollback()

    finally:
        cursor.close()
        connection.close()

if __name__ == "__main__":
    file_path = "apartment_urls.txt"  # Update with your file path
    apartments = read_urls_from_file(file_path)
    process_apartment_images(apartments)
