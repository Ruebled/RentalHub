import os
import subprocess
import urllib.parse

def download_images_from_file(file_path):
    # Create a folder for downloaded images if it doesn't exist
    folder_name = '..\\ApartmentImages'
    if not os.path.exists(folder_name):
        os.makedirs(folder_name)

    # Read URLs from file
    with open(file_path, 'r') as file:
        urls = file.readlines()

    # Download images using curl
    for url in urls:
        url = url.strip()  # Remove leading/trailing whitespace and newlines
        if url:
            try:
                # Encode URL to handle special characters properly
                encoded_url = urllib.parse.quote(url, safe=':/?&=')  # Encode special characters

                # Extract filename from URL
                filename = os.path.basename(urllib.parse.urlparse(encoded_url).path)

                # Create full path to save the image
                save_path = os.path.join(folder_name, filename)

                # Execute curl command to download image
                command = ['curl', '-L', '-s', '-o', save_path, encoded_url]
                subprocess.run(command, check=True)

                print(f"Downloaded: {filename}")
            except Exception as e:
                print(f"Failed to download {url}. Error: {str(e)}")

if __name__ == '__main__':
    # Specify the path to the file containing URLs
    file_path = 'urls.txt'
    download_images_from_file(file_path)
