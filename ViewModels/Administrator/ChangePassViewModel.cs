﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Administrator
{
    public class ChangePassViewModel
    {
        public string NewPass { get; set; }
        public string ConfirmNewPass { get; set; }
        public string  EmailAddress { get; set; }
    }
}
