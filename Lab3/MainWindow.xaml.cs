using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace TILab3
{

    struct Message
    {
        public string Text;
        public BigInteger Signature;
    }

    public partial class MainWindow : Window
    {

        Keys key = new Keys();
        Message message;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            key.GenerateKeys();
            textPubKey.Text = key.PublicKey[0].ToString() + ", " + key.PublicKey[1].ToString();
            textPrivKey.Text = key.PrivateKey[0].ToString() + ", " + key.PrivateKey[1].ToString();
        }

        private void btnHashAndSend_Click(object sender, RoutedEventArgs e)
        {
            message.Text = textMessage.Text;
            message.Signature = Algorithm(GenerateHash(message.Text), key.PrivateKey);
            textSignature.Text = message.Signature.ToString();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            message.Text = textMessage.Text;
        }

        private BigInteger GenerateHash(string Message)
        {
            BigInteger p = 8389, q = 9431, H = 1000, n;
            n = p * q;
            for (int i = 0; i < Message.Length; i++)
            {
                H = Power(H + Message[i], 2, n);
            }
            return H;
        }

        static BigInteger Algorithm(BigInteger Hash, long[] Key)
        {
            BigInteger Result;

            Result = Power(Hash, Key[0], Key[1]);

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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BigInteger MessageHash = GenerateHash(message.Text);
            BigInteger SignatureHash = Algorithm(message.Signature, key.PublicKey);
            if (MessageHash == SignatureHash)
            {
                labelResult.Content = "Correct.";
            }
            else
            {
                labelResult.Content = "Incorrect. Message hash: " + MessageHash.ToString() + ". Hash form signature: " + SignatureHash.ToString() + ".";
            }
        }
    }
}
