import os
import oracledb as cx_Oracle

# Function to read all images from a folder and return as a list of tuples (filename, image_data)
def read_images_from_folder(folder_path):
    images = []
    for filename in os.listdir(folder_path):
        if filename.endswith(".jpg") or filename.endswith(".jpeg") or filename.endswith(".png"):
            with open(os.path.join(folder_path, filename), "rb") as f:
                image_data = f.read()
                images.append((filename, image_data))
    return images

# Function to insert images into Oracle database
def insert_images_into_oracle(images):
    dsn = cx_Oracle.makedsn('localhost', '1521', service_name='ORCL')
    connection = cx_Oracle.connect(user='test', password='test', dsn=dsn)
    
    cursor = connection.cursor()

    try:
        for image in images:
            filename, image_data = image
            cursor.execute("""
                INSERT INTO USERIMAGES (USERIMAGEID, IMAGE)
                VALUES (USERIMAGE_ID_SEQ.NEXTVAL, :blob_data)
            """, {'blob_data': image_data})
            print(f"Inserted {filename} into the database.")
        
        connection.commit()
        print("All images inserted successfully.")
    
    except cx_Oracle.Error as error:
        print(f"Error inserting image: {error}")
        connection.rollback()
    
    finally:
        cursor.close()
        connection.close()

if __name__ == "__main__":
    folder_path = "./UserImages"
    images = read_images_from_folder(folder_path)
    insert_images_into_oracle(images)
