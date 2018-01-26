using Avv.Wpf.Models;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avv.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// 公開情報のｿｰｽとなるﾓﾃﾞﾙを設定、または取得します。
        /// </summary>
        private MenuModel Source
        {
            get { return _Source; }
            set { SetProperty(ref _Source, value);}
        }
        private MenuModel _Source;

        /// <summary>
        /// 現在のﾜｰｸｽﾍﾟｰｽを表します。
        /// </summary>
        public WorkSpaceViewModel Current
        {
            get { return _Current; }
            set { SetProperty(ref _Current, value); }
        }
        private WorkSpaceViewModel _Current;

        /// <summary>
        /// ﾒﾆｭｰﾂﾘｰﾋﾞｭｰ構成
        /// </summary>
        public SynchronizationContextCollection<MenuItemViewModel> MenuItems
        {
            get { return _MenuItems; }
            set { SetProperty(ref _MenuItems, value); }
        }
        private SynchronizationContextCollection<MenuItemViewModel> _MenuItems;

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        public MainWindowViewModel()
        {
            Source = MenuModel.Instance;

            MenuItems = Source.MenuItems.ToSyncedSynchronizationContextCollection(
                model => 
                model.Type == MenuItemType.Favorite
                    ? new MenuItemByFavoriteViewModel(this, model)
                    : new MenuItemViewModel(this, model),
                SynchronizationContext.Current
            );

            // 初回起動時のﾜｰｸｽﾍﾟｰｽ
            Current = new RankingViewModel();
        }

        /// <summary>
        /// ｶﾚﾝﾄを変更します。
        /// </summary>
        /// <param name="sender">変更の起点となるﾒﾆｭｰｱｲﾃﾑ</param>
        /// <param name="args">引数(なし)</param>
        public void OnCurrentChanging(object sender, EventArgs args)
        {
            switch (((MenuItemViewModel)sender).Type)
            {
                case MenuItemType.SearchByWord:
                    Current = new SearchByWordViewModel();
                    break;
                case MenuItemType.Ranking:
                    Current = new RankingViewModel();
                    break;
                default:
                    break;
            }
        }

#region IDisposable Support

        protected override void OnDisposing()
        {
            base.OnDisposing();

            MenuItems.Dispose();
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();

            MenuItems = null;
        }
        
#endregion
    }
}
