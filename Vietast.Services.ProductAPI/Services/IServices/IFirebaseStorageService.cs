namespace Vietast.Services.ProductAPI.Services.IServices
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadImageAsync(string fileName, Stream imageStream);
        Task DeleteImage(string imageUrl);
    }
}
