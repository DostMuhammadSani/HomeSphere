using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Components.Forms;

namespace FrontEndLoginSignUp
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IBrowserFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }


public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IBrowserFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Size > 0)
            {
                // Open the file stream
                await using var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // 5MB max

                // Prepare upload parameters
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation()
                        .Height(500)
                        .Width(500)
                        .Crop("fill")
                        .Quality("auto")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
