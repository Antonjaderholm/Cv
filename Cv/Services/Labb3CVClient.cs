using Cv.Models;

public class Labb3CVClient
{
    private readonly HttpClient _httpClient;

    public Labb3CVClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7160/api/");
    }

    // Skills methods
    public async Task<List<Skill>> GetSkillsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("skills");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Skill>>() ?? new List<Skill>();
        }
        catch (Exception)
        {
            return new List<Skill>();
        }
    }

    public async Task<Skill> AddSkillAsync(Skill skill)
    {
        Console.WriteLine($"Sending POST request to add skill: {skill.Name}");
        var response = await _httpClient.PostAsJsonAsync("api/Skills", skill);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Skill>();
    }

    public async Task DeleteSkillAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/Skills/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Projects methods
    public async Task<List<Project>> GetProjectsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("projects");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Project>>() ?? new List<Project>();
        }
        catch (Exception)
        {
            return new List<Project>();
        }
    }

    public async Task<Project> AddProjectAsync(Project project)
    {
        Console.WriteLine($"Sending POST request to add project: {project.Title}");
        var response = await _httpClient.PostAsJsonAsync("api/Projects", project);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Project>();
    }

    public async Task DeleteProjectAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/Projects/{id}");
        response.EnsureSuccessStatusCode();
    }
}