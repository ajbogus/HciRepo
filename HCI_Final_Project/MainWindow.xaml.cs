using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace HCI_Final_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileSelector_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".pdf";
            Nullable<bool> result = fileDialog.ShowDialog();

            if (result == true)
            {
                chosenFile.Text = fileDialog.FileName;
                fillTextBlock(chosenFile.Text);
            }
        }


        private void fillTextBlock(string filename)
        {
            string toPutInUI = "";
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                toPutInUI += line;
            }
            textBlock.Document.Blocks.Clear();
            textBlock.Document.Blocks.Add(new Paragraph(new Run(toPutInUI)));
            //textBlock.Text = toPutInUI;
        }

        private void defineBttn_Click(object sender, RoutedEventArgs e)
        {
            //Someone wants to define a word
            // Let's popup our "define1.xaml" window to give a definition
            var defineWindow = new Define1();
            defineWindow.Show();
        }

        private void textBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            //Empty
        }

        private void textBlock_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            RichTextBox txtBox = (RichTextBox)sender;
            var defineWindow = new Define2(txtBox.Selection.Text);
            defineWindow.Show();
        }
    }
}
