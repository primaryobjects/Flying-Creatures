using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingCreatures.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
