using System;

namespace UGPangya.API
{
    public interface IDisposeable : IDisposable
    {
        bool Disposed { get; set; }
    }
}