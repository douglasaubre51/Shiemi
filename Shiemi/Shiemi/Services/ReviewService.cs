using Shiemi.Models;
using Shiemi.Utilities;
using System.Net.Http.Json;

namespace Shiemi.Services;

public class ReviewService
{
    private readonly string _reviewBaseURI;

    private readonly HttpClient _httpClient;

    public ReviewService(RestClient restClient)
    {
        _httpClient = restClient.GetClient();
        _reviewBaseURI = $"{_httpClient.BaseAddress}/Review";
    }

    public IAsyncEnumerable<Review?> GetReviewsByProject(int projectId)
         => _httpClient.GetFromJsonAsAsyncEnumerable<Review>(
            $"{_reviewBaseURI}/{projectId}/all"
            );
    public async Task<Review?> GetById(int reviewId)
         => await _httpClient.GetFromJsonAsync<Review>(
            $"{_reviewBaseURI}/{reviewId}/review"
            );

    public async Task<bool> CreateReview(Review review)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<Review>(
          $"{_reviewBaseURI}",
          review
          );
        if (response.IsSuccessStatusCode is false)
            return false;

        return true;
    }
}
