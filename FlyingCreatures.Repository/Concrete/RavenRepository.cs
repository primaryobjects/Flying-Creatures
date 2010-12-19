﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
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
    public class RavenRepository<T> : IRepository<T> where T : class
    {
        private IDocumentSession _context;

        protected IDocumentSession Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<RavenUnitOfWork>().Context;
                }

                return _context;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Query<T>();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.Context.Query<T>().Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.Context.Query<T>().SingleOrDefault<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.Context.Query<T>().First<T>(where);
        }

        public virtual void Delete(T entity)
        {
            this.Context.Delete(entity);
        }

        public virtual void Add(T entity)
        {
            this.Context.Store(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
