# metrikr

A tool to visualize your metrics over time

## METRICS

- Maintainability index (midx)
- Class coupling (ccoup)
- Cyclomatic complexity (ccomp)
- Depth of inheritance (dofin)
- Code Coverage (ccov)

## Workflow

Prerequisites:
- current sources locally pulled
- config file adjusted to point to the pulled directory

### 1. Generating assets => MetricSources

- metrics.xml
- sonarqube.json

Brainstorming:
- xlsx file

### 2. Creating a run

- parsing metric sources
- generates a run => json structure with results

### 3. Use runs to visualize metrics over time

- create dataset that feeds a line chart in a simple html page
- feed a xlsx sheet
  - powerpivot table
  - charts

## Resources

- [How to: Generate code metrics data](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-generate-code-metrics-data?view=vs-2022)
- [dotnet / roslyn-analyzers](https://github.com/dotnet/roslyn-analyzers)

## Domain

### Configuration

- Configuration
  - RepositoryPath
  - Projects
    - Name
    - ProjectId
    - ProjectPath
    - Metrics
      - Id
      - Name

- Run
  - Name
  - Date
  - Participants
    - ProjectId
    - Results
      - MetricId
      - Value
