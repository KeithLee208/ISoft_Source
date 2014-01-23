using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Drawing;
namespace ISoft
{
    class PublicClass
    {
        private string ConvertStr(string str)
        {
            string pp = "";
            switch (str)
            {
                case "AddressWidth":
                    pp = "��ַ���";
                    break;
                case "Architecture":
                    pp = "�ṹ";
                    break;
                case "Availability":
                    pp = "����";
                    break;
                case "Caption":
                    pp = "�ڲ����";
                    break;
                case "CpuStatus":
                    pp = "���������";
                    break;
                case "CreationClassName":
                    pp = "����������";
                    break;
                case "CurrentClockSpeed":
                    pp = "��ǰʱ���ٶ�";
                    break;
                case "CurrentVoltage":
                    pp = "��ǰ��ѹ";
                    break;
                case "DataWidth":
                    pp = "���ݿ��";
                    break;
                case "Description":
                    pp = "����";
                    break;
                case "DeviceID":
                    pp = "�汾";
                    break;
                case "ExtClock":
                    pp = "�ⲿʱ��";
                    break;
                case "L2CacheSize":
                    pp = "��������";
                    break;
                case "L2CacheSpeed":
                    pp = "���������ٶ�";
                    break;
                case "Level":
                    pp = "����";
                    break;
                case "LoadPercentage":
                    pp = "���ϰٷֱ�";
                    break;
                case "Manufacturer":
                    pp = "������";
                    break;
                case "MaxClockSpeed":
                    pp = "���ʱ���ٶ�";
                    break;
                case "Name":
                    pp = "����";
                    break;
                case "PowerManagementSupported":
                    pp = "��Դ����֧��";
                    break;
                case "ProcessorId":
                    pp = "����������";
                    break;
                case "ProcessorType":
                    pp = "����������";
                    break;
                case "Role":
                    pp = "����";
                    break;
                case "SocketDesignation":
                    pp = "�������";
                    break;
                case "Status":
                    pp = "״̬";
                    break;
                case "StatusInfo":
                    pp = "״̬��Ϣ";
                    break;
                case "Stepping":
                    pp = "�ּ�";
                    break;
                case "SystemCreationClassName":
                    pp = "ϵͳ����������";
                    break;
                case "SystemName":
                    pp = "ϵͳ����";
                    break;
                case "UpgradeMethod":
                    pp = "��������";
                    break;
                case "Version":
                    pp = "�ͺ�";
                    break;
                case "Family":
                    pp = "����";
                    break;
                case "Revision":
                    pp = "�޶��汾��";
                    break;
                case "PoweredOn":
                    pp = "��Դ����";
                    break; 
                case "Product":
                    pp = "��Ʒ";
                    break; 
                    
            }
            if (pp == "")
                pp = str;
            return pp;
        }
        public void InsertInfo(string Key, ref ListView lst, bool DontInsertNull)
        {
            lst.Items.Clear();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Key);
            try
            {
                foreach (ManagementObject share in searcher.Get())
                {
                    ListViewGroup grp;
                    try
                    {
                        grp = lst.Groups.Add(share["Name"].ToString(), share["Name"].ToString());
                    }
                    catch
                    {
                        grp = lst.Groups.Add(share.ToString(), share.ToString());
                    }

                    if (share.Properties.Count <= 0)
                    {
                        MessageBox.Show("No Information Available", "No Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    foreach (PropertyData PC in share.Properties)
                    {
                        ListViewItem item = new ListViewItem(grp);
                        if (lst.Items.Count % 2 != 0)
                            item.BackColor = Color.White;
                        else
                            item.BackColor = Color.WhiteSmoke;
                        item.Text =ConvertStr(PC.Name);
                        if (PC.Value != null && PC.Value.ToString() != "")
                        {
                            switch (PC.Value.GetType().ToString())
                            {
                                case "System.String[]":
                                    string[] str = (string[])PC.Value;
                                    string str2 = "";
                                    foreach (string st in str)
                                        str2 += st + " ";
                                    item.SubItems.Add(str2);
                                    break;
                                case "System.UInt16[]":
                                    ushort[] shortData = (ushort[])PC.Value;
                                    string tstr2 = "";
                                    foreach (ushort st in shortData)
                                        tstr2 += st.ToString() + " ";
                                    item.SubItems.Add(tstr2);
                                    break;
                                default:
                                    item.SubItems.Add(PC.Value.ToString());
                                    break;
                            }
                        }
                        else
                        {
                            if (!DontInsertNull)
                                item.SubItems.Add("û����Ϣ");
                            else
                                continue;
                        }

                        lst.Items.Add(item);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string[] info = new string[2];
        public void getInfo1(ListView lv)
        {
            info[0] = "����ϵͳ";
            info[1]=Environment.OSVersion.VersionString;
            ListViewItem item = new ListViewItem(info, "����ϵͳ");
            lv.Items.Add(item);

            RegistryKey mykey = Registry.LocalMachine;
            mykey = mykey.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
            string a = (string)mykey.GetValue("RegisteredOrganization");
            mykey.Close();
            info[0] = "ע���û�";
            info[1] = a;
            ListViewItem item1 = new ListViewItem(info, "ע���û�");
            lv.Items.Add(item1);

            info[0] = "Windows�ļ���";
            info[1] = Environment.GetEnvironmentVariable("WinDir");
            ListViewItem item2 = new ListViewItem(info, "Windows�ļ���");
            lv.Items.Add(item2);

            info[0] = "ϵͳ�ļ���";
            info[1] = Environment.SystemDirectory.ToString();
            ListViewItem item3= new ListViewItem(info, "ϵͳ�ļ���");
            lv.Items.Add(item3);

            info[0] = "���������";
            info[1] = Environment.MachineName.ToString();
            ListViewItem item4 = new ListViewItem(info, "���������");
            lv.Items.Add(item4);

            info[0] = "��������ʱ��";
            info[1] = DateTime.Now.ToString();
            ListViewItem item5 = new ListViewItem(info, "��������ʱ��");
            lv.Items.Add(item5);

            string MyInfo = "";
            ManagementObjectSearcher MySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject MyObject in MySearcher.Get())
            {
                MyInfo += MyObject["InstallDate"].ToString().Substring(0, 8);
            }
            MyInfo = MyInfo.Insert(4, "-");
            MyInfo = MyInfo.Insert(7,"-");
            info[0] = "ϵͳ��װ����";
            info[1] = MyInfo;
            ListViewItem item6 = new ListViewItem(info, "ϵͳ��װ����");
            lv.Items.Add(item6);


            String MyInfo1 = "";
            ManagementObjectSearcher MySearcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject MyObject in MySearcher1.Get())
            {
                MyInfo1 += MyObject["LastBootUpTime"].ToString().Substring(0, 8);
            }
            MyInfo1 = MyInfo1.Insert(4, "-");
            MyInfo1 = MyInfo1.Insert(7, "-");
            info[0] = "ϵͳ����ʱ��";
            info[1] = MyInfo1;
            ListViewItem item7 = new ListViewItem(info, "ϵͳ����ʱ��");
            lv.Items.Add(item7);

            Microsoft.VisualBasic.Devices.Computer My = new Microsoft.VisualBasic.Devices.Computer();
            info[0]="�����ڴ�����(M)";
            info[1] = (My.Info.TotalPhysicalMemory / 1024 / 1024).ToString();
            ListViewItem item8 = new ListViewItem(info, "�����ڴ�����(M)");
            lv.Items.Add(item8);

            info[0] = "�����ڴ�����(M)";
            info[1] = (My.Info.TotalVirtualMemory / 1024 / 1024).ToString();
            ListViewItem item9 = new ListViewItem(info, "�����ڴ�����(M)");
            lv.Items.Add(item9);

            info[0] = "���������ڴ�����(M)";
            info[1] =(My.Info.AvailablePhysicalMemory / 1024 / 1024).ToString();
            ListViewItem item10 = new ListViewItem(info, "���������ڴ�����(M)");
            lv.Items.Add(item10);

            info[0] = "���������ڴ�����(M)";
            info[1] = (My.Info.AvailableVirtualMemory / 1024 / 1024).ToString();
            ListViewItem item11 = new ListViewItem(info, "���������ڴ�����(M)");
            lv.Items.Add(item11);

            info[0] = "ϵͳ������";
            info[1] = Environment.GetEnvironmentVariable("SystemDrive");
            ListViewItem item12 = new ListViewItem(info, "ϵͳ������");
            lv.Items.Add(item12);

            info[0] = "����Ŀ¼";
            info[1] = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ListViewItem item13 = new ListViewItem(info, "ϵͳ������");
            lv.Items.Add(item13);

            info[0] = "�û�������Ŀ¼";
            info[1] = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            ListViewItem item14 = new ListViewItem(info, "�û�������Ŀ¼");
            lv.Items.Add(item14);

            info[0] = "�ղؼ�Ŀ¼";
            info[1] = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
            ListViewItem item15 = new ListViewItem(info, "�ղؼ�Ŀ¼");
            lv.Items.Add(item15);

            info[0] = "Internet��ʷ��¼";
            info[1] = Environment.GetFolderPath(Environment.SpecialFolder.History);
            ListViewItem item16 = new ListViewItem(info, "Internet��ʷ��¼");
            lv.Items.Add(item16);

            info[0] = "Internet��ʱ�ļ�";
            info[1] = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            ListViewItem item17 = new ListViewItem(info, "Internet��ʱ�ļ�");
            lv.Items.Add(item17);
            
 
        }

        public void getInfo4(ListView lv)
        {
            Process[] myprocesses = Process.GetProcesses();
            foreach (Process myprocess in myprocesses)
            {
                info[0] = myprocess.ProcessName;
                info[1] = "ID:" + myprocess.Id.ToString() + "," + "���ȼ�:" + myprocess.BasePriority.ToString() + "," + "�߳�����:" + myprocess.Threads.Count.ToString();
                ListViewItem item = new ListViewItem(info, "�û�����");
                lv.Items.Add(item);
            }
        }

    }

}
