using Api.Controllers.Bases;
using Client.Contracts.Home;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class HomeController : BaseController
{
    [HttpGet(HomeRequest.ActionRoute)]
    public async Task<HomeResponse> Get() => new("Hello from the API!");
}