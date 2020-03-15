using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WifiShare
{
    class Helper
    {

        /// <summary>
        /// 获取承载网络的设置信息，包括ssid名称、对应密码、开启状态
        /// </summary>
        /// <param name="lang">系统语言,english与chinese</param>
        /// <returns>返回”开启状态##ssid名称##密码“</returns>
        public string getHostednetworkSetting(string lang)
        {
            string result = string.Empty;
            string result1 = this.ExeCmd("netsh wlan show hostednetwork");

            string wifiState = string.Empty;
            string ssidName = string.Empty;
            string password = string.Empty;
            Regex pattern = null;
            Match matchMode = null;
            if (lang == "english")
            {
                //开启状态
                string strWifiState = string.Empty;
                pattern = new Regex(@"Status                 :.*\r\n");
                matchMode = pattern.Match(result1);
                if (matchMode.Success)
                {
                    strWifiState = matchMode.Value.Substring(25, matchMode.Value.Length - 27);
                }
                if (strWifiState == "Not available")
                {
                    wifiState = "0";
                }
                else
                {
                    wifiState = "1";
                }

                //ssidName
                pattern = new Regex("SSID name              : \".*\r\n");
                matchMode = pattern.Match(result1);
                if (matchMode.Success)
                {
                    ssidName = matchMode.Value.Substring(26, matchMode.Value.Length - 29);
                }

                //pasword
                string result2 = this.ExeCmd("netsh wlan show hostednetwork setting=security");
                pattern = new Regex(@"User security key      :.*\r\n");
                matchMode = pattern.Match(result2);
                if (matchMode.Success)
                {
                    password = matchMode.Value.Substring(25, matchMode.Value.Length - 27);
                }
            }
            else
            {
                string strWifiState = string.Empty;
                pattern = new Regex(@"状态                 :.*\r\n");
                matchMode = pattern.Match(result1);
                if (matchMode.Success)
                {
                    strWifiState = matchMode.Value.Substring(21, matchMode.Value.Length - 23);
                }
                if (strWifiState == "Not available")
                {
                    wifiState = "0";
                }
                else
                {
                    wifiState = "1";
                }

                pattern = new Regex(@"SSID 名称              :“.*\r\n");
                matchMode = pattern.Match(result1);
                if (matchMode.Success)
                {
                    ssidName = matchMode.Value.Substring(23, matchMode.Value.Length - 26);
                }

                string result2 = this.ExeCmd("netsh wlan show hostednetwork setting=security");
                pattern = new Regex(@"用户安全密钥      :.*\r\n");
                matchMode = pattern.Match(result2);
                if (matchMode.Success)
                {
                    password = matchMode.Value.Substring(14, matchMode.Value.Length - 16);
                }
            }

            return wifiState+"##"+ssidName + "##" + password;
        }

        /// <summary>
        /// 查看wlan密码，支持中文与英文系统
        /// </summary>
        /// <param name="lang">系统语言，english与chinese</param>
        /// <returns></returns>
        public string getWlanPassword(string lang)
        {
            string resultPasswords = string.Empty;
            if (lang == "english")
            {
                string cmd1 = "netsh wlan show profiles";
                string result1 = this.ExeCmd(cmd1);

                Regex pattern = new Regex(@"All User Profile     :.*\r\n");//所有用户配置文件 : X907
                Match matchMode = pattern.Match(result1);
                while (matchMode.Success)
                {
                    string value = matchMode.Value;
                    string wifiName = value.Substring(23, value.Length - 25);

                    string cmd2 = "netsh wlan show profiles name=" + wifiName + " key=clear";
                    string result2 = this.ExeCmd(cmd2);
                    Regex pattern1 = new Regex(@"Key Content            :.*\r\n");
                    Match matchMode1 = pattern1.Match(result2);
                    string wifiPwd = string.Empty;
                    if (matchMode1.Success)
                    {
                        wifiPwd = matchMode1.Value.Substring(25, matchMode1.Value.Length - 27);
                    }

                    resultPasswords = resultPasswords + "SSID:" + wifiName + "----PWD:" + wifiPwd + "\r\n";

                    matchMode = matchMode.NextMatch();
                }
                resultPasswords = resultPasswords.Substring(0, resultPasswords.Length - 2);
            }
            else
            {
                string cmd1 = "netsh wlan show profiles";
                string result1 = this.ExeCmd(cmd1);

                Regex pattern = new Regex(@"所有用户配置文件 :.*\r\n");//所有用户配置文件 : X907
                Match matchMode = pattern.Match(result1);
                while (matchMode.Success)
                {
                    string value = matchMode.Value;
                    string wifiName = value.Substring(11, value.Length - 13);

                    string cmd2 = "netsh wlan show profiles name=" + wifiName + " key=clear";
                    string result2 = this.ExeCmd(cmd2);
                    Regex pattern1 = new Regex(@"关键内容            :.*\r\n");
                    Match matchMode1 = pattern1.Match(result2);
                    string wifiPwd = string.Empty;
                    if (matchMode1.Success)
                    {
                        wifiPwd = matchMode1.Value.Substring(18, matchMode1.Value.Length - 20);
                    }

                    resultPasswords = resultPasswords + "SSID:" + wifiName + "----PWD:" + wifiPwd + "\r\n";

                    matchMode = matchMode.NextMatch();
                }
                resultPasswords = resultPasswords.Substring(0, resultPasswords.Length - 2);
            }
            return resultPasswords;
        }

        /// <summary>
        /// 执行cmd命令,并等待执行完成返回结果（可以利用线程实现异步，但不会脱离主程序）
        /// </summary>
        /// <param name="cmd">cmd命令,如“ipconfig”</param>
        /// <returns>返回命令执行结果</returns>
        public string ExeCmd(string cmd)
        {
            Process p = new Process();
            //初始化start方法的属性
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C " + cmd;
            p.StartInfo.UseShellExecute = false;//重定向前必须设置,否则无法达到隐藏窗口的效果
            p.StartInfo.RedirectStandardInput = false;//重定向
            p.StartInfo.RedirectStandardOutput = true;//重定向
            p.StartInfo.CreateNoWindow = true;//不显示窗口
            //启动进程
            p.Start();
            string strOutput = p.StandardOutput.ReadToEnd();//获取输出信息
            p.WaitForExit(500);//等待进程退出
            p.Close();//释放资源
            return strOutput;
        }
    }
}
