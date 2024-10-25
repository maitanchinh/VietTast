using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Vietast.Services.ProductAPI.Utils
{
    public class FirebaseInitializer
    {
        public static void InitializeFirebase()
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("vietast-88c83-firebase-adminsdk-r2wvw-2dd8dce736.json"),
            });
        }
    }
}
