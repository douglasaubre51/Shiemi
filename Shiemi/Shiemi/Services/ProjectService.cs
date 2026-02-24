using Shiemi.Dtos;
using Shiemi.Utilities;
using Shiemi.Wrappers;
using System.Net.Http.Json;

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

    public async Task DeleteProject(int projectId)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/{projectId}/delete"
            );
        if(response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("EditProject post: error: " + response.StatusCode);
        }
    }

    public async Task EditProject(EditProjectDto dto)
    {
        var response = await _client.PostAsJsonAsync<EditProjectDto>(
            $"{projectBaseUri}/edit",
            dto);
        if(response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("EditProject post: error: " + response.StatusCode);
        }
    }

    public async Task Create(CreateProjectDto dto)
    {
        await _client.PostAsJsonAsync<CreateProjectDto>(
            projectBaseUri,
            dto
        );
    }

    public async Task RemoveDevFromProject(int projectId, int clientId)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/{projectId}/{clientId}/remove-client");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine($"RemoveDevFromProject get error:{response.StatusCode}");
            return;
        }
    }
    public async Task AddDevToProject(int projectId, int clientId)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/{projectId}/{clientId}/add-client");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine($"get error:{response.StatusCode}");
            return;
        }
    }

    public async Task<List<SearchProjectsDto>?> GetSearchedProjects(string title)
    {
        var response = await _client.GetAsync($"{projectBaseUri}/{title}/search");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine($"get error:{response.StatusCode}");
            return null!;
        }

        return await response.Content.ReadFromJsonAsync<List<SearchProjectsDto>>();
    }

    public async Task<List<UserDetailsDto>?> GetAllPotentialCandidates(int projectId)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/{projectId}/devs-contacted/all");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine($"get error:{response.StatusCode}");
            return null!;
        }

        return await response.Content.ReadFromJsonAsync<List<UserDetailsDto>>();
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
    public async Task<ProjectDto?> GetById(int id)
    {
        var response = await _client.GetAsync(
            $"{projectBaseUri}/{id}");

        Debug.WriteLine($"GetProjectById: status: {response.StatusCode}");

        return await response.Content.ReadFromJsonAsync<ProjectDto>();
    }
}