using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Final_Project
{
    /// <summary>
    /// Interaction logic for Define2.xaml
    /// </summary>
    public partial class Define2 : Window
    {
        private string word;
        public Define2()
        {
            InitializeComponent();
        }

        public Define2(String wordtoDefine)
        {
            InitializeComponent();
            word = wordtoDefine;
            wordLabel.Content = word;
            
        }
    }
}
