using SurfsUp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfsUp.Interfaces.Logging
{
    public interface ILogger
    {
        Task LogInfo(Enums.LogType logType, string message);

        Task LogError(Enums.LogType logType, string message, Exception ex);
    }
}
