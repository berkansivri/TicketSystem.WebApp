using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketSystem.Common;
using TicketSystem.Entities;
using TicketSystem.WebApp.Models;

namespace TicketSystem.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public int GetCurrentUser()
        {
            Users user = CurrentSession.user;
            if (user != null)
                return user.Id;
            else
                return 0;
        }
    }
}