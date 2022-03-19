using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.IO;
using CataloguingHelper.Exceptions;
using CataloguingHelper.Services;

namespace CataloguingHelper
{
    public partial class MainWindow : Window
    {
        private ImageService _imageService;
        private Regex _regex;

        public MainWindow()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            _imageService = new ImageService();
            _regex = new Regex("[^0-9]+");
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = _regex.IsMatch(e.Text);
        private void OnOpenFolderClick(object sender, RoutedEventArgs e) => txtBoxDirPath.Text = _imageService.OpenDialog().SelectedPath;

        private void OnRenameImagesClick(object sender, RoutedEventArgs e)
        {
            lbDone.Content = string.Empty;

            bool isSugarNumberEmpty = txtBoxNumber.Text.Length == 0;
            bool isDirPathEmpty = txtBoxDirPath.Text.Length == 0;
            bool isSugarNumberInvalid = txtBoxNumber.Text.Length != txtBoxNumber.MaxLength;

            try
            {
                if (isSugarNumberEmpty && isDirPathEmpty)
                    throw new InvalidSugarNumberAndDirectoryPathException();

                if (isSugarNumberInvalid)
                    throw new InvalidSugarNumberException();

                if (isDirPathEmpty)
                    throw new InvalidDirectoryPathException();
            }
            catch 
            { 
                return;
            }

            _imageService.RenameImages(Convert.ToInt32(txtBoxNumber.Text), txtBoxDirPath.Text, txtBoxNumber.MaxLength);
            lbDone.Content = "Fotky byly přejmenovány.";
        }
    }
}
