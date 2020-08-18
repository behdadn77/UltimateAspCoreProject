
//using Microsoft.AspNet.Identity;
using Common.Enumeration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using Common.Messages.Alerts;

namespace Common.GenericController
{
    public class GenericController:Controller
    {
        public ControllerInfo fillControllerInfo()
        {
            return new ControllerInfo() { ModelState = ModelState, httpContext = HttpContext };
        }
       

        public void SendAlert(string key, MessageType alerts)
        {
            switch (alerts)
            {
                case MessageType.Success:
                    TempData["GlobalSuccess"] = Alerts.Success[key];
                    break;
                case MessageType.Error:
                    TempData["GlobalWarning"] = Alerts.Warning[key];
                    break;
                case MessageType.Warning:
                    TempData["GlobalError"] = Alerts.Error[key];
                    break;
                case MessageType.Info:
                    TempData["GlobalInfo"] = Alerts.Info[key];
                    break;
                default:
                    break;
            }
        }
    }


}
