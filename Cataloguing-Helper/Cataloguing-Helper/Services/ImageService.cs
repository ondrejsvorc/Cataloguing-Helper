using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CataloguingHelper.Services
{
    internal class ImageService
    {
        private FolderBrowserDialog _folderBrowserDialog;

        public string FolderPath { get; internal set; }

        public ImageService()
        {
            _folderBrowserDialog = new FolderBrowserDialog();
        }

        public FolderBrowserDialog OpenDialog()
        {
            _folderBrowserDialog.ShowDialog();
            return _folderBrowserDialog;
        }

        public void RenameImages(int currentSugarNumber, string imagesPath, int sugarMaxLength)
        {
            string path = GetNewFolderPath();
            Directory.CreateDirectory(path);
            string imageName;
            int zerosCount;
            int imageIndex = 0;
            string finalSugarNumber;
            string[] imagesToRename = Directory.GetFiles(imagesPath);

            foreach (string image in imagesToRename)
            {
                zerosCount = sugarMaxLength - currentSugarNumber.ToString().Length;
                finalSugarNumber = $"{GetZeros(zerosCount)}{currentSugarNumber}";
                imageName = ++imageIndex % 2 > 0 ? $"{finalSugarNumber}_přední strana.jpg" : $"{finalSugarNumber}_zadní strana.jpg";
                if (imageIndex % 2 == 0) currentSugarNumber++;
                File.Copy(image, Path.Combine(path, imageName));
            }
        }

        private string GetNewFolderPath()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string newFolderName = $"Přejmenované fotky {DateTime.Now:dd/MM/yy HH-mm-ss}";
            return Path.Combine(desktopPath, newFolderName);
        }

        private string GetZeros(int zerosCount) => string.Empty.PadLeft(zerosCount, '0');
    }
}
