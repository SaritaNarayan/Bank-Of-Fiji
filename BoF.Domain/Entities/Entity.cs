using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BoF.Domain.Entities
{
    public abstract class Entity
    {

        #region Members
        private Guid _guidId = Guid.NewGuid();
        private DateTime? _createdDate = DateTime.Now;
        private string _createdBy ="";
        private DateTime? _lastModifiedDate = DateTime.Now;
        private string _lastModifiedBy = "";
        #endregion

        #region Properties

        public virtual int Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string LastModifiedBy { get; set; }

        //public virtual int Month { get; set; }

        //public virtual int Year { get; set; }

        //TODO: These values need to be defaulted
        public virtual DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        //public virtual string CreatedBy
        //{
        //    get { ; }
        //    set { _createdBy = value; }
        //}
        public virtual DateTime? LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { _lastModifiedDate = value; }
        }
        //public virtual string LastModifiedBy
        //{
        //    get { return _lastModifiedBy; }
        //    set { _lastModifiedBy = value; }
        //}
        #endregion

        #region Public Methods

        public virtual void CreateTimestamp()
        {
            _createdDate = DateTime.Now;
            _createdBy = HttpContext.Current.User.Identity.Name;
            _lastModifiedDate = DateTime.Now;
            _lastModifiedBy = HttpContext.Current.User.Identity.Name;
        }

        public virtual void UpdateTimestamp()
        {
            _lastModifiedDate = DateTime.Now;
            _lastModifiedBy = HttpContext.Current.User.Identity.Name;
        }
        #endregion

    
    
    }
}