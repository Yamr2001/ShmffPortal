using ShmffPortal.Models;
using ShmffPortal.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShmffPortal.UnitOfWorkF
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private NewPortalDBEntities context = new NewPortalDBEntities();

        private IGenericRepository<NEWADVERTISMENT> _NEWADVERTISMENTSRepository;

        public IGenericRepository<NEWADVERTISMENT> NEWADVERTISMENTSRepository
        {
            get
            {
                if (this._NEWADVERTISMENTSRepository == null)
                    this._NEWADVERTISMENTSRepository = new GenericRepository<NEWADVERTISMENT>(context);
                return _NEWADVERTISMENTSRepository;
            }
        }




        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}