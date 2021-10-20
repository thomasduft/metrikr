using Serilog;
using System;

namespace metrikr.Utils
{
  public static class ConsoleHelper
  {
    public static void Exit(string reason)
    {
      Console.WriteLine(reason);
      Log.Information(reason);
      Environment.Exit(1);
    }

    public static void WriteYellow(string value)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write(value);
      Log.Information(value);
      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void WriteLineSuccess(string value)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(value);
      Log.Information(value);
      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void WriteLineError(string value)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(value);
      Log.Information(value);
      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void WriteLine(string value)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(value);
      Log.Information(value);
    }

    public static void WriteLineBackground(string value)
    {
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine(value);
      Log.Information(value);
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
