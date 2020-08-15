using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Core.Entities.Location
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
    }
}
