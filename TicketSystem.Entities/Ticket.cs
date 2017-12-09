using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Entities
{
    [Table("Tickets")]
    public class Ticket : EntityBase
    {
        [Required,StringLength(100)]
        public string Title { get; set; }
        [Required,StringLength(500)]
        public string Text { get; set; }
        [StringLength(50)]
        public string Img { get; set; }
        [Required]
        public bool IsSolved { get; set; }

        [Key,Required]
        public virtual Users Owner { get; set; }
    }
}
