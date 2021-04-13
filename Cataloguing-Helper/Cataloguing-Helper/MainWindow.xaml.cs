using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;
using MessageBox = System.Windows.MessageBox;

namespace CataloguingHelper
{
    public partial class MainWindow : Window
    {
        FolderBrowserDialog folderBrowserDialog;
        Regex regex;

        int imageIndex;

        bool bothRenamed;
        bool keepProcessing;

        public MainWindow()
        {
            InitializeComponent();
            
            folderBrowserDialog = new FolderBrowserDialog();
            regex = new Regex("[^0-9]+");
            imageIndex = 0;
        }
        
        private void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            txtBoxDirPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void RenameImages(object sender, RoutedEventArgs e)
        {
            lbDone.Content = string.Empty;

            keepProcessing = true;
            
            // Input validation
            if (txtBoxNumber.Text == string.Empty && txtBoxDirPath.Text == string.Empty)
            {
                MessageBox.Show("Musíte zadat číslo prvního cukru (např. 003447) a také vybrat složku s fotkami.");
                keepProcessing = false;
            }
            else if (txtBoxNumber.Text == string.Empty && (txtBoxNumber.Text.Length != 6 || txtBoxNumber.Text.Length != 4))
            {
                MessageBox.Show("Musíte zadat číslo prvního cukru (např. 003447).");
                keepProcessing = false;
            }
            else if (txtBoxDirPath.Text == string.Empty)
            {
                MessageBox.Show("Musíte vybrat složku s fotkami. Klikněte na tlačítko vedle textového pole.");
                keepProcessing = false;
            }

            if (keepProcessing)
            {
                string imageName;
                string zeros = "";

                bool whatToCheck;

                int imageNumber = 0;

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string newFolderName = Path.Combine(desktopPath, $"Přejmenované fotky {DateTime.Now:dd/MM/yy HH-mm-ss}");

                string[] imagesToRename = Directory.GetFiles(txtBoxDirPath.Text);

                Directory.CreateDirectory(newFolderName);

                // Determination of how big the number is
                if (!(Convert.ToInt32(txtBoxNumber.Text[2].ToString()) > 0))
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text.Substring(2));
                    zeros = "000";
                }
                else if (!(Convert.ToInt32(txtBoxNumber.Text[1].ToString()) > 0))
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text.Substring(2));
                    zeros = "00";
                    whatToCheck = imageNumber < 100;
                }
                else
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text.Substring(1));
                    zeros = "0";
                    whatToCheck = imageNumber < 100;
                }

                if (Convert.ToInt32(txtBoxNumber.Text[0].ToString()) > 0)
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text);
                    zeros = "";
                }
                
                // imageIndex only acquieres three values: -1, 0, 1  
                // (we never overwhelm our RAM)
                foreach (string image in imagesToRename)
                {
                    int currentLength = (zeros + imageNumber).Length;

                    if (currentLength != txtBoxNumber.Text.Length)
                    {
                        if (currentLength == txtBoxNumber.Text.Length + 1)
                        {
                            zeros = zeros.Substring(1);
                        }
                    }

                    if (++imageIndex % 2 > 0)
                    {
                        imageName = $"{zeros}{imageNumber}_přední strana.jpg";      // If a photo position, relative to other photos, is even-numbered
                        imageIndex = -1;
                    }
                    else
                    {
                        imageName = $"{zeros}{imageNumber}_zadní strana.jpg";       // If a photo position, relative to other photos, is odd-numbered
                        bothRenamed = true;
                    }

                    File.Copy(image, Path.Combine(newFolderName, imageName));

                    if (bothRenamed)                                                // If both photos are renamed, increment a photo number
                    {
                        imageNumber++;
                        bothRenamed = false;
                    }
                }

                lbDone.Content = "Fotky byly přejmenovány.";
            }
        }
    }
}
