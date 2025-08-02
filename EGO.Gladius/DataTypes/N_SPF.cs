using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace EGO.Gladius.DataTypes;

public struct N_SPF
{

    #region ' props '
    public LinkedList<MethodInfo>? CapturedContext { get; }

    public object[]? Parameters { get; }

    public Exception? Exception { get; }

    public string? Message { get; }
    #endregion ' props '


    #region ' ctors '
    public N_SPF() : this(default, default, default, default) { }
    public N_SPF(MethodInfo capturedContext) : this(capturedContext, default, default, default) { }
    public N_SPF(object[] parameters) : this(default, parameters, default, default) { }
    public N_SPF(Exception exception) : this(default, default, exception, default) { }
    public N_SPF(string message) : this(default, default, default, message) { }
    public N_SPF(Exception exception, string message) : this(default, default, exception, message) { }
    public N_SPF(object[] parameters, Exception exception) : this(default, parameters, exception, default) { }
    public N_SPF(object[] parameters, Exception exception, string message) : this(default, parameters, exception, message) { }
    public N_SPF(object[] parameters, string message) : this(default, parameters, default, message) { }
    public N_SPF(MethodInfo capturedContext, object[] parameters) : this(capturedContext, parameters, default, default) { }
    public N_SPF(MethodInfo capturedContext, Exception exception) : this(capturedContext, default, exception, default) { }
    public N_SPF(MethodInfo capturedContext, string message) : this(capturedContext, default, default, message) { }
    public N_SPF(MethodInfo capturedContext, Exception exception, string message) : this(capturedContext, default, exception, message) { }
    public N_SPF(MethodInfo capturedContext, object[] parameters, Exception exception) : this(capturedContext, parameters, exception, default) { }
    public N_SPF(MethodInfo capturedContext, object[] parameters, string message) : this(capturedContext, parameters, default, message) { }

    public N_SPF(MethodInfo? capturedContext, object[]? parameters, Exception? exception, string? message)
    {
        CapturedContext = capturedContext is not null ?
            new LinkedList<MethodInfo>([capturedContext]) :
            default;

        Parameters = parameters;
        Exception = exception;
        Message = message;
    }
    #endregion ' ctors '

    #region ' generators '
    public static N_SPF Gen() => new();
    public static N_SPF Gen(MethodInfo capturedContext) => new(capturedContext);
    public static N_SPF Gen(object[] parameters) => new(parameters);
    public static N_SPF Gen(Exception exception) => new(exception);
    public static N_SPF Gen(string message) => new(new SPFST(GenerateStackTrace()), message);
    public static N_SPF Gen(Exception exception, string message) => new(exception, message);
    public static N_SPF Gen(object[] parameters, Exception exception) => new(parameters, exception);
    public static N_SPF Gen(object[] parameters, string message) => new(parameters, message);
    public static N_SPF Gen(object[] parameters, Exception exception, string message) => new(parameters, exception, message);
    public static N_SPF Gen(MethodInfo capturedContext, object[] parameters) => new(capturedContext, parameters);
    public static N_SPF Gen(MethodInfo capturedContext, Exception exception) => new(capturedContext, exception);
    public static N_SPF Gen(MethodInfo capturedContext, string message) => new(capturedContext, message);
    public static N_SPF Gen(MethodInfo capturedContext, Exception exception, string message) => new(capturedContext, exception, message);
    public static N_SPF Gen(MethodInfo capturedContext, object[] parameters, Exception exception) => new(capturedContext, parameters, exception);
    public static N_SPF Gen(MethodInfo capturedContext, object[] parameters, string message) => new(capturedContext, parameters, message);
    public static N_SPF Gen(MethodInfo capturedContext, object[] parameters, Exception exception, string message) => new(capturedContext, parameters, exception, message);
    #endregion ' generators '


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

    public void Throw() => throw new N_SPFE(this);
    public N_SPFE GenSPFE() => new(this);
}
