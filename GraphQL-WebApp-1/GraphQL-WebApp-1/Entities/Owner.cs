using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.Entities
{
    public class Owner
    {
        [Key]       
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Account> Accounts { get;  }
    }   
}
