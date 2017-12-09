using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Name"), StringLength(30)]
        public string Name { get; set; }

        [DisplayName("Surname"), StringLength(30)]
        public string Surname { get; set; }

        [DisplayName("E-mail"), Required, StringLength(50),EmailAddress(ErrorMessage = "Please enter a proper E-mail for {0} field")]
        public string Email { get; set; }

        [DisplayName("Password"),Required,StringLength(30),DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Password (again)"),Required,StringLength(30),Compare("Password"),DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
