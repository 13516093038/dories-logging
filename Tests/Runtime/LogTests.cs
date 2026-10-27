using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Dories.Logging.Tests
{
    public class LogTests
    {
        private class RecordingSink : ILogSink
        {
            public readonly List<(LogLevel level, string tag, string message)> Entries = new();
            public Exception LastException;

            public void Log(LogLevel level, string tag, string message, UnityEngine.Object context)
                => Entries.Add((level, tag, message));

            public void LogException(string tag, Exception exception, UnityEngine.Object context)
                => LastException = exception;
        }

        [TearDown]
        public void TearDown() => Log.Sink = null;

        [Test]
        public void CustomSink_ReceivesLevelTagAndMessage()
        {
            var sink = new RecordingSink();
            Log.Sink = sink;

            Log.Debug("A", "d");
            Log.Info("A", "i");
            Log.Warn("B", "w");
            Log.Error("C", "e");
            var ex = new InvalidOperationException("x");
            Log.Exception("C", ex);

            CollectionAssert.AreEqual(new[]
            {
                (LogLevel.Debug, "A", "d"),
                (LogLevel.Info, "A", "i"),
                (LogLevel.Warn, "B", "w"),
                (LogLevel.Error, "C", "e"),
            }, sink.Entries);
            Assert.AreSame(ex, sink.LastException);
        }

        [Test]
        public void NullSink_FallsBackToUnityLogSink()
        {
            Log.Sink = null;

            Assert.IsInstanceOf<UnityLogSink>(Log.Sink);
            LogAssert.Expect(LogType.Error, "[Tag] message");
            Log.Error("Tag", "message");
            LogAssert.Expect(LogType.Warning, "no tag");
            Log.Warn(null, "no tag");
        }
    }
}
