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

namespace TILab1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static string CaesarEncrypt(string SourceText, int key)
        {
            string Result = "";
            int i, StrLen = SourceText.Length;

            for (i = 0; i < StrLen; i++)
            {
                if ((SourceText[i] >= 'A' && SourceText[i] <= 'Z'))
                    Result += (char)((SourceText[i] + key - 'A') % 26 + 'A');
                else if ((SourceText[i] >= 'a' && SourceText[i] <= 'z'))
                    Result += (char)((SourceText[i] + key - 'a') % 26 + 'a');
                else if ((SourceText[i] >= 'А' && SourceText[i] <= 'Я'))
                    Result += (char)((SourceText[i] + key - 'А') % 33 + 'А');
                else if ((SourceText[i] >= 'а' && SourceText[i] <= 'я'))
                    Result += (char)((SourceText[i] + key - 'а') % 33 + 'а');
                else
                    Result += SourceText[i];
            }
            return Result;
        }

        static string CaesarDecrypt(string SourceText, int key)
        {
            string Result = "";
            int i, StrLen = SourceText.Length;

            for (i = 0; i < StrLen; i++)
            {
                if ((SourceText[i] >= 'A' && SourceText[i] <= 'Z'))
                    Result += (char)((26 + ((SourceText[i] - 'A') - (key % 26))) % 26 + 'A');
                else if ((SourceText[i] >= 'a' && SourceText[i] <= 'z'))
                    Result += (char)((26 + ((SourceText[i] - 'a') - (key % 26))) % 26 + 'a');
                else if ((SourceText[i] >= 'А' && SourceText[i] <= 'Я'))
                    Result += (char)((33 + ((SourceText[i] - 'а') - (key % 33))) % 33 + 'а');
                else if ((SourceText[i] >= 'а' && SourceText[i] <= 'я'))
                    Result += (char)((33 + ((SourceText[i] - 'А') - (key % 33))) % 33 + 'А');
                else
                    Result += SourceText[i];
            }
            return Result;
        }

        static string ColumnEncrypt(string SourceText, string key)
        {
            string Result = "";
            int KeyLength = key.Length, NumOfRows = (int)Math.Ceiling((double)SourceText.Length / KeyLength) + 2, SourceLength = SourceText.Length;
            char[,] Matrix = new char[NumOfRows, KeyLength];
            int i, j;
            int MinInd, Ind = 0;
            for (i = 0; i < NumOfRows; i++)
            {
                for (j = 0; j < KeyLength; j++)
                {
                    Matrix[i, j] = (char)0;
                }
            }
            for (i = 0; i < KeyLength; i++)
            {
                Matrix[0, i] = key[i];
            }
            for (i = 0, Ind = 0; i < KeyLength; i++)
            {
                if (Matrix[0, i] > Matrix[0, Ind])
                    Ind = i;
            }
            for (i = 0; i < KeyLength; i++)
            {              
                for (j = 0, MinInd = Ind; j < KeyLength; j++)
                {
                    if (((Matrix[0, j] < Matrix[0, MinInd]) || ((Matrix[0, j] == Matrix[0, MinInd]) && (Matrix[1, MinInd] != (char)0))) && (Matrix[1, j] == (char)0))
                    {
                        MinInd = j;
                    }
                }
                if (Matrix[1, MinInd] == 0)
                    Matrix[1, MinInd] = (char)(i + 1);
            }
            Ind = 0;
            for (i = 2; i < NumOfRows; i++)
            {
                for (j = 0; j < KeyLength; j++)
                {
                    if (Ind < SourceLength)
                    {
                        Matrix[i, j] = SourceText[Ind++];
                    }
                }
            }

            for (Ind = 1; Ind <= KeyLength; Ind++)
            {
                for (j = 0; j < KeyLength; j++)
                {
                    if (Matrix[1, j] == Ind)
                    {
                        for (i = 2; i < NumOfRows; i++)
                        {
                            if (Matrix[i, j] != 0)
                            {
                                Result += Matrix[i, j];
                            }
                        }
                    }
                }
            }

            return Result;
        }

        static string ColumnDecrypt(string SourceText, string key)
        {
            string Result = "";
            int KeyLength = key.Length, NumOfRows = (int)Math.Ceiling((double)SourceText.Length / KeyLength) + 2, SourceLength = SourceText.Length;
            int Mod = SourceLength % KeyLength;
            char[,] Matrix = new char[NumOfRows, KeyLength];
            int i, j, k;
            int MinInd, Ind = 0;
            for (i = 0; i < NumOfRows; i++)
            {
                for (j = 0; j < KeyLength; j++)
                {
                    Matrix[i, j] = (char)0;
                }
            }
            for (i = 0; i < KeyLength; i++)
            {
                Matrix[0, i] = key[i];
            }
            for (i = 0, Ind = 0; i < KeyLength; i++)
            {
                if (Matrix[0, i] > Matrix[0, Ind])
                    Ind = i;
            }
            for (i = 0; i < KeyLength; i++)
            {
                for (j = 0, MinInd = Ind; j < KeyLength; j++)
                {
                    if (((Matrix[0, j] < Matrix[0, MinInd]) || ((Matrix[0, j] == Matrix[0, MinInd]) && (Matrix[1, MinInd] != (char)0))) && (Matrix[1, j] == (char)0))
                    {
                        MinInd = j;
                    }
                }
                if (Matrix[1, MinInd] == 0)
                    Matrix[1, MinInd] = (char)(i + 1);
            }
            MinInd = 1;
            Ind = 0;
            for (k = 0; k < KeyLength; k++)
            {
                for (i = 0; i < KeyLength; i++)
                {
                    if (Matrix[1, i] == MinInd)
                    {
                        for (j = 2; j < ((i < Mod) ? NumOfRows : NumOfRows - 1); j++)
                        {
                            Matrix[j, i] = SourceText[Ind++];
                        }
                        MinInd++;
                    }
                }
            }
            Ind = 0;
            for (i = 2; i < NumOfRows && Ind < SourceLength; i++)
            {
                for (j = 0; j < KeyLength && Ind < SourceLength; j++)
                {
                    Result += Matrix[i, j];
                    Ind++;
                }
            }

            return Result;
        }

        static string RailFenceEncrypt(string SourceText, int key)
        {
            string Result = "";
            int Period = 2 * (key - 1);
            int SourceLength = SourceText.Length;
            int Mod;
            int i, j;
            for (i = 1; i <= key; i++)
            {
                for (j = 0; j < SourceLength; j++)
                {
                    Mod = (j + 1) % Period;
                    if (Mod == 0)
                        Mod = Period;
                    if (Mod > key)
                    {
                        Mod = key - (Mod - key);
                    }
                    if (Mod == i)
                        Result += SourceText[j];
                }
            }            
            return Result;
        }

        static string RailFenceDecrypt(string SourceText, int key)
        {
            string Result = "";
            int Period = 2 * (key - 1);
            int SourceLength = SourceText.Length;
            int Mod;
            int i, j, Counter, Ind;
            string[] Cipher = new string[key];
            bool isDown;
            for (i = 0; i < key; i++)
            {
                Cipher[i] = "";
            }
            Ind = 0;
            for (i = 1; i <= key; i++)
            {
                for (j = 0, Counter = 0; j < SourceLength; j++)
                {
                    Mod = (j + 1) % Period;
                    if (Mod == 0)
                        Mod = Period;
                    if (Mod > key)
                    {
                        Mod = key - (Mod - key);
                    }
                    if (Mod == i)
                        Counter++;
                }
                for (j = 0; j < Counter; j++)
                {
                    Cipher[i - 1] += SourceText[Ind++];
                }
            }
            isDown = true;
            Counter = 0;
            for (i = 0; Counter < SourceLength;)
            {
                Result += Cipher[i][0];
                Cipher[i] = Cipher[i].Substring(1);
                Counter++;
                if ((i + 1) == key)
                    isDown = false;
                if ((i + 1) == 1)
                    isDown = true;
                if (isDown)
                    i++;
                else
                    i--;          
            }
            return Result;
        }

        static string RotatingLatticeEncrypt(string SourceText, bool[,] Mask)
        {
            string Result = "";
            int i, j, k, Ind = 0;
            while (SourceText.Length % 25 != 0)
                SourceText += " ";
            int SourceLength = SourceText.Length;
            char[,] Cipher = new char[5, 5];
            int NumOfBlocks = SourceLength / 25;

            for (k = 0; k < NumOfBlocks; k++)
            {
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        if (Mask[i, j])
                            Cipher[i, j] = SourceText[Ind++];
                    }
                }
                Mask[2, 2] = false;
                for (j = 0; j < 5; j++)
                {
                    for (i = 4; i >= 0; i--)
                    {
                        if (Mask[i, j])
                            Cipher[j, 4 - i] = SourceText[Ind++];
                    }
                }
                for (i = 4; i >= 0; i--)
                {
                    for (j = 4; j >= 0; j--)
                    {
                        if (Mask[i, j])
                            Cipher[4 - i, 4 - j] = SourceText[Ind++];
                    }
                }
                
                for (j = 4; j >= 0; j--)
                {
                    for (i = 0; i < 5; i++)
                    {
                        if (Mask[i, j])
                            Cipher[4 - j, i] = SourceText[Ind++];
                    }
                }
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        Result += Cipher[i, j];
                    }
                }
                Mask[2, 2] = true;
            }
            
            return Result;
        }

        static string RotatingLatticeDecrypt(string SourceText, bool[,] Mask)
        {
            string Result = "";
            int i, j, k, Ind = 0;
            //while (SourceText.Length % 25 != 0)
            //    SourceText += " ";
            int SourceLength = SourceText.Length;
            char[,] Cipher = new char[5, 5];
            int NumOfBlocks = SourceLength / 25;

            for (k = 0; k < NumOfBlocks; k++)
            {
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        Cipher[i, j] = SourceText[Ind++];
                    }
                }
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        if (Mask[i, j])
                            Result += Cipher[i, j];
                    }
                }
                Mask[2, 2] = false;
                for (j = 0; j < 5; j++)
                {
                    for (i = 4; i >= 0; i--)
                    {
                        if (Mask[i, j])
                            Result += Cipher[j, 4 - i];
                    }
                }
                for (i = 4; i >= 0; i--)
                {
                    for (j = 4; j >= 0; j--)
                    {
                        if (Mask[i, j])
                           Result += Cipher[4 - i, 4 - j];
                    }
                }

                for (j = 4; j >= 0; j--)
                {
                    for (i = 0; i < 5; i++)
                    {
                        if (Mask[i, j])
                            Result += Cipher[4 - j, i];
                    }
                }                
                Mask[2, 2] = true;
            }

            return Result;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)RailFenceRadioButton.IsChecked)
            {
                ResultText.Text = RailFenceEncrypt(SourceText.Text, Int32.Parse(Key.Text));
            }
            else if ((bool)СolumnRadioButton.IsChecked)
            {
                ResultText.Text = ColumnEncrypt(SourceText.Text, Key.Text.ToUpper());
            }
            else if ((bool)RotatingLatticeRadioButton.IsChecked)
            {
                bool[,] Mask = new bool[5, 5];
                int i, j;
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        Mask[i, j] = false;
                    }
                }
                i = 0;
                j = 0;
                foreach (FrameworkElement bufButton in wrapPanel.Children)
                {
                    if (bufButton is RadioButton)
                    {
                        RadioButton radioButton = (RadioButton)bufButton;
                        if ((bool)radioButton.IsChecked)
                        {
                            Mask[i, j] = true;
                        }
                        else
                        {
                            Mask[i, j] = false;

                        }
                        if (j < 4)
                            j++;
                        else
                        {
                            j = 0;
                            i++;
                        }
                    }
                }

                ResultText.Text = RotatingLatticeEncrypt(SourceText.Text, Mask);

            }
            else if ((bool)CaesarRadioButton.IsChecked)
            {
                ResultText.Text = CaesarEncrypt(SourceText.Text, Int32.Parse(Key.Text));
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)RailFenceRadioButton.IsChecked)
            {
                ResultText.Text = RailFenceDecrypt(SourceText.Text, Int32.Parse(Key.Text));
            }
            else if ((bool)СolumnRadioButton.IsChecked)
            {
                ResultText.Text = ColumnDecrypt(SourceText.Text, Key.Text.ToUpper());
            }
            else if ((bool)RotatingLatticeRadioButton.IsChecked)
            {

                bool[,] Mask = new bool[5, 5];
                int i, j;
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        Mask[i, j] = false;
                    }
                }
                i = 0;
                j = 0;
                foreach (FrameworkElement bufButton in wrapPanel.Children)
                {
                    if (bufButton is RadioButton)
                    {
                        RadioButton radioButton = (RadioButton)bufButton;
                        if ((bool)radioButton.IsChecked)
                        {
                            Mask[i, j] = true;
                        }
                        else
                        {
                            Mask[i, j] = false;

                        }
                        if (j < 4)
                            j++;
                        else
                        {
                            j = 0;
                            i++;
                        }
                    }
                }

                ResultText.Text = RotatingLatticeDecrypt(SourceText.Text, Mask);

            }
            else if ((bool)CaesarRadioButton.IsChecked)
            {
                ResultText.Text = CaesarDecrypt(SourceText.Text, Int32.Parse(Key.Text));
            }         
        }

        private void RotatingLatticeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            wrapPanel.Visibility = Visibility.Visible;
            Key.IsEnabled = false;
        }

        private void CaesarRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            wrapPanel.Visibility = Visibility.Hidden;
            Key.IsEnabled = true;
        }

        private void СolumnRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            wrapPanel.Visibility = Visibility.Hidden;
            Key.IsEnabled = true;
        }

        private void RailFenceRadioButton_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel.Visibility = Visibility.Hidden;
            Key.IsEnabled = true;
        }
    }
}
