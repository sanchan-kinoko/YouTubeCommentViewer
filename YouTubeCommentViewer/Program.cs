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
                // ��d�N�����`�F�b�N
                if (!createdNew)
                {
                    MessageBox.Show(
                        string.Format("{0} �͊��Ɏ��s���ł��B", Application.ProductName),
                        "�x�� / Warning",
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
                // �~���[�e�b�N�X���擾����Ă���ꍇ�ɂ̂݃����[�X
                if (createdNew)
                {
                    mutex.ReleaseMutex();
                }
            }



        }
    }
}