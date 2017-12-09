using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("E-mail"), Required, StringLength(50)]
        public string Email { get; set; }

        [DisplayName("Password"), Required, StringLength(30)]
        public string Password { get; set; }
    }
}
