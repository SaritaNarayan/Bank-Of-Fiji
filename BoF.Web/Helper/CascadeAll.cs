using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace BoF.Web.Helpers
{
    public class CascadeAll : IHasOneConvention, //Actually Apply the convention
        IHasManyConvention,
        IReferenceConvention,
        IHasManyToManyConvention,

        IHasOneConventionAcceptance, //Test to see if we should use the convention
        IHasManyConventionAcceptance, //I think we could skip these since it will always be true
        IReferenceConventionAcceptance, //adding them for reference later
        IHasManyToManyConventionAcceptance
    {
        //One to One
        public void Accept(IAcceptanceCriteria<IOneToOneInspector> criteria)
        {
            //criteria.Expect(x => (true));
        }

        public void Apply(IOneToOneInstance instance)
        {
            instance.Cascade.All();
        }

        //One to Many
        public void Accept(IAcceptanceCriteria<IOneToManyCollectionInspector> criteria)
        {
            //criteria.Expect(x => (true));
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }

        //Many to One
        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            // criteria.Expect(x => (true));
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.All();
        }

        //Many to Many
        public void Accept(IAcceptanceCriteria<IManyToManyCollectionInspector> criteria)
        {
            // criteria.Expect(x => (true));
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }

    
    }
}