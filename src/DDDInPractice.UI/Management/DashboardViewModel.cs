using DDDInPractice.Logic;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.UI.Common;
using DDDInPractice.UI.SnackMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDInPractice.UI.Management
{
    public class DashboardViewModel : ViewModel
    {
        public IReadOnlyList<SnackMachineDto> SnackMachines { get; private set; }
        public Command<SnackMachineDto> ShowSnackMachineCommand { get; private set; }

        public DashboardViewModel()
        {
            RefreshAll();

            ShowSnackMachineCommand = new Command<SnackMachineDto>(x => x != null, ShowSnackMachine);
        }

        private void ShowSnackMachine(SnackMachineDto snackMachineDto)
        {
            SnackMachine snackMachine = new SnackMachine();

            if (snackMachine == null)
            {
                //MessageBox.Show("Snack machine was not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _dialogService.ShowDialog(new SnackMachineViewModel(snackMachine));
            RefreshAll();
        }

        private void RefreshAll()
        {
            SnackMachines = new List<SnackMachineDto>()
            {
                new SnackMachineDto(1, 10)
            };
            
            Notify(nameof(SnackMachines));
        }
    }
}
