using System.Net.Http.Json;
using Client.Exceptions;

namespace Client;

public abstract class EndpointBase
{
    private readonly HttpClient client;

    protected EndpointBase(HttpClient client)
    {
        this.client = client;
    }

    internal async Task<TResponse> Post<TRequest, TResponse>(TRequest command, CancellationToken cancellationToken) where TRequest : RequestBase
    {
        var response = await client.PostAsJsonAsync(command.GetActionRoute(), command, cancellationToken);
        await CatchErrorsAndThrow(response);
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken) ?? throw new ResponseEmptyException(command.GetActionRoute());
    }

    internal async Task<TResponse> Get<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : RequestBase
    {
        var response = await client.GetAsync(request.GetActionRoute(), cancellationToken);
        await CatchErrorsAndThrow(response);
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken) ?? throw new ResponseEmptyException(request.GetActionRoute());
    }

    private static async Task CatchErrorsAndThrow(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new ApiClientException(string.Join(", ", errorResponse.Messages));
        }
    }
}