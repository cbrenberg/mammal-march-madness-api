using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MMM_Bracket.API.Resources
{
    public class RegistrationRequestResource
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}