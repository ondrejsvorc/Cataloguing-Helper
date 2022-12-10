using Cataloguing_Helper.Services.Image;
using System;
using System.IO;
using System.Text;

namespace CataloguingHelper.Services.Image
{
    public class ImageService : IImageService
    {
        public void RenameImages(int currentSugarNumber, string imagesPath, int sugarMaxLength = 6)
        {
            // Create a new folder to store the renamed images.
            string path = GetNewFolderPath();
            Directory.CreateDirectory(path);

            // Get the images to be renamed.
            string[] imagesToRename = Directory.GetFiles(imagesPath);

            // Loop through the images and rename them.
            for (int i = 0; i < imagesToRename.Length; i++)
            {
                // Calculate the number of zeros needed to pad the sugar number.
                int zerosCount = sugarMaxLength - currentSugarNumber.ToString().Length;
                string finalSugarNumber = $"{GetZeros(zerosCount)}{currentSugarNumber}";

                // Generate the new image name.
                string imageName;
                if (i % 2 == 0)
                {
                    imageName = $"{finalSugarNumber}_přední strana.jpg";
                }
                else
                {
                    imageName = $"{finalSugarNumber}_zadní strana.jpg";
                    currentSugarNumber++;
                }

                // Copy the image to the new folder with the new name.
                File.Copy(imagesToRename[i], Path.Combine(path, imageName));
            }
        }

        private string GetNewFolderPath()
        {
            // Get the path of the desktop folder.
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Generate a new folder name with the current date and time.
            string newFolderName = $"Přejmenované fotky {DateTime.Now:dd/MM/yy HH-mm-ss}";

            // Use a StringBuilder to create the folder path.
            var folderPathBuilder = new StringBuilder();
            folderPathBuilder.Append(desktopPath);
            folderPathBuilder.Append("\\");
            folderPathBuilder.Append(newFolderName);

            return folderPathBuilder.ToString();
        }

        private string GetZeros(int zerosCount)
        {
            // Use a char array to store the zeros.
            char[] zeros = new char[zerosCount];
            for (int i = 0; i < zeros.Length; i++)
            {
                zeros[i] = '0';
            }

            return new string(zeros);
        }
    }
}
