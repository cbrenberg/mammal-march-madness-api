using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Resources;



[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
  private IAuthenticationService _authService;

  public AuthenticationController(IAuthenticationService authService)
  {
    _authService = authService;
  }

  [AllowAnonymous]
  [HttpPost, Route("request")]
  public ActionResult RequestToken([FromBody] TokenRequestResource request)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest("Invalid Request");
    }

    string token;
    if (_authService.IsAuthenticated(request, out token))
    {
      return Ok(token);
    }

    return BadRequest("Invalid Request");

  }
}