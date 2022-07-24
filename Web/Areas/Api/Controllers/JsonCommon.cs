using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web.Areas.Api.Controllers
{
    public static class JsonCommon
    {
        public static string ConvertToJson(object item)
        {
            return JsonSerializer.Serialize(item, new JsonSerializerOptions 
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            });
            //return JsonConvert.SerializeObject(item, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});
        }
    }
}
