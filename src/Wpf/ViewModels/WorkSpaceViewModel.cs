using Avv.Wpf.Models;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.ViewModels
{
    public abstract class WorkSpaceViewModel : ViewModelBase
    {
        protected WorkSpaceViewModel(WorkSpaceModel model)
        {
            WSSource = model;

            Items = WSSource.Items.ToSyncedSynchronizationContextCollection(m => m, AnonymousSynchronizationContext.Current);
        }

        /// <summary>
        /// 本ｲﾝｽﾀﾝｽのみで使用するｿｰｽﾓﾃﾞﾙ
        /// </summary>
        private WorkSpaceModel WSSource { get; set; }

        /// <summary>
        /// 本ﾒﾆｭｰｱｲﾃﾑの子要素
        /// </summary>
        public SynchronizationContextCollection<VideoModel> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private SynchronizationContextCollection<VideoModel> _Items;

    }
}
