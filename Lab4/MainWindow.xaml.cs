using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TILab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LFSR LFSR = LFSR.getInstance();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                LFSR.PathToFile = openFileDialog.FileName;
                label_File.Content = $"File: {openFileDialog.FileName}";
            }
        }

        private void button_setKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LFSR.setInitValue(ulong.Parse(input_Key.Text));
                label_Key.Content = $"Key: {input_Key.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_encrypt_Click(object sender, RoutedEventArgs e)
        {
            Thread UpadateThread = new Thread(() => UpadateProgress());
            Thread CryptThread = new Thread(() => LFSR.Start(LFSR.AlgorithmMode.Encrypt));
            UpadateThread.Start();
            CryptThread.Start();
        }

        private void UpadateProgress()
        {
            bool isReady = false;
            Dispatcher.Invoke(() =>
            {
                LFSR.isReady = false;
            });
            while (!isReady)
            {
                Dispatcher.Invoke(() =>
                {
                    lock (LFSR._look)
                    {
                        progress.Value = LFSR.Progress;
                        if (LFSR.isReady)
                        {
                            isReady = false;
                        }
                    }
                });
                Thread.Sleep(300);
            }
            Dispatcher.Invoke(() =>
            {
                progress.Value = LFSR.Progress;
            });
        }

        private void button_decrypt_Click(object sender, RoutedEventArgs e)
        {
            Thread UpadateThread = new Thread(() => UpadateProgress());
            Thread CryptThread = new Thread(() => LFSR.Start(LFSR.AlgorithmMode.Decrypt));
            UpadateThread.Start();          
            CryptThread.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                LFSR.PathToFile = files[0];
                label_File.Content = $"File: {files[0]}";
            }
        }
    }
}
