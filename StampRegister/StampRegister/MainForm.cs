using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StampRegister.Properties;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StampRegister;

public partial class MainForm : Form
{
    private enum Columns
    {
        Name,
        Roma,
        Gender,
        Honorific,
        Export,
        Stamp,
        Image,
        Request,
        Release,
        Url1,
        Url2,
        Origin,
    }

    const int waitSeconds = 60;

    private bool stop = false;

    private bool menuCancel = false;

    private ChromeDriver driver = default!;

    public MainForm()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 名前のリストを読み込む。
    /// </summary>
    /// <param name="filePath">読み込むファイル名</param>
    private void LoadNameList(string filePath)
    {
        nameList.Items.Clear();

        if (filePath != "")
        {
            FileStream stream;
            IXLWorkbook workbook;

            try
            {
                // Excelファイルを開く
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                workbook = new XLWorkbook(stream);
            }
            catch
            {
                nameListFile.Text = "";
                MessageBox.Show("名前情報ファイルを開けませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (workbook)
            {
                IXLWorksheet worksheet = workbook.Worksheet(1);

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
        }

        releaseCount.Text = CountRelease().ToString();
    }

    /// <summary>
    /// 名前のリストを保存する。
    /// </summary>
    /// <param name="filePath">書き出すファイル名</param>
    private void SaveNameList(string filePath)
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
    private int CountRelease()
    {
        int relNum = 0;

        foreach (ListViewItem item in nameList.Items)
        {
            relNum += 2;
        }

        return relNum;
    }

    /// <summary>
    /// 登録開始の処理をする。
    /// </summary>
    private void StartRegister()
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
    private void StopRegister()
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
    private async Task Login()
    {
        driver.Navigate().GoToUrl("https://creator.line.me/signup/line_auth");
        driver.FindElement(By.TagName("h1"));

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        var loginButtons = driver.FindElements(By.XPath("//button[text()='ログイン']"));
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        if (loginButtons.Count > 0)
        {
            driver.FindElement(By.Name("tid")).SendKeys(mailAddress.Text);
            driver.FindElement(By.Name("tpasswd")).SendKeys(password.Text);
            await Task.Delay(200);

            loginButtons[0].Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            while (driver.FindElements(By.XPath("//span[text()='認証番号で本人確認']")).Count > 0)
            {
                await Task.Delay(100);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//a[text()='マイページ']"));
        }
    }

    private void Escape_Click(object sender, EventArgs e)
    {
        stop = true;
    }

    private async void ExportImages_Click(object sender, EventArgs e)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        StartRegister();

        // フォルダ作成
        if (!Directory.Exists(desktopPath + @"\LINE zip"))
        {
            Directory.CreateDirectory(desktopPath + @"\LINE zip");
        }

        // Illustrator起動
        Illustrator.Application app = new();

        foreach (ListViewItem item in nameList.Items)
        {
            for (int i = int.Parse(item.SubItems[(int)Columns.Export].Text); i < Settings.Default.VerCount; i++)
            {
                string name = item.SubItems[(int)Columns.Name].Text;
                string stampName;
                string pandaName;
                string fileName;

                // パンダの名前
                pandaName = item.SubItems[(int)Columns.Gender].Text == "男" ? Settings.Default.BoyStampName : Settings.Default.GirlStampName;

                // 出力ファイル名
                stampName = pandaName + "-" + name + "-" + (i + 1);

                // 既にzipがあるかどうか
                if (File.Exists(desktopPath + @"\LINE zip\" + stampName + @".zip"))
                {
                    continue;
                }

                // 特殊パターンかどうか
                if (item.SubItems[(int)Columns.Origin].Text != "")
                {
                    fileName = pandaName + "-" + item.SubItems[(int)Columns.Origin].Text + "-" + (i + 1) + ".ai";
                }
                else
                {
                    // ひらがな・カタカナ・漢字判定
                    if (Regex.IsMatch(name, @"^[\p{IsHiragana}\p{IsKatakana}\da-zA-Zａ-ｚＡ-Ｚ～！？★☆●○♪]+$"))
                    {
                        fileName = pandaName + "-ひらがな" + name.Length + "文字-" + (i + 1) + ".ai";
                    }
                    else if (Regex.IsMatch(name, @"^[^\p{IsHiragana}\p{IsKatakana}\da-zA-Zａ-ｚＡ-Ｚ～！？★☆●○♪]+$"))
                    {
                        fileName = pandaName + "-漢字" + name.Length + "文字-" + (i + 1) + ".ai";
                    }
                    else
                    {
                        continue;
                    }
                }

                // Illustratorファイルを開く
                string filePath = Application.StartupPath + @"\スタンプ\" + fileName;

                if (!File.Exists(filePath))
                {
                    continue;
                }

                Illustrator.Document? doc = null;

                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        doc = app.Open(filePath);
                        break;
                    }
                    catch
                    {
                        await Task.Delay(1000);
                    }
                }

                if (doc is null)
                {
                    MessageBox.Show("ファイルを開けませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 名前を置換
                foreach (Illustrator.TextFrame textFrame in doc.TextFrames)
                {
                    textFrame.Contents = textFrame.Contents.Replace("***", name);
                    Application.DoEvents();
                }

                // 書き出し
                doc.Export(desktopPath + @"\a", Illustrator.AiExportType.aiPNG24);
                Application.DoEvents();

                // ファイルを閉じる
                doc.Close(Illustrator.AiSaveOptions.aiDoNotSaveChanges);
                Application.DoEvents();

                // リネーム
                for (int j = 1; j <= 40; j++)
                    File.Move(desktopPath + @"\images\a_" + j.ToString("D2") + ".png", desktopPath + @"\images\" + j.ToString("D2") + ".png");

                File.Move(desktopPath + @"\images\a_41.png", desktopPath + @"\images\main.png");
                File.Move(desktopPath + @"\images\a_42.png", desktopPath + @"\images\tab.png");
                File.Delete(desktopPath + @"\images\a_43.png");
                File.Delete(desktopPath + @"\images\a_44.png");

                // zipに圧縮
                ZipFile.CreateFromDirectory(desktopPath + @"\images", desktopPath + @"\LINE zip\" + stampName + @".zip");
                Directory.Delete(desktopPath + @"\images", true);

                // 完了状況保存
                item.SubItems[(int)Columns.Export].Text = (i + 1).ToString();

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

    private async void Start_Click(object sender, EventArgs e)
    {
        // 販売する国
        string[] countries = Settings.Default.Countries.Split(',');

        StartRegister();
        await Login();
        if (stop) { StopRegister(); return; }

        foreach (ListViewItem item in nameList.Items)
        {
            if (item.SubItems[(int)Columns.Gender].Text != "男" && item.SubItems[(int)Columns.Gender].Text != "女")
            {
                continue;
            }

            for (int i = int.Parse(item.SubItems[(int)Columns.Stamp].Text); i < Settings.Default.VerCount; i++)
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
                switch (item.SubItems[(int)Columns.Honorific].Text)
                {
                    case "くん":
                        if (i == 0)
                        {
                            baseTitle_en = Settings.Default.BoyTitle1_en;
                            baseTitle_jp = Settings.Default.BoyTitle1_jp;
                        }
                        else
                        {
                            baseTitle_en = Settings.Default.BoyTitle2_en;
                            baseTitle_jp = Settings.Default.BoyTitle2_jp;
                        }

                        baseDescription_en = Settings.Default.BoyDescription1_en;
                        baseDescription_jp = Settings.Default.BoyDescription1_jp;
                        baseTaste = Settings.Default.BoyTaste1;
                        baseChara = Settings.Default.BoyChara1;

                        break;

                    case "ちゃん":
                        if (i == 0)
                        {
                            baseTitle_en = Settings.Default.GirlTitle1_en;
                            baseTitle_jp = Settings.Default.GirlTitle1_jp;
                        }
                        else
                        {
                            baseTitle_en = Settings.Default.GirlTitle2_en;
                            baseTitle_jp = Settings.Default.GirlTitle2_jp;
                        }

                        baseDescription_en = Settings.Default.GirlDescription1_en;
                        baseDescription_jp = Settings.Default.GirlDescription1_jp;
                        baseTaste = Settings.Default.GirlTaste1;
                        baseChara = Settings.Default.GirlChara1;

                        break;

                    case "さん":
                        if (item.SubItems[(int)Columns.Gender].Text == "男")
                        {
                            if (i == 0)
                            {
                                baseTitle_en = Settings.Default.SanBoyTitle1_en;
                                baseTitle_jp = Settings.Default.SanBoyTitle1_jp;
                            }
                            else
                            {
                                baseTitle_en = Settings.Default.SanBoyTitle2_en;
                                baseTitle_jp = Settings.Default.SanBoyTitle2_jp;
                            }

                            baseDescription_en = Settings.Default.SanBoyDescription1_en;
                            baseDescription_jp = Settings.Default.SanBoyDescription1_jp;
                            baseTaste = Settings.Default.SanBoyTaste1;
                            baseChara = Settings.Default.SanBoyChara1;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                baseTitle_en = Settings.Default.SanGirlTitle1_en;
                                baseTitle_jp = Settings.Default.SanGirlTitle1_jp;
                            }
                            else
                            {
                                baseTitle_en = Settings.Default.SanGirlTitle2_en;
                                baseTitle_jp = Settings.Default.SanGirlTitle2_jp;
                            }

                            baseDescription_en = Settings.Default.SanGirlDescription1_en;
                            baseDescription_jp = Settings.Default.SanGirlDescription1_jp;
                            baseTaste = Settings.Default.SanGirlTaste1;
                            baseChara = Settings.Default.SanGirlChara1;
                        }

                        break;

                    default:
                        if (item.SubItems[(int)Columns.Gender].Text == "男")
                        {
                            if (i == 0)
                            {
                                baseTitle_en = Settings.Default.AllBoyTitle1_en;
                                baseTitle_jp = Settings.Default.AllBoyTitle1_jp;
                            }
                            else
                            {
                                baseTitle_en = Settings.Default.AllBoyTitle2_en;
                                baseTitle_jp = Settings.Default.AllBoyTitle2_jp;
                            }

                            baseDescription_en = Settings.Default.AllBoyDescription1_en;
                            baseDescription_jp = Settings.Default.AllBoyDescription1_jp;
                            baseTaste = Settings.Default.AllBoyTaste1;
                            baseChara = Settings.Default.AllBoyChara1;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                baseTitle_en = Settings.Default.AllGirlTitle1_en;
                                baseTitle_jp = Settings.Default.AllGirlTitle1_jp;
                            }
                            else
                            {
                                baseTitle_en = Settings.Default.AllGirlTitle2_en;
                                baseTitle_jp = Settings.Default.AllGirlTitle2_jp;
                            }

                            baseDescription_en = Settings.Default.AllGirlDescription1_en;
                            baseDescription_jp = Settings.Default.AllGirlDescription1_jp;
                            baseTaste = Settings.Default.AllGirlTaste1;
                            baseChara = Settings.Default.AllGirlChara1;
                        }

                        break;
                }

                enTitle = baseTitle_en.Replace("***", item.SubItems[(int)Columns.Roma].Text);
                jpTitle = baseTitle_jp.Replace("***", item.SubItems[(int)Columns.Name].Text).Replace("@@@", item.SubItems[(int)Columns.Honorific].Text);
                enDescription = baseDescription_en.Replace("***", item.SubItems[(int)Columns.Roma].Text);
                jpDescription = baseDescription_jp.Replace("***", item.SubItems[(int)Columns.Name].Text).Replace("@@@", item.SubItems[(int)Columns.Honorific].Text);

                try
                {
                    // 登録ページに移動
                    driver.Navigate().GoToUrl(driver.FindElement(By.TagName("base")).GetAttribute("href") + "sticker/create");
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    // 各項目入力
                    driver.FindElement(By.Name("meta[en][title]")).SendKeys(enTitle);
                    driver.FindElement(By.Name("meta[en][description]")).SendKeys(enDescription);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//option[@value='ja']")).Click();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//span[text()='追加']")).Click();
                    await Task.Delay(100);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.Name("meta[ja][title]")).SendKeys(jpTitle);
                    driver.FindElement(By.Name("meta[ja][description]")).SendKeys(jpDescription);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.Name("copyright")).SendKeys(Settings.Default.Copyright);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElements(By.XPath("//select[@data-test='select-style-category']/option"))[baseTaste].Click();
                    await Task.Delay(500);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElements(By.XPath("//select[@data-test='select-character-category']/option"))[baseChara].Click();
                    await Task.Delay(500);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }


                    driver.FindElement(By.XPath("//span[text()='選択したエリアで販売する']/../..//input")).Click();
                    await Task.Delay(500);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    foreach (IWebElement element in driver.FindElements(By.XPath("//details[@class='MdCMN28AreaListItem']")))
                    {
                        element.FindElement(By.TagName("input")).Click();
                        element.Click();
                        await Task.Delay(500);

                        Application.DoEvents();
                        if (stop) { StopRegister(); return; }
                    }

                    foreach (IWebElement element in driver.FindElements(By.XPath("//input[@name='areas[]']")))
                    {
                        if (Array.IndexOf(countries, element.GetAttribute("value")) != -1)
                        {
                            element.Click();
                            await Task.Delay(200);

                            Application.DoEvents();
                            if (stop) { StopRegister(); return; }
                        }
                    }

                    // 自動で販売開始クリック
                    driver.FindElement(By.XPath("//span[text()='自動で販売を開始']/../..//input")).Click();
                    await Task.Delay(500);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    // 保存ボタンクリック
                    driver.FindElement(By.XPath("//form")).Submit();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//p[contains(text(),'保存しますか？')]/../..//span[text()='OK']")).Click();

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//dt[contains(text(),'ステータス')]"));
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }
                }
                catch (Exception ex)
                {
                    StopRegister();
                    MessageBox.Show("エラーが発生しました\n\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // URL&完了状況保存
                item.SubItems[(int)Columns.Url1 + i].Text = driver.Url.Replace("?saved=true", "");
                item.SubItems[(int)Columns.Stamp].Text = (i + 1).ToString();
            }
        }

        StopRegister();
        MessageBox.Show("終了！");
    }

    private async void RegisterImages_Click(object sender, EventArgs e)
    {
        string desptopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        StartRegister();
        await Login();
        if (stop) { StopRegister(); return; } // ログイン

        foreach (ListViewItem item in nameList.Items)
        {
            for (int i = int.Parse(item.SubItems[(int)Columns.Image].Text); i < Settings.Default.VerCount; i++)
            {
                string name = item.SubItems[(int)Columns.Name].Text;
                string pandaName;
                string stampName;

                // パンダの名前
                pandaName = item.SubItems[(int)Columns.Gender].Text == "男" ? Settings.Default.BoyStampName : Settings.Default.GirlStampName;

                // スタンプ名
                stampName = pandaName + "-" + name + "-" + (i + 1);

                if (item.SubItems[(int)Columns.Url1 + i].Text == "")
                {
                    continue;
                }

                // zipファイル存在確認
                if (!File.Exists(desptopPath + @"\LINE zip\" + stampName + ".zip"))
                {
                    MessageBox.Show(stampName + ".zipが存在しません", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                try
                {
                    // ページに移動
                    driver.Navigate().GoToUrl(item.SubItems[(int)Columns.Url1 + i].Text + "/image");
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//option[@value='40']")).Click();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//p[contains(text(),'スタンプの個数を変更します。')]/../..//span[text()='OK']")).Click();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(desptopPath + @"\LINE zip\" + stampName + ".zip");

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    while (driver.FindElements(By.XPath("//input[@class='cm-product-image']")).Count < 42)
                    {
                        await Task.Delay(100);
                    }

                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    File.Delete(desptopPath + @"\LINE zip\" + stampName + ".zip");
                }
                catch (Exception ex)
                {
                    StopRegister();
                    MessageBox.Show("エラーが発生しました\n\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 完了状況保存
                item.SubItems[(int)Columns.Image].Text = (i + 1).ToString();
            }
        }

        StopRegister();
        MessageBox.Show("終了！");
    }

    private async void Request_Click(object sender, EventArgs e)
    {
        StartRegister();
        await Login();
        if (stop) { StopRegister(); return; }

        foreach (ListViewItem item in nameList.Items)
        {
            for (int i = 0; i < Settings.Default.VerCount; i++)
            {
                IWebElement requestButton;
                string reqComp = item.SubItems[(int)Columns.Request].Text;

                if (reqComp == "3" || reqComp == (i + 1).ToString())
                {
                    continue;
                }

                if (item.SubItems[(int)Columns.Url1 + i].Text == "")
                {
                    continue;
                }

                try
                {
                    // アイテム管理ページに移動
                    driver.Navigate().GoToUrl(item.SubItems[(int)Columns.Url1 + i].Text);
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    requestButton = driver.FindElement(By.XPath("//a[text()='リクエスト' and @class='mdBtn']"));

                    if (requestButton.FindElement(By.XPath("..")).GetAttribute("class").IndexOf("ExDisabled") != -1)
                        continue;

                    // リクエストボタンクリック
                    requestButton.Click();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//span[text()='同意します']/..//input")).Click();
                    await Task.Delay(500);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//span[@data-test='dialog-btn-ok']")).Click();

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//span[contains(text(),'審査待ち') and @class='MdStatus_waiting_for_review']"));
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }
                }
                catch (Exception ex)
                {
                    StopRegister();
                    MessageBox.Show("エラーが発生しました\n\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 完了状況保存
                item.SubItems[(int)Columns.Request].Text = reqComp == "0" ? (i + 1).ToString() : "3";
            }
        }

        StopRegister();
        MessageBox.Show("終了！");
    }

    private async void Release_Click(object sender, EventArgs e)
    {
        StartRegister();
        await Login();
        if (stop) { StopRegister(); return; }

        foreach (ListViewItem item in nameList.Items)
        {
            for (int i = 0; i < Settings.Default.VerCount; i++)
            {
                IWebElement releaseButton;
                string relComp = item.SubItems[(int)Columns.Release].Text;

                if (relComp == "3" || relComp == (i + 1).ToString())
                {
                    continue;
                }

                if (item.SubItems[(int)Columns.Url1 + i].Text == "")
                {
                    continue;
                }

                try
                {
                    // アイテム管理ページに移動
                    driver.Navigate().GoToUrl(item.SubItems[(int)Columns.Url1 + i].Text);
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    releaseButton = driver.FindElement(By.XPath("//label[text()='リリース']"));

                    if (releaseButton.FindElement(By.TagName("input")).GetAttribute("disabled") == "disabled")
                        continue;

                    // リリースボタンクリック
                    releaseButton.Click();
                    await Task.Delay(1000);

                    driver.FindElements(By.XPath("//span[@data-test='dialog-btn-ok']"))[2].Click();

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//span[contains(text(),'販売中') and @class='MdStatus_ready_for_sale']"));
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }
                }
                catch (Exception ex)
                {
                    StopRegister();
                    MessageBox.Show("エラーが発生しました\n\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 完了状況保存
                item.SubItems[(int)Columns.Release].Text = relComp == "0" ? (i + 1).ToString() : "3";
            }
        }

        StopRegister();
        MessageBox.Show("終了！");
    }

    private async void Change_Click(object sender, EventArgs e)
    {
        StartRegister();
        await Login();
        if (stop) { StopRegister(); return; }

        foreach (ListViewItem item in nameList.Items)
        {
            for (int i = 0; i < Settings.Default.VerCount; i++)
            {
                int baseTaste;
                int baseChara;

                // 入力文字列識別
                switch (item.SubItems[(int)Columns.Honorific].Text)
                {
                    case "くん":
                        baseTaste = Settings.Default.BoyTaste1;
                        baseChara = Settings.Default.BoyChara1;
                        break;

                    case "ちゃん":
                        baseTaste = Settings.Default.GirlTaste1;
                        baseChara = Settings.Default.GirlChara1;
                        break;

                    case "さん":
                        if (item.SubItems[(int)Columns.Gender].Text == "男")
                        {
                            baseTaste = Settings.Default.SanBoyTaste1;
                            baseChara = Settings.Default.SanBoyChara1;
                        }
                        else
                        {
                            baseTaste = Settings.Default.SanGirlTaste1;
                            baseChara = Settings.Default.SanGirlChara1;
                        }

                        break;

                    default:
                        if (item.SubItems[(int)Columns.Gender].Text == "男")
                        {
                            baseTaste = Settings.Default.AllBoyTaste1;
                            baseChara = Settings.Default.AllBoyChara1;
                        }
                        else
                        {
                            baseTaste = Settings.Default.AllGirlTaste1;
                            baseChara = Settings.Default.AllGirlChara1;
                        }

                        break;
                }

                try
                {
                    // アイテム管理ページに移動
                    driver.Navigate().GoToUrl(item.SubItems[(int)Columns.Url1 + i].Text + "/update");
                    await Task.Delay(3000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElements(By.XPath("//select[@data-test='select-style-category']/option"))[baseTaste].Click();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElements(By.XPath("//select[@data-test='select-character-category']/option"))[baseChara].Click();
                    await Task.Delay(1000);

                    // 保存ボタンクリック
                    driver.FindElement(By.XPath("//main/form")).Submit();
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//p[text()='保存しますか？']/../..//span[text()='OK']")).Click();

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//dt[contains(text(),'ステータス')]"));

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }
                }
                catch (Exception ex)
                {
                    StopRegister();
                    MessageBox.Show("エラーが発生しました\n\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        StopRegister();
        MessageBox.Show("終了！");
    }

    private void StampSetting_Click(object sender, EventArgs e)
    {
        using SettingForm form = new();
        form.ShowDialog(this);
    }

    private void Browse_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new()
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
    private async Task OpenStampPage(int number)
    {
        ListViewItem selectedItem = ((ListView)nameMenu.SourceControl).SelectedItems[0];

        // ページに移動
        driver.Navigate().GoToUrl(selectedItem.SubItems[(int)Columns.Url1 + (number - 1)].Text);

        if (driver.Url.IndexOf("https://access.line.me/") >= 0)
        {
            await Login(); // ログイン
            driver.Navigate().GoToUrl(selectedItem.SubItems[(int)Columns.Url1 + (number - 1)].Text);
        }
    }

    private void LoadSetting_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new()
        {
            Title = "設定ファイルの読み込み",
            Filter = "設定ファイル (*.config)|*.config|すべてのファイル (*.*)|*.*"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            Settings.Default.Save();
            File.Copy(dialog.FileName, ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath, true);
            Settings.Default.Reload();
            LoadProperties();
        }
    }

    private void SaveSetting_Click(object sender, EventArgs e)
    {
        using SaveFileDialog dialog = new()
        {
            FileName = "Setting.config",
            Title = "設定ファイルの書き出し",
            Filter = "設定ファイル (*.config)|*.config|すべてのファイル (*.*)|*.*",
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            SaveProperties();
            File.Copy(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath, dialog.FileName, true);
        }
    }

    private void NameMenu_Opening(object sender, CancelEventArgs e)
    {
        if (menuCancel)
        {
            e.Cancel = true;
            return;
        }

        if (nameList.SelectedItems.Count <= 0)
        {
            e.Cancel = true;
            return;
        }

        var list = (ListView)nameMenu.SourceControl;

        openStampPage1.Enabled = list.SelectedItems[0].SubItems[(int)Columns.Url1].Text != "";
        openStampPage2.Enabled = list.SelectedItems[0].SubItems[(int)Columns.Url2].Text != "";
    }

    private async void OpenStampPage1_Click(object sender, EventArgs e)
    {
        await OpenStampPage(1);
    }

    private async void OpenStampPage2_Click(object sender, EventArgs e)
    {
        await OpenStampPage(2);
    }

    /// <summary>
    /// 設定を読み込む。
    /// </summary>
    void LoadProperties()
    {
        this.Bounds = Settings.Default.Rectangle;
        this.WindowState = Settings.Default.WindowState;
        mailAddress.Text = Settings.Default.MailAddress;
        password.Text = Settings.Default.Password;
        nameListFile.Text = Settings.Default.NameListFile;
        LoadNameList(nameListFile.Text);
    }

    /// <summary>
    /// 設定を保存する。
    /// </summary>
    void SaveProperties()
    {
        Settings.Default.Rectangle = WindowState == FormWindowState.Normal ? this.Bounds : this.RestoreBounds;
        Settings.Default.WindowState = WindowState;
        Settings.Default.MailAddress = mailAddress.Text;
        Settings.Default.Password = password.Text;
        Settings.Default.NameListFile = nameListFile.Text;
        Settings.Default.Save();
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        LoadProperties();

        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        service.HideCommandPromptWindow = true;

        ChromeOptions options = new();
        //options.AddArgument("user-data-dir=" + AppDomain.CurrentDomain.BaseDirectory + "Profile");

        driver = new ChromeDriver(service, options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        await Login();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveProperties();
        SaveNameList(nameListFile.Text);

        driver.Quit();
    }

    private async void AutoDelete_Click(object sender, EventArgs e)
    {
        StartRegister();
        await Login();
        if (stop) { StopRegister(); return; }

        foreach (ListViewItem item in nameList.Items)
        {
            for (int i = int.Parse(item.SubItems[(int)Columns.Stamp].Text) - 1; i >= 0; i--)
            {
                IWebElement deleteButton;

                try
                {
                    // アイテム管理ページに移動
                    driver.Navigate().GoToUrl(item.SubItems[(int)Columns.Url1 + i].Text);
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    deleteButton = driver.FindElement(By.XPath("//label[contains(text(),'削除')]"));

                    // 削除ボタンクリック
                    deleteButton.Click();
                    await Task.Delay(1000);

                    driver.FindElements(By.XPath("//span[@data-test='dialog-btn-ok']"))[2].Click();

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }

                    driver.FindElement(By.XPath("//th[contains(text(),'タイトル')]"));
                    await Task.Delay(1000);

                    Application.DoEvents();
                    if (stop) { StopRegister(); return; }
                }
                catch (Exception ex)
                {
                    StopRegister();
                    MessageBox.Show("エラーが発生しました\n\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 完了状況保存
                item.SubItems[(int)Columns.Export].Text = i.ToString();
                item.SubItems[(int)Columns.Stamp].Text = i.ToString();
                item.SubItems[(int)Columns.Image].Text = i.ToString();
                item.SubItems[(int)Columns.Url1 + i].Text = "";
            }
        }
    }
}
