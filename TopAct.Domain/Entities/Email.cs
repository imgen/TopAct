using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopAct.Domain.Entities
{
    public class Email : Entity
    {
        public string EmailAddress { get; private set; }
    }
}
