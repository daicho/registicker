using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace StampRegister
{
    public partial class Form1 : Form
    {
        enum Columns
        {
            name,
            roma,
            gender,
            honorific,
            export,
            stamp,
            image,
            request,
            release,
            url1,
            url2,
            origin
        }

        const int waitSeconds = 60;
        private bool stop = false;
        private bool menuCancel = false;
        private bool save = true;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Webbrowserの読み込みが終わるまで待機する。
        /// </summary>
        /// <returns>中断ボタンが押されたかどうか</returns>
        private bool WaitLoad()
        {
            // 読み込みを開始するまで待機
            while (!mainBrowser.IsBusy && mainBrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                Thread.Sleep(10);
                Application.DoEvents();
                if (stop) return true;
            }

            // 読み込みが完了するまで待機
            do
            {
                Thread.Sleep(10);
                Application.DoEvents();
                if (stop) return true;
            } while (mainBrowser.IsBusy || mainBrowser.ReadyState != WebBrowserReadyState.Complete);

            return false;
        }

        /// <summary>
        /// HTMLの要素を属性を使って取得する。
        /// </summary>
        /// <param name="tagName">タグ名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">値</param>
        /// <returns>見つかったHTML要素</returns>
        private HtmlElement SearchElementByAttribute(string tagName, string attribute, string value)
        {
            foreach (HtmlElement element in mainBrowser.Document.GetElementsByTagName(tagName))
            {
                if (element.GetAttribute(attribute) == value)
                    return element;
            }

            return null;
        }

        /// <summary>
        /// HTMLの要素をインナーテキストを使って取得する。
        /// </summary>
        /// <param name="tagName">タグ名</param>
        /// <param name="text">インナーテキスト</param>
        /// <returns>見つかったHTML要素</returns>
        private HtmlElement SearchElementByInnerText(string tagName, string text)
        {
            foreach (HtmlElement element in mainBrowser.Document.GetElementsByTagName(tagName))
            {
                if (element.InnerText == text)
                    return element;
            }

            return null;
        }

        /// <summary>
        /// HTMLの要素を属性とインナーテキストを使って取得する。
        /// </summary>
        /// <param name="tagName">タグ名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">値</param>
        /// <param name="text">インナーテキスト</param>
        /// <returns>見つかったHTML要素</returns>
        private HtmlElement SearchElementByMix(string tagName, string attribute, string value, string text)
        {
            foreach (HtmlElement element in mainBrowser.Document.GetElementsByTagName(tagName))
            {
                if (element.GetAttribute(attribute) == value && element.InnerText.IndexOf(text) != -1)
                    return element;
            }

            return null;
        }

        /// <summary>
        /// 名前のリストを読み込む。
        /// </summary>
        /// <param name="filePath">読み込むファイル名</param>
        void LoadNameList(string filePath)
        {
            XLWorkbook workbook;
            IXLWorksheet worksheet;

            nameList.Items.Clear();

            if (filePath != "")
            {
                try
                {
                    // Excelファイルを開く
                    workbook = new XLWorkbook(filePath);
                }
                catch
                {
                    nameListFile.Text = "";
                    MessageBox.Show("名前情報ファイルを開けませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                worksheet = workbook.Worksheet(1);
                
                for (int i = 0; worksheet.Cell(i + 2, 1).Value.ToString() != ""; i++)
                {
                    int col = 1;

                    nameList.Items.Add(worksheet.Cell(i + 2, col++).Value.ToString());             // 名前
                    nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col++).Value.ToString()); // ローマ字
                    nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col++).Value.ToString()); // 性別
                    nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col++).Value.ToString()); // 敬称

                    // 画像出力完了
                    if (worksheet.Cell(i + 2, col).Value.ToString() == "")
                        nameList.Items[i].SubItems.Add("0");
                    else
                        nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col).Value.ToString());
                    col++;

                    // 新規登録完了
                    if (worksheet.Cell(i + 2, col).Value.ToString() == "")
                        nameList.Items[i].SubItems.Add("0");
                    else
                        nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col).Value.ToString());
                    col++;

                    // 画像登録完了
                    if (worksheet.Cell(i + 2, col).Value.ToString() == "")
                        nameList.Items[i].SubItems.Add("0");
                    else
                        nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col).Value.ToString());
                    col++;

                    // タグ付け完了
                    /*if (worksheet.Cell(i + 2, col).Value.ToString() == "")
                        nameList.Items[i].SubItems.Add("0");
                    else
                        nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col).Value.ToString());*/
                    col++;

                    // リクエスト完了
                    if (worksheet.Cell(i + 2, col).Value.ToString() == "")
                        nameList.Items[i].SubItems.Add("0");
                    else
                        nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col).Value.ToString());
                    col++;

					// リリース完了
					if (worksheet.Cell(i + 2, col).Value.ToString() == "")
                        nameList.Items[i].SubItems.Add("0");
                    else
                        nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col).Value.ToString());
                    col++;

                    nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col++).Value.ToString()); // URL1
                    nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col++).Value.ToString()); // URL2
                    nameList.Items[i].SubItems.Add(worksheet.Cell(i + 2, col++).Value.ToString()); // Illustratorファイル名
                }
            }

			releaseCount.Text = countRelease().ToString();
        }

        /// <summary>
        /// 名前のリストを保存する。
        /// </summary>
        /// <param name="filePath">書き出すファイル名</param>
        void SaveNameList(string filePath)
        {
            XLWorkbook workbook;
            IXLWorksheet worksheet;

            if (filePath != "")
            {
                try
                {
                    // Excelファイルを開く
                    workbook = new XLWorkbook(filePath);
                }
                catch
                {
                    MessageBox.Show("名前情報ファイルを保存できませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                worksheet = workbook.Worksheet(1);

                // リストを書き出し
                int i = 2;
                foreach (ListViewItem item in nameList.Items)
                {
					for (int j = 0; j < 12; j++)
						worksheet.Cell(i, j + (j < 7 ? 1 : 2)).Value = item.SubItems[j].Text;
                    i++;
                }

                workbook.Save();
            }
        }

		/// <summary>
		/// リリース未完数をカウントする
		/// </summary>
		/// <returns>リリース未完数</returns>
		int countRelease()
		{
			int relNum = 0;
			
			foreach (ListViewItem item in nameList.Items)
				relNum += 2;

			return relNum;
		}

        /// <summary>
        /// 登録開始の処理をする。
        /// </summary>
        void StartRegister()
        {
            stop = false;
            menuCancel = true;
            this.Text = "StampRegister [実行中]";
            escape.Enabled = true;
            exportImages.Enabled = false;
            start.Enabled = false;
            registerImages.Enabled = false;
            //tagStart.Enabled = false;
            request.Enabled = false;
            release.Enabled = false;
            settingFile.Enabled = false;
            loginInfo.Enabled = false;
            stampInfo.Enabled = false;
            nameListFile.Enabled = false;
            browse.Enabled = false;
            nameReload.Enabled = false;
            broserMenu.Enabled = false;
            nameMenu.Enabled = false;
        }

        /// <summary>
        /// 終了の処理をする。
        /// </summary>
        void StopRegister()
        {
            menuCancel = false;
            SaveNameList(nameListFile.Text);
            this.Text = "StampRegister";
            exportImages.Enabled = true;
            start.Enabled = true;
            registerImages.Enabled = true;
            //tagStart.Enabled = true;
            request.Enabled = true;
            release.Enabled = true;
            exportImages.Enabled = true;
            settingFile.Enabled = true;
            loginInfo.Enabled = true;
            stampInfo.Enabled = true;
            nameListFile.Enabled = true;
            browse.Enabled = true;
            nameReload.Enabled = true;
            broserMenu.Enabled = true;
            nameMenu.Enabled = true;
        }
        
        /// <summary>
        /// ログインする。
        /// </summary>
        /// <returns>中断ボタンが押されたかどうか</returns>
        bool Login()
        {
            HtmlElement loginButton;

            mainBrowser.Navigate("https://creator.line.me/signup/line_auth");
            if (WaitLoad()) return true;

            if ((loginButton = SearchElementByAttribute("input", "value", "ログイン")) != null)
            {
                mainBrowser.Document.GetElementById("tid").SetAttribute("value", mailAddress.Text);
                mainBrowser.Document.GetElementById("tpasswd").SetAttribute("value", password.Text);

                loginButton.Enabled = true;
                loginButton.InvokeMember("click");

                if (WaitLoad()) return true;
            }

            return false;
        }

        private void Escape_Click(object sender, EventArgs e)
        {
            stop = true;
        }

        private void ExportImages_Click(object sender, EventArgs e)
        {
            string desptopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            StartRegister();

            // フォルダ作成
            if (!Directory.Exists(desptopPath + @"\LINE zip"))
                Directory.CreateDirectory(desptopPath + @"\LINE zip");

            // Illustrator起動
            dynamic app = Activator.CreateInstance(Type.GetTypeFromProgID("Illustrator.Application"));

            foreach (ListViewItem item in nameList.Items)
            {
                for (int i = int.Parse(item.SubItems[(int)Columns.export].Text); i < 2; i++)
                {
                    string name = item.SubItems[(int)Columns.name].Text;
                    string stampName = name + item.SubItems[(int)Columns.gender].Text + (i + 1);
                    string pandaName;
                    string fileName;

                    // 既にzipがあるかどうか
                    if (File.Exists(desptopPath + @"\LINE zip\" + stampName + @".zip"))
                        continue;

                    // パンダの名前
                    if (item.SubItems[(int)Columns.gender].Text == "男")
                        pandaName = "Mr.パンダ" + (i + 1);
                    else
                        pandaName = "Missパンダ" + (i + 1);

                    // 特殊パターンかどうか
                    if (item.SubItems[(int)Columns.origin].Text != "")
                    {
                        fileName = pandaName + "-" + item.SubItems[(int)Columns.origin].Text + ".ai";
                    }
                    else
                    {
                        // ひらがな・カタカナ・漢字判定
                        if (Regex.IsMatch(name, @"^[\p{IsHiragana}\p{IsKatakana}\da-zA-Zａ-ｚＡ-Ｚ～！？★☆●○♪]+$"))
                            fileName = pandaName + "-ひらがな" + name.Length + "文字.ai";
                        else if (Regex.IsMatch(name, @"^[^\p{IsHiragana}\p{IsKatakana}\da-zA-Zａ-ｚＡ-Ｚ～！？★☆●○♪]+$"))
                            fileName = pandaName + "-漢字" + name.Length + "文字.ai";
                        else
                            continue;
                    }

                    // Illustratorファイルを開く
                    if (!File.Exists(Application.StartupPath + @"\スタンプ\" + fileName))
                        continue;

                    dynamic doc = app.Open(Application.StartupPath + @"\スタンプ\" + fileName);

                    // 名前を置換
                    foreach (dynamic TextFrame in doc.TextFrames)
                    {
                        TextFrame.Contents = TextFrame.Contents.Replace("***", name);
                        Application.DoEvents();
                    }

                    // 書き出し
                    doc.Export(desptopPath + @"\a", Illustrator.AiExportType.aiPNG24);
                    Application.DoEvents();

                    // ファイルを閉じる
                    doc.Close(Illustrator.AiSaveOptions.aiDoNotSaveChanges);
                    Application.DoEvents();

                    // リネーム
                    for (int j = 1; j <= 40; j++)
                        File.Move(desptopPath + @"\images\a_" + j.ToString("D2") + ".png", desptopPath + @"\images\" + j.ToString("D2") + ".png");

                    File.Move(desptopPath + @"\images\a_41.png", desptopPath + @"\images\main.png");
                    File.Move(desptopPath + @"\images\a_42.png", desptopPath + @"\images\tab.png");
                    File.Delete(desptopPath + @"\images\a_43.png");
                    File.Delete(desptopPath + @"\images\a_44.png");

                    // zipに圧縮
                    System.IO.Compression.ZipFile.CreateFromDirectory(desptopPath + @"\images", desptopPath + @"\LINE zip\" + stampName + @".zip");
                    Directory.Delete(desptopPath + @"\images", true);

                    // 完了状況保存
                    item.SubItems[(int)Columns.export].Text = (i + 1).ToString();

                    Application.DoEvents();

                    if (stop)
                    {
                        // Illustratorを終了
                        app.Quit();
                        StopRegister();
                        return;
                    }
                }
            }

            // Illustratorを終了
            app.Quit();
            StopRegister();
            MessageBox.Show("終了！");
        }

        private void Start_Click(object sender, EventArgs e)
        {
            string[] countries = { "JP", "TW", "TH", "AU", "US", "CA", "GU", "BR", "GB", "SE", "ES" }; // 販売する国

            StartRegister();
            if (Login()) { StopRegister(); return; } // ログイン

            foreach (ListViewItem item in nameList.Items)
            {
                if (item.SubItems[(int)Columns.gender].Text != "男" && item.SubItems[(int)Columns.gender].Text != "女") continue;
                
                for (int i = int.Parse(item.SubItems[(int)Columns.stamp].Text); i < 2; i++)
                {
                    HtmlElement okButton;

                    string enTitle;
                    string jpTitle;
                    string enDescription;
                    string jpDescription;

                    string baseTitle_en;
                    string baseTitle_jp;
                    string baseDescription_en;
                    string baseDescription_jp;
                    int baseTaste;
                    int baseChara;

                    // 入力文字列識別
                    switch (item.SubItems[(int)Columns.honorific].Text)
                    {
                        case "くん":
                            if (i == 0)
                            {
                                baseTitle_en = Properties.Settings.Default.BoyTitle1_en;
                                baseTitle_jp = Properties.Settings.Default.BoyTitle1_jp;
                            }
                            else
                            {
                                baseTitle_en = Properties.Settings.Default.BoyTitle2_en;
                                baseTitle_jp = Properties.Settings.Default.BoyTitle2_jp;
                            }

                            baseDescription_en = Properties.Settings.Default.BoyDescription1_en;
                            baseDescription_jp = Properties.Settings.Default.BoyDescription1_jp;
                            baseTaste = Properties.Settings.Default.BoyTaste1;
                            baseChara = Properties.Settings.Default.BoyChara1;

                            break;

                        case "ちゃん":
                            if (i == 0)
                            {
                                baseTitle_en = Properties.Settings.Default.GirlTitle1_en;
                                baseTitle_jp = Properties.Settings.Default.GirlTitle1_jp;
                            }
                            else
                            {
                                baseTitle_en = Properties.Settings.Default.GirlTitle2_en;
                                baseTitle_jp = Properties.Settings.Default.GirlTitle2_jp;
                            }

                            baseDescription_en = Properties.Settings.Default.GirlDescription1_en;
                            baseDescription_jp = Properties.Settings.Default.GirlDescription1_jp;
                            baseTaste = Properties.Settings.Default.GirlTaste1;
                            baseChara = Properties.Settings.Default.GirlChara1;

                            break;

                        case "さん":
                            if (item.SubItems[(int)Columns.gender].Text == "男")
                            {
                                if (i == 0)
                                {
                                    baseTitle_en = Properties.Settings.Default.SanBoyTitle1_en;
                                    baseTitle_jp = Properties.Settings.Default.SanBoyTitle1_jp;
                                }
                                else
                                {
                                    baseTitle_en = Properties.Settings.Default.SanBoyTitle2_en;
                                    baseTitle_jp = Properties.Settings.Default.SanBoyTitle2_jp;
                                }

                                baseDescription_en = Properties.Settings.Default.SanBoyDescription1_en;
                                baseDescription_jp = Properties.Settings.Default.SanBoyDescription1_jp;
                                baseTaste = Properties.Settings.Default.SanBoyTaste1;
                                baseChara = Properties.Settings.Default.SanBoyChara1;
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    baseTitle_en = Properties.Settings.Default.SanGirlTitle1_en;
                                    baseTitle_jp = Properties.Settings.Default.SanGirlTitle1_jp;
                                }
                                else
                                {
                                    baseTitle_en = Properties.Settings.Default.SanGirlTitle2_en;
                                    baseTitle_jp = Properties.Settings.Default.SanGirlTitle2_jp;
                                }

                                baseDescription_en = Properties.Settings.Default.SanGirlDescription1_en;
                                baseDescription_jp = Properties.Settings.Default.SanGirlDescription1_jp;
                                baseTaste = Properties.Settings.Default.SanGirlTaste1;
                                baseChara = Properties.Settings.Default.SanGirlChara1;
                            }

                            break;

                        default:
                            if (item.SubItems[(int)Columns.gender].Text == "男")
                            {
                                if (i == 0)
                                {
                                    baseTitle_en = Properties.Settings.Default.AllBoyTitle1_en;
                                    baseTitle_jp = Properties.Settings.Default.AllBoyTitle1_jp;
                                }
                                else
                                {
                                    baseTitle_en = Properties.Settings.Default.AllBoyTitle2_en;
                                    baseTitle_jp = Properties.Settings.Default.AllBoyTitle2_jp;
                                }

                                baseDescription_en = Properties.Settings.Default.AllBoyDescription1_en;
                                baseDescription_jp = Properties.Settings.Default.AllBoyDescription1_jp;
                                baseTaste = Properties.Settings.Default.AllBoyTaste1;
                                baseChara = Properties.Settings.Default.AllBoyChara1;
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    baseTitle_en = Properties.Settings.Default.AllGirlTitle1_en;
                                    baseTitle_jp = Properties.Settings.Default.AllGirlTitle1_jp;
                                }
                                else
                                {
                                    baseTitle_en = Properties.Settings.Default.AllGirlTitle2_en;
                                    baseTitle_jp = Properties.Settings.Default.AllGirlTitle2_jp;
                                }

                                baseDescription_en = Properties.Settings.Default.AllGirlDescription1_en;
                                baseDescription_jp = Properties.Settings.Default.AllGirlDescription1_jp;
                                baseTaste = Properties.Settings.Default.AllGirlTaste1;
                                baseChara = Properties.Settings.Default.AllGirlChara1;
                            }

                            break;
                    }

                    enTitle = baseTitle_en.Replace("***", item.SubItems[(int)Columns.roma].Text);
                    jpTitle = baseTitle_jp.Replace("***", item.SubItems[(int)Columns.name].Text).Replace("@@@", item.SubItems[(int)Columns.honorific].Text);
                    enDescription = baseDescription_en.Replace("***", item.SubItems[(int)Columns.roma].Text);
                    jpDescription = baseDescription_jp.Replace("***", item.SubItems[(int)Columns.name].Text).Replace("@@@", item.SubItems[(int)Columns.honorific].Text);

                    // 登録ページに移動
                    mainBrowser.Navigate("https://creator.line.me" + mainBrowser.Document.GetElementsByTagName("base")[0].GetAttribute("href") + "sticker/create");
                    if (WaitLoad()) { StopRegister(); return; }

                    // 完全に読み込むまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (SearchElementByInnerText("span", "選択したエリアで販売する") == null);

                    // 各項目入力
                    mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("meta[en][title]")[0].InnerText = enTitle;
                    mainBrowser.Document.GetElementsByTagName("textarea").GetElementsByName("meta[en][description]")[0].InnerText = enDescription;
                    mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("copyright")[0].InnerText = Properties.Settings.Default.Copyright;
                    SearchElementByInnerText("span", "選択したエリアで販売する").Parent.GetElementsByTagName("input")[0].InvokeMember("click");

                    // セレクトボックスの項目数を減らす
                    HtmlElement lang = SearchElementByAttribute("select", "ng-model", "selected_desc_lang");
                    foreach (HtmlElement langOption in lang.All)
                    {
                        if (langOption.GetAttribute("value") == "0")
                            langOption.SetAttribute("label", "JapaneseJapaneseJapaneseJapaneseJapanese");
                        else if (langOption.GetAttribute("value") != "")
                            langOption.OuterHtml = "";
                    }

                    // 手動部分が終わるまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("meta[ja][title]").Count == 0);

                    mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("meta[ja][title]")[0].InnerText = jpTitle;
                    mainBrowser.Document.GetElementsByTagName("textarea").GetElementsByName("meta[ja][description]")[0].InnerText = jpDescription;

                    foreach (HtmlElement element in mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("areas[]"))
                    {
                        if (element.GetAttribute("type") == "checkbox")
                        {
                            if (Array.IndexOf(countries, element.GetAttribute("value")) != -1)
                            {
                                element.SetAttribute("checked", "checked");
                            }
                            else
                            {
                                element.SetAttribute("checked", "");
                            }
                        }
                    }

                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("meta[ja][title]")[0].GetAttribute("className") == "ng-scope ng-isolate-scope ng-valid-pattern ng-valid-max-eaw ng-valid-bad-word ng-valid-contains-uri ng-dirty ng-valid-required ng-valid-min-eaw ng-invalid ng-invalid-duplicated");

                    // 保存ボタンクリック
                    SearchElementByAttribute("input", "type", "submit").InvokeMember("click");

                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while ((okButton = SearchElementByAttribute("a", "ng-click", "close(true)")) == null);

                    okButton.InvokeMember("click");
                    if (WaitLoad()) { StopRegister(); return; }

                    // URL&完了状況保存
                    item.SubItems[(int)Columns.url1 + i].Text = mainBrowser.Url.ToString().Replace("?saved=true", "");
                    item.SubItems[(int)Columns.stamp].Text = (i + 1).ToString();
                }
            }
            
            StopRegister();
            MessageBox.Show("終了！");
            Restart("");
        }

        private void RegisterImages_Click(object sender, EventArgs e)
        {
            string desptopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // 作業用フォルダ作成
            if (!Directory.Exists(desptopPath + @"\LINE zip"))
                Directory.CreateDirectory(desptopPath + @"\LINE zip");
            if (!Directory.Exists(desptopPath + @"\LINE select zip"))
                Directory.CreateDirectory(desptopPath + @"\LINE select zip");

            // 残っているファイルを戻す
            foreach (string file in Directory.GetFiles(desptopPath + @"\LINE select zip"))
                File.Move(file, desptopPath + @"\LINE zip\" + Path.GetFileName(file));

            StartRegister();
            if (Login()) { StopRegister(); return; } // ログイン

            foreach (ListViewItem item in nameList.Items)
            {
                for (int i = int.Parse(item.SubItems[(int)Columns.image].Text); i < 2; i++)
                {
                    HtmlElement perSet;
                    HtmlElement okButton;
                    string stampName = item.SubItems[(int)Columns.name].Text + item.SubItems[(int)Columns.gender].Text + (i + 1);

                    if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

                    // zipファイルを移動
                    if (File.Exists(desptopPath + @"\LINE zip\" + stampName + ".zip"))
                    {
                        File.Move(desptopPath + @"\LINE zip\" + stampName + ".zip", desptopPath + @"\LINE select zip\" + stampName + ".zip");
                    }
                    else
                    {
                        MessageBox.Show(stampName + ".zipが存在しません", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // ページに移動
                    mainBrowser.Navigate(item.SubItems[(int)Columns.url1 + i].Text + "/image");
                    if (WaitLoad()) { StopRegister(); return; }

                    if (SearchElementByInnerText("h1", "エラーが発生しました") != null)
                        continue;

                    // 完全に読み込むまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                    } while ((perSet = SearchElementByAttribute("select", "ng-model", "sticker.stickersPerSet")) == null);

                    // セレクトボックスの項目数を減らす
                    foreach (HtmlElement perSetOption in perSet.All)
                    {
                        if (perSetOption.GetAttribute("value") != "8" && perSetOption.GetAttribute("value") != "40")
                            perSetOption.OuterHtml = "";
                    }

                    // アップロードボタン無効化
                    mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("file")[0].SetAttribute("disabled", "disabled");

                    // 見出しの変更
                    foreach (HtmlElement element in mainBrowser.Document.GetElementsByTagName("h1"))
                    {
                        if (element.InnerText == "スタンプの編集")
                        {
                            element.InnerText = "「" + stampName + "」のスタンプの編集";
                            break;
                        }
                    }

                    // スタンプ個数を変更するまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (perSet.GetAttribute("selectedIndex") == "0");

                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while ((okButton = SearchElementByAttribute("a", "ng-click", "close(true)")) == null);

                    // OKボタンクリック
                    okButton.InvokeMember("click");

                    // アップロードボタン有効化
                    mainBrowser.Document.GetElementsByTagName("input").GetElementsByName("file")[0].SetAttribute("disabled", "");

                    // アップロードが終わるまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (SearchElementByAttribute("div", "ng-if", "sticker.stickerType === 'static' || image.key === 'tab'") == null);

                    string[] files = Directory.GetFiles(desptopPath + @"\LINE zip\", "*.zip");
                    foreach (string file in files)
                    {
                        if (file.IndexOf("■") >= 0)
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {

                            }
                        }
                    }
                    
                    File.Move(desptopPath + @"\LINE select zip\" + stampName + ".zip", desptopPath + @"\LINE zip\■" + stampName + ".zip");

                    // 完了状況保存
                    item.SubItems[(int)Columns.image].Text = (i + 1).ToString();
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
            Restart("");
        }
        
        /*private void TagStart_Click(object sender, EventArgs e)
        {
            XLWorkbook workbook;
            DateTime startTime;

            try
            {
                // Excelファイルを開く
                workbook = new XLWorkbook(tagFilePath.Text);
            }
            catch
            {
                MessageBox.Show("タグ情報ファイルを開けませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartRegister();
            if (Login()) { StopRegister(); return; } // ログイン

            foreach (ListViewItem item in nameList.Items)
            {
                for (int i = int.Parse(item.SubItems[(int)Columns.tag].Text); i < 2; i++)
                {
                    IXLWorksheet worksheet;

                    if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

                    // 性別によって参照するシートを変える
                    if (item.SubItems[(int)Columns.gender].Text == "女")
                        worksheet = workbook.Worksheet(1 + i);
                    else
                        worksheet = workbook.Worksheet(3 + i);

                    for (int j = 0; j < 40; j++)
                    {
                        HtmlElement okButton;
                        
                        if (worksheet.Cell(j + 2, 2).Value.ToString() == "") continue;

TagRegister:
                        // タグ登録ページに移動
                        mainBrowser.Navigate(item.SubItems[(int)Columns.url1 + i].Text + "/tag#/" + (j + 1).ToString("D2"));
                        if (WaitLoad()) { StopRegister(); return; }

                        if (SearchElementByInnerText("h1", "エラーが発生しました") != null)
                            continue;

                        // 完全に読み込むまで待機
                        startTime = DateTime.Now;
                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }
                            if ((DateTime.Now - startTime).TotalSeconds >= waitSeconds) goto TagRegister;
                        } while (SearchElementByMix("div", "ng-click", "toggleTag($event, tagId)", "まさか") == null);

                        // タグクリック
                        for (int k = 0; k < 3; k++)
                        {
                            if (worksheet.Cell(j + 2, k + 2).Value.ToString() != "")
                            {
                                string tagName = worksheet.Cell(j + 2, k + 2).Value.ToString();
                                HtmlElement tag = SearchElementByMix("div", "ng-click", "toggleTag($event, tagId)", tagName);

                                if (tag.GetAttribute("className") != "ng-binding ng-scope ExSelected")
                                {
                                    tag.InvokeMember("click");

                                    startTime = DateTime.Now;
                                    do
                                    {
                                        Thread.Sleep(10);
                                        Application.DoEvents();
                                        if (stop) { StopRegister(); return; }
                                        if ((DateTime.Now - startTime).TotalSeconds >= waitSeconds) goto TagRegister;
                                    } while (SearchElementByMix("div", "ng-click", "toggleTag($event, tagId)", tagName).GetAttribute("className") != "ng-binding ng-scope ExSelected");
                                }
                            }
                        }

                        // 保存ボタンクリック
                        SearchElementByInnerText("a", "保存").InvokeMember("click");

                        startTime = DateTime.Now;
                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }
                            if ((DateTime.Now - startTime).TotalSeconds >= waitSeconds) goto TagRegister;
                        } while ((okButton = SearchElementByAttribute("a", "ng-click", "close(true)")) == null);

                        okButton.InvokeMember("click");
                        if (WaitLoad()) { StopRegister(); return; }
                    }

                    // 完了状況保存
                    item.SubItems[(int)Columns.tag].Text = (i + 1).ToString();

                    // 再起動
                    Restart("/tag");
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
        }*/

        private void Request_Click(object sender, EventArgs e)
        {
            int cnt = 0;

            StartRegister();
            if (Login()) { StopRegister(); return; } // ログイン

            foreach (ListViewItem item in nameList.Items)
            {
				for (int i = 0; i < 2; i++)
                {
                    HtmlElement requestButton;
                    HtmlElement agreeCheck;
					string reqComp = item.SubItems[(int)Columns.request].Text;

					if (reqComp == "3" || reqComp == (i + 1).ToString())
						continue;

					if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

                    // アイテム管理ページに移動
                    mainBrowser.Navigate(item.SubItems[(int)Columns.url1 + i].Text);
                    if (WaitLoad()) { StopRegister(); return; }

                    if (SearchElementByInnerText("h1", "エラーが発生しました") != null)
                        continue;

                    // 完全に読み込むまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (SearchElementByInnerText("a", "削除") == null &&
                             SearchElementByInnerText("a", "編集") == null &&
                             SearchElementByInnerText("label", "編集に戻す  ") == null);

                    if ((requestButton = SearchElementByInnerText("label", "リクエスト  ")) == null)
                        continue;

                    if (requestButton.All[0].GetAttribute("disabled") == "disabled")
                        continue;

                    do
                    {
                        // リクエストボタンクリック
                        requestButton.All[0].InvokeMember("click");

                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }
                        } while (mainBrowser.IsBusy || mainBrowser.ReadyState != WebBrowserReadyState.Complete || (agreeCheck = SearchElementByAttribute("input", "ng-model", "photoAgreement")) == null);

                        agreeCheck.InvokeMember("click");

                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }
                        } while (mainBrowser.IsBusy || mainBrowser.ReadyState != WebBrowserReadyState.Complete || SearchElementByAttribute("input", "ng-model", "photoAgreement").GetAttribute("className") != "mdInputCheck ng-dirty ng-valid ng-valid-required");

                        SearchElementByAttribute("a", "ng-click", "submitted = true; photoAgreement && close(true)").InvokeMember("click");
                        if (WaitLoad()) { StopRegister(); return; }

                        // 完全に読み込むまで待機
                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }

                            if (SearchElementByInnerText("h1", "エラーが発生しました") != null)
                            {
                                // アイテム管理ページに移動
                                mainBrowser.Navigate(item.SubItems[(int)Columns.url1 + i].Text);
                                if (WaitLoad()) { StopRegister(); return; }
                            }
                        } while (SearchElementByInnerText("a", "削除") == null &&
                                 SearchElementByInnerText("a", "編集") == null &&
                                 SearchElementByInnerText("label", "編集に戻す  ") == null);
                    } while ((requestButton = SearchElementByInnerText("label", "リクエスト  ")) != null);

					// 完了状況保存
					if (reqComp == "0")
						item.SubItems[(int)Columns.request].Text = (i + 1).ToString();
					else
						item.SubItems[(int)Columns.request].Text = "3";

					// 再起動
					if (++cnt == restartCount.Value)
                        Restart("/req");
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
        }

        private void Release_Click(object sender, EventArgs e)
        {
            int cnt = 0;

            StartRegister();
            if (Login()) { StopRegister(); return; } // ログイン

            foreach (ListViewItem item in nameList.Items)
			{
				for (int i = 0; i < 2; i++)
				{
                    HtmlElement okButton;
                    HtmlElement releaseButton;
					string relComp = item.SubItems[(int)Columns.release].Text;

					if (relComp == "3" || relComp == (i + 1).ToString())
						continue;

					if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

                    // アイテム管理ページに移動
                    mainBrowser.Navigate(item.SubItems[(int)Columns.url1 + i].Text);
                    if (WaitLoad()) { StopRegister(); return; }

                    if (SearchElementByInnerText("h1", "エラーが発生しました") != null)
                        continue;

                    // 完全に読み込むまで待機
                    do
                    {
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    } while (SearchElementByInnerText("a", "削除") == null &&
                             SearchElementByInnerText("a", "編集") == null &&
                             SearchElementByInnerText("label", "編集に戻す  ") == null);

                    if ((releaseButton = SearchElementByInnerText("label", "リリース  ")) == null)
                        continue;

                    do
                    {
                        // リリースボタンクリック
                        releaseButton.All[0].InvokeMember("click");

                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }
                        } while ((okButton = SearchElementByAttribute("a", "ng-click", "close(true)")) == null);

                        okButton.InvokeMember("click");
                        if (WaitLoad()) { StopRegister(); return; }

                        // 完全に読み込むまで待機
                        do
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }

                            if (SearchElementByInnerText("h1", "エラーが発生しました") != null)
                            {
                                // アイテム管理ページに移動
                                mainBrowser.Navigate(item.SubItems[(int)Columns.url1 + i].Text);
                                if (WaitLoad()) { StopRegister(); return; }
                            }
                        } while (SearchElementByInnerText("a", "削除") == null &&
                                 SearchElementByInnerText("a", "編集") == null &&
                                 SearchElementByInnerText("label", "編集に戻す  ") == null);
                    } while ((releaseButton = SearchElementByInnerText("label", "リリース  ")) != null);

					// 完了状況保存
					if (relComp == "0")
						item.SubItems[(int)Columns.release].Text = (i + 1).ToString();
					else
						item.SubItems[(int)Columns.release].Text = "3";

					// 再起動
					if (++cnt == restartCount.Value)
                        Restart("/rel");
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
        }

        private void StampSetting_Click(object sender, EventArgs e)
        {
            var f = new Form2();
            f.ShowDialog(this);
        }

        /*private void TagBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "タグ情報ファイルの選択";
            dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx|すべてのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
                tagFilePath.Text = dialog.FileName;
        }*/

        private void Browse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "名前一覧ファイルの選択";
            dialog.Filter = "Excelファイル (*.xlsx)|*.xlsx|すべてのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nameListFile.Text = dialog.FileName;
                LoadNameList(nameListFile.Text);
            }
        }

        private void NameReload_Click(object sender, EventArgs e)
        {
            LoadNameList(nameListFile.Text);
        }

        private void MainBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            url.Text = mainBrowser.Url.ToString();
            back.Enabled = mainBrowser.CanGoBack;
            forward.Enabled = mainBrowser.CanGoForward;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            mainBrowser.GoBack();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            mainBrowser.GoForward();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            mainBrowser.Refresh();
        }

        private void Url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                mainBrowser.Navigate(url.Text);
            }
        }

        private void MainBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ListViewItem curItem = null;
            int number = 0;
            string stampName;

            foreach (ListViewItem item in nameList.Items)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (item.SubItems[(int)Columns.url1 + i].Text != "" && mainBrowser.Url.ToString().IndexOf(item.SubItems[(int)Columns.url1 + i].Text) != -1)
                    {
                        curItem = item;
                        number = i + 1;
                        break;
                    }
                }
            }

            if (curItem == null) return;

            stampName = curItem.SubItems[(int)Columns.name].Text + curItem.SubItems[(int)Columns.gender].Text + number;

            foreach (HtmlElement element in mainBrowser.Document.GetElementsByTagName("h1"))
            {
                if (element.InnerText == "アイテム管理 ")
                {
                    element.InnerText = "「" + stampName + "」のアイテム管理";
                    break;
                }
            }
        }

        /// <summary>
        /// アイテム管理ページを開く。
        /// </summary>
        /// <param name="number">スタンプの番号</param>
        void OpenStampPage(int number)
        {
            ListViewItem selectedItem = ((ListView)nameMenu.SourceControl).SelectedItems[0];

            // ページに移動
            mainBrowser.Navigate(selectedItem.SubItems[(int)Columns.url1 + (number - 1)].Text);
            WaitLoad();

            if (mainBrowser.Url.ToString() == "https://creator.line.me/ja/")
            {
                Login(); // ログイン
                mainBrowser.Navigate(selectedItem.SubItems[(int)Columns.url1 + (number - 1)].Text);
            }
        }

        private void LoadSetting_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "設定ファイルの読み込み";
            dialog.Filter = "設定ファイル (*.config)|*.config|すべてのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Save();
                File.Copy(dialog.FileName, ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath, true);
                Properties.Settings.Default.Reload();
                LoadProperties();
            }
        }

        private void SaveSetting_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.FileName = "Setting.config";
            dialog.Title = "設定ファイルの書き出し";
            dialog.Filter = "設定ファイル (*.config)|*.config|すべてのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveProperties();
                File.Copy(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath, dialog.FileName, true);
            }
        }

        private void NameMenu_Opening(object sender, CancelEventArgs e)
        {
            if (menuCancel) e.Cancel = true;

            if (nameList.SelectedItems.Count > 0)
            {
                var list = (ListView)nameMenu.SourceControl;

                if (list.SelectedItems[0].SubItems[(int)Columns.url1].Text == "")
                    openStampPage1.Enabled = false;
                else
                    openStampPage1.Enabled = true;

                if (list.SelectedItems[0].SubItems[(int)Columns.url2].Text == "")
                    openStampPage2.Enabled = false;
                else
                    openStampPage2.Enabled = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OpenStampPage1_Click(object sender, EventArgs e)
        {
            OpenStampPage(1);
        }

        private void OpenStampPage2_Click(object sender, EventArgs e)
        {
            OpenStampPage(2);
        }

        /// <summary>
        /// 設定を読み込む。
        /// </summary>
        void LoadProperties()
        {
            split.SplitterDistance = Properties.Settings.Default.SplitWidth;
            this.Bounds = Properties.Settings.Default.Rectangle;
            this.WindowState = Properties.Settings.Default.WindowState;
            mailAddress.Text = Properties.Settings.Default.MailAddress;
            password.Text = Properties.Settings.Default.Password;
            nameListFile.Text = Properties.Settings.Default.NameListFile;
            //tagFilePath.Text = Properties.Settings.Default.TagFilePath;
            restartCount.Value = Properties.Settings.Default.RestartCount;
            LoadNameList(nameListFile.Text);
        }

        /// <summary>
        /// 設定を保存する。
        /// </summary>
        void SaveProperties()
        {
            if (WindowState == FormWindowState.Normal)
                Properties.Settings.Default.Rectangle = this.Bounds;
            else
                Properties.Settings.Default.Rectangle = this.RestoreBounds;

            Properties.Settings.Default.WindowState = WindowState;
            Properties.Settings.Default.SplitWidth = split.SplitterDistance;
            Properties.Settings.Default.MailAddress = mailAddress.Text;
            Properties.Settings.Default.Password = password.Text;
            Properties.Settings.Default.NameListFile = nameListFile.Text;
            //Properties.Settings.Default.TagFilePath = tagFilePath.Text;
            Properties.Settings.Default.RestartCount = restartCount.Value;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// 再起動する。
        /// </summary>
        private void Restart(string arg)
        {
            save = false;

            SaveProperties();
            SaveNameList(nameListFile.Text);

            // 再起動
            Process.Start(Application.ExecutablePath, arg);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProperties();

            if (Environment.GetCommandLineArgs().Length >= 2) { 
                switch (Environment.GetCommandLineArgs()[1])
                {
                    case "/tag":
                        //tagStart.PerformClick();
                        break;

                    case "/req":
                        request.PerformClick();
                        break;

                    case "/rel":
                        release.PerformClick();
                        break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (save) {
                SaveProperties();
                SaveNameList(nameListFile.Text);
            }
        }
	}
}
