using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Allergy
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int AllAutid { get; set; }

        [Display(Name = "Description"), StringLength(100), Required(ErrorMessage = "{0} is required")]
        public string AllSdesc { get; set; } = null!;

        [Display(Name = "Status")]
        public bool AllState { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string AllUsrid { get; set; } = null!;

        public DateTime AllCdate { get; set; }
        public DateTime AllUdate { get; set; }
    }
}
