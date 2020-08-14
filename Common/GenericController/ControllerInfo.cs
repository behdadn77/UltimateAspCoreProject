using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Common.GenericController
{
    public class ControllerInfo 
    {
        public Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState { get; set; }
        public Microsoft.AspNetCore.Http.HttpContext httpContext { get; set; }
    }
}
