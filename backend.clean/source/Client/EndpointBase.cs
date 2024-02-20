using System.Net.Http.Json;
using Client.Contracts;
using Client.Exceptions;

namespace Client;

public abstract class EndpointBase(HttpClient client)
{
    internal async Task<TResponse> Post<TRequest, TResponse>(TRequest command, CancellationToken cancellationToken)
        where TRequest : RequestBase
    {
        var response = await client.PostAsJsonAsync(command.GetActionRoute(), command, cancellationToken);
        await CatchErrorsAndThrow(response);
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken) ??
               throw new ResponseEmptyException(command.GetActionRoute());
    }

    internal async Task<TResponse> Get<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : RequestBase
    {
        var response = await client.GetAsync(request.GetActionRoute(), cancellationToken);
        await CatchErrorsAndThrow(response);
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken) ??
               throw new ResponseEmptyException(request.GetActionRoute());
    }

    private static async Task CatchErrorsAndThrow(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new ServerClientException(string.Join(", ", errorResponse?.Messages ?? Array.Empty<string>()));
        }
    }
}