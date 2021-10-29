using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WasteProject.Data.Entities
{
    public class Gifts
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        public int Point { get; set; }
        [StringLength(50)]
        public string Url { get; set; }

    }
}
