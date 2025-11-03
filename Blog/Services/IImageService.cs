namespace Blog.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
        bool DeleteImage(string fileName);
        string GetImageUrl(string fileName);
    }
}
