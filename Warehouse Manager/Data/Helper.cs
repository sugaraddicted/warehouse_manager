using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Warehouse_Manager.Data
{
    public static class Helper
    {
        public static ImageSource ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return null;
            }

            try
            {
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(imageData))
                {
                    stream.Position = 0;
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }

                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static byte[] ImageToByteArray(string imagePath)
        {
            byte[] byteArray = null;

            try
            {
                // Read the file into a byte array
                byteArray = File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log or display an error message
                Console.WriteLine($"Error converting image to byte array: {ex.Message}");
            }

            return byteArray;
        }
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["warehouse"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["admin"];
            string password = appConfig["oracle_db"];
            string hostname = appConfig["warehouse.c3dclaak6852.eu-north-1.rds.amazonaws.com"];
            string port = appConfig["1521"];

            return "Data Source=warehouse.c3dclaak6852.eu-north-1.rds.amazonaws.com;Initial Catalog=warehouse;User ID=admin;Password=oracle_db;";
        }
    }
}
