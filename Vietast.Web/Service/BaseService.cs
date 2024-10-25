using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Vietast.Web.Models;
using Vietast.Web.Service.IService;
using static Vietast.Web.Utils.SD;

namespace Vietast.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory clientFactory, ITokenProvider tokenProvider)
        {
            _clientFactory = clientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            HttpClient client = _clientFactory.CreateClient();
            HttpRequestMessage message = new HttpRequestMessage();
            if (requestDTO.ContentType == ContentType.MultipartFormData)
            {
                message.Headers.Add("Accept", "*/*");
            }
            else
            {
                message.Headers.Add("Accept", "application/json");
            }
            if (withBearer)
            {
                message.Headers.Add("Authorization", $"Bearer {_tokenProvider.GetToken()}");
            }
            message.RequestUri = new Uri(requestDTO.Url);

            if (requestDTO.Data != null)
            {
                if (requestDTO.ContentType == ContentType.MultipartFormData)
                {
                    var multipartContent = new MultipartFormDataContent();
                    var properties = requestDTO.Data.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        var propName = property.Name;
                        var propValue = property.GetValue(requestDTO.Data);

                        // Handle byte[] (e.g., file upload)
                        if (propValue is IFormFile formFile)
                        {
                            if (formFile != null)
                            {
                                multipartContent.Add(new StreamContent(formFile.OpenReadStream()), property.Name, formFile.FileName);
                            }
                            //using (var memoryStream = new MemoryStream())
                            //{
                            //    await formFile.CopyToAsync(memoryStream);
                            //    var fileBytes = memoryStream.ToArray();
                            //    var byteArrayContent = new ByteArrayContent(fileBytes);
                            //    multipartContent.Add(byteArrayContent, propName, "filename");
                            //}   
                        }
                        else
                        {
                            // Handle other types as string content
                            multipartContent.Add(new StringContent(propValue?.ToString() ?? string.Empty), propName);
                        }
                    }

                    message.Content = multipartContent;
                }
                else
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }
            }

            HttpResponseMessage apiResponse = new HttpResponseMessage();
            switch (requestDTO.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
            apiResponse = await client.SendAsync(message);
            try
            {
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDTO { IsSuccess = false, Message = "Not found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDTO { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDTO { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.BadRequest:
                        return new ResponseDTO { IsSuccess = false, Message = "Bad request" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDTO { IsSuccess = false, Message = "Internal server error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return responseDTO;
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
