using Avv.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.ViewModels
{
    public class RankingViewModel : WorkSpaceViewModel
    {
        public RankingViewModel() : this(new SearchByWordModel())
        {

        }

        public RankingViewModel(WorkSpaceModel model) : base(model)
        {

        }
    }
}
