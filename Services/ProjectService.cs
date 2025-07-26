using CommunityToolkit.Maui.Core.Extensions;
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

        public ProjectService(IConnectivity connectivity, JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonCasing = jsonSerializerOptions;
            _connectivity = connectivity;
        }

        // fetch all projects
        // GET projects/get-all
        public async Task<ObservableCollection<ProjectModel>?> GetAllProjects()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("offline!");
                return null;
            }

            string url = "http://localhost:3000/project/get-all";
            HttpClient _client = new HttpClient();

            HttpResponseMessage response = await _client.GetAsync(url);
            string payloadString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"getall projects error: {payloadString}");
                return null;
            }

            // success
            // parse into doc
            BsonDocument bsonDocument = BsonDocument.Parse(payloadString);
            // select dto as array
            BsonArray bsonArray = bsonDocument["projectsDto"].AsBsonArray;

            ObservableCollection<ProjectModel>? projectModels = [];
            // deserialize each bson object into projectmodel and convert to list
            projectModels = bsonArray
                .Select(
                x => BsonSerializer.Deserialize<ProjectModel>(x.AsBsonDocument)
                )
                .ToObservableCollection();

            return projectModels;
        }

        // fetch all projects by userId
        // GET: userId
        public async Task<ObservableCollection<ProjectModel>?> GetAllProjectsById(string userId)
        {
            // check connection!
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("offline!");
                return null;
            }

            string url = "http://localhost:3000/project/getall-by-userid?userId=" + userId;
            HttpClient _client = new HttpClient();

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
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return false;
            }

            string url = "http://localhost:3000/project/create";
            HttpClient _client = new HttpClient();

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
