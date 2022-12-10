namespace Cataloguing_Helper.Services.Image
{
    public interface IImageService
    {
        /// <summary>
        /// Renames images based on current sugar number, path of the images to be renamed. 
        /// </summary>
        /// <param name="currentSugarNumber"></param>
        /// <param name="imagesPath"></param>
        /// <param name="sugarMaxLength"></param>
        void RenameImages(int currentSugarNumber, string imagesPath, int sugarMaxLength = 6);
    }
}
