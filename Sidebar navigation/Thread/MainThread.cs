using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sidebar_navigation.Thread
{
    public class MainThread
    {
        public void RunInMainThread(Action action)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
