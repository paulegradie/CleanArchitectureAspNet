using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Authorize]
public class GeneralBaseController : BaseController
{
    
}