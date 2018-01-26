using Avv.Apps.Commons;
using Avv.Apps.Parameters;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Models
{
    public class MenuModel : BindableBase
    {
        /// <summary>
        /// ｼﾝｸﾞﾙﾄﾝﾊﾟﾀｰﾝのｲﾝｽﾀﾝｽ
        /// </summary>
        public static MenuModel Instance {
            get
            {
                if (_Instance == null)
                {

                    // ﾓﾃﾞﾙを設定ﾌｧｲﾙから取得する。
                    _Instance = XmlUtil.ReadXml<MenuModel>(Variables.MenuModelPath);

                    if (_Instance == null)
                    {
                        // 設定ﾌｧｲﾙから取得できなかった場合はﾃﾞﾌｫﾙﾄ値を設定する。
                        _Instance = new MenuModel();
                        _Instance.MenuItems = new ObservableSynchronizedCollection<MenuItemModel>();
                        _Instance.MenuItems.Add(new MenuItemModel("TEST1", MenuItemType.SearchByWord));
                        _Instance.MenuItems.Add(new MenuItemModel("TEST2", MenuItemType.Ranking));
                        _Instance.MenuItems.Add(new MenuItemModel("FAV0", MenuItemType.Favorite)
                            {
                                Children = new ObservableSynchronizedCollection<MenuItemModel>
                                {
                                    new MenuItemModel("FAV1", MenuItemType.Favorite),
                                    new MenuItemModel("FAV2", MenuItemType.Favorite),
                                    new MenuItemModel("FAV3", MenuItemType.Favorite)
                                }
                            });
                        _Instance.MenuItems.Add(new MenuItemModel("TEST4", MenuItemType.Temporary));
                        _Instance.MenuItems.Add(new MenuItemModel("TEST5", MenuItemType.SearchByMylist));
                        _Instance.MenuItems.Add(new MenuItemModel("TEST6", MenuItemType.Mylist));
                        _Instance.MenuItems.Add(new MenuItemModel("TEST7", MenuItemType.Setting));
                    }
                }
                return _Instance;
            }
        }
        private static MenuModel _Instance;

        /// <summary>
        /// ﾌﾟﾗｲﾍﾞｰﾄｺﾝｽﾄﾗｸﾀ
        /// </summary>
        private MenuModel()
        {

        }

        /// <summary>
        /// ﾒﾆｭｰﾂﾘｰﾋﾞｭｰ構成
        /// </summary>
        public ObservableSynchronizedCollection<MenuItemModel> MenuItems
        {
            get { return _MenuItems; }
            set { SetProperty(ref _MenuItems, value); }
        }
        private ObservableSynchronizedCollection<MenuItemModel> _MenuItems;

        #region IDisposable Support

        protected override void OnDisposing()
        {
            base.OnDisposing();

            // ｲﾝｽﾀﾝｽのﾃﾞｰﾀを保存する。
            XmlUtil.SaveXml(Variables.MenuModelPath, this);
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();

            if (MenuItems != null)
            {
                MenuItems.Clear();
                MenuItems = null;
            }
        }

        #endregion

    }
}
