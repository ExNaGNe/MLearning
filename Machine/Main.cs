using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using static Machine.CONST;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace Machine
{
    public partial class Main : Form
    {
        //System.Timers.Timer timer;
        string PythonPath = string.Empty;
        bool Check_device = false;
        bool Actived = false;
        ProcessStartInfo myProcessStartInfo;
        Process myProcess;

        public Main()
        {
            InitializeComponent();

            //timer = new System.Timers.Timer(MOUSEINTERVAL);
            //timer.Elapsed += MouseLocation;
            //timer.AutoReset = true;

            DeviceListup();
            text_sensitive.MaxLength = 2;
            //timer.Enabled = true;

            RegisterHotKey(this.Handle, STARTKEYID, KeyModifiers.None, Keys.F1);
            RegisterHotKey(this.Handle, ENDKEYID, KeyModifiers.None, Keys.F2);

            Set_Py();
        }

        private void but_close_Click(object sender, EventArgs e)    //종료 버튼 클릭
        {
            Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)    //폼 종료시
        {
            //timer.Enabled = false;
            //timer.Elapsed -= MouseLocation;
            UnregisterHotKey(this.Handle, STARTKEYID);
            UnregisterHotKey(this.Handle, ENDKEYID);
            try
            {
                myProcess.Close();
            }
            catch { }
        }

        protected override void WndProc(ref Message m)      //단축키 작동
        {
            switch (m.Msg)
            {
                case HOTKEY:
                    if (m.WParam == (IntPtr)STARTKEYID)     //프로그램 작동
                    {
                        if(!Check_device)
                        {
                            MessageBox.Show("연결된 비디오 디바이스가 없습니다.");
                            return;
                        }
                        int a;
                        if(string.IsNullOrEmpty(text_end.Text))
                        {
                            MessageBox.Show("종료 단축키를 설정 해 주세요.");
                            return;
                        }
                        else if(!int.TryParse(text_sensitive.Text, out a))
                        {
                            MessageBox.Show("감도는 숫자로 입력해 주세요.");
                            return;
                        }

                        Run_Py();
                    }
                    else if (m.WParam == (IntPtr)ENDKEYID)  //프로그램 작동 종료
                    {
                        try
                        {
                            myProcess.Close();
                            this.Visible = true;
                            Actived = false;
                            this.BringToFront();
                        }
                        catch { }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void Set_Py()
        {
            myProcessStartInfo = new ProcessStartInfo();
            myProcessStartInfo.FileName = $"{ROOT}{MYPYTHON}";
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardInput = true;
            myProcessStartInfo.RedirectStandardOutput = false;
        }

        private void Run_Py()
        {
            myProcessStartInfo.Arguments = $"{combo_camera.SelectedIndex} {text_sensitive.Text}";
            myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;

            if (!Check_device)
            {
                MessageBox.Show("연결된 비디오 입력 기기가 없습니다.");
                return;
            }
            if(Actived)
            { 
                MessageBox.Show("이미 실행 중 입니다.");
                return;
            }

            this.Hide();
            int camera = combo_camera.SelectedIndex;
            string sensitive = text_sensitive.Text;
            Thread th = new Thread(() =>
            {
                Actived = true;
                try
                {
                    myProcess.Start();
                    myProcess.WaitForExit();
                    myProcess.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} {ex.StackTrace}");
                    Invoke(new Action(delegate ()
                    {
                        Visible = true;
                    }
                    ));
                }
                finally
                {
                    Actived = false;
                    Invoke(new Action(delegate ()
                    {
                        Visible = true;
                    }));
                }
            });
            th.Start();
            MessageBox.Show("로딩 중(약 25초 후에 실행됩니다)");
        }

        private void DeviceListup()         //비디오 입력장치 리스트
        {
            combo_camera.Items.Clear();
            using (var cameras = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE(PNPClass = 'Image' OR PNPClass = 'Camera')"))
            {
                foreach (var camera in cameras.Get())
                {
                    combo_camera.Items.Add(camera["Caption"].ToString());
                }
            }

            if (combo_camera.Items.Count > 0)
            {
                combo_camera.SelectedIndex = 0;
                Check_device = true;
            }
            else
            {
                Check_device = false;
                MessageBox.Show(this, "연결된 장치가 없습니다.");
            }
        }

        private void but_refresh_Click(object sender, EventArgs e)  //비디오 입력장리 리스트 새로고침
        {
            DeviceListup();
        }

        [DllImport("user32.dll")]   //단축키 등록
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);
        [DllImport("user32.dll")]   //단축키 해제
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private void 장치새로고침ToolStripMenuItem_Click(object sender, EventArgs e)      //메뉴 스트립 버튼 클릭
        {
            DeviceListup();
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)         //트레이 아이콘 더블 클릭시 실행 중지
        {
            try
            {
                myProcess.Close();
            }
            catch
            {

            }
            finally
            {
                this.Visible = true;
            }
        }

        private void 실행ToolStripMenuItem_Click(object sender, EventArgs e)              //트레이 아이콘 실행 메뉴
        {
            Run_Py();
        }

        private void 중지ToolStripMenuItem_Click(object sender, EventArgs e)              //트레이 아이콘 중지 메뉴
        {
            try
            {
                myProcess.Close();
            }
            catch
            {

            }
            finally
            {
                this.Visible = true;
            }
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)              //트레이 아이콘 종료 메뉴
        {
            try
            {
                myProcess.Close();
                myProcess.WaitForExit();
            }
            catch
            { }
            finally
            {
                this.Close();
            }
        }

        private void text_sensitive_KeyPress(object sender, KeyPressEventArgs e)        //감도 입력시 숫자만으로 제한
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
                e.Handled = true;
        }

    }

    public static class CONST
    {
        public const int MOUSEINTERVAL = 10;
        public const int STARTKEYID = 30001;
        public const int ENDKEYID = 30002;
        public const int HOTKEY = 0x312;

        public const string MYPYTHON = @"/test/test.exe";
        public static string ROOT = System.Windows.Forms.Application.StartupPath;

        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }
    }

}
