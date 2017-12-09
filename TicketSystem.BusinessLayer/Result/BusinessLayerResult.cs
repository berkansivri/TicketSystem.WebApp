using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Entities.Messages;

namespace TicketSystem.BusinessLayer.Result
{
    public class BusinessLayerResult<T>
    {
        public List<ErrorMessageObject> Errors { get; set; }
        public T result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObject>();
        }
        public void AddError (ErrorMessageCode code, string obj)
        {
            Errors.Add(new ErrorMessageObject() { Code=code, Message=obj });
        }
    }
}
