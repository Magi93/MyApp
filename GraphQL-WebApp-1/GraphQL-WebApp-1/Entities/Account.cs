using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public TypeOfAccount Type { get; set; }
        public string Description { get; set; }

        [ForeignKey("OwnerId")]
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
