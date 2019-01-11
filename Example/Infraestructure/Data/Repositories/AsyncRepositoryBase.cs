using Example.Infraestructure.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Example.Infraestructure.Data.Repositories
{
    public abstract class AsyncRepositoryBase<TEntityType, TContextType> : IAsyncRepository<TEntityType>
        where TEntityType : class
        where TContextType : DbContext
    {
        private readonly IDbContextFactory<TContextType> ContextFactory;

        protected AsyncRepositoryBase(IDbContextFactory<TContextType> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        /// <summary>
        /// Implementación de <see cref="IAsyncRepository{TEntityType}.Create(TEntityType)"/>
        /// </summary>
        public virtual async Task Create(TEntityType record)
        {
            try
            {
                using (TContextType context = CreateContext())
                {
                    context.Entry(record).State = EntityState.Added;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Data", ex);
            }
        }

        /// <summary>
        /// Implementación de <see cref="IAsyncRepository{TEntityType}.Delete(TEntityType)"/>
        /// </summary>
        public virtual async Task Delete(TEntityType record)
        {
            try
            {
                using (TContextType context = CreateContext())
                {
                    context.Entry(record).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Data", ex);
            }
        }

        /// <summary>
        /// Constructor del contexto
        /// </summary>
        /// <returns>Contexto</returns>
        protected virtual TContextType CreateContext()
        {
            return ContextFactory.Create();
        }
    }
}