using System.Net.Http.Json;
using Shiemi.Dtos;
using Shiemi.Utilities;
using Shiemi.Wrappers;

namespace Shiemi.Services;

internal record ProjectWrapper(List<ProjectDto> Projects);

public class ProjectService
{
    private readonly HttpClient _client;
    private readonly string projectBaseUri;

    public ProjectService(RestClient restClient)
    {
        _client = restClient.GetClient();
        projectBaseUri = $"{_client.BaseAddress}/Project";
    }

    public async Task Create(CreateProjectDto dto)
    {
        await _client.PostAsJsonAsync<CreateProjectDto>(
            projectBaseUri,
            dto
        );
    }

    public async Task<List<ProjectDto>?> GetAllByUser(int id)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/all/{id}"
        );
        Debug.WriteLine($"GetAllProjectsByUser status: {response.StatusCode}");

        var wrap = await response.Content.ReadFromJsonAsync<ProjectsWrap>();
        return wrap!.Projects;
    }

    public async Task<List<ProjectDto>?> GetUserJoinedProjects(int id)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/all/{id}/user-joined"
        );
        Debug.WriteLine($"GetAllProjectsByUser status: {response.StatusCode}");

        var wrap = await response.Content.ReadFromJsonAsync<ProjectsWrap>();
        return wrap!.Projects;
    }

    public async Task<List<ProjectDto>?> GetAll()
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/all"
        );

        Debug.WriteLine($"GetAllProjects status: {response.StatusCode} ");

        var wrapper = await response.Content.ReadFromJsonAsync<ProjectWrapper>();
        return wrapper!.Projects;
    }
}