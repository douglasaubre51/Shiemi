using Shiemi.Dtos.ProjectsDtos;
using Shiemi.Models;
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


    // CREATE
    public async Task Create(ProjectDto dto)
        => await _client.PostAsJsonAsync<ProjectDto>(
            projectBaseUri,
            dto
            );


    // READ
    public async Task<List<Project>?> GetAll(int id)
    {
        HttpResponseMessage response = await _client.GetAsync(
            $"{projectBaseUri}/all/{id}"
            );

        Debug.WriteLine($"GetAllProjects status: {response.StatusCode}");

        var payload = await response.Content.ReadFromJsonAsync<ProjectsWrap>();
        return payload!.Projects;
    }
}
