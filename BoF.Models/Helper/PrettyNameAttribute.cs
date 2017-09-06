using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Web.Helpers
{
    public class PrettyNameAttribute:Attribute
    {

        public PrettyNameAttribute(string pName)
        {
            PrettyName = pName;
        }

        public string PrettyName { get; set; }
    
    }
}