using Cataloguing_Helper.Dialogs;
using CataloguingHelper.Exceptions;
using CataloguingHelper.Services.Image;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CataloguingHelper
{
    public partial class MainWindow : Window
    {
        private readonly FolderDialogManager _folderDialogManager;
        private readonly ImageService _imageService;
        private readonly Regex _regex;

        public MainWindow()
        {
            InitializeComponent();

            _folderDialogManager = new FolderDialogManager();
            _imageService = new ImageService();
            _regex = new Regex("[^0-9]+");
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void OnOpenFolderClick(object sender, RoutedEventArgs e)
        {
            txtBoxDirPath.Text = _folderDialogManager.OpenDialog().SelectedPath;
        }

        private void OnRenameImagesClick(object sender, RoutedEventArgs e)
        {
            // Reset label content.
            lbDone.Content = string.Empty;

            // Check if the sugar number and directory path are empty.
            bool isSugarNumberEmpty = string.IsNullOrEmpty(txtBoxNumber.Text);
            bool isDirPathEmpty = string.IsNullOrEmpty(txtBoxDirPath.Text);

            // Check if the sugar number is invalid.
            bool isSugarNumberInvalid = txtBoxNumber.Text.Length != txtBoxNumber.MaxLength;

            try
            {
                // Throw an exception if the sugar number and directory path are empty.
                if (isSugarNumberEmpty && isDirPathEmpty)
                    throw new InvalidSugarNumberAndDirectoryPathException();

                // Throw an exception if the sugar number is invalid.
                if (isSugarNumberInvalid)
                    throw new InvalidSugarNumberException();

                // Throw an exception if the directory path is empty.
                if (isDirPathEmpty)
                    throw new InvalidDirectoryPathException();
            }
            catch
            {
                return;
            }

            // Rename the images.
            _imageService.RenameImages(Convert.ToInt32(txtBoxNumber.Text), txtBoxDirPath.Text);

            // Update the done label.
            lbDone.Content = "Fotky byly přejmenovány.";
        }
    }
}
