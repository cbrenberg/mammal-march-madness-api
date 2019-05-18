using System;

namespace MMM_Bracket.API.Domain.Models
{
  public class UserResource
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string IsAdmin { get; set; }
    public string RefreshToken { get; set; }
  }
}
