using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Views.Services
{
    public class WpfUserInputService : IUserInputService
    {
        public string GetNewFavoriteName(string old)
        {
            return old.Substring(0, 3) + DateTime.Now.Ticks.ToString();
        }
    }
}
