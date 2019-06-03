using Newtonsoft.Json;

namespace MMM_Bracket.API.Resources
{
    public class RefreshTokenRequestParams
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
