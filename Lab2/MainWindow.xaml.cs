using System.Numerics;
using System.Windows;

namespace TILab2
{

    public partial class MainWindow : Window
    {
        Keys key = new Keys();
        ulong[] Cipher;

        public MainWindow()
        {
            InitializeComponent();
        }


        static ulong[] Algorithm(ulong[] Source, long[] Key)
        {
            ulong[] Result = new ulong[Source.Length];

            for (int i = 0; i < Source.Length; i++)
            {
                Result[i] = (ulong)Power(Source[i], Key[0], Key[1]);
            }

            return Result;
        }

        static BigInteger Power(BigInteger a, BigInteger z, BigInteger n)
        {
            BigInteger a1 = a, z1 = z, x = 1;
            while (z1 != 0)
            {
                while (z1 % 2 == 0)
                {
                    z1 = z1 / 2;
                    a1 = (a1 * a1) % n;
                }
                z1 = z1 - 1;
                x = (x * a1) % n;
            }
            return x;
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {

            string InputText = textInput.Text;
            ulong[] Source = new ulong[InputText.Length];

            for (int i = 0; i < InputText.Length; i++)
            {
                Source[i] = InputText[i];
            }

            Cipher = Algorithm(Source, key.PublicKey);

            textCrypt.Text = "";

            for (int i = 0; i < InputText.Length; i++)
            {
                textCrypt.Text += Cipher[i] + " ";
            }

        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            ulong[] Text;

            Text = Algorithm(Cipher, key.PrivateKey);

            textDecrypt.Text = "";

            for (int i = 0; i < Text.Length; i++)
            {
                textDecrypt.Text += (char)Text[i];
            }
        }

        private void btnGenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            key.GenerateKeys();
            textPubKey.Text = key.PublicKey[0].ToString() + ", " + key.PublicKey[1].ToString();
            textPrivKey.Text = key.PrivateKey[0].ToString() + ", " + key.PrivateKey[1].ToString();
        }
    }
}
