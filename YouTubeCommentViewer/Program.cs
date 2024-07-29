using System.Threading;

namespace YouTubeCommentViewer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool createdNew;
            var mutex = new Mutex(true, Application.ProductName, out createdNew);
            try
            {
                // 二重起動をチェック
                if (!createdNew)
                {
                    MessageBox.Show(
                        string.Format("{0} は既に実行中です。", Application.ProductName),
                        "警告 / Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    return;
                }
                ApplicationConfiguration.Initialize();
                var formStartUp = new FormStartUp();
                formStartUp.Show();
                Application.Run(new FormSettings(formStartUp));
            }
            finally
            {
                // ミューテックスが取得されている場合にのみリリース
                if (createdNew)
                {
                    mutex.ReleaseMutex();
                }
            }



        }
    }
}