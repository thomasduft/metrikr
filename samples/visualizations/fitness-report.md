# JointForces fitness report

<script src="https://cdn.jsdelivr.net/npm/chart.xkcd@1.1/dist/chart.xkcd.min.js"></script>

## Cognitive Complexity

<div><svg class="cognitive_complexity-chart"></svg></div>
<script>
  new chartXkcd.Line(document.querySelector('.cognitive_complexity-chart'), {
      title: 'Cognitive Complexity',
      data: {
        labels: ['pi-2201-sample'],
        datasets: [
          {
  label: 'Audit module',
  data: [111]
},
{
  label: 'Message history module',
  data: [107]
},
{
  label: 'Trending module',
  data: [157]
},

        ]
      }
  });
</script>

## Cyclomatic Complexity

<div><svg class="complexity-chart"></svg></div>
<script>
  new chartXkcd.Line(document.querySelector('.complexity-chart'), {
      title: 'Cyclomatic Complexity',
      data: {
        labels: ['pi-2201-sample'],
        datasets: [
          {
  label: 'Audit module',
  data: [360]
},
{
  label: 'Message history module',
  data: [434]
},
{
  label: 'Trending module',
  data: [640]
},

        ]
      }
  });
</script>

## Overall Coverage

<div><svg class="coverage-chart"></svg></div>
<script>
  new chartXkcd.Line(document.querySelector('.coverage-chart'), {
      title: 'Overall Coverage',
      data: {
        labels: ['pi-2201-sample'],
        datasets: [
          {
  label: 'Audit module',
  data: [65.9]
},
{
  label: 'Message history module',
  data: [95.1]
},
{
  label: 'Trending module',
  data: [78.2]
},

        ]
      }
  });
</script>
