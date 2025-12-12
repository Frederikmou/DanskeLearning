using System.Net.Http.Json; 
using Core.Models;



namespace DanskeLearning.Services.LearningAssignmentService;

public class LearningAssignmentService : ILearningAssignmentService
{
    private readonly HttpClient _httpClient;

    public LearningAssignmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LearningAssignment> CreateAssignmentAsync(LearningAssignment assignment)
    {
        var response = await _httpClient.PostAsJsonAsync("api/AdminAssign", assignment);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LearningAssignment>() ?? throw new InvalidOperationException();
    }

    public async Task<List<LearningAssignment>> GetAllAssignmentsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LearningAssignment>>("api/AdminAssign") ?? new List<LearningAssignment>();
    }

    public async Task<LearningAssignment?> GetAssignmentByIdAsync(int assignmentId)
    {
        return await _httpClient.GetFromJsonAsync<LearningAssignment>($"api/AdminAssign/{assignmentId}");
    }

    public async Task<List<LearningAssignment>> GetAssignmentsByUserAsync(Guid userId)
    {
        return await _httpClient.GetFromJsonAsync<List<LearningAssignment>>($"api/AdminAssign/user/{userId}") ?? new List<LearningAssignment>();
    }

    public async Task<List<LearningAssignment>> GetAssignmentsByStatusAsync(Guid userId, bool status)
    {
        return await _httpClient.GetFromJsonAsync<List<LearningAssignment>>($"api/AdminAssign/user/{userId}/status/{status}") ?? new List<LearningAssignment>();
    }

    public async Task DeleteAssignmentAsync(int assignmentId)
    {
        var response = await _httpClient.DeleteAsync($"api/AdminAssign/{assignmentId}");
        response.EnsureSuccessStatusCode();
    }
}