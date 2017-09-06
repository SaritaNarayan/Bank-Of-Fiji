using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Web.Helpers
{
    public class SettingsAttributes : Attribute
    {

        public bool ForNavigation { get; set; }
        public string DisplayName { get; set; }
        public int OrderNumber { get; set; }
        public Guid Id { get; set; }

        public SettingsAttributes(bool yesNo, string displayName, int orderNumber, string id)
        {
            ForNavigation = yesNo;
            DisplayName = displayName;
            OrderNumber = orderNumber;
            Id = Guid.Parse(id);
        }
    }
}