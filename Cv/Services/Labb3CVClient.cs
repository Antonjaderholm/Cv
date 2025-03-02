using Cv.Models;

public class Labb3CVClient
{
    private readonly HttpClient _httpClient;
    private const string SkillsEndpoint = "api/Skills";
    private const string ProjectsEndpoint = "api/Projects";

    public Labb3CVClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Skill>> GetSkillsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Skill>>(SkillsEndpoint) ?? new List<Skill>();
    }

    public async Task<Skill> AddSkillAsync(Skill skill)
    {
        var response = await _httpClient.PostAsJsonAsync(SkillsEndpoint, skill);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Skill>();
    }

    public async Task DeleteSkillAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{SkillsEndpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Project>>(ProjectsEndpoint) ?? new List<Project>();
    }

    public async Task<Project> AddProjectAsync(Project project)
    {
        var response = await _httpClient.PostAsJsonAsync(ProjectsEndpoint, project);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Project>();
    }

    public async Task DeleteProjectAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{ProjectsEndpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }
}