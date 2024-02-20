using Api.Controllers;
using Client.Home;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Api;

public class HomeController : BaseController
{
    [HttpGet(HomeRequest.ActionRoute)]
    public Task<HomeResponse> Get() => Task.FromResult<HomeResponse>(new("Hello from the API!"));
}