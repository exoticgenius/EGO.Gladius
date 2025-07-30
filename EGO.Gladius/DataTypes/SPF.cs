using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace EGO.Gladius.DataTypes;


/// <summary>
/// super position fault
/// </summary>
public struct SPF
{
    #region ' props '
    public LinkedList<MethodInfo>? CapturedContext { get; }

    public object[]? Parameters { get; }

    public Exception? Exception { get; }

    public string? Message { get; }
    #endregion ' props '

    #region ' ctors '
    public SPF() : this(default, default, default, default) { }
    public SPF(MethodInfo capturedContext) : this(capturedContext, default, default, default) { }
    public SPF(object[] parameters) : this(default, parameters, default, default) { }
    public SPF(Exception exception) : this(default, default, exception, default) { }
    public SPF(string message) : this(default, default, default, message) { }
    public SPF(Exception exception, string message) : this(default, default, exception, message) { }
    public SPF(object[] parameters, Exception exception) : this(default, parameters, exception, default) { }
    public SPF(object[] parameters, Exception exception, string message) : this(default, parameters, exception, message) { }
    public SPF(object[] parameters, string message) : this(default, parameters, default, message) { }
    public SPF(MethodInfo capturedContext, object[] parameters) : this(capturedContext, parameters, default, default) { }
    public SPF(MethodInfo capturedContext, Exception exception) : this(capturedContext, default, exception, default) { }
    public SPF(MethodInfo capturedContext, string message) : this(capturedContext, default, default, message) { }
    public SPF(MethodInfo capturedContext, Exception exception, string message) : this(capturedContext, default, exception, message) { }
    public SPF(MethodInfo capturedContext, object[] parameters, Exception exception) : this(capturedContext, parameters, exception, default) { }
    public SPF(MethodInfo capturedContext, object[] parameters, string message) : this(capturedContext, parameters, default, message) { }

    public SPF(MethodInfo? capturedContext, object[]? parameters, Exception? exception, string? message)
    {
        CapturedContext = capturedContext is not null ?
            new LinkedList<MethodInfo>([capturedContext]) :
            default;

        Parameters = parameters;
        Exception = exception;
        Message = message;

        Console.WriteLine(message ?? "");
        Console.WriteLine(exception?.Message ?? "");
        Console.WriteLine(capturedContext?.Name ?? "");
    }
    #endregion ' ctors '

    #region ' generators '
    public static SPF Gen() => new();
    public static SPF Gen(MethodInfo capturedContext) => new(capturedContext);
    public static SPF Gen(object[] parameters) => new(parameters);
    public static SPF Gen(Exception exception) => new(exception);
    public static SPF Gen(string message) => new(new SPFST(GenerateStackTrace()), message);
    public static SPF Gen(Exception exception, string message) => new(exception, message);
    public static SPF Gen(object[] parameters, Exception exception) => new(parameters, exception);
    public static SPF Gen(object[] parameters, string message) => new(parameters, message);
    public static SPF Gen(object[] parameters, Exception exception, string message) => new(parameters, exception, message);
    public static SPF Gen(MethodInfo capturedContext, object[] parameters) => new(capturedContext, parameters);
    public static SPF Gen(MethodInfo capturedContext, Exception exception) => new(capturedContext, exception);
    public static SPF Gen(MethodInfo capturedContext, string message) => new(capturedContext, message);
    public static SPF Gen(MethodInfo capturedContext, Exception exception, string message) => new(capturedContext, exception, message);
    public static SPF Gen(MethodInfo capturedContext, object[] parameters, Exception exception) => new(capturedContext, parameters, exception);
    public static SPF Gen(MethodInfo capturedContext, object[] parameters, string message) => new(capturedContext, parameters, message);
    public static SPF Gen(MethodInfo capturedContext, object[] parameters, Exception exception, string message) => new(capturedContext, parameters, exception, message);
    #endregion ' generators '

    public void Throw() => throw new SPFE(this);
    public SPFE GenSPFE() => new(this);

    private static bool WithFile;

    public static void IncludeFileInfo() =>
        Interlocked.Exchange(ref WithFile, true);

    public static void ExcludeFileInfo() =>
        Interlocked.Exchange(ref WithFile, false);

    private static string GenerateStackTrace()
    {
        var frames = new StackTrace(WithFile).GetFrames();
        var sb = new StringBuilder();

        foreach (var frame in frames)
        {
            var source = frame.GetFileName();

            if (source is null) continue;

            sb.AppendLine(string.Format(
                "at {0} line: {1}",
                source,
                frame.GetFileLineNumber()));
        }

        return sb.ToString();
    }
}

public class SPFST : Exception
{
    private readonly string stackTrace;

    public SPFST(string stackTrace)
    {
        this.stackTrace = stackTrace;
    }

    public override string? StackTrace => stackTrace;
}
