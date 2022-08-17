using System;

namespace Web.Areas.Api.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
