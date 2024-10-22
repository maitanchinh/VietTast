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
        public BaseService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            HttpClient client = _clientFactory.CreateClient();
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(requestDTO.Url);
            if (requestDTO.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
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
