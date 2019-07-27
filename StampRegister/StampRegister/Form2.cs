using System;
using System.Windows.Forms;

namespace StampRegister
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                Properties.Settings.Default.SettingRectangle = this.Bounds;
            else
                Properties.Settings.Default.SettingRectangle = this.RestoreBounds;

			Properties.Settings.Default.BoyStampName = boyStampName.Text;
			Properties.Settings.Default.GirlStampName = girlStampName.Text;
			Properties.Settings.Default.Copyright = copyright.Text;
			Properties.Settings.Default.Countries = countries.Text;

			Properties.Settings.Default.BoyTitle1_en = boyTitle1_en.Text;
            Properties.Settings.Default.BoyTitle1_jp = boyTitle1_jp.Text;
            Properties.Settings.Default.BoyTitle2_en = boyTitle2_en.Text;
            Properties.Settings.Default.BoyTitle2_jp = boyTitle2_jp.Text;
            Properties.Settings.Default.BoyDescription1_en = boyDescription1_en.Text;
            Properties.Settings.Default.BoyDescription1_jp = boyDescription1_jp.Text;
            Properties.Settings.Default.BoyTaste1 = boyTaste1.SelectedIndex;
            Properties.Settings.Default.BoyChara1 = boyChara1.SelectedIndex;

            Properties.Settings.Default.GirlTitle1_en = girlTitle1_en.Text;
            Properties.Settings.Default.GirlTitle1_jp = girlTitle1_jp.Text;
            Properties.Settings.Default.GirlTitle2_en = girlTitle2_en.Text;
            Properties.Settings.Default.GirlTitle2_jp = girlTitle2_jp.Text;
            Properties.Settings.Default.GirlDescription1_en = girlDescription1_en.Text;
            Properties.Settings.Default.GirlDescription1_jp = girlDescription1_jp.Text;
            Properties.Settings.Default.GirlTaste1 = girlTaste1.SelectedIndex;
            Properties.Settings.Default.GirlChara1 = girlChara1.SelectedIndex;

            Properties.Settings.Default.SanBoyTitle1_en = sanBoyTitle1_en.Text;
            Properties.Settings.Default.SanBoyTitle1_jp = sanBoyTitle1_jp.Text;
            Properties.Settings.Default.SanBoyTitle2_en = sanBoyTitle2_en.Text;
            Properties.Settings.Default.SanBoyTitle2_jp = sanBoyTitle2_jp.Text;
            Properties.Settings.Default.SanBoyDescription1_en = sanBoyDescription1_en.Text;
            Properties.Settings.Default.SanBoyDescription1_jp = sanBoyDescription1_jp.Text;
            Properties.Settings.Default.SanBoyTaste1 = sanBoyTaste1.SelectedIndex;
            Properties.Settings.Default.SanBoyChara1 = sanBoyChara1.SelectedIndex;

            Properties.Settings.Default.SanGirlTitle1_en = sanGirlTitle1_en.Text;
            Properties.Settings.Default.SanGirlTitle1_jp = sanGirlTitle1_jp.Text;
            Properties.Settings.Default.SanGirlTitle2_en = sanGirlTitle2_en.Text;
            Properties.Settings.Default.SanGirlTitle2_jp = sanGirlTitle2_jp.Text;
            Properties.Settings.Default.SanGirlDescription1_en = sanGirlDescription1_en.Text;
            Properties.Settings.Default.SanGirlDescription1_jp = sanGirlDescription1_jp.Text;
            Properties.Settings.Default.SanGirlTaste1 = sanGirlTaste1.SelectedIndex;
            Properties.Settings.Default.SanGirlChara1 = sanGirlChara1.SelectedIndex;

            Properties.Settings.Default.AllBoyTitle1_en = allBoyTitle1_en.Text;
            Properties.Settings.Default.AllBoyTitle1_jp = allBoyTitle1_jp.Text;
            Properties.Settings.Default.AllBoyTitle2_en = allBoyTitle2_en.Text;
            Properties.Settings.Default.AllBoyTitle2_jp = allBoyTitle2_jp.Text;
            Properties.Settings.Default.AllBoyDescription1_en = allBoyDescription1_en.Text;
            Properties.Settings.Default.AllBoyDescription1_jp = allBoyDescription1_jp.Text;
            Properties.Settings.Default.AllBoyTaste1 = allBoyTaste1.SelectedIndex;
            Properties.Settings.Default.AllBoyChara1 = allBoyChara1.SelectedIndex;

            Properties.Settings.Default.AllGirlTitle1_en = allGirlTitle1_en.Text;
            Properties.Settings.Default.AllGirlTitle1_jp = allGirlTitle1_jp.Text;
            Properties.Settings.Default.AllGirlTitle2_en = allGirlTitle2_en.Text;
            Properties.Settings.Default.AllGirlTitle2_jp = allGirlTitle2_jp.Text;
            Properties.Settings.Default.AllGirlDescription1_en = allGirlDescription1_en.Text;
            Properties.Settings.Default.AllGirlDescription1_jp = allGirlDescription1_jp.Text;
            Properties.Settings.Default.AllGirlTaste1 = allGirlTaste1.SelectedIndex;
            Properties.Settings.Default.AllGirlChara1 = allGirlChara1.SelectedIndex;
            
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                Properties.Settings.Default.SettingRectangle = this.Bounds;
            else
                Properties.Settings.Default.SettingRectangle = this.RestoreBounds;

            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Bounds = Properties.Settings.Default.SettingRectangle;

			boyStampName.Text = Properties.Settings.Default.BoyStampName;
			girlStampName.Text = Properties.Settings.Default.GirlStampName;
			copyright.Text = Properties.Settings.Default.Copyright;
			countries.Text = Properties.Settings.Default.Countries;

			boyTitle1_en.Text = Properties.Settings.Default.BoyTitle1_en;
            boyTitle1_jp.Text = Properties.Settings.Default.BoyTitle1_jp;
            boyTitle2_en.Text = Properties.Settings.Default.BoyTitle2_en;
            boyTitle2_jp.Text = Properties.Settings.Default.BoyTitle2_jp;
            boyDescription1_en.Text = Properties.Settings.Default.BoyDescription1_en;
            boyDescription1_jp.Text = Properties.Settings.Default.BoyDescription1_jp;
            boyTaste1.SelectedIndex = Properties.Settings.Default.BoyTaste1;
            boyChara1.SelectedIndex = Properties.Settings.Default.BoyChara1;

            girlTitle1_en.Text = Properties.Settings.Default.GirlTitle1_en;
            girlTitle1_jp.Text = Properties.Settings.Default.GirlTitle1_jp;
            girlTitle2_en.Text = Properties.Settings.Default.GirlTitle2_en;
            girlTitle2_jp.Text = Properties.Settings.Default.GirlTitle2_jp;
            girlDescription1_en.Text = Properties.Settings.Default.GirlDescription1_en;
            girlDescription1_jp.Text = Properties.Settings.Default.GirlDescription1_jp;
            girlTaste1.SelectedIndex = Properties.Settings.Default.GirlTaste1;
            girlChara1.SelectedIndex = Properties.Settings.Default.GirlChara1;

            sanBoyTitle1_en.Text = Properties.Settings.Default.SanBoyTitle1_en;
            sanBoyTitle1_jp.Text = Properties.Settings.Default.SanBoyTitle1_jp;
            sanBoyTitle2_en.Text = Properties.Settings.Default.SanBoyTitle2_en;
            sanBoyTitle2_jp.Text = Properties.Settings.Default.SanBoyTitle2_jp;
            sanBoyDescription1_en.Text = Properties.Settings.Default.SanBoyDescription1_en;
            sanBoyDescription1_jp.Text = Properties.Settings.Default.SanBoyDescription1_jp;
            sanBoyTaste1.SelectedIndex = Properties.Settings.Default.SanBoyTaste1;
            sanBoyChara1.SelectedIndex = Properties.Settings.Default.SanBoyChara1;

            sanGirlTitle1_en.Text = Properties.Settings.Default.SanGirlTitle1_en;
            sanGirlTitle1_jp.Text = Properties.Settings.Default.SanGirlTitle1_jp;
            sanGirlTitle2_en.Text = Properties.Settings.Default.SanGirlTitle2_en;
            sanGirlTitle2_jp.Text = Properties.Settings.Default.SanGirlTitle2_jp;
            sanGirlDescription1_en.Text = Properties.Settings.Default.SanGirlDescription1_en;
            sanGirlDescription1_jp.Text = Properties.Settings.Default.SanGirlDescription1_jp;
            sanGirlTaste1.SelectedIndex = Properties.Settings.Default.SanGirlTaste1;
            sanGirlChara1.SelectedIndex = Properties.Settings.Default.SanGirlChara1;

            allBoyTitle1_en.Text = Properties.Settings.Default.AllBoyTitle1_en;
            allBoyTitle1_jp.Text = Properties.Settings.Default.AllBoyTitle1_jp;
            allBoyTitle2_en.Text = Properties.Settings.Default.AllBoyTitle2_en;
            allBoyTitle2_jp.Text = Properties.Settings.Default.AllBoyTitle2_jp;
            allBoyDescription1_en.Text = Properties.Settings.Default.AllBoyDescription1_en;
            allBoyDescription1_jp.Text = Properties.Settings.Default.AllBoyDescription1_jp;
            allBoyTaste1.SelectedIndex = Properties.Settings.Default.AllBoyTaste1;
            allBoyChara1.SelectedIndex = Properties.Settings.Default.AllBoyChara1;

            allGirlTitle1_en.Text = Properties.Settings.Default.AllGirlTitle1_en;
            allGirlTitle1_jp.Text = Properties.Settings.Default.AllGirlTitle1_jp;
            allGirlTitle2_en.Text = Properties.Settings.Default.AllGirlTitle2_en;
            allGirlTitle2_jp.Text = Properties.Settings.Default.AllGirlTitle2_jp;
            allGirlDescription1_en.Text = Properties.Settings.Default.AllGirlDescription1_en;
            allGirlDescription1_jp.Text = Properties.Settings.Default.AllGirlDescription1_jp;
            allGirlTaste1.SelectedIndex = Properties.Settings.Default.AllGirlTaste1;
            allGirlChara1.SelectedIndex = Properties.Settings.Default.AllGirlChara1;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                Properties.Settings.Default.SettingRectangle = this.Bounds;
            else
                Properties.Settings.Default.SettingRectangle = this.RestoreBounds;
        }
    }
}
