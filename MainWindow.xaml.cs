using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Smile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppSB.LogEvent += AppSB_LogEvent;
        }

        System.ComponentModel.BackgroundWorker boardTask;
        Smile.ProcessingSmileBoard smileProcessing;
        private void btCompare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (boardTask != null && boardTask.IsBusy)
                {
                    System.Windows.MessageBox.Show("Processing is running. Please wait to finish processing!!");
                    return;
                }
                btCompare.IsEnabled = false;
                boardTask = new System.ComponentModel.BackgroundWorker();
                boardTask.RunWorkerCompleted += boardTask_RunWorkerCompleted;
                boardTask.DoWork += boardTask_DoWork;
                boardTask.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                AppSB.Log(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        void boardTask_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.richtxtLog.Dispatcher.Invoke(new Action(() =>
                {
                    this.richtxtLog.Document.Blocks.Clear();
                    AppSB.Log("Start the processing at " + DateTime.Now.ToLongTimeString() + Environment.NewLine);
                    AppSB.Log("Please wait ... (I perform high performance computing)");
                }));

            smileProcessing = new Smile.ProcessingSmileBoard();
        }

        void boardTask_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            AppSB.Log("Smile program is Complete :)");
            btCompare.IsEnabled = true;
        }

        void AppSB_LogEvent(string msg)
        {
            this.richtxtLog.Dispatcher.Invoke(new Action(() =>
            {
                this.richtxtLog.AppendText(msg + Environment.NewLine);
                this.richtxtLog.ScrollToEnd();
            }));
        }
    }
}
