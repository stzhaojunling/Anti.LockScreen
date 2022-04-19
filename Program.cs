using System;
using System.Windows.Forms;
using System.Threading;

namespace Anti.LockScreen
{
    static class Program
    {     
        [STAThread]
        static void Main()
        {
            bool IsCreateNew = false;
            var mutex = new Mutex(true, "Local\\Anti.LockScreen.By.ZhaoJunling", out IsCreateNew);
            if (IsCreateNew) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new fmSetting());
            } else {
                ApiHelper.TryActivePreInstance();
            }
        }
    }
}
