using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Core.Entities.Location
{
    public class City
    {
        public City() 
        {

        }
        [Key]
        public int Id { get; set; }

        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }


        public string Code { get; set; }

        public string Title { get; set; }

    }
}
