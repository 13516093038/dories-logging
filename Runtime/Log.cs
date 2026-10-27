using System;
using System.Diagnostics;
using UnityEngine;

namespace Dories.Logging
{
    /// <summary>
    /// Dories 各模块统一的日志入口。
    /// Debug 级别只在 Editor、Development Build 或定义了 DORIES_LOG 时编译进包。
    /// </summary>
    public static class Log
    {
        private static ILogSink _sink = new UnityLogSink();

        /// <summary>
        /// 当前输出端，设为 null 时恢复默认的 <see cref="UnityLogSink"/>。
        /// </summary>
        public static ILogSink Sink
        {
            get => _sink;
            set => _sink = value ?? new UnityLogSink();
        }

        [HideInCallstack, Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD"), Conditional("DORIES_LOG")]
        public static void Debug(string tag, string message, UnityEngine.Object context = null)
            => _sink.Log(LogLevel.Debug, tag, message, context);

        [HideInCallstack]
        public static void Info(string tag, string message, UnityEngine.Object context = null)
            => _sink.Log(LogLevel.Info, tag, message, context);

        [HideInCallstack]
        public static void Warn(string tag, string message, UnityEngine.Object context = null)
            => _sink.Log(LogLevel.Warn, tag, message, context);

        [HideInCallstack]
        public static void Error(string tag, string message, UnityEngine.Object context = null)
            => _sink.Log(LogLevel.Error, tag, message, context);

        [HideInCallstack]
        public static void Exception(string tag, Exception exception, UnityEngine.Object context = null)
            => _sink.LogException(tag, exception, context);
    }
}
