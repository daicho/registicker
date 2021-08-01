using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
		ChromeDriver driver;

		public Form1()
        {
            InitializeComponent();
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

			releaseCount.Text = CountRelease().ToString();
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
		int CountRelease()
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
            request.Enabled = false;
            release.Enabled = false;
			change.Enabled = false;
			settingFile.Enabled = false;
            loginInfo.Enabled = false;
            stampInfo.Enabled = false;
            nameListFile.Enabled = false;
            browse.Enabled = false;
            nameReload.Enabled = false;
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
			escape.Enabled = false;
			exportImages.Enabled = true;
            start.Enabled = true;
            registerImages.Enabled = true;
            request.Enabled = true;
            release.Enabled = true;
			change.Enabled = true;
            exportImages.Enabled = true;
			settingFile.Enabled = true;
            loginInfo.Enabled = true;
            stampInfo.Enabled = true;
            nameListFile.Enabled = true;
            browse.Enabled = true;
            nameReload.Enabled = true;
            nameMenu.Enabled = true;
        }
        
        /// <summary>
        /// ログインする。
        /// </summary>
        void Login()
        {
			driver.Navigate().GoToUrl("https://creator.line.me/signup/line_auth");
			driver.FindElementByTagName("h1");

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
			var loginButtons = driver.FindElementsByXPath("//button[text()='ログイン']");
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			if (loginButtons.Count > 0)
			{
				driver.FindElementByName("tid").SendKeys(mailAddress.Text);
				driver.FindElementByName("tpasswd").SendKeys(password.Text);
				Thread.Sleep(200);

				loginButtons[0].Click();
				
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
				while (driver.FindElementsByXPath("//span[text()='認証番号で本人確認']").Count > 0)
					;

				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
				driver.FindElementByXPath("//a[text()='マイページ']");
			}
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
                    string stampName;
					string pandaName;
                    string fileName;

					// パンダの名前
					if (item.SubItems[(int)Columns.gender].Text == "男")
						pandaName = Properties.Settings.Default.BoyStampName;
					else
						pandaName = Properties.Settings.Default.GirlStampName;

					// 出力ファイル名
					stampName = pandaName + "-" + name + "-" + (i + 1);

					// 既にzipがあるかどうか
					if (File.Exists(desptopPath + @"\LINE zip\" + stampName + @".zip"))
                        continue;

                    // 特殊パターンかどうか
                    if (item.SubItems[(int)Columns.origin].Text != "")
                    {
                        fileName = pandaName + "-" + item.SubItems[(int)Columns.origin].Text + "-" + (i + 1) + ".ai";
                    }
                    else
                    {
                        // ひらがな・カタカナ・漢字判定
                        if (Regex.IsMatch(name, @"^[\p{IsHiragana}\p{IsKatakana}\da-zA-Zａ-ｚＡ-Ｚ～！？★☆●○♪]+$"))
                            fileName = pandaName + "-ひらがな" + name.Length + "文字-" + (i + 1) + ".ai";
                        else if (Regex.IsMatch(name, @"^[^\p{IsHiragana}\p{IsKatakana}\da-zA-Zａ-ｚＡ-Ｚ～！？★☆●○♪]+$"))
                            fileName = pandaName + "-漢字" + name.Length + "文字-" + (i + 1) + ".ai";
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
			string[] countries = Properties.Settings.Default.Countries.Split(','); // 販売する国

            StartRegister();
			Login();
            if (stop) { StopRegister(); return; }

            foreach (ListViewItem item in nameList.Items)
            {
                if (item.SubItems[(int)Columns.gender].Text != "男" && item.SubItems[(int)Columns.gender].Text != "女") continue;
                
                for (int i = int.Parse(item.SubItems[(int)Columns.stamp].Text); i < 2; i++)
                {
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

					try
					{
						// 登録ページに移動
						driver.Navigate().GoToUrl(driver.FindElementByTagName("base").GetAttribute("href") + "sticker/create");
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						// 各項目入力
						driver.FindElementByName("meta[en][title]").SendKeys(enTitle);
						driver.FindElementByName("meta[en][description]").SendKeys(enDescription);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//option[@value='ja']").Click();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//span[text()='追加']/..").Click();
						Thread.Sleep(100);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByName("meta[ja][title]").SendKeys(jpTitle);
						driver.FindElementByName("meta[ja][description]").SendKeys(jpDescription);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByName("copyright").SendKeys(Properties.Settings.Default.Copyright);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementsByXPath("//select[@data-test='select-style-category']/option")[baseTaste].Click();
						Thread.Sleep(500);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementsByXPath("//select[@data-test='select-character-category']/option")[baseChara].Click();
						Thread.Sleep(500);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }


						driver.FindElementByXPath("//span[text()='選択したエリアで販売する']/../..//input").Click();
						Thread.Sleep(500);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						foreach (IWebElement element in driver.FindElementsByXPath("//div[@class='MdCMN27Collapsable ExCollapsed']"))
						{
							element.FindElement(By.TagName("input")).Click();
							element.Click();
							Thread.Sleep(200);

							Application.DoEvents();
							if (stop) { StopRegister(); return; }
						}

						foreach (IWebElement element in driver.FindElementsByXPath("//input[@name='areas[]']"))
						{
							if (Array.IndexOf(countries, element.GetAttribute("value")) != -1)
							{
								element.Click();
								Thread.Sleep(200);

								Application.DoEvents();
								if (stop) { StopRegister(); return; }
							}
						}

						// 保存ボタンクリック
						driver.FindElementByXPath("//main/form").Submit();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//p[text()='保存しますか？']/../..//span[text()='OK']").Click();

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//dt[contains(text(),'ステータス')]");
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }
					}
					catch
					{
						StopRegister();
						MessageBox.Show("エラーが発生しました", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					// URL&完了状況保存
					item.SubItems[(int)Columns.url1 + i].Text = driver.Url.Replace("?saved=true", "");
                    item.SubItems[(int)Columns.stamp].Text = (i + 1).ToString();
				}
			}
            
            StopRegister();
            MessageBox.Show("終了！");
        }

        private void RegisterImages_Click(object sender, EventArgs e)
        {
            string desptopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			
            StartRegister();
			Login();
			if (stop) { StopRegister(); return; } // ログイン

            foreach (ListViewItem item in nameList.Items)
            {
                for (int i = int.Parse(item.SubItems[(int)Columns.image].Text); i < 2; i++)
                {
					string name = item.SubItems[(int)Columns.name].Text;
					string pandaName;
					string stampName;

					// パンダの名前
					if (item.SubItems[(int)Columns.gender].Text == "男")
						pandaName = Properties.Settings.Default.BoyStampName;
					else
						pandaName = Properties.Settings.Default.GirlStampName;

					// スタンプ名
					stampName = pandaName + "-" + name + "-" + (i + 1);

					if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

                    // zipファイル存在確認
                    if (!File.Exists(desptopPath + @"\LINE zip\" + stampName + ".zip"))
                    {
                        MessageBox.Show(stampName + ".zipが存在しません", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

					try
					{
						// ページに移動
						driver.Navigate().GoToUrl(item.SubItems[(int)Columns.url1 + i].Text + "/image");
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//option[@value='40']").Click();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//p[contains(text(),'スタンプの個数を変更します。')]/../..//a[text()='OK']").Click();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//input[@type='file']").SendKeys(desptopPath + @"\LINE zip\" + stampName + ".zip");

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//div[@ng-if=\"sticker.stickerType !== 'animation' || image.key === 'tab'\"]");
						Thread.Sleep(200);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						File.Delete(desptopPath + @"\LINE zip\" + stampName + ".zip");
					}
					catch
					{
						StopRegister();
						MessageBox.Show("エラーが発生しました", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

                    // 完了状況保存
                    item.SubItems[(int)Columns.image].Text = (i + 1).ToString();
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
        }
        
        private void Request_Click(object sender, EventArgs e)
        {
			StartRegister();
			Login();
			if (stop) { StopRegister(); return; } // ログイン

			foreach (ListViewItem item in nameList.Items)
            {
				for (int i = 0; i < 2; i++)
				{
					IWebElement requestButton;
					string reqComp = item.SubItems[(int)Columns.request].Text;

					if (reqComp == "3" || reqComp == (i + 1).ToString())
						continue;

					if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

					try
					{
						// アイテム管理ページに移動
						driver.Navigate().GoToUrl(item.SubItems[(int)Columns.url1 + i].Text);
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						requestButton = driver.FindElementByXPath("//a[contains(text(),'リクエスト') and @class='mdBtn']");
					
						if (requestButton.FindElement(By.XPath("..")).GetAttribute("class").IndexOf("ExDisabled") != -1)
							continue;

						// リクエストボタンクリック
						requestButton.Click();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//span[text()='同意します']/..//input").Click();
						Thread.Sleep(500);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//span[@data-test='dialog-btn-ok']").Click();

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//dt[contains(text(),'ステータス')]");
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }
					}
					catch
					{
						StopRegister();
						MessageBox.Show("エラーが発生しました", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					// 完了状況保存
					if (reqComp == "0")
						item.SubItems[(int)Columns.request].Text = (i + 1).ToString();
					else
						item.SubItems[(int)Columns.request].Text = "3";
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
        }

        private void Release_Click(object sender, EventArgs e)
		{
			StartRegister();
			Login();
			if (stop) { StopRegister(); return; } // ログイン

			foreach (ListViewItem item in nameList.Items)
			{
				for (int i = 0; i < 2; i++)
				{
					IWebElement releaseButton;
					string relComp = item.SubItems[(int)Columns.release].Text;

					if (relComp == "3" || relComp == (i + 1).ToString())
						continue;

					if (item.SubItems[(int)Columns.url1 + i].Text == "")
                        continue;

					try
					{
						// アイテム管理ページに移動
						driver.Navigate().GoToUrl(item.SubItems[(int)Columns.url1 + i].Text);
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						releaseButton = driver.FindElementByXPath("//label[contains(text(),'リリース')]");

						if (releaseButton.FindElement(By.TagName("input")).GetAttribute("disabled") == "disabled")
							continue;

						// リリースボタンクリック
						releaseButton.Click();
						Thread.Sleep(1000);

						driver.FindElementsByXPath("//span[@data-test='dialog-btn-ok']")[2].Click();

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//dt[contains(text(),'ステータス')]");
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }
					}
					catch
					{
						StopRegister();
						MessageBox.Show("エラーが発生しました", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					
					// 完了状況保存
					if (relComp == "0")
						item.SubItems[(int)Columns.release].Text = (i + 1).ToString();
					else
						item.SubItems[(int)Columns.release].Text = "3";
                }
            }

            StopRegister();
            MessageBox.Show("終了！");
		}

		private void Change_Click(object sender, EventArgs e)
		{
			StartRegister();
			Login();
			if (stop) { StopRegister(); return; }

			foreach (ListViewItem item in nameList.Items)
			{
				for (int i = 0; i < 2; i++)
				{
					int baseTaste;
					int baseChara;
					
					// 入力文字列識別
					switch (item.SubItems[(int)Columns.honorific].Text)
					{
						case "くん":
							baseTaste = Properties.Settings.Default.BoyTaste1;
							baseChara = Properties.Settings.Default.BoyChara1;
							break;

						case "ちゃん":
							baseTaste = Properties.Settings.Default.GirlTaste1;
							baseChara = Properties.Settings.Default.GirlChara1;
							break;

						case "さん":
							if (item.SubItems[(int)Columns.gender].Text == "男")
							{
								baseTaste = Properties.Settings.Default.SanBoyTaste1;
								baseChara = Properties.Settings.Default.SanBoyChara1;
							}
							else
							{
								baseTaste = Properties.Settings.Default.SanGirlTaste1;
								baseChara = Properties.Settings.Default.SanGirlChara1;
							}

							break;

						default:
							if (item.SubItems[(int)Columns.gender].Text == "男")
							{
								baseTaste = Properties.Settings.Default.AllBoyTaste1;
								baseChara = Properties.Settings.Default.AllBoyChara1;
							}
							else
							{
								baseTaste = Properties.Settings.Default.AllGirlTaste1;
								baseChara = Properties.Settings.Default.AllGirlChara1;
							}

							break;
					}

					try
					{
						// アイテム管理ページに移動
						driver.Navigate().GoToUrl(item.SubItems[(int)Columns.url1 + i].Text + "/update");
						Thread.Sleep(3000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementsByXPath("//select[@data-test='select-style-category']/option")[baseTaste].Click();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementsByXPath("//select[@data-test='select-character-category']/option")[baseChara].Click();
						Thread.Sleep(1000);

						// 保存ボタンクリック
						driver.FindElementByXPath("//main/form").Submit();
						Thread.Sleep(1000);

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//p[text()='保存しますか？']/../..//span[text()='OK']").Click();

						Application.DoEvents();
						if (stop) { StopRegister(); return; }

						driver.FindElementByXPath("//dt[contains(text(),'ステータス')]");

						Application.DoEvents();
						if (stop) { StopRegister(); return; }
					}
					catch
					{
						StopRegister();
						MessageBox.Show("エラーが発生しました", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
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

        private void Browse_Click(object sender, EventArgs e)
        {
			var dialog = new OpenFileDialog
			{
				Title = "名前一覧ファイルの選択",
				Filter = "Excelファイル (*.xlsx)|*.xlsx|すべてのファイル (*.*)|*.*"
			};

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

        /// <summary>
        /// アイテム管理ページを開く。
        /// </summary>
        /// <param name="number">スタンプの番号</param>
        void OpenStampPage(int number)
        {
            ListViewItem selectedItem = ((ListView)nameMenu.SourceControl).SelectedItems[0];

            // ページに移動
            driver.Navigate().GoToUrl(selectedItem.SubItems[(int)Columns.url1 + (number - 1)].Text);
			
			if (driver.Url.IndexOf("https://access.line.me/") >= 0)
            {
                Login(); // ログイン
				driver.Navigate().GoToUrl(selectedItem.SubItems[(int)Columns.url1 + (number - 1)].Text);
            }
        }

        private void LoadSetting_Click(object sender, EventArgs e)
        {
			var dialog = new OpenFileDialog
			{
				Title = "設定ファイルの読み込み",
				Filter = "設定ファイル (*.config)|*.config|すべてのファイル (*.*)|*.*"
			};

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
			var dialog = new SaveFileDialog
			{
				FileName = "Setting.config",
				Title = "設定ファイルの書き出し",
				Filter = "設定ファイル (*.config)|*.config|すべてのファイル (*.*)|*.*"
			};

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
            this.Bounds = Properties.Settings.Default.Rectangle;
            this.WindowState = Properties.Settings.Default.WindowState;
            mailAddress.Text = Properties.Settings.Default.MailAddress;
            password.Text = Properties.Settings.Default.Password;
            nameListFile.Text = Properties.Settings.Default.NameListFile;
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
            Properties.Settings.Default.MailAddress = mailAddress.Text;
            Properties.Settings.Default.Password = password.Text;
            Properties.Settings.Default.NameListFile = nameListFile.Text;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProperties();

			ChromeDriverService service = ChromeDriverService.CreateDefaultService();
			service.HideCommandPromptWindow = true;

			ChromeOptions options = new ChromeOptions();
			//options.AddArgument("user-data-dir=" + AppDomain.CurrentDomain.BaseDirectory + "Profile");

			driver = new ChromeDriver(service, options);
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProperties();
            SaveNameList(nameListFile.Text);

			driver.Quit();
        }
	}
}
