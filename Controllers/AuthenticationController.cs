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
public class AuthorizationController : ControllerBase
{
  private ITokenAuthenticationService _tokenAuthService;
  private IMapper _mapper;
  private IUserService _userService;

  public AuthorizationController(ITokenAuthenticationService tokenAuthService, IUserService userService, IMapper mapper)
  {
    _tokenAuthService = tokenAuthService;
    _mapper = mapper;
    _userService = userService;
  }

  [AllowAnonymous]
  [HttpPost, Route("request")]
  public async Task<ActionResult> RequestUserWithToken([FromBody] TokenRequestResource request)//TODO rename tokenrequestresource to logincredentialsresource
  {
    if (!ModelState.IsValid)
    {
      return BadRequest("Invalid Request");
    }

    var authorizedUser = await getAuthorizedUser(request);

    if (authorizedUser != null)
    {
      var userResource = _mapper.Map<User, UserResource>(authorizedUser);
      string token = _tokenAuthService.CreateTokenForValidUser(userResource);
      return Ok(token);
    }

    return Unauthorized("Invalid Credentials");
  }

  private async Task<User> getAuthorizedUser(TokenRequestResource request)
  {
    return await _userService.Authenticate(request.Username, request.Password);
  }
}