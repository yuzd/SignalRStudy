﻿using System;
using System.Collections.Generic;
using SignalR.SLAB;
using SignalR.SLAB.Loggers;

namespace SignalR.SLAB.Services
{
    public class HubServerLogger : IHubLogger
    {
        public HubServerLogger()
        {
            RegisterLog();
        }

        private readonly Dictionary<int, Action<string>> _exectueLogDict = new Dictionary<int, Action<string>>();

        private void RegisterLog()
        {
            var globalLogger = new GlobalLogger();
            globalLogger.RegisterLogger(_exectueLogDict);

            var hubServerLogger = new Loggers.HubServerLogger();
            hubServerLogger.RegisterLogger(_exectueLogDict);
        }

        public void Log(int log, string message)
        {
            if (_exectueLogDict.ContainsKey(log))
            {
                _exectueLogDict[log].Invoke(message);
                return;
            }

            _exectueLogDict[GlobalType.GlobalWarning].Invoke(message);
        }
    }
}
