using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace csharp_peripherals
{
    public static class ClipboardMonitorHost
    {
        private static Thread _thread;
        private static ClipboardMonitor _monitor;

        public static void Start(Action<string> onClipboardChanged)
        {
            if (_thread != null && _thread.IsAlive)
                return;

            _thread = new Thread(() =>
            {
                _monitor = new ClipboardMonitor();
                _monitor.OnClipboardTextChanged += onClipboardChanged;
                Application.Run(_monitor);
            });

            _thread.SetApartmentState(ApartmentState.STA);
            _thread.IsBackground = true;
            _thread.Start();
        }

        public static void Stop()
        {
            if (_monitor != null && !_monitor.IsDisposed)
            {
                _monitor.Invoke(new Action(() => _monitor.Close()));
            }
        }
    }
}
