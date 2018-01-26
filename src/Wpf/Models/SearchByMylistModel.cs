using Avv.Apps.Commons;
using Avv.Apps.Parameters;
using Avv.Wpf.Views.Services;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace Avv.Wpf.Models
{
    public class SearchByMylistModel : WorkSpaceModel
    {
        /// <summary>
        /// 検索ﾜｰﾄﾞ
        /// </summary>
        public string Word
        {
            get { return _Word; }
            set { SetProperty(ref _Word, value); }
        }
        private string _Word = null;

        /// <summary>
        /// ﾀｲﾄﾙ
        /// </summary>
        public string MylistTitle
        {
            get { return _MylistTitle; }
            set { SetProperty(ref _MylistTitle, value); }
        }
        private string _MylistTitle = null;

        /// <summary>
        /// 作成者
        /// </summary>
        public string MylistCreator
        {
            get { return _MylistCreator; }
            set { SetProperty(ref _MylistCreator, value); }
        }
        private string _MylistCreator = null;

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime MylistDate
        {
            get { return _MylistDate; }
            set { SetProperty(ref _MylistDate, value); }
        }
        private DateTime _MylistDate = default(DateTime);

        /// <summary>
        /// 指定された検索ﾜｰﾄﾞで一覧を再読み込みします。
        /// </summary>
        public override void Reload()
        {
            var url = GetMylistUrl(Word);

            //// TODO
            url = "http://www.nicovideo.jp/mylist/36253814";
            //if (url == null)
            //{
            //    ServiceFactory.MessageService.ShowError("IDが正しくありません。");
            //    return;
            //}

            //var txt = GetSmileVideoHtmlText(url);
            var txt = File.ReadAllText("C:\\Work\\VBNET\\Avv\\新しいテキスト ドキュメント (2).txt");
            if (IsError(txt))
            {
                return;
            }

            // TODO あとで消す。
            Clipboard.SetText(txt);


            if (url == HttpRequestConst.MylistDefault)
            {
                MylistTitle = "Toriaezu Mylist";

                var result = DynamicJson.Parse(txt);

                // 自分のﾘｽﾄを検索した。
                ServiceFactory.MessageService.ShowError(result["status"]);
                foreach (dynamic item in result["mylistitem"])
                {
                    if (item["item_type"] != "0")
                    {
                        continue;
                    }

                    var model = new VideoModel()
                    {
                        Title = item["title"],
                        ContentId = item["video_id"],
                        ViewCounter = item["view_counter"],
                        MylistCounter = item["mylist_counter"],
                        StartTime = DateTime.Parse(item["update_time"])
                    };

                    Items.Add(model);
                    //length_seconds
                    //
                    //num_res
                    //
                    //first_retrieve
                    //
                    //item_id
                    //description

                }
            }
            else
            {
                // 他人のﾘｽﾄを検索した。
                var result = XDocument.Load(new StringReader(txt)).Root;
                var channel = result.Descendants("channel").First();

                MylistTitle = channel.Element("title").Value;
                MylistCreator = channel.Element(XName.Get("creator", "http://purl.org/dc/elements/1.1/")).Value;
                MylistDate = DateTime.Parse(channel.Element("lastBuildDate").Value);

                // TODO descriptionにもうちょっとあるらしい
                Items = new ObservableSynchronizedCollection<VideoModel>(
                    channel.Descendants("item").Select(
                        item => new VideoModel()
                        {
                            Title = item.Element("title").Value,
                            ContentId = item.Element("link").Value,
                            StartTime = DateTime.Parse(channel.Element("pubDate").Value)
                        }
                    )
                );
            }

            ServiceFactory.MessageService.ShowError("owatta2");
        }

        /// <summary>
        /// 指定された検索ﾜｰﾄﾞからUrlを取得します。
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string GetMylistUrl(string src)
        {
            string dst = src.Trim().Split('/').LastOrDefault();

            if (string.IsNullOrEmpty(dst))
            {
                return null;
            }
            else if (dst == "home" || dst == "list")
            {
                return HttpRequestConst.MylistDefault;
            }
            else if (dst.StartsWith("ml"))
            {
                return HttpRequestConst.MylistUrl + dst;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 検索結果にｴﾗｰがないか確認します。
        /// </summary>
        /// <param name="txt">検索結果</param>
        /// <returns>ｴﾗｰ：true / 正常：false</returns>
        private bool IsError(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                ServiceFactory.MessageService.ShowError("何らかの通信エラーです。");
                return true;
            }
            else if (txt.Contains("error:410"))
            {
                ServiceFactory.MessageService.ShowError("検索結果が見つかりませんでした。");
                return true;
            }
            else if (txt.Contains("error:403"))
            {
                ServiceFactory.MessageService.ShowError("非公開に設定されています。");
                return true;
            }
            else if (txt.Contains("お探しのページは、表示を許可していない可能性があります。"))
            {
                ServiceFactory.MessageService.ShowError("お探しのページは、表示を許可していない可能性があります。");
                return true;
            }
            else if (txt.Contains("error:"))
            {
                ServiceFactory.MessageService.ShowError("検索結果が見つかりません。ログイン状態か通信状況を確認してください。");
                return true;
            }
            else if (txt.Contains("<?xml ")　&& txt.Contains("\"item_type\""))
            {
                ServiceFactory.MessageService.ShowError("検索結果が見つかりません。ログイン状態か通信状況を確認してください。");
                return true;
            }

            return false;
        }
    }
}
