using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avv.Wpf.Models;

namespace Avv.Wpf.ViewModels
{
    public class MenuItemByFavoriteViewModel : MenuItemViewModel
    {
        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        /// <param name="vm">本ｲﾝｽﾀﾝｽを保持する親のViewModel</param>
        /// <param name="model">本ｲﾝｽﾀﾝｽを構成するﾓﾃﾞﾙ</param>
        public MenuItemByFavoriteViewModel(MainWindowViewModel vm, MenuItemModel model) : base(vm, model)
        {

        }

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        /// <param name="vm">本ｲﾝｽﾀﾝｽを保持する親のViewModel</param>
        /// <param name="parent">親要素</param>
        /// <param name="model">本ｲﾝｽﾀﾝｽを構成するﾓﾃﾞﾙ</param>
        public MenuItemByFavoriteViewModel(MainWindowViewModel vm, MenuItemViewModel parent, MenuItemModel model) : base(vm, parent, model)
        {
            if (parent is MenuItemByFavoriteViewModel)
            {
                ChildrenRemoved += ((MenuItemByFavoriteViewModel)parent).OnChildrenRemoved;
            }
        }

        /// <summary>
        /// TODO 後で消す。
        /// </summary>
        public bool IsEnabled { get { return (Parent != null); } }

        /// <summary>
        /// ﾒﾆｭｰｱｲﾃﾑをｸﾘｯｸした際のｲﾍﾞﾝﾄ
        /// </summary>
        public EventHandler ChildrenRemoved;

        /// <summary>
        /// Renameﾎﾞﾀﾝ押下時処理
        /// </summary>
        public void Rename()
        {
            Source.Rename();
        }

        /// <summary>
        /// Addﾎﾞﾀﾝ押下時処理
        /// </summary>
        public void AddChildren()
        {
            // TODO 
            Source.Children.Add(new MenuItemModel("Child", MenuItemType.Favorite));
        }

        /// <summary>
        /// Removeﾎﾞﾀﾝ押下時処理
        /// </summary>
        public void RemoveChildren()
        {
            ChildrenRemoved(this, new EventArgs());
        }

        /// <summary>
        /// Removeﾎﾞﾀﾝ押下時処理の実装
        /// </summary>
        /// <param name="sender">削除する要素</param>
        /// <param name="args">未使用</param>
        public void OnChildrenRemoved(object sender, EventArgs args)
        {
            // TODO senderに紐付くﾃﾞｰﾀを削除する。
            // TODO senderの子要素に紐付くﾃﾞｰﾀも再帰的に削除する。

            // 要素を削除
            Source.Children.Remove(((MenuItemByFavoriteViewModel)sender).Source);
        }

        protected override void OnDisposing()
        {
            base.OnDisposing();

            if (Parent != null)
            {
                ChildrenRemoved -= ((MenuItemByFavoriteViewModel)Parent).OnChildrenRemoved;
            }
        }
    }
}
