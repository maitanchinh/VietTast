﻿using static Vietast.Web.Utils.SD;

namespace Vietast.Web.Models
{
    public class RequestDTO
    {
        public ContentType ContentType { get; set; } = ContentType.Json;
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
