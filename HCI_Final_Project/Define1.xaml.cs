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
    /// Interaction logic for Define1.xaml
    /// </summary>
    public partial class Define1 : Window
    {
        public Define1()
        {
            InitializeComponent();
        }
        public string _word;


        private void bttnDefine_Click(object sender, RoutedEventArgs e)
        {
            //This is where we will need to do our work to define the given word
            //Possible links: https://dictionaryapi.com/ and https://dictionary-api.cambridge.org/api/resources#c# // will look into this later
            _word = wordToDefine.Text;
        }
    }
}
