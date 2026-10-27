using System;

namespace Dories.Logging
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }

    /// <summary>
    /// 日志输出端。实现它并赋给 <see cref="Log.Sink"/> 即可接管所有 Dories 模块的日志。
    /// </summary>
    public interface ILogSink
    {
        void Log(LogLevel level, string tag, string message, UnityEngine.Object context);

        void LogException(string tag, Exception exception, UnityEngine.Object context);
    }
}
