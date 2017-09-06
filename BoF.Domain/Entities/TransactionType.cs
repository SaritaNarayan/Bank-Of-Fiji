using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class TransactionType:Entity
    {
        public virtual String TransName { get; set; }
    }
}