using System;
using System.Windows.Forms;

namespace Cataloguing_Helper.Dialogs
{
    public class FolderDialogManager : IDisposable
    {
        private readonly FolderBrowserDialog _folderBrowserDialog;

        public FolderDialogManager()
        {
            // Initiate the FolderBrowserDialog object.
            _folderBrowserDialog = new FolderBrowserDialog();
        }

        public FolderBrowserDialog OpenDialog()
        {
            // Show the dialog and return it.
            _folderBrowserDialog.ShowDialog();
            return _folderBrowserDialog;
        }

        public void Dispose()
        {
            // Dispose the FolderBrowserDialog object when the class is disposed.
            _folderBrowserDialog.Dispose();
        }
    }
}
