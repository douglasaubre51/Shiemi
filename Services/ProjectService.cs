using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Shiemi.Dto;
using Shiemi.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shiemi.Services
{
    public class ProjectService
    {
        //di
        IConnectivity _connectivity;
        JsonSerializerOptions _jsonCasing;

        HttpClient _client;

        public ProjectService(IConnectivity connectivity, JsonSerializerOptions jsonSerializerOptions)
        {
            _client = new HttpClient();
            _jsonCasing = jsonSerializerOptions;

            _connectivity = connectivity;
        }

        // fetch all projects by userId
        // GET: userId
        public async Task<ObservableCollection<ProjectModel>?> GetAllProjects(string userId)
        {
            // check connection!
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("offline!");
                return null;
            }

            string url = "http://localhost:3000/project/get-all?userId=" + userId;

            HttpResponseMessage response = await _client.GetAsync(url);
            string payload = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // parse into bson document
            BsonDocument bsonDoc = BsonDocument.Parse(payload);
            // extract projectsDto
            BsonArray projectsDto = bsonDoc["projectsDto"].AsBsonArray;

            // serialize each nested doc and convert to list
            ObservableCollection<ProjectModel>? projects = [];
            projects = [.. projectsDto.Select(b => BsonSerializer.Deserialize<ProjectModel>(b.AsBsonDocument))];

            return projects;
        }

        //POST: create
        public async Task<bool> AddProject(ProjectDto project)
        {
            string url = "http://localhost:3000/project/create";

            HttpResponseMessage response = await _client
                .PostAsJsonAsync<ProjectDto>(
                url,
                project,
                _jsonCasing
                );
            string jsonString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode is false)
            {
                Debug.WriteLine($"Addproject error: {jsonString}");
                return false;
            }

            // success
            return true;
        }
    }
}
