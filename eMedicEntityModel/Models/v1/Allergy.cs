using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Allergy
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int AllAutid { get; set; }

        [Display(Name = "Description"), StringLength(100)]
        public string AllSdesc { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool AllState { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string AllUsrid { get; set; } = string.Empty;

        public DateTime AllCdate { get; set; }
        public DateTime? AllUdate { get; set; }
    }
}
