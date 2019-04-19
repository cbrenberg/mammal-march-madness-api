using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Users
  {
    public Users()
    {
      BracketPicks = new HashSet<BracketPicks>();
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string IsAdmin { get; set; }

    public virtual ICollection<BracketPicks> BracketPicks { get; set; }
  }
}
