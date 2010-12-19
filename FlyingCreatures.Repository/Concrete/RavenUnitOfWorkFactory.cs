using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using FlyingCreatures.Repository.Interface;

namespace FlyingCreatures.Repository.Concrete
{
    public class RavenUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static Func<IDocumentStore> _objectContextDelegate;
        private static readonly Object _lockObject = new Object();

        public static void SetObjectContext(Func<IDocumentStore> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }

        #region IUnitOfWorkFactory Members

        public IUnitOfWork Create()
        {
            IDocumentStore context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
            }

            return new RavenUnitOfWork(context);
        }

        #endregion
    }
}
