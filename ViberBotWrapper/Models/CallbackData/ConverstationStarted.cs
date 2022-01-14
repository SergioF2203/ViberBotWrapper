using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models.CallbackData
{
    public class ConverstationStarted : BaseIncomingMessage
    {
        public bool Subscribed { get; set; }
    }
}
