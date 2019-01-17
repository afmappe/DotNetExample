using System;

namespace Example.Util
{
    public abstract class Disposable : IDisposable
    {
        #region Implementation of IDisposable

        /// <summary>
        /// Detecta llamados redundantes
        /// </summary>
        private bool disposed = false;

        ~Disposable()
        {
            // Termina y no ejecuta el dispose
            Dispose(false);
        }

        /// <summary>
        /// Implementación of <see cref="IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Implementación protegida del patrón Dispose.
        /// </summary>
        /// <param name="disposing">Flag para el destructor de la clase</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    InternalDispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Se utiliza para agregar los objetos o instancias que se deben liberar cuando se llama el Dispose
        /// </summary>
        protected abstract void InternalDispose();
    }
}