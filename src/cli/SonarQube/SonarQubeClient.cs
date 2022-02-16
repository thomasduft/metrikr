using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using metrikr.Extensions;

namespace metrikr.SonarQube;

public class SonarQubeClient
{
  private readonly string _domain;
  private readonly HttpClient _client;

  public SonarQubeClient(string domain, string apiKey)
  {
    _domain = domain;

    _client = CreateClient(apiKey);
  }

  public async Task<ProjectInfo> GetProjects()
  {
    var response = await _client.GetAsync(CreateUri("api/components/search?qualifiers=TRK"));
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    var info = json.FromJson<ProjectInfo>();

    return await Task.FromResult(info);
  }

  public async Task<MetricInfo> GetMetrics()
  {
    var response = await _client.GetAsync(CreateUri("api/metrics/search"));
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    var info = json.FromJson<MetricInfo>();

    return await Task.FromResult(info);
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
    // TODO: review/rework and test

    var client = new HttpClient();

    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKey);

    return client;
  }

  private string CreateUri(string endpoint)
  {
    return $"{_domain}/{endpoint}";
  }
}