using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Raven.Client;
using FlyingCreatures.Repository.Interface;

namespace FlyingCreatures.Repository.Concrete
{
    public class RavenUnitOfWork : IUnitOfWork
    {
        public IDocumentSession Context { get; private set; }

        public RavenUnitOfWork(IDocumentStore session)
        {
            Context = session.OpenSession();
        }

        #region IUnitOfWork Members

        public void Commit()
        {
            Context.SaveChanges();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
