using DDDInPractice.UI.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public DashboardViewModel Dashboard { get; private set; }

        public MainViewModel()
        {
            Dashboard = new DashboardViewModel();
        }
    }
}
