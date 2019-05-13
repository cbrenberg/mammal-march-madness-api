using System;

namespace MMM_Bracket.API.Domain.Models
{
  public class UserResource
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string IsAdmin { get; set; }
    public string Token { get; set; }
  }
}
