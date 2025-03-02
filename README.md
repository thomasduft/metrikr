# MetrikR

A tool to visualize your [SonarQube](https://www.sonarqube.org/) metrics over time.

## Workflow

Prerequisites:

- SonarQube api key available
- metrikr installed as a global dotnet tool
- [configuration](#configuration-configjson) file prepared

### 1. Creating a run

> metrikr new-run "pi-2201" config.json \<my-sonarqube-apikey>

- based on the configured projects scans and pulls the sonarqube metrics
- generates a json-file known as a **run** that can be versioned

### 2. Use runs to visualize metrics over time

> metrikr visualize config.json

- reads the runs
- based on the configured visualization strategy creates the visualization

## How to use

> metrikr -h

```console
Usage: metrikr [options] [command]

Options:
  -?|-h|--help  Show help information

Commands:
  new-run        Creates a new run (i.e. new-run "pi-2201" config.json <my-sonarqube-apikey>).
  quality-gates  Creates a quality gate markdown overview page based on the configured projects (i.e. quality-gates config.json).
  visualize      Visualizes runs based on a strategy (i.e. visualize config.json).

Use "metrikr [command] --help" for more information about a command.
```

### Configuration (config.json)

```json
{
  // domain where your sonarqube is running
  "SonarQubeDomain": "https://sonar-qube.com",
  // directory where runs are stored and versioned
  "RunsDirectory": "../../samples/runs",
  // directory where visualizations are stored and versioned
  "VisualizationsDirectory": "../../samples/visualizations",
  // strategy of how runs will be visualized
  "VisualizationStrategy": "html",
  // sonarqube projects to scan for metrics with some human readable meta-data
  "Projects": [
    {
      "Id": "audit",
      "Name": "Audit module"
    },
    {
      "Id": "message-history",
      "Name": "Message history module"
    },
    {
      "Id": "trending",
      "Name": "Trending module"
    }
  ],
  // sonarqube available metrics to be visualized over time with some human readable meta-data
  "Metrics": [
    {
      "Id": "complexity",
      "Name": "Cyclomatic Complexity",
      "Description": "Super description for Cyclomatic Complexity"
    },
    {
      "Id": "cognitive_complexity",
      "Name": "Cognitive Complexity",
      "Description": "Super description for Cognitive Complexity"
    },
    {
      "Id": "coverage",
      "Name": "Overall Coverage",
      "Description": "Super description for Overall Coverage"
    }
  ]
}
```
