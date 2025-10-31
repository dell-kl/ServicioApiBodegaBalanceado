using Microsoft.AspNetCore.Http;

namespace ServicioApiBodegaBalanceado.Utility.ShareFuncionality
{
    public class SaveImages<T> where T : class
    {
        private readonly string _basePath;

        public T newClass;

        public SaveImages(string basePath, T newClass)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
            this.newClass = newClass;
        }

        public async Task<string> SaveImageAsync(IEnumerable<IFormFile> formFiles)
        {
            return null;
        }

        /// <summary>
        /// Deletes an image file
        /// </summary>
        /// <param name="filePath">Full path to the image file</param>
        /// <returns>True if deletion was successful</returns>
        public bool DeleteImage(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting image: {ex.Message}", ex);
            }
        }
    }
}