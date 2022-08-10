namespace tomware.MetrikR.Visualization;

public interface IVisualizationStrategy
{
  string Key { get; }

  void Visualize(VisualizationParam param);
}