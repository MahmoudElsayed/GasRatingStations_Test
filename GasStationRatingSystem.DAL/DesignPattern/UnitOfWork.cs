using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Threading.Tasks;

namespace GasStationRatingSystem.DAL
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        #region Fields

        bool _IsDisposed = false;

        public UnitOfWork(T GasStationRatingSystemDbContext) => this.GasStationRatingSystemDbContext = GasStationRatingSystemDbContext;

        #endregion

        #region Props

        public T GasStationRatingSystemDbContext { get; }
        public IDbContextTransaction DbContextTransaction { get; set; }

        public bool IsDisposed { get => _IsDisposed; }

        #endregion

        #region Methods

        public virtual void Commit() => GasStationRatingSystemDbContext.Database.CurrentTransaction.Commit();

        public bool SaveChanges()
        {
            try
            {
                return GasStationRatingSystemDbContext.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await GasStationRatingSystemDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            GasStationRatingSystemDbContext.Dispose();
            _IsDisposed = true;
        }
        #endregion
    }

}
