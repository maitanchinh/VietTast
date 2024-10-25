using Firebase.Storage;
using Vietast.Services.ProductAPI.Services.IServices;
namespace Vietast.Services.ProductAPI.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly FirebaseStorage _firebaseStorage;
        public FirebaseStorageService()
        {
            _firebaseStorage = new FirebaseStorage("vietast-88c83.appspot.com");
        }
        public Task DeleteImage(string imageUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImageAsync(string fileName, Stream imageStream)
        {
            var task = await _firebaseStorage
                .Child("products")
                .Child(fileName)
                .PutAsync(imageStream);
            return task;
        }
    }
}
