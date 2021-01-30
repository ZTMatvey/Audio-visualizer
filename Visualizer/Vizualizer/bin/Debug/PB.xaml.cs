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

namespace LedControllerWS2801
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PB : UserControl
    {


        public PB()
        {
            InitializeComponent();
        }

        public void Value(int Value)
        {
            progrsbar.Value = Value;
        }
        public void Maximum(int Maximum)
        {
            progrsbar.Maximum = Maximum;
        }
        public void Minimum(int Minimum)
        {
            progrsbar.Minimum = Minimum;
        }

    }
}

