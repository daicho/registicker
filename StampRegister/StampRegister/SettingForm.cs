using StampRegister.Properties;
using System;
using System.Windows.Forms;

namespace StampRegister;

public partial class SettingForm : Form
{
    public SettingForm()
    {
        InitializeComponent();
    }

    private void ok_Click(object sender, EventArgs e)
    {
        Settings.Default.SettingRectangle = WindowState == FormWindowState.Normal ? this.Bounds : this.RestoreBounds;

        Settings.Default.BoyStampName = boyStampName.Text;
        Settings.Default.GirlStampName = girlStampName.Text;
        Settings.Default.Copyright = copyright.Text;
        Settings.Default.Countries = countries.Text;

        Settings.Default.BoyTitle1_en = boyTitle1_en.Text;
        Settings.Default.BoyTitle1_jp = boyTitle1_jp.Text;
        Settings.Default.BoyTitle2_en = boyTitle2_en.Text;
        Settings.Default.BoyTitle2_jp = boyTitle2_jp.Text;
        Settings.Default.BoyDescription1_en = boyDescription1_en.Text;
        Settings.Default.BoyDescription1_jp = boyDescription1_jp.Text;
        Settings.Default.BoyTaste1 = boyTaste1.SelectedIndex;
        Settings.Default.BoyChara1 = boyChara1.SelectedIndex;

        Settings.Default.GirlTitle1_en = girlTitle1_en.Text;
        Settings.Default.GirlTitle1_jp = girlTitle1_jp.Text;
        Settings.Default.GirlTitle2_en = girlTitle2_en.Text;
        Settings.Default.GirlTitle2_jp = girlTitle2_jp.Text;
        Settings.Default.GirlDescription1_en = girlDescription1_en.Text;
        Settings.Default.GirlDescription1_jp = girlDescription1_jp.Text;
        Settings.Default.GirlTaste1 = girlTaste1.SelectedIndex;
        Settings.Default.GirlChara1 = girlChara1.SelectedIndex;

        Settings.Default.SanBoyTitle1_en = sanBoyTitle1_en.Text;
        Settings.Default.SanBoyTitle1_jp = sanBoyTitle1_jp.Text;
        Settings.Default.SanBoyTitle2_en = sanBoyTitle2_en.Text;
        Settings.Default.SanBoyTitle2_jp = sanBoyTitle2_jp.Text;
        Settings.Default.SanBoyDescription1_en = sanBoyDescription1_en.Text;
        Settings.Default.SanBoyDescription1_jp = sanBoyDescription1_jp.Text;
        Settings.Default.SanBoyTaste1 = sanBoyTaste1.SelectedIndex;
        Settings.Default.SanBoyChara1 = sanBoyChara1.SelectedIndex;

        Settings.Default.SanGirlTitle1_en = sanGirlTitle1_en.Text;
        Settings.Default.SanGirlTitle1_jp = sanGirlTitle1_jp.Text;
        Settings.Default.SanGirlTitle2_en = sanGirlTitle2_en.Text;
        Settings.Default.SanGirlTitle2_jp = sanGirlTitle2_jp.Text;
        Settings.Default.SanGirlDescription1_en = sanGirlDescription1_en.Text;
        Settings.Default.SanGirlDescription1_jp = sanGirlDescription1_jp.Text;
        Settings.Default.SanGirlTaste1 = sanGirlTaste1.SelectedIndex;
        Settings.Default.SanGirlChara1 = sanGirlChara1.SelectedIndex;

        Settings.Default.AllBoyTitle1_en = allBoyTitle1_en.Text;
        Settings.Default.AllBoyTitle1_jp = allBoyTitle1_jp.Text;
        Settings.Default.AllBoyTitle2_en = allBoyTitle2_en.Text;
        Settings.Default.AllBoyTitle2_jp = allBoyTitle2_jp.Text;
        Settings.Default.AllBoyDescription1_en = allBoyDescription1_en.Text;
        Settings.Default.AllBoyDescription1_jp = allBoyDescription1_jp.Text;
        Settings.Default.AllBoyTaste1 = allBoyTaste1.SelectedIndex;
        Settings.Default.AllBoyChara1 = allBoyChara1.SelectedIndex;

        Settings.Default.AllGirlTitle1_en = allGirlTitle1_en.Text;
        Settings.Default.AllGirlTitle1_jp = allGirlTitle1_jp.Text;
        Settings.Default.AllGirlTitle2_en = allGirlTitle2_en.Text;
        Settings.Default.AllGirlTitle2_jp = allGirlTitle2_jp.Text;
        Settings.Default.AllGirlDescription1_en = allGirlDescription1_en.Text;
        Settings.Default.AllGirlDescription1_jp = allGirlDescription1_jp.Text;
        Settings.Default.AllGirlTaste1 = allGirlTaste1.SelectedIndex;
        Settings.Default.AllGirlChara1 = allGirlChara1.SelectedIndex;

        Settings.Default.Save();

        this.Close();
    }

    private void cancel_Click(object sender, EventArgs e)
    {
        Settings.Default.SettingRectangle = WindowState == FormWindowState.Normal ? this.Bounds : this.RestoreBounds;

        this.Close();
    }

    private void SettingForm_Load(object sender, EventArgs e)
    {
        this.Bounds = Settings.Default.SettingRectangle;

        boyStampName.Text = Settings.Default.BoyStampName;
        girlStampName.Text = Settings.Default.GirlStampName;
        copyright.Text = Settings.Default.Copyright;
        countries.Text = Settings.Default.Countries;

        boyTitle1_en.Text = Settings.Default.BoyTitle1_en;
        boyTitle1_jp.Text = Settings.Default.BoyTitle1_jp;
        boyTitle2_en.Text = Settings.Default.BoyTitle2_en;
        boyTitle2_jp.Text = Settings.Default.BoyTitle2_jp;
        boyDescription1_en.Text = Settings.Default.BoyDescription1_en;
        boyDescription1_jp.Text = Settings.Default.BoyDescription1_jp;
        boyTaste1.SelectedIndex = Settings.Default.BoyTaste1;
        boyChara1.SelectedIndex = Settings.Default.BoyChara1;

        girlTitle1_en.Text = Settings.Default.GirlTitle1_en;
        girlTitle1_jp.Text = Settings.Default.GirlTitle1_jp;
        girlTitle2_en.Text = Settings.Default.GirlTitle2_en;
        girlTitle2_jp.Text = Settings.Default.GirlTitle2_jp;
        girlDescription1_en.Text = Settings.Default.GirlDescription1_en;
        girlDescription1_jp.Text = Settings.Default.GirlDescription1_jp;
        girlTaste1.SelectedIndex = Settings.Default.GirlTaste1;
        girlChara1.SelectedIndex = Settings.Default.GirlChara1;

        sanBoyTitle1_en.Text = Settings.Default.SanBoyTitle1_en;
        sanBoyTitle1_jp.Text = Settings.Default.SanBoyTitle1_jp;
        sanBoyTitle2_en.Text = Settings.Default.SanBoyTitle2_en;
        sanBoyTitle2_jp.Text = Settings.Default.SanBoyTitle2_jp;
        sanBoyDescription1_en.Text = Settings.Default.SanBoyDescription1_en;
        sanBoyDescription1_jp.Text = Settings.Default.SanBoyDescription1_jp;
        sanBoyTaste1.SelectedIndex = Settings.Default.SanBoyTaste1;
        sanBoyChara1.SelectedIndex = Settings.Default.SanBoyChara1;

        sanGirlTitle1_en.Text = Settings.Default.SanGirlTitle1_en;
        sanGirlTitle1_jp.Text = Settings.Default.SanGirlTitle1_jp;
        sanGirlTitle2_en.Text = Settings.Default.SanGirlTitle2_en;
        sanGirlTitle2_jp.Text = Settings.Default.SanGirlTitle2_jp;
        sanGirlDescription1_en.Text = Settings.Default.SanGirlDescription1_en;
        sanGirlDescription1_jp.Text = Settings.Default.SanGirlDescription1_jp;
        sanGirlTaste1.SelectedIndex = Settings.Default.SanGirlTaste1;
        sanGirlChara1.SelectedIndex = Settings.Default.SanGirlChara1;

        allBoyTitle1_en.Text = Settings.Default.AllBoyTitle1_en;
        allBoyTitle1_jp.Text = Settings.Default.AllBoyTitle1_jp;
        allBoyTitle2_en.Text = Settings.Default.AllBoyTitle2_en;
        allBoyTitle2_jp.Text = Settings.Default.AllBoyTitle2_jp;
        allBoyDescription1_en.Text = Settings.Default.AllBoyDescription1_en;
        allBoyDescription1_jp.Text = Settings.Default.AllBoyDescription1_jp;
        allBoyTaste1.SelectedIndex = Settings.Default.AllBoyTaste1;
        allBoyChara1.SelectedIndex = Settings.Default.AllBoyChara1;

        allGirlTitle1_en.Text = Settings.Default.AllGirlTitle1_en;
        allGirlTitle1_jp.Text = Settings.Default.AllGirlTitle1_jp;
        allGirlTitle2_en.Text = Settings.Default.AllGirlTitle2_en;
        allGirlTitle2_jp.Text = Settings.Default.AllGirlTitle2_jp;
        allGirlDescription1_en.Text = Settings.Default.AllGirlDescription1_en;
        allGirlDescription1_jp.Text = Settings.Default.AllGirlDescription1_jp;
        allGirlTaste1.SelectedIndex = Settings.Default.AllGirlTaste1;
        allGirlChara1.SelectedIndex = Settings.Default.AllGirlChara1;
    }

    private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Settings.Default.SettingRectangle = WindowState == FormWindowState.Normal ? this.Bounds : this.RestoreBounds;
    }
}
