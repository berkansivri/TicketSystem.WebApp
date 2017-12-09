using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Entities
{
    [Table("Users")]
    public class Users : EntityBase
    {
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string Surname { get; set; }
        [Required,StringLength(50)]
        public string Email { get; set; }
        [Required,StringLength(30)]
        public string Password { get; set; }

        public string Phone { get; set; }
        public string Department { get; set; }
        public bool IsAuth { get; set; }
        public bool IsAdmin { get; set; }

        public virtual List<Ticket> Ticket { get; set; }
        public virtual List<PcDetail> Pc { get; set; }
    }
}
