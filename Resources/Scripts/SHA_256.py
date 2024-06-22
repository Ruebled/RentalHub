import hashlib

# List of passwords to hash
passwords = [
    "password1", "password2", "password3", "password4", "password5",
    "password6", "password7", "password8", "password9", "password10",
    "password11", "password12", "password13", "password14", "password15",
    "password16", "password17", "password18", "password19", "password20",
    "password21"
]

# Function to hash a password using SHA-256
def hash_password(password):
    sha256 = hashlib.sha256()
    sha256.update(password.encode('utf-8'))
    return sha256.hexdigest()

# Calculate hashes for each password
hashed_passwords = [hash_password(password) for password in passwords]

# Print hashes
for i, hashed_password in enumerate(hashed_passwords, start=1):
    print(f"{i}. {hashed_password}")
