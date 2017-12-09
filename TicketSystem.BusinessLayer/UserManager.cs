using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.BusinessLayer.Result;
using TicketSystem.Entities;
using TicketSystem.Entities.Messages;
using TicketSystem.Entities.ValueObjects;

namespace TicketSystem.BusinessLayer
{
    public class UserManager : ManagerBase<Users>
    {
        public BusinessLayerResult<Users> Login(LoginViewModel model)
        {
            BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();
            res.result = Find(x => x.Email == model.Email && x.Password == model.Password);
            if (res.result != null)
            {

            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPasswordWrong, "Email and Password not match.");
            }
            return res;
        }

        public BusinessLayerResult<Users> Register(RegisterViewModel model)
        {
            Users user = Find(x => x.Email == model.Email);
            BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();

            if (user != null)
            {
                res.AddError(ErrorMessageCode.EmailAlreadyExist, "This e-mail already registered.");
            }
            else
            {
                int dbResult = Insert(new Users()
                {
                    Email = model.Email,
                    Password = model.Password,
                    IsAdmin = false,
                    IsAuth = false,
                    Name = model.Name,
                    Surname = model.Surname,
                });
                if (dbResult > 0)
                {
                    res.result = Find(x => x.Email == model.Email);
                }
                else
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "An problem occur. Please try again later.");
                }
            }

            return res;
        }

        public BusinessLayerResult<Users> UpdateProfile(Users model)
        {
            Users user = Find(x => x.Email == model.Email);
            BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();

            if (user != null && user.Id != model.Id)
            {
                res.AddError(ErrorMessageCode.EmailAlreadyExist, "This e-mail adress already registered.");
            }
            res.result = Find(x => x.Id == model.Id);
            res.result.Email = model.Email;
            res.result.Name = model.Name;
            res.result.Surname = model.Surname;
            res.result.Password = model.Password;
            res.result.Phone = model.Phone;
            res.result.Department = model.Department;
            
            if(Update(res.result)==0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Profile could not update. Please try again later.");
            }
            return res;
        }

        public BusinessLayerResult<Users> GetUserById(int id)
        {
            BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();
            res.result = Find(x => x.Id == id);

            if(res.result==null)
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "User could not found.");
            }

            return res;
        }

        public BusinessLayerResult<Users> RemoveUserById(int id)
        {
            BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();
            res.result = Find(x => x.Id == id);

            if(res.result!=null)
            {
                if(Delete(res.result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "User could not remove!");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "User could not find");
            }
            return res;
        }
    }
}
