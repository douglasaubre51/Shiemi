using Shiemi.Dtos.ProjectsDtos;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class ProjectService
{
    private readonly HttpClient _client;
    private string projectBaseUri;

    public ProjectService(RestClient restClient)
    {
        _client = restClient.GetClient();
        projectBaseUri = $"{_client.BaseAddress}/Project";
    }


    public async Task Create(ProjectDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync<ProjectDto>(
            projectBaseUri,
            dto
            );

        Debug.WriteLine("create project status code: " + response.StatusCode);
    }
}
