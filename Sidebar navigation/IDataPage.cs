using Sidebar_navigation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidebar_navigation
{
    public  interface IDataPage
    {
        CurResponce CurResponce { get; set; }
    }
}
