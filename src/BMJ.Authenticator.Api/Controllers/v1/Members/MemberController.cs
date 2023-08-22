﻿using BMJ.Authenticator.Api.Caching;
using BMJ.Authenticator.Application.UseCases.Users.Commands.LoginUser;
using BMJ.Authenticator.Application.UseCases.Users.Queries.GetAllUsers;
using BMJ.Authenticator.Application.UseCases.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BMJ.Authenticator.Api.Controllers.v1.Members;

[Authorize]
[ApiVersion("1.0")]
public class MemberController : ApiControllerBase
{
    /// <summary>
    /// Reatrieves a user token to an authenticated user.
    /// </summary>
    /// <param name="loginCommandRequest">User information to get authenticated</param>
    /// <returns>Json Web Token</returns>
    [AllowAnonymous]
    [OutputCache(PolicyName = nameof(TokenCachePolicy))]
    [HttpPost("loginAsync")]
    public async Task<IActionResult> LoginAsync(LoginUserCommandRequest loginCommandRequest)
    {
        return Ok(await Mediator.Send(loginCommandRequest));
    }

    [OutputCache(PolicyName = nameof(AuthenticatorBaseCachePolicy))]
    [HttpGet("getAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await Mediator.Send(new GetAllUsersQueryRequest()));
    }

    [OutputCache(PolicyName = nameof(ByIdCachePolicy))]
    [HttpGet("getByIdAsync")]
    public async Task<IActionResult> GetByIdAsync(GetUserByIdQueryRequest getUserByIdRequest)
    {
        return Ok(await Mediator.Send(getUserByIdRequest));
    }
}
