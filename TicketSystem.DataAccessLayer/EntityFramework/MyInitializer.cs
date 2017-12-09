using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.DataAccessLayer.EntityFramework;
using TicketSystem.Entities;

namespace TicketSystem.BusinessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Users admin = new Users()
            {
                Name = "Berkan",
                Surname = "Sivri",
                Email = "Berkansivri@gmail.com",
                Password = "123",
                IsAdmin = true,
                IsAuth = true,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Department = "IT",
                Phone = "123456789",
                ModifiedUser = 1
            };
            context.Users.Add(admin);

            for (int i = 1; i < 10; i++)
            {
                Users user = new Users()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Phone=FakeData.PhoneNumberData.GetPhoneNumber(),
                    Department=FakeData.PlaceData.GetStreetName(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedUser = 1,
                    Password = "1",
                    IsAdmin = false,
                    IsAuth = i % 5 == 0 ? true : false,
                    
                };
                context.Users.Add(user);
            }
            context.SaveChanges();

            List<Users> userlist = context.Users.ToList();
            foreach (Users user in userlist)
            {
                PcDetail pc = new PcDetail()
                {
                    PcName = user.Name + "." + user.Surname,
                    IP = FakeData.NetworkData.GetIpAddress(),
                    MAC = FakeData.NetworkData.GetMacAddress(),
                    Owner = user,
                    CreatedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedUser = user.Id,
                };
                context.PcDetails.Add(pc);
            }
            context.SaveChanges();

            List<Users> users = context.Users.ToList();

            for (int j = 1; j < 30; j++)
            {
                Users user = users[FakeData.NumberData.GetNumber(1, (users.Count - 1))];
                Ticket ticket = new Ticket()
                {
                    Title = FakeData.NameData.GetCompanyName(),
                    Text = FakeData.PlaceData.GetPostCode() + " " + FakeData.PlaceData.GetAddress(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(),
                    ModifiedUser = user.Id,
                    Img = "/Images/default.png",
                    Owner = user,
                    IsSolved = j%5==0?false:true,
                };
                context.Tickets.Add(ticket);
            }
            context.SaveChanges();
        }
    }
}
