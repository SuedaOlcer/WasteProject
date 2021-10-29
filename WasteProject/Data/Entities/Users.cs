using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WasteProject.Data.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "{0} uzunluğu  {1} rakam olmalıdır.", MinimumLength = 11)]
        public string Tc { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Adress { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        public int CardNo { get; set; }
        public ICollection<UserPoints> UserPoints { get; set; }
        public ICollection<WasteTransaction> WasteTransaction { get; set; }

    }
}
