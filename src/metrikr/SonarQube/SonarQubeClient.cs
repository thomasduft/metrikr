using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using tomware.MetrikR.Extensions;

namespace tomware.MetrikR.SonarQube;

public class SonarQubeClient
{
  private readonly string _domain;
  private readonly HttpClient _client;

  public SonarQubeClient(string domain, string apiKey)
  {
    _domain = domain;

    _client = CreateClient(apiKey);
  }

  public async Task<SonarQubeProjectInfo> GetProjects()
  {
    // PageSize set to max = 500 items
    var response = await _client.GetAsync(CreateUri("api/components/search?ps=500&qualifiers=TRK"));
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    return json.FromJson<SonarQubeProjectInfo>();
  }

  public async Task<SonarQubeProjectInfo> CreateProject()
  {
    // PageSize set to max = 500 items
    var response = await _client.PostAsync(CreateUri("api/projects/create"), null);
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    return json.FromJson<SonarQubeProjectInfo>();
  }

  public async Task<MetricInfo> GetMetrics()
  {
    // PageSize set to max = 500 items
    var response = await _client.GetAsync(CreateUri("api/metrics/search?ps=500"));
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    return json.FromJson<MetricInfo>();
  }

  public async Task<string> GetProjectBadgeToken(string projectId)
  {
    var tokenResponse = await _client.GetAsync(CreateUri($"api/project_badges/token?project={projectId}"));
    tokenResponse.EnsureSuccessStatusCode();

    var json = await tokenResponse.Content.ReadAsStringAsync();
    var token = json.FromJson<SonarQubeToken>();

    return token.Token;
  }

  public async Task<MeasureInfo> GetMetricsForProject(string projectId, string metricIds)
  {
    var queryParams = $"component={projectId}&metricKeys={metricIds}";
    var response = await _client.GetAsync(CreateUri($"api/measures/component?{queryParams}"));
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    var info = json.FromJson<MeasureInfo>();

    return await Task.FromResult(info);
  }

  private static HttpClient CreateClient(string apiKey)
  {
    var client = new HttpClient();
    var username = apiKey;
    var password = string.Empty;
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
      "Basic",
      Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))
    );

    return client;
  }

  private string CreateUri(string endpoint)
  {
    return $"{_domain}/{endpoint}";
  }

  private record SonarQubeToken(string Token);
}