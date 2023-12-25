using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Helpers
{
    public static class ImageExtension
    {
        public static bool IsValidSize(this IFormFile formFile, float kb =20000)
        {
            return formFile.Length >= kb * 1024;
        }
        public static bool IsValidType(this IFormFile formFile, string type = "Image")
        {
            return formFile.FileName.Contains(type);
        }

        public static async Task<string> SaveAsync(this IFormFile formFile)
        {
            string fileName = Path.Combine(PathConstant.PathFolder,formFile.FileName);
            using (FileStream fs = File.Create(Path.Combine(PathConstant.RootPath, fileName)))
            {
                await formFile.CopyToAsync(fs);
            }
            return fileName;
        }
    }
}
