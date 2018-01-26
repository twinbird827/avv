using Avv.Wpf.Models;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Avv.Wpf.ViewModels
{
    public class MenuItemViewModel : ViewModelBase
    {
        /// <summary>
        /// 本ｲﾝｽﾀﾝｽを保持する親のViewModel
        /// </summary>
        public MainWindowViewModel VM { get; private set; }

        /// <summary>
        /// 本ﾒﾆｭｰｱｲﾃﾑの親要素
        /// </summary>
        public MenuItemViewModel Parent { get; private set; }

        /// <summary>
        /// 本ｲﾝｽﾀﾝｽを構成するﾓﾃﾞﾙ
        /// </summary>
        public MenuItemModel Source { get; private set; }

        /// <summary>
        /// ｱｲﾃﾑ名
        /// </summary>
        public string Name
        {
            get { return Source.Name; }
            set { SetProperty(ref _Name, value); Source.Name = _Name; }
        }
        private string _Name = null;

        /// <summary>
        /// ﾒﾆｭｰの種類
        /// </summary>
        public MenuItemType Type
        {
            get { return Source.Type; }
            set { SetProperty(ref _Type, value); Source.Type = _Type; }
        }
        private MenuItemType _Type;

        /// <summary>
        /// 本ﾒﾆｭｰｱｲﾃﾑの子要素
        /// </summary>
        public SynchronizationContextCollection<MenuItemViewModel> Children
        {
            get { return _Children; }
            set { SetProperty(ref _Children, value); }
        }
        private SynchronizationContextCollection<MenuItemViewModel> _Children;

        /// <summary>
        /// ﾒﾆｭｰｱｲﾃﾑをｸﾘｯｸした際のｲﾍﾞﾝﾄ
        /// </summary>
        public EventHandler Click;

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        /// <param name="vm">本ｲﾝｽﾀﾝｽを保持する親のViewModel</param>
        /// <param name="model">本ｲﾝｽﾀﾝｽを構成するﾓﾃﾞﾙ</param>
        public MenuItemViewModel(MainWindowViewModel vm, MenuItemModel model)
        {
            VM = vm;
            Source = model;
            Parent = null;

            Name = Source.Name;
            Type = Source.Type;
            Children = model.Children.ToSyncedSynchronizationContextCollection(
                m =>
                m.Type == MenuItemType.Favorite
                    ? new MenuItemByFavoriteViewModel(vm, this, m)
                    : new MenuItemViewModel(vm, this, m),
                SynchronizationContext.Current
            ); ;

            // ﾒﾆｭｰｱｲﾃﾑをｸﾘｯｸした際はﾜｰｸｽﾍﾟｰｽのｶﾚﾝﾄを変更する。
            Click += VM.OnCurrentChanging;
        }

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        /// <param name="vm">本ｲﾝｽﾀﾝｽを保持する親のViewModel</param>
        /// <param name="model">本ｲﾝｽﾀﾝｽを構成するﾓﾃﾞﾙ</param>
        public MenuItemViewModel(MainWindowViewModel vm, MenuItemViewModel parent, MenuItemModel model) : this(vm, model)
        {
            Parent = parent;
        }

        /// <summary>
        /// ﾒﾆｭｰｱｲﾃﾑをｸﾘｯｸした際の処理
        /// </summary>
        public void OnClicked()
        {
            // Clickｲﾍﾞﾝﾄを発行する。
            Click(this, new EventArgs());
        }

        #region IDisposable Support

        protected override void OnDisposing()
        {
            base.OnDisposing();

            // ｸﾘｯｸｲﾍﾞﾝﾄの購読解除
            Click -= VM.OnCurrentChanging;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();

            VM = null;
            Source = null;
        }
        
        #endregion
    }
}
