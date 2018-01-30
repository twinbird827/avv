using Avv.Apps.Parameters;
using Avv.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Avv.Wpf.Views.Windows.Dummy
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        private bool assemblyLoaded { get; set; }

        public Window1()
        {
            assemblyLoaded = typeof(System.Windows.Interactivity.Interaction) != null;
            InitializeComponent();

            var model = new SearchByMylistModel();
            model.Word = HttpRequestConst.MylistDefault;
            model.Reload();
        }
    }
}
