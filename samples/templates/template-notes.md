# Fitness report

<script src="https://cdn.jsdelivr.net/npm/chart.xkcd@1.1/dist/chart.xkcd.min.js"></script>

## Module XY

<div class="container">
  <svg class="metric-chart"></svg>
</div>
<script>
  new chartXkcd.Line(document.querySelector('.metric-chart'),
    {
      title: 'Metrics title',
      data: {
        labels: ['pi-2201', 'pi-2202', 'pi-2203', 'pi-2204'],
        datasets: [
          {
            label: 'module a',
            data: [30, 70, 200, 300],
          },
          {
            label: 'module b',
            data: [10, 15.8, 30.7, 70.5],
          },
          {
            label: 'module c',
            data: [60, 20, 30, 700],
          },
          {
            label: 'module d',
            data: [0, 1, 30, 700],
          },
          {
            label: 'module e',
            data: [60, 20, 30, 500],
          },
          {
            label: 'module f',
            data: [200, 1, 30, 70],
          }
        ],
      }
    });
</script>
