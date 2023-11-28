using Api.AccessPolicies;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Authorize(Policy = Policies.AdminPolicy)]
public class AdminOnlyBaseController : BaseController
{
}