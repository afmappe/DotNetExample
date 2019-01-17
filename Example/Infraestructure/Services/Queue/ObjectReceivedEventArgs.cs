using System;

namespace Example.Infraestructure.Services.Queue
{
    public class ObjectReceivedEventArgs : EventArgs
    {
        public object Data { get; set; }
    }
}