using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGPangya.API
{
    public interface IDisposeable : IDisposable
    {
        bool Disposed { get; set; }
    }
}
