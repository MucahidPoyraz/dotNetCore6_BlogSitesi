using DTOs.Images;
using Entity.Enums;
using Microsoft.AspNetCore.Http;

namespace BL.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageTypes imageType, string folderName = null);
        void Delete(string imageName);
    }
}
