using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WasteProject.Data.Entities
{
    public class UserPoints
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Point { get; set; }
        public Users User { get; set; }

    }
}
