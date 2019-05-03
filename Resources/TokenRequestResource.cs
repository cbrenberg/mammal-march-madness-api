using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MMM_Bracket.API.Resources
{
  public class TokenRequestResource
  {
    [Required]
    [JsonProperty("username")]
    public string Username { get; set; }

    [Required]
    [JsonProperty("password")]
    public string Password { get; set; }
  }
}