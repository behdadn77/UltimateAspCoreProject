﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Location
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public ICollection<State> States { get; set; }
        public Region Region{ get; set; }
    }
}
