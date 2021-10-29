using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WasteProject.Data.Entities
{
    public class WasteTransaction
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WasteId { get; set; }
        public int WasteAmount { get; set; }
        public int Point { get; set; }

        public Users User { get; set; }
        public WasteTypes Waste { get; set; }


    }
}
