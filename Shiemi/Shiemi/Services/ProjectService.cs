using Shiemi.Dtos;
using Shiemi.Utilities;
using Shiemi.Wrappers;
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
        => await _client.PostAsJsonAsync<ProjectDto>(
            projectBaseUri,
            dto
            );

    public async Task<List<ProjectDto>?> GetAllByUser(int id)
    {
        HttpResponseMessage response = await _client.GetAsync(
            $"{projectBaseUri}/all/{id}"
            );
        Debug.WriteLine($"GetAllProjectsByUser status: {response.StatusCode}");

        var wrap = await response.Content.ReadFromJsonAsync<ProjectsWrap>();
        return wrap!.Projects;
    }
    public async Task<List<ProjectDto>?> GetAll()
    {
        HttpResponseMessage response = await _client.GetAsync(
            $"{projectBaseUri}/all"
            );

        Debug.WriteLine($"GetAllProjects status: {response.StatusCode} ");

        return await response.Content.ReadFromJsonAsync<List<ProjectDto>>();
    }
}
