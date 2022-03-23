# JointForces fitness report

<script src="https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-autocolors"></script>
<script>Chart.register(window['chartjs-plugin-autocolors']);</script>

## Cognitive Complexity

<div><p>Super description for Cognitive Complexity</p>
<canvas id="cognitive_complexity-chart"></canvas>
<script>
  new Chart(document.getElementById('cognitive_complexity-chart'), {
    type: 'line',
    options: {
      title: {
        display: true,
        text: 'Cognitive Complexity'
      },
      plugins: {
        legend: {
          position: 'bottom'
        }
      }
    }, 
    data: {
      labels: ['pi-2201-sample'],
      datasets: [
        {
  data: [111],
  label: 'Audit module',
  fill: false
},
{
  data: [107],
  label: 'Message history module',
  fill: false
},
{
  data: [157],
  label: 'Trending module',
  fill: false
},

      ]
    }
  });
</script>
</div>


## Cyclomatic Complexity

<div><p>Super description for Cyclomatic Complexity</p>
<canvas id="complexity-chart"></canvas>
<script>
  new Chart(document.getElementById('complexity-chart'), {
    type: 'line',
    options: {
      title: {
        display: true,
        text: 'Cyclomatic Complexity'
      },
      plugins: {
        legend: {
          position: 'bottom'
        }
      }
    }, 
    data: {
      labels: ['pi-2201-sample'],
      datasets: [
        {
  data: [360],
  label: 'Audit module',
  fill: false
},
{
  data: [434],
  label: 'Message history module',
  fill: false
},
{
  data: [640],
  label: 'Trending module',
  fill: false
},

      ]
    }
  });
</script>
</div>


## Overall Coverage

<div><p>Super description for Overall Coverage</p>
<canvas id="coverage-chart"></canvas>
<script>
  new Chart(document.getElementById('coverage-chart'), {
    type: 'line',
    options: {
      title: {
        display: true,
        text: 'Overall Coverage'
      },
      plugins: {
        legend: {
          position: 'bottom'
        }
      }
    }, 
    data: {
      labels: ['pi-2201-sample'],
      datasets: [
        {
  data: [65.9],
  label: 'Audit module',
  fill: false
},
{
  data: [95.1],
  label: 'Message history module',
  fill: false
},
{
  data: [78.2],
  label: 'Trending module',
  fill: false
},

      ]
    }
  });
</script>
</div>

