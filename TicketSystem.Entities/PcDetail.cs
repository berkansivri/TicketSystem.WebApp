using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Entities
{
    [Table("PcDetails")]
    public class PcDetail : EntityBase
    {
        public string PcName { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        [Key,Required]
        public virtual Users Owner { get; set; }
    }
}
