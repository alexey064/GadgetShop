using Newtonsoft.Json;

namespace Web.Areas.Api.Controllers
{
    public static class JsonCommon
    {
        public static string ConvertToJson(object item)
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
