using NLog;
using SurfsUp.Interfaces.Logging;
using SurfsUp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfsUp.Logging
{
    public class LELogger : SurfsUp.Interfaces.Logging.ILogger
    {
        // If using NLog
        private static Logger log = LogManager.GetCurrentClassLogger();

        public async Task LogInfo(Enums.LogType logType, string message)
        {
            log.Info(String.Format("{0} : {1}", logType.ToString(), message));
        }

        public async Task LogError(Enums.LogType logType, string message, Exception ex)
        {
            log.Error(String.Format("{0} : {1}", logType.ToString(), message), ex);
        }
    }
}
