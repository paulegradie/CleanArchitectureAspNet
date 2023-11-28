using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[AllowAnonymous]
public class AnonymousBaseController : BaseController
{
    
}