using System;
using System.Diagnostics;
using Serilog;

namespace metrikr.Utils
{
  public static class CmdHelper
  {
    public static string Error = "";

    public static bool ExecuteCommand(string command)
    {
      Log.Information($"Running command: {command}");

      Error = "";

      try
      {
        // create the ProcessStartInfo using "cmd" as the program to be run,
        // and "/c " as the parameters.
        // Incidentally, /c tells cmd that we want it to execute the command that follows,
        // and then exit.
        ProcessStartInfo info = new("cmd", "/c " + command)
        {
          // The following commands are needed to redirect the standard output.
          // This means that it will be redirected to the Process.StandardOutput StreamReader.
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false,
          // Do not create the black window.
          CreateNoWindow = true
        };
        // Now we create a process, assign its ProcessStartInfo and start it
        Process proc = new()
        {
          StartInfo = info,
        };

        proc.Start();
        // Get the output into a string
        string output = proc.StandardOutput.ReadToEnd();
        Error = proc.StandardError.ReadToEnd();

        // TODO: better ideas?
        if (output.ToLowerInvariant().Contains("error"))
        {
          Error = output;
        }

        return Error == string.Empty;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
