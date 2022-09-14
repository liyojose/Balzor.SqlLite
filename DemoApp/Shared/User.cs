using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Shared
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
    }

    public class Localuser : User
    {
        public bool Updated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
