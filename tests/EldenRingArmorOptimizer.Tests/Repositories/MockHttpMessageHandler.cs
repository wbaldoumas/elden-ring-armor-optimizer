using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace EldenRingArmorOptimizer.Tests.Repositories;

[ExcludeFromCodeCoverage]
public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly string _response;

    public MockHttpMessageHandler(string response)
    {
        _response = response;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(
            new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(_response)
            }
        );
    }
}
