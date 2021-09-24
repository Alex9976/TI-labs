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

namespace TILab4
{

    public partial class MainWindow : Window
    {
        TrustedCenter trustedCenter = new TrustedCenter();
        User user = new User();
        Reviewer reviewer = new Reviewer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void butGenerateN_Click(object sender, RoutedEventArgs e)
        {
            trustedCenter.GenerateN();
            textN.Text = trustedCenter.n.ToString();
        }

        private void butGenerateV_Click(object sender, RoutedEventArgs e)
        {
            user.Generate(trustedCenter.n);
            textV.Text = user.v.ToString();
            textS.Text = user.s.ToString();
        }

        private void butChangeS_Click(object sender, RoutedEventArgs e)
        {
            user.GenerateNewS();
            textS.Text = user.s.ToString();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (reviewer.Check(ref user))
            {
                labelResult.Content = "Pass";
            }
            else
            {
                labelResult.Content = "Fail";
            }
        }
    }
}
