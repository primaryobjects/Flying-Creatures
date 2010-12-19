using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using System.Threading;
using StructureMap;
using FlyingCreatures.Repository.Interface;

/*
Copyright © 2011 Kory Becker (http://www.primaryobjects.com)

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace FlyingCreatures.Repository.Concrete
{
    public class UnitOfWork
    {
        private const string HTTPCONTEXTKEY = "FlyingCreatures.Repository.Base.HttpContext.Key";
        private static IUnitOfWorkFactory _unitOfWorkFactory;
        private static readonly Hashtable _threads = new Hashtable();

        public static void Commit()
        {
            IUnitOfWork unitOfWork = GetUnitOfWork();

            if (unitOfWork != null)
            {
                unitOfWork.Commit();
            }
        }

        public static IUnitOfWork Current 
        {
            get
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();

                if (unitOfWork == null)
                {
                    _unitOfWorkFactory = ObjectFactory.GetInstance<IUnitOfWorkFactory>();
                    unitOfWork = _unitOfWorkFactory.Create();
                    SaveUnitOfWork(unitOfWork);
                }

                return unitOfWork;
            }
        }

        private static IUnitOfWork GetUnitOfWork()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HTTPCONTEXTKEY))
                {
                    return (IUnitOfWork)HttpContext.Current.Items[HTTPCONTEXTKEY];
                }

                return null;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();
                    return null;
                }
                else
                {
                    lock (_threads.SyncRoot)
                    {
                        return (IUnitOfWork)_threads[Thread.CurrentThread.Name];
                    }
                }
            }
        }

        private static void SaveUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[HTTPCONTEXTKEY] = unitOfWork;
            }
            else
            {
                lock(_threads.SyncRoot)
                {
                    _threads[Thread.CurrentThread.Name] = unitOfWork;
                }
            }
        }
    }
}
