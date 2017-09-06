using BoF.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BoF.Web.Database_Access_Layer
{
    public class FileContext: DbContext
    {
        public FileContext() : base("ApplicationServices") { }

        //public DbSet<FileUploadModel> FileUpload { get; set; }
       
    }
}