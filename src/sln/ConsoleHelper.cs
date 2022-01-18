using System;

namespace Sln
{
  public static class ConsoleHelper
  {
    public static void Exit(string reason)
    {
      System.Console.WriteLine(reason);
      Environment.Exit(1);
    }

    public static void WriteLineYellow(string value)
    {
      System.Console.ForegroundColor = System.ConsoleColor.Yellow;
      System.Console.WriteLine(value);
      System.Console.ForegroundColor = System.ConsoleColor.White;
    }

    public static void WriteLineSuccess(string value)
    {
      System.Console.ForegroundColor = System.ConsoleColor.Green;
      System.Console.WriteLine(value);
      System.Console.ForegroundColor = System.ConsoleColor.White;
    }

    public static void WriteLineError(string value)
    {
      System.Console.ForegroundColor = System.ConsoleColor.Red;
      System.Console.WriteLine(value);
      System.Console.ForegroundColor = System.ConsoleColor.White;
    }

    public static void WriteLine(string value)
    {
      System.Console.ForegroundColor = System.ConsoleColor.White;
      System.Console.WriteLine(value);
    }
  }
}