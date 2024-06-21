from faker import Faker
import cx_Oracle  # Oracle DB driver for Python

# Connect to Oracle database
connection = cx_Oracle.connect("username", "password", "hostname/service_name")

# Create Faker instance
fake = Faker()

# Insert users into Users table
cursor = connection.cursor()
for _ in range(100):
    username = fake.user_name()
    email = fake.email()
    hashed_password = hash_password(fake.password())  # Assuming hash_password function is defined as per previous example
    full_name = fake.name()
    phone_number = fake.phone_number()
    user_type = fake.random_element(elements=('Guest', 'Host', 'Admin'))
    created_at = fake.date_time_this_decade()

    cursor.execute("""
        INSERT INTO Users (Username, PasswordHash, Email, FullName, PhoneNumber, UserType, CreatedAt)
        VALUES (:username, :hashed_password, :email, :full_name, :phone_number, :user_type, TO_TIMESTAMP(:created_at, 'YYYY-MM-DD HH24:MI:SS'))
    """, {
        'username': username,
        'hashed_password': hashed_password,
        'email': email,
        'full_name': full_name,
        'phone_number': phone_number,
        'user_type': user_type,
        'created_at': created_at
    })

connection.commit()
cursor.close()
connection.close()
