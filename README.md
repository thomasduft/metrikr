# metrikr

A tool to visualize your metrics over time.

## Workflow

Prerequisites:

- SonarQube api key available

### 1. Creating a run

- pulling configures metrics from SonarQube
- generates a run => json structure with results
  - json file
  - need to be stored and versioned in git

### 2. Use runs to visualize metrics over time

- create dataset that feeds a line chart in a simple html page
- feed a xlsx sheet
  - powerpivot table
  - charts

## Resources

- [How to: Generate code metrics data](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-generate-code-metrics-data?view=vs-2022)
- [dotnet / roslyn-analyzers](https://github.com/dotnet/roslyn-analyzers)
- [Analysis Tools](https://github.com/analysis-tools-dev/static-analysis#csharp)

## Domain

### Configuration

- tbd

- Configuration
  - RepositoryPath
  - Metrics
  - Projects
    - Name
    - Domain
    - ProjectId
    - ProjectPath

- Run
  - Name
  - Date
  - Participants
    - ProjectId
    - Results
      - MetricId
      - Value
