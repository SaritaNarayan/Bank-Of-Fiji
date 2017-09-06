using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoF.Web.Models
{
    public class TransferTableModel
    {
        public int Id { get; set; }
        public string TransferDetails { get; set; }
        public string TransferFrom { get; set; }
        public string TransferTo { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateModified { get; set; }
        public string Status { get; set; }
        
    }
}