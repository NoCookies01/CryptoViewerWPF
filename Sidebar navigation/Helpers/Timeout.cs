using Sidebar_navigation.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sidebar_navigation.Helpers
{
    public class Timeout
    {
        private CancellationTokenSource _cancelationToken;
        private Task _delay;

        public void Run(Action action, int delay)
        {
            Cancel();
            _cancelationToken = new CancellationTokenSource();
            _delay = Task.Delay(delay).ContinueWith((task) =>
            {
                action.Invoke();
            }, _cancelationToken.Token);
        }

        public void Cancel()
        {
            if (_delay != null)
            {
                _cancelationToken.Cancel();
            }
        }
    }
}
