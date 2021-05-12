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
using System.Net.Http;
using System.Threading.Tasks;

namespace HCI_Final_Project
{
    /// <summary>
    /// Interaction logic for Define2.xaml
    /// </summary>
    public partial class Define2 : Window
    {
        public Define2()
        {
            InitializeComponent();
        }


        //0. Word
        //1. Definition of word
        //2. Syllables
        //3. Part of Speech
        //4. AudioAccessPoint
        public Define2(List<string> list)
        {
            InitializeComponent();
            wordLabel.Content = list[0];
            defLabel.Text = list[1];
            syllableLabel.Content = list[2];
            partOfSpeechLabel.Content = list[3];
            //examples.Text = list[4];
            playSound(list[4]);
            
        }

        public void playSound(string audioAccessPoint)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = audioAccessPoint;
            player.Play();
        }

    }
}
