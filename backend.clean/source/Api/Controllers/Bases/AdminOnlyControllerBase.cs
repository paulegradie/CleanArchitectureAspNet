using Authentication.Abstractions.AccessPolicies;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.Bases;

[Authorize(Policy = UserPolicies.AdminPolicy)]
public class AdminOnlyBaseController : BaseController
{
}