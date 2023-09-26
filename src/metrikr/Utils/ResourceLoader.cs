using System.IO;
using System.Reflection;

namespace tomware.MetrikR.Utils;

public static class ResourceLoader
{
  public static string GetResource(string template)
  {
    var assembly = Assembly.GetExecutingAssembly();
    var resourcePath = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
    var resourceName = $"{resourcePath}.Templates.{template}.liquid";

    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader reader = new(stream))
    {
      return reader.ReadToEnd();
    }

    throw new FileNotFoundException($"Template with name '{template}' does not exist!");
  }
}