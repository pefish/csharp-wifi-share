using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WifiShare
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FormMain : Window
    {

        private bool wifiState; //指示是否已经打开承载网络
        private bool shareState; //指示是否已经开启共享
        public FormMain()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //获取承载网络设置信息
            string strSetting = string.Empty;
            if (System.Globalization.CultureInfo.InstalledUICulture.NativeName == "English (United States)")
            {
                strSetting = GlobalVars.Helper.getHostednetworkSetting("english");
            }
            else
            {
                strSetting = GlobalVars.Helper.getHostednetworkSetting("chinese");
            }

            string[] settings = strSetting.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
            if (settings[0] == "0")
            {
                this.wifiState = false;
            }
            else
            {
                this.wifiState = true;
            }

            this.ssid.Text = settings[1];
            if (settings[2] != "<Not specified>")
            {
                this.pwd.Password = settings[2];
            }
            //更新网卡列表
            this.updateInterfaces();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.wifiState)
            {
                if (!this.check())
                {
                    return;
                }

                //开启承载网络
                string cmd1 = "netsh wlan set hostednetwork mode=allow ssid=" + this.ssid.Text + " key=" + this.pwd.Password;
                string cmd2 = "netsh wlan start hostednetwork";
                GlobalVars.Helper.ExeCmd(cmd1);
                GlobalVars.Helper.ExeCmd(cmd2);
                this.wifiState = true;
                this.startShare.Content = "关闭";

            }
            else
            {
                string cmd3 = "netsh wlan stop hostednetwork";
                GlobalVars.Helper.ExeCmd(cmd3);
                this.wifiState = false;
                this.startShare.Content = "开启";
            }

            //刷新界面中网卡列表
            this.updateInterfaces();
        }

        private void updateInterfaces()
        {
			this.interfaceLists1.Items.Clear();
            this.interfaceLists2.Items.Clear();

			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface interfaceSingle in interfaces)
            {
				this.interfaceLists1.Items.Add(interfaceSingle.Name);
                this.interfaceLists2.Items.Add(interfaceSingle.Name);
            }
            this.interfaceLists1.SelectedIndex = 0;
            this.interfaceLists2.SelectedIndex = 0;

        }

        private bool check()
        {
            if (this.ssid.Text == string.Empty)
            {
                MessageBox.Show("SSID不能为空");
                return false;
            }

            if (this.pwd.Password == string.Empty)
            {
                MessageBox.Show("密钥不能为空");
                return false;
            }

            return true;
        }

        private void displayPwd_Checked(object sender, RoutedEventArgs e)
        {
            this.pwdDisplay.Visibility = System.Windows.Visibility.Visible;
            this.pwdDisplay.Text = this.pwd.Password;
            this.pwd.Visibility = System.Windows.Visibility.Hidden;
        }

        private void displayPwd_Unchecked(object sender, RoutedEventArgs e)
        {
            this.pwd.Visibility = System.Windows.Visibility.Visible;
            this.pwd.Password = this.pwdDisplay.Text;
            this.pwdDisplay.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lookState_Click(object sender, RoutedEventArgs e)
        {
            string cmd4 = "netsh wlan show hostednetwork";
            MessageBox.Show(GlobalVars.Helper.ExeCmd(cmd4));
        }

        private void lookPwd_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(System.Globalization.CultureInfo.InstalledUICulture.NativeName);
            string result;
            if (System.Globalization.CultureInfo.InstalledUICulture.NativeName == "English (United States)")
            {
                result = GlobalVars.Helper.getWlanPassword("english");
            }
            else
            {
                result = GlobalVars.Helper.getWlanPassword("chinese");
            }
            MessageBox.Show(result);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string vbsOpenShareCodes = Properties.Resources.vbsOpenShare;
            string fileName = System.IO.Path.GetTempPath() + "vbsOpenShareCodes.vbs";
            StreamWriter sw = new StreamWriter(fileName,false);
            sw.Write(vbsOpenShareCodes);
            sw.Close();

            string cmd = "cscript " + fileName + " \"" + this.interfaceLists2.SelectedValue + "\" \"" + this.interfaceLists1.SelectedValue + "\" off";
            GlobalVars.Helper.ExeCmd(cmd);
            string cmd2 = "cscript " + fileName + " \"" + this.interfaceLists2.SelectedValue + "\" \"" + this.interfaceLists1.SelectedValue + "\" on";
            GlobalVars.Helper.ExeCmd(cmd2);
            MessageBox.Show("共享成功");
        }

    }
}
