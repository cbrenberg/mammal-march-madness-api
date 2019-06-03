using Newtonsoft.Json;

namespace MMM_Bracket.API.Domain.Models.Configuration
{
  public class JWTSettings
  {
    [JsonProperty("secret")]
    public string SecretKey { get; set; }

    [JsonProperty("issuer")]
    public string Issuer { get; set; }

    [JsonProperty("audience")]
    public string Audience { get; set; }

    [JsonProperty("accessExpiration")]
    public int AccessExpiration { get; set; }

    [JsonProperty("refreshExpiration")]
    public int RefreshExpiration { get; set; }
  }
}