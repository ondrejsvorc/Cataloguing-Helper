﻿using System;
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
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        Regex regex = new Regex("[^0-9]+");

        int imageIndex = 0;

        bool bothRenamed;
        bool keepProcessing;

        public MainWindow()
        {
            InitializeComponent();
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

            if (txtBoxNumber.Text == string.Empty && txtBoxDirPath.Text == string.Empty)
            {
                MessageBox.Show("Musíte zadat číslo prvního cukru (např. 003447) a také vybrat složku s fotkami.");
                keepProcessing = false;
            }
            else if (txtBoxNumber.Text == string.Empty || txtBoxNumber.Text.Length != 6)
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

                int imageNumber = 0;

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string newFolderName = Path.Combine(desktopPath, $"Přejmenované fotky {DateTime.Now:dd/MM/yy HH-mm-ss}");

                string[] imagesToRename = Directory.GetFiles(txtBoxDirPath.Text);

                Directory.CreateDirectory(newFolderName);

                if (!(Convert.ToInt32(txtBoxNumber.Text[1].ToString()) > 0))
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text.Substring(2));
                    zeros = "00";
                }
                else
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text.Substring(1));
                    zeros = "0";
                }

                if (Convert.ToInt32(txtBoxNumber.Text[0].ToString()) > 0)
                {
                    imageNumber = Convert.ToInt32(txtBoxNumber.Text);
                    zeros = "";
                }

                foreach (string image in imagesToRename)
                {
                    if (++imageIndex % 2 > 0)
                    {
                        imageName = $"{zeros}{imageNumber}_přední strana.jpg";
                        imageIndex = -1;
                    }
                    else
                    {
                        imageName = $"{zeros}{imageNumber}_zadní strana.jpg";
                        bothRenamed = true;
                    }

                    File.Copy(image, Path.Combine(newFolderName, imageName));

                    if (bothRenamed)
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
