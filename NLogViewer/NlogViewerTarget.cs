using System;
using NLog.Common;
using NLog.Targets;

namespace NLogViewer
{
    [Target("NlogViewer")]
    public class NlogViewerTarget : Target
    {
        #region Public Events

        public event Action<AsyncLogEventInfo> LogReceived;

        #endregion Public Events

        #region Protected Methods

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);

            var handler = LogReceived;
            if (handler != null) handler(logEvent);
        }

        #endregion Protected Methods
    }
}