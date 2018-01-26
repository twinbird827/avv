using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Apps.Commons
{
    public struct FLASHINFO
    {
        [ComVisible(false)]
        public int cbsize;

        [ComVisible(false)]
        public IntPtr hwnd;

        [ComVisible(false)]
        public int dwFlags;

        [ComVisible(false)]
        public int uCount;

        [ComVisible(false)]
        public int dwTimeout;
    }
}
