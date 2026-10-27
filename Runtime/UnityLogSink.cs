using System;
using UnityEngine;

namespace Dories.Logging
{
    /// <summary>
    /// 默认输出端，转发到 UnityEngine.Debug，格式为 "[tag] message"。
    /// </summary>
    public class UnityLogSink : ILogSink
    {
        [HideInCallstack]
        public void Log(LogLevel level, string tag, string message, UnityEngine.Object context)
        {
            var text = string.IsNullOrEmpty(tag) ? message : $"[{tag}] {message}";
            switch (level)
            {
                case LogLevel.Warn:
                    UnityEngine.Debug.LogWarning(text, context);
                    break;
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(text, context);
                    break;
                default:
                    UnityEngine.Debug.Log(text, context);
                    break;
            }
        }

        [HideInCallstack]
        public void LogException(string tag, Exception exception, UnityEngine.Object context)
        {
            UnityEngine.Debug.LogException(exception, context);
        }
    }
}
