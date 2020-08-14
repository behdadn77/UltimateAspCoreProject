using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Core.Entities.Location
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        public int CountryId{ get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }


        public string Title { get; set; }

        public string Code { get; set; }

        public ICollection<City> Cities { get; set; }


    }
}
