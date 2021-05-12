using System;
using System.Collections.Generic;
using System.Net.Http;
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


        private async void bttnDefine_Click(object sender, RoutedEventArgs e)
        {
            //This is where we will need to do our work to define the given word
            string m1_word = wordToDefine.Text;
            HCI_Final_Project.MainWindow window = new HCI_Final_Project.MainWindow();
            List<string> listOfResults = await window.define(m1_word);
            definition.Text = listOfResults[1];
            pronunciation.Text = listOfResults[2];
            partOfSpeechLabel.Text = listOfResults[3];
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = listOfResults[4];
            player.Play();

        }
    }
}
