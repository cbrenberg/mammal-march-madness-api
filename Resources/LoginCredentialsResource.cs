using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MMM_Bracket.API.Resources
{
  public class LoginCredentialsResource
  {
    [Required]
    [JsonProperty("username")]
    public string Username { get; set; }

    [Required]
    [JsonProperty("password")]
    public string Password { get; set; }
  }
}