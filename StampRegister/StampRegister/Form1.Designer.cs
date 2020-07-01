namespace StampRegister
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.change = new System.Windows.Forms.Button();
			this.escape = new System.Windows.Forms.Button();
			this.exportImages = new System.Windows.Forms.Button();
			this.registerImages = new System.Windows.Forms.Button();
			this.start = new System.Windows.Forms.Button();
			this.request = new System.Windows.Forms.Button();
			this.release = new System.Windows.Forms.Button();
			this.settingFile = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.loadSetting = new System.Windows.Forms.Button();
			this.saveSetting = new System.Windows.Forms.Button();
			this.nameInfo = new System.Windows.Forms.GroupBox();
			this.releaseCount = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nameReload = new System.Windows.Forms.Button();
			this.browse = new System.Windows.Forms.Button();
			this.nameListFile = new System.Windows.Forms.TextBox();
			this.nameList = new System.Windows.Forms.ListView();
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.roma = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.honorific = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.exportComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.stampComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.requestComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.releaseComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.url1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.url2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.origin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nameMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openStampPage1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.openStampPage2 = new System.Windows.Forms.ToolStripMenuItem();
			this.stampInfo = new System.Windows.Forms.GroupBox();
			this.stampSetting = new System.Windows.Forms.Button();
			this.loginInfo = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.mailAddress = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2.SuspendLayout();
			this.settingFile.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.nameInfo.SuspendLayout();
			this.nameMenu.SuspendLayout();
			this.stampInfo.SuspendLayout();
			this.loginInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 7;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28477F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28477F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28477F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.2887F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28442F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28442F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28816F));
			this.tableLayoutPanel2.Controls.Add(this.change, 6, 0);
			this.tableLayoutPanel2.Controls.Add(this.escape, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.exportImages, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.registerImages, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.start, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.request, 4, 0);
			this.tableLayoutPanel2.Controls.Add(this.release, 5, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 700);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(627, 29);
			this.tableLayoutPanel2.TabIndex = 18;
			// 
			// change
			// 
			this.change.Dock = System.Windows.Forms.DockStyle.Fill;
			this.change.Location = new System.Drawing.Point(537, 0);
			this.change.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.change.Name = "change";
			this.change.Size = new System.Drawing.Size(87, 29);
			this.change.TabIndex = 22;
			this.change.Text = "修正";
			this.change.UseVisualStyleBackColor = true;
			this.change.Click += new System.EventHandler(this.Change_Click);
			// 
			// escape
			// 
			this.escape.Dock = System.Windows.Forms.DockStyle.Fill;
			this.escape.Enabled = false;
			this.escape.Location = new System.Drawing.Point(3, 0);
			this.escape.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.escape.Name = "escape";
			this.escape.Size = new System.Drawing.Size(83, 29);
			this.escape.TabIndex = 16;
			this.escape.Text = "中断";
			this.escape.UseVisualStyleBackColor = true;
			this.escape.Click += new System.EventHandler(this.Escape_Click);
			// 
			// exportImages
			// 
			this.exportImages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.exportImages.Location = new System.Drawing.Point(92, 0);
			this.exportImages.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.exportImages.Name = "exportImages";
			this.exportImages.Size = new System.Drawing.Size(83, 29);
			this.exportImages.TabIndex = 16;
			this.exportImages.Text = "画像出力";
			this.exportImages.UseVisualStyleBackColor = true;
			this.exportImages.Click += new System.EventHandler(this.ExportImages_Click);
			// 
			// registerImages
			// 
			this.registerImages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.registerImages.Location = new System.Drawing.Point(270, 0);
			this.registerImages.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.registerImages.Name = "registerImages";
			this.registerImages.Size = new System.Drawing.Size(83, 29);
			this.registerImages.TabIndex = 20;
			this.registerImages.Text = "画像登録";
			this.registerImages.UseVisualStyleBackColor = true;
			this.registerImages.Click += new System.EventHandler(this.RegisterImages_Click);
			// 
			// start
			// 
			this.start.Dock = System.Windows.Forms.DockStyle.Fill;
			this.start.Location = new System.Drawing.Point(181, 0);
			this.start.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(83, 29);
			this.start.TabIndex = 17;
			this.start.Text = "新規登録";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.Start_Click);
			// 
			// request
			// 
			this.request.Dock = System.Windows.Forms.DockStyle.Fill;
			this.request.Location = new System.Drawing.Point(359, 0);
			this.request.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.request.Name = "request";
			this.request.Size = new System.Drawing.Size(83, 29);
			this.request.TabIndex = 21;
			this.request.Text = "リクエスト";
			this.request.UseVisualStyleBackColor = true;
			this.request.Click += new System.EventHandler(this.Request_Click);
			// 
			// release
			// 
			this.release.Dock = System.Windows.Forms.DockStyle.Fill;
			this.release.Location = new System.Drawing.Point(448, 0);
			this.release.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.release.Name = "release";
			this.release.Size = new System.Drawing.Size(83, 29);
			this.release.TabIndex = 19;
			this.release.Text = "リリース";
			this.release.UseVisualStyleBackColor = true;
			this.release.Click += new System.EventHandler(this.Release_Click);
			// 
			// settingFile
			// 
			this.settingFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.settingFile.Controls.Add(this.tableLayoutPanel1);
			this.settingFile.Location = new System.Drawing.Point(16, 15);
			this.settingFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.settingFile.Name = "settingFile";
			this.settingFile.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.settingFile.Size = new System.Drawing.Size(627, 59);
			this.settingFile.TabIndex = 0;
			this.settingFile.TabStop = false;
			this.settingFile.Text = "設定ファイル";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.loadSetting, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.saveSetting, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 22);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(611, 29);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// loadSetting
			// 
			this.loadSetting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loadSetting.Location = new System.Drawing.Point(0, 0);
			this.loadSetting.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.loadSetting.Name = "loadSetting";
			this.loadSetting.Size = new System.Drawing.Size(302, 29);
			this.loadSetting.TabIndex = 1;
			this.loadSetting.Text = "読み込み";
			this.loadSetting.UseVisualStyleBackColor = true;
			this.loadSetting.Click += new System.EventHandler(this.LoadSetting_Click);
			// 
			// saveSetting
			// 
			this.saveSetting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.saveSetting.Location = new System.Drawing.Point(308, 0);
			this.saveSetting.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.saveSetting.Name = "saveSetting";
			this.saveSetting.Size = new System.Drawing.Size(303, 29);
			this.saveSetting.TabIndex = 2;
			this.saveSetting.Text = "書き出し";
			this.saveSetting.UseVisualStyleBackColor = true;
			this.saveSetting.Click += new System.EventHandler(this.SaveSetting_Click);
			// 
			// nameInfo
			// 
			this.nameInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameInfo.Controls.Add(this.releaseCount);
			this.nameInfo.Controls.Add(this.label3);
			this.nameInfo.Controls.Add(this.nameReload);
			this.nameInfo.Controls.Add(this.browse);
			this.nameInfo.Controls.Add(this.nameListFile);
			this.nameInfo.Controls.Add(this.nameList);
			this.nameInfo.Location = new System.Drawing.Point(16, 240);
			this.nameInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.nameInfo.Name = "nameInfo";
			this.nameInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.nameInfo.Size = new System.Drawing.Size(627, 452);
			this.nameInfo.TabIndex = 12;
			this.nameInfo.TabStop = false;
			this.nameInfo.Text = "名前情報";
			// 
			// releaseCount
			// 
			this.releaseCount.AutoSize = true;
			this.releaseCount.Location = new System.Drawing.Point(65, 60);
			this.releaseCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.releaseCount.Name = "releaseCount";
			this.releaseCount.Size = new System.Drawing.Size(15, 15);
			this.releaseCount.TabIndex = 18;
			this.releaseCount.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 60);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 17;
			this.label3.Text = "リリース : ";
			// 
			// nameReload
			// 
			this.nameReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nameReload.Location = new System.Drawing.Point(591, 22);
			this.nameReload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.nameReload.Name = "nameReload";
			this.nameReload.Size = new System.Drawing.Size(28, 26);
			this.nameReload.TabIndex = 16;
			this.nameReload.Text = "再";
			this.nameReload.UseVisualStyleBackColor = true;
			this.nameReload.Click += new System.EventHandler(this.NameReload_Click);
			// 
			// browse
			// 
			this.browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browse.Location = new System.Drawing.Point(560, 22);
			this.browse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(28, 26);
			this.browse.TabIndex = 14;
			this.browse.Text = "...";
			this.browse.UseVisualStyleBackColor = true;
			this.browse.Click += new System.EventHandler(this.Browse_Click);
			// 
			// nameListFile
			// 
			this.nameListFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameListFile.Location = new System.Drawing.Point(8, 24);
			this.nameListFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.nameListFile.Name = "nameListFile";
			this.nameListFile.ReadOnly = true;
			this.nameListFile.Size = new System.Drawing.Size(547, 22);
			this.nameListFile.TabIndex = 13;
			// 
			// nameList
			// 
			this.nameList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.roma,
            this.gender,
            this.honorific,
            this.exportComp,
            this.stampComp,
            this.imageComp,
            this.requestComp,
            this.releaseComp,
            this.url1,
            this.url2,
            this.origin});
			this.nameList.ContextMenuStrip = this.nameMenu;
			this.nameList.FullRowSelect = true;
			this.nameList.GridLines = true;
			this.nameList.HideSelection = false;
			this.nameList.Location = new System.Drawing.Point(8, 79);
			this.nameList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.nameList.MultiSelect = false;
			this.nameList.Name = "nameList";
			this.nameList.Size = new System.Drawing.Size(609, 365);
			this.nameList.TabIndex = 15;
			this.nameList.UseCompatibleStateImageBehavior = false;
			this.nameList.View = System.Windows.Forms.View.Details;
			// 
			// name
			// 
			this.name.Text = "名前";
			this.name.Width = 52;
			// 
			// roma
			// 
			this.roma.Text = "ローマ字";
			this.roma.Width = 52;
			// 
			// gender
			// 
			this.gender.Text = "性別";
			this.gender.Width = 25;
			// 
			// honorific
			// 
			this.honorific.Text = "敬称";
			this.honorific.Width = 40;
			// 
			// exportComp
			// 
			this.exportComp.Text = "出";
			this.exportComp.Width = 25;
			// 
			// stampComp
			// 
			this.stampComp.Text = "登";
			this.stampComp.Width = 25;
			// 
			// imageComp
			// 
			this.imageComp.Text = "画";
			this.imageComp.Width = 25;
			// 
			// requestComp
			// 
			this.requestComp.Text = "ﾘｸ";
			this.requestComp.Width = 25;
			// 
			// releaseComp
			// 
			this.releaseComp.Text = "ﾘﾘ";
			this.releaseComp.Width = 25;
			// 
			// url1
			// 
			this.url1.Text = "URL1";
			// 
			// url2
			// 
			this.url2.Text = "URL2";
			// 
			// origin
			// 
			this.origin.Text = ".ai";
			this.origin.Width = 40;
			// 
			// nameMenu
			// 
			this.nameMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.nameMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStampPage1,
            this.toolStripSeparator1,
            this.openStampPage2});
			this.nameMenu.Name = "nameMenu";
			this.nameMenu.Size = new System.Drawing.Size(276, 58);
			this.nameMenu.Opening += new System.ComponentModel.CancelEventHandler(this.NameMenu_Opening);
			// 
			// openStampPage1
			// 
			this.openStampPage1.Name = "openStampPage1";
			this.openStampPage1.Size = new System.Drawing.Size(275, 24);
			this.openStampPage1.Text = "[ver.1]のアイテム管理ページを開く";
			this.openStampPage1.Click += new System.EventHandler(this.OpenStampPage1_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(272, 6);
			// 
			// openStampPage2
			// 
			this.openStampPage2.Name = "openStampPage2";
			this.openStampPage2.Size = new System.Drawing.Size(275, 24);
			this.openStampPage2.Text = "[ver.2]のアイテム管理ページを開く";
			this.openStampPage2.Click += new System.EventHandler(this.OpenStampPage2_Click);
			// 
			// stampInfo
			// 
			this.stampInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stampInfo.Controls.Add(this.stampSetting);
			this.stampInfo.Location = new System.Drawing.Point(16, 174);
			this.stampInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.stampInfo.Name = "stampInfo";
			this.stampInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.stampInfo.Size = new System.Drawing.Size(627, 59);
			this.stampInfo.TabIndex = 8;
			this.stampInfo.TabStop = false;
			this.stampInfo.Text = "スタンプ情報";
			// 
			// stampSetting
			// 
			this.stampSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stampSetting.Location = new System.Drawing.Point(8, 22);
			this.stampSetting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.stampSetting.Name = "stampSetting";
			this.stampSetting.Size = new System.Drawing.Size(611, 29);
			this.stampSetting.TabIndex = 9;
			this.stampSetting.Text = "スタンプ詳細設定";
			this.stampSetting.UseVisualStyleBackColor = true;
			this.stampSetting.Click += new System.EventHandler(this.StampSetting_Click);
			// 
			// loginInfo
			// 
			this.loginInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.loginInfo.Controls.Add(this.label1);
			this.loginInfo.Controls.Add(this.label2);
			this.loginInfo.Controls.Add(this.password);
			this.loginInfo.Controls.Add(this.mailAddress);
			this.loginInfo.Location = new System.Drawing.Point(16, 81);
			this.loginInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.loginInfo.Name = "loginInfo";
			this.loginInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.loginInfo.Size = new System.Drawing.Size(627, 85);
			this.loginInfo.TabIndex = 3;
			this.loginInfo.TabStop = false;
			this.loginInfo.Text = "ログイン情報";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 26);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "メールアドレス";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 58);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "パスワード";
			// 
			// password
			// 
			this.password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.password.Location = new System.Drawing.Point(108, 54);
			this.password.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.password.Name = "password";
			this.password.Size = new System.Drawing.Size(509, 22);
			this.password.TabIndex = 7;
			this.password.UseSystemPasswordChar = true;
			// 
			// mailAddress
			// 
			this.mailAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mailAddress.Location = new System.Drawing.Point(108, 22);
			this.mailAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.mailAddress.Name = "mailAddress";
			this.mailAddress.Size = new System.Drawing.Size(509, 22);
			this.mailAddress.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 744);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.settingFile);
			this.Controls.Add(this.nameInfo);
			this.Controls.Add(this.stampInfo);
			this.Controls.Add(this.loginInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Form1";
			this.Text = "StampRegister";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.settingFile.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.nameInfo.ResumeLayout(false);
			this.nameInfo.PerformLayout();
			this.nameMenu.ResumeLayout(false);
			this.stampInfo.ResumeLayout(false);
			this.loginInfo.ResumeLayout(false);
			this.loginInfo.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mailAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox loginInfo;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.GroupBox stampInfo;
        private System.Windows.Forms.Button stampSetting;
        private System.Windows.Forms.GroupBox nameInfo;
        private System.Windows.Forms.ListView nameList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader roma;
        private System.Windows.Forms.ColumnHeader stampComp;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.TextBox nameListFile;
        private System.Windows.Forms.ColumnHeader gender;
        private System.Windows.Forms.Button escape;
        private System.Windows.Forms.GroupBox settingFile;
        private System.Windows.Forms.Button saveSetting;
        private System.Windows.Forms.Button loadSetting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip nameMenu;
        private System.Windows.Forms.ColumnHeader url1;
        private System.Windows.Forms.ColumnHeader url2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ColumnHeader honorific;
        private System.Windows.Forms.ToolStripMenuItem openStampPage1;
        private System.Windows.Forms.ToolStripMenuItem openStampPage2;
        private System.Windows.Forms.Button exportImages;
        private System.Windows.Forms.ColumnHeader exportComp;
        private System.Windows.Forms.ColumnHeader origin;
        private System.Windows.Forms.Button release;
        private System.Windows.Forms.Button registerImages;
        private System.Windows.Forms.ColumnHeader imageComp;
        private System.Windows.Forms.ColumnHeader releaseComp;
        private System.Windows.Forms.Button request;
        private System.Windows.Forms.ColumnHeader requestComp;
        private System.Windows.Forms.Button nameReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label releaseCount;
		private System.Windows.Forms.Button change;
	}
}

