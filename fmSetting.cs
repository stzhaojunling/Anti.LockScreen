using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
    

namespace Anti.LockScreen
{
    public partial class fmSetting : Form
    {
        #region Tick Utils
        public uint TickDiff(uint uEnd, uint uStart)
        {
            if (uEnd >= uStart) {
                return uEnd - uStart;
            }
            return uint.MaxValue - uStart + uEnd;
        }
        public uint CurrentTick { get { return (uint)Environment.TickCount; } }
        #endregion Tick Utils
        protected override void WndProc(ref Message m)
        {
            ApiHelper.OnWtsSessionChange(ref m);
            base.WndProc(ref m);
        }
        #region UI Operation
        private bool mIsCn = false;
        public fmSetting()
        {            
            InitializeComponent();
            mIsCn = System.Globalization.CultureInfo.CurrentUICulture.Name == "zh-CN";            
            if (mIsCn) {
                lblIdle.Text = "间隔时间(分)";
                lblSend.Text = "连续发送次数";
                btnExist.Text = "退出";
                Text = "防锁屏";
                cbStartup.Text = "开机启动";
                cbMinimize.Text = "最小化";
            }
            niTray.Icon = Icon = Icon.ExtractAssociatedIcon(Environment.GetCommandLineArgs()[0]);
            #region Init Parameter Combox
            for(int i = 1; i <= 15; i++) {
                cbInterval.Items.Add(i.ToString());
                cbRepeatCount.Items.Add(i.ToString());
            }
            for(int i = 20; i <= 100; i += 5) {
                cbRepeatCount.Items.Add(i.ToString());
            }
            var SettingStr = ApiHelper.OnAppInit(Handle).Split(',');
            cbInterval.SelectedIndex = int.Parse(SettingStr[0]);
            cbRepeatCount.SelectedIndex = int.Parse(SettingStr[1]);
            #endregion Init Parameter Combox
            cbStartup.Checked = File.Exists(DefaultlnkPath);
            cbMinimize.Checked = ApiHelper.StartupMinimize;
            if (cbMinimize.Checked) {
                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }
        }
        private bool allowClose = false;       // ContextMenu's Exit command used
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (allowClose) {
                var SettingStr = string.Format("{0},{1}", cbInterval.SelectedIndex, cbRepeatCount.SelectedIndex);
                ApiHelper.OnAppExit(Handle, SettingStr);
            } else {
                this.Hide();
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
        private void miExit_Click(object sender, EventArgs e)
        {
            allowClose = true;
            Application.Exit();
        }
        private void niTray_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) {
                this.Hide();                
            }
        }
        private void fmSetting_Click(object sender, EventArgs e)
        {
            mAppTimerTiggerCount += 10;//取消自动隐藏
        }
        #endregion UI Operation
        private DateTime mLastSendKeyTime = DateTime.MinValue;
        private uint mLastSendKeyTick = 0;
        private uint mIdleBeginTick = 0;
        private int mSendKeysCount = 0;
        private int mTotalSendKeysCount = 0;           
        private int mAppTimerTiggerCount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ApiHelper.IsSessionLocked) {
                return;
            }            
            if (mAppTimerTiggerCount++ == 1) {//如果没有动作,2秒后自动隐藏
                this.Hide();
                if (cbMinimize.Checked) {
                    this.Opacity = 1;
                    this.ShowInTaskbar = true;
                }
            }            
            uint SettingTick = uint.Parse(cbInterval.Text) * 1000 * 60;//convert minate to ms
            var LastInputTick = ApiHelper.GetLastInputTick();
            uint sysInputIdle = TickDiff(CurrentTick, LastInputTick);            
            if (sysInputIdle >= SettingTick) {
                if (mSendKeysCount == 0) {
                    mIdleBeginTick = LastInputTick;
                }
                if (mSendKeysCount < int.Parse(cbRepeatCount.Text)) {                    
                    #region Send Key Event
                    for (int i = 0; i < 2; i++) {
                        ApiHelper.keybd_event((byte)Keys.NumLock, 0, 0, 0);
                        ApiHelper.keybd_event((byte)Keys.NumLock, 0, 2, 0);
                    }
                    #endregion Key Event
                    mLastSendKeyTime = DateTime.Now;
                    mLastSendKeyTick = CurrentTick;
                    mSendKeysCount++;
                    mTotalSendKeysCount++;
                    addToSendKeyHistory();
                    ApiHelper.Log(string.Format("Send Key (Setting: {0:N0}, Idle {1:N0}, Send Count {2}/{3})", SettingTick, sysInputIdle, mSendKeysCount, cbRepeatCount.Text));
                }
            } else if (mSendKeysCount > 0) {//检测是否是上次发送
                /*
                 *           Last Key Send       Current
                 *             | 10ms               |      
                 *       ------+------+-------------+-------> time line
                 *                    |             | 
                 *                Other Key 
                 */ 
                if (TickDiff(LastInputTick, mLastSendKeyTick) > 10) {//距离上次发送超过10ms
                    ApiHelper.Log(string.Format("Reset Send Keys Count {0} -> 0, Last Input {1} Last Send {2}", 
                        mSendKeysCount, LastInputTick, mLastSendKeyTick));
                    mSendKeysCount = 0;
                }
            }
            #region Update Msg
            var MsgPrimary = mIsCn ? string.Format("已连续发送 {0} 次, 共 {1} 次", mSendKeysCount, mTotalSendKeysCount)
                                   : string.Format("Send: {0}, Total: {1}", mSendKeysCount, mTotalSendKeysCount);
            niTray.Text = this.Text + Environment.NewLine + MsgPrimary;
            var Msg2 = string.Empty;
            var lst = mSendKeysCount > 0 ? mIdleBeginTick : ApiHelper.GetLastInputTick();
            var idleSeconds = TickDiff(CurrentTick, lst) / 1000.0;
            if (idleSeconds <= 60) {
                Msg2 = mIsCn ? string.Format("空闲 {0:N0} 秒", idleSeconds)
                             : string.Format("Idle {0:N0} Seconds", idleSeconds);
            } else {
                Msg2 = mIsCn ? string.Format("空闲 {0:N1} 分", idleSeconds / 60.0)
                             : string.Format("Idle {0:N1} Minutes", idleSeconds / 60.0);
            }
            if (mLastSendKeyTime.Ticks > 0) {
                Msg2 += (mIsCn ? ", 上次发送 " : ", Last Sent: ") + mLastSendKeyTime.ToLongTimeString();
            }
            lblMsgs.Text = MsgPrimary + Environment.NewLine + Msg2;
            #endregion Update Msg
        }
        #region SendKeyHistory
        private void lblSendTimes_DoubleClick(object sender, EventArgs e)
        {
            var sb = new System.Text.StringBuilder();
            foreach(var s in mSendKeyHistory) {
                sb.Append(s).Append(Environment.NewLine);
            }
            if (sb.Length > 0) {
                sb.Append('-', 36).Append(Environment.NewLine);
            }
            var ass = Assembly.GetExecutingAssembly();
            sb.AppendLine(ass.GetName().Version.ToString());
            var cp = ass.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
            sb.Append((cp as AssemblyCopyrightAttribute).Copyright);
            MessageBox.Show(sb.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private List<string> mSendKeyHistory = new List<string>();
        private void addToSendKeyHistory()
        {
            if (mSendKeyHistory.Count >= 20) {
                mSendKeyHistory.RemoveAt(0);
            }
            mSendKeyHistory.Add(string.Format("{1}/{2} {0}", mLastSendKeyTime.ToString("yyyy-MM-dd HH:mm:ss"), mSendKeysCount, mTotalSendKeysCount));
        }
        #endregion SendKeyHistory

        private string DefaultlnkPath
        {
            get {
                var startup = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                var p = Path.Combine(startup, "Anti.LockScreen.lnk");
                return p;
            }
        }

        private void cbStartup_CheckedChanged(object sender, EventArgs e)
        {
            var lnkName = DefaultlnkPath;
            if (cbStartup.Checked) {
                if (File.Exists(lnkName)) {
                    return;
                }
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcut = shell.CreateShortcut(lnkName) as IWshRuntimeLibrary.IWshShortcut;
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                shortcut.WindowStyle = 1;
                shortcut.Description = "Anti Lock Screen";
                shortcut.IconLocation = shortcut.TargetPath;
                shortcut.Save();
                if (File.Exists(lnkName)) {
                    var msg = string.Format("Add to Startup Succ\r\n{0}", lnkName);
                    ApiHelper.Log(msg);
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    cbStartup.Checked = false;
                }
            } else {
                if (File.Exists(lnkName)) {
                    File.Delete(lnkName);
                }
            }
        }

        private void cbMinimize_CheckedChanged(object sender, EventArgs e)
        {
            ApiHelper.StartupMinimize = cbMinimize.Checked;
        }
    }
}
