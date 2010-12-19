using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
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
