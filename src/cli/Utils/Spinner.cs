using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace metrikr.Utils
{
  public class Spinner : IDisposable
  {
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Pattern _pattern;
    private Task _task;
    private int _frameIndex;
    private int _lineLength;

    public bool Stopped { get; private set; }
    public string Text { get; set; }

    public Spinner(string text, Pattern pattern = null)
    {
      _cancellationTokenSource = new CancellationTokenSource();
      _pattern = pattern;
      Text = text;
    }

    public static TResult Start<TResult>(
      string text,
      Func<Spinner, TResult> function,
      Pattern pattern = null
    )
    {
      if (pattern == null) pattern = Patterns.Line;

      using (var spinner = new Spinner(text, pattern))
      {
        spinner.Start();
        return function(spinner);
      }
    }

    public void Dispose()
    {
      if (!_cancellationTokenSource.IsCancellationRequested)
      {
        Dispose(true);
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      Stop(null, null, Environment.NewLine);
    }

    public void Start()
    {
      if (_task != null) throw new InvalidOperationException("Spinner is already running");
 
      SetCursorVisibility(false);
      Stopped = false;

      _task = Task.Run(async () =>
      {
        _frameIndex = 0;
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
          Render();
          await Task.Delay(_pattern.Interval);
        }
      });
    }

    public void Stop(string text, ConsoleColor? color, string terminator)
    {
      if (_cancellationTokenSource.IsCancellationRequested) return;

      _cancellationTokenSource.Cancel();
      _task?.Wait();

      Text = text ?? Text;
      Stopped = true;

      Render(true);
      System.Console.Write(terminator);
      SetCursorVisibility(true);
    }

    private void Render(bool stopping = false)
    {
      var pattern = _pattern;
      var frame = pattern.Frames[_frameIndex++ % pattern.Frames.Length];

      ClearCurrentConsoleLine(_lineLength);
      _lineLength = frame.Length + 1 + Text.Length;

      if (!stopping)
      {
        System.Console.Write(frame);
        System.Console.Write(" ");
      }
      else
      {
        var values = Enumerable.Range(0, frame.Length + 1).Select(x => " ");
        System.Console.Write(string.Join(string.Empty, values));
      }

      System.Console.Write(Text);
      System.Console.Out.Flush();
    }

    private void ClearCurrentConsoleLine(int length)
    {
      int currentLineCursor = System.Console.CursorTop;
      System.Console.SetCursorPosition(0, System.Console.CursorTop);
      System.Console.Write(new string(' ', length));
      System.Console.SetCursorPosition(0, currentLineCursor);
      System.Console.Out.Flush();
    }

    public void SetCursorVisibility(bool visible)
    {
      System.Console.CursorVisible = visible;
    }
  }

  public static class Patterns
  {
    public static readonly Pattern Line = new Pattern(new string[]
    {
      "-",
      "\\",
      "|",
      "/"
    }, interval: 130);

    public static readonly Pattern Pipe = new Pattern(new string[]
    {
      "┤",
      "┘",
      "┴",
      "└",
      "├",
      "┌",
      "┬",
      "┐"
    }, interval: 100);

 
    public static readonly Pattern SimpleDots = new Pattern(new string[]
    {
      ".  ",
      ".. ",
      "...",
      "   "
    }, interval: 400);

    public static readonly Pattern SimpleDotsScrolling = new Pattern(new string[]
    {
      ".  ",
     ".. ",
      "...",
      " ..",
      "  .",
      "   "
    }, interval: 200);

    public static readonly Pattern Star = new Pattern(new string[]
    {
      "+",
      "x",
      "*"
    }, interval: 80);

    public static readonly Pattern Flip = new Pattern(new string[]
    {
      "_",
      "_",
      "_",
      "-",
      "`",
      "`",
      "'",
      "´",
      "-",
      "_",
      "_",
      "_"
    }, interval: 70);

    public static readonly Pattern Balloon = new Pattern(new string[]
    {
      " ",
      ".",
      "o",
      "O",
      "@",
      "*",
      " "
    }, interval: 140);

    public static readonly Pattern Balloon2 = new Pattern(new string[]
    {
      ".",
      "o",
      "O",
      "°",
      "O",
      "o",
      "."
    }, interval: 120);

    public static readonly Pattern Noise = new Pattern(new string[]
    {
      "▓",
      "▒",
      "░"
    }, interval: 100);

    public static readonly Pattern BouncingBar = new Pattern(new string[]
    {
      "[    ]",
      "[=   ]",
      "[==  ]",
      "[=== ]",
      "[ ===]",
      "[  ==]",
      "[   =]",
      "[    ]",
      "[   =]",
      "[  ==]",
      "[ ===]",
      "[====]",
      "[=== ]",
      "[==  ]",
      "[=   ]"
    }, interval: 80);
 
    public static readonly Pattern Shark = new Pattern(new string[]
    {
      "▐|\\____________▌",
      "▐_|\\___________▌",
      "▐__|\\__________▌",
      "▐___|\\_________▌",
      "▐____|\\________▌",
      "▐_____|\\_______▌",
      "▐______|\\______▌",
      "▐_______|\\_____▌",
      "▐________|\\____▌",
      "▐_________|\\___▌",
      "▐__________|\\__▌",
      "▐___________|\\_▌",
      "▐____________|\\▌",
      "▐____________/|▌",
      "▐___________/|_▌",
      "▐__________/|__▌",
      "▐_________/|___▌",
      "▐________/|____▌",
      "▐_______/|_____▌",
      "▐______/|______▌",
      "▐_____/|_______▌",
      "▐____/|________▌",
      "▐___/|_________▌",
      "▐__/|__________▌",
      "▐_/|___________▌",
      "▐/|____________▌"
    }, interval: 120);

    public static readonly Pattern Dqpb = new Pattern(new string[]
    {
      "d",
      "q",
      "p",
      "b"
    }, interval: 100);
  }

  public class Pattern
  {
    public string[] Frames { get; }
    public int Interval { get; }

    public Pattern(string[] frames, int interval)
    {
      Frames = frames;
      Interval = interval;
    }
  }
}
