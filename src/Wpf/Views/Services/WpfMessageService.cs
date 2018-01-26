using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Avv.Wpf.Views.Services
{
    class WpfMessageService : IMessageService
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
