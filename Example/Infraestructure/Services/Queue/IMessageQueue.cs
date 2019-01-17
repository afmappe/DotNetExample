using System;

namespace Example.Infraestructure.Services.Queue
{
    public interface IMessageQueue : IDisposable
    {
        /// <summary>
        ///
        /// </summary>
        event EventHandler<ObjectReceivedEventArgs> ObjectReceive;

        /// <summary>
        ///
        /// </summary>
        string Path { get; set; }

        /// <summary>
        ///
        /// </summary>
        void BeginReceive();

        /// <summary>
        ///
        /// </summary>
        void Purge();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        object Receive();

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        void Send(object obj);
    }
}