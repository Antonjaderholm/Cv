using System.Net.Http.Json;
using Cv.Models;

public class Labb3CVClient
{
    private readonly HttpClient _httpClient;
    private const string SkillsEndpoint = "api/Skills";
    private const string ProjectsEndpoint = "api/Projects";

    public Labb3CVClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7160/");
    }

    // Skills methods
    public async Task<Skill> AddSkillAsync(Skill skill)
    {
        Console.WriteLine($"Sending request to add skill: {skill.Name}");
        var response = await _httpClient.PostAsJsonAsync(SkillsEndpoint, skill);
        Console.WriteLine($"Response status: {response.StatusCode}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Skill>();
        Console.WriteLine($"Skill added successfully with ID: {result.Id}");
        return result;
    }
    // Skills methods
    public async Task<List<Skill>> GetSkillsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Skill>>(SkillsEndpoint) ?? new List<Skill>();
        }
        catch
        {
            return new List<Skill>();
        }
    }
   
    public async Task DeleteSkillAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{SkillsEndpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Projects methods
    public async Task<Project> AddProjectAsync(Project project)
    {
        Console.WriteLine($"Sending request to add project: {project.Title}");
        var response = await _httpClient.PostAsJsonAsync(ProjectsEndpoint, project);
        Console.WriteLine($"Response status: {response.StatusCode}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Project>();
        Console.WriteLine($"Project added successfully with ID: {result.Id}");
        return result;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Project>>(ProjectsEndpoint) ?? new List<Project>();
        }
        catch
        {
            return new List<Project>();
        }
    }
    public async Task DeleteProjectAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{ProjectsEndpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }
}