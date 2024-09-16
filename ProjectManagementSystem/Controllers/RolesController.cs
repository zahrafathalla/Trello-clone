﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Abstractions;
using ProjectManagementSystem.CQRS.Roles.Command;
using ProjectManagementSystem.CQRS.Users.Queries;
using ProjectManagementSystem.Errors;
using ProjectManagementSystem.Helper;
using ProjectManagementSystem.ViewModel;

namespace ProjectManagementSystem.Controllers;

public class RolesController : BaseController
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddRoleToUser")]
    public async Task<Result<bool>> AddRoleToUser([FromBody] AddRoleToUserViewModel viewModel)
    {
      var resultuser = await _mediator.Send(new GetUserByEmailQuery(viewModel.Email));

        if (resultuser.IsSuccess)
        {
            var response = await _mediator.Send(new AddRoleToUserCommand(resultuser.Data,viewModel.RoleName));
            return Result.Success(true);
        }
        return Result.Failure<bool>(UserErrors.UserNotFound);
    }
}
