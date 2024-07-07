﻿using Oracle.ManagedDataAccess.Client;

using RentalHub.Repositories;

using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

public class ImageRepository : RepositoryBase
{
    private readonly string _connectionString;

    public ImageRepository()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }

    public BitmapImage GetImageById(int imageId)
    {
        if(imageId < 0)
        {
            return null;
        }

        string query = "SELECT Image FROM Images WHERE ImageID = :ImageID";
        OracleParameter parameter = new OracleParameter("ImageID", imageId);

        try
        {
            byte[] imageBytes = null;

            // Execute query and map result to byte array
            ExecuteQuery(query, (reader) =>
            {
                imageBytes = (byte[])reader["Image"];
                return imageBytes; // Return value is ignored due to IEnumerable<T>
            }, parameter);

            if (imageBytes != null)
            {
                // Convert byte array to BitmapImage
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(imageBytes);
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            // Log or handle exception
        }

        return null; // Return null if image not found or error occurred
    }               
}