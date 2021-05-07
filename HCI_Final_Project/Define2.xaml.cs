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

        public  Define2(String wordtoDefine)
        {
            InitializeComponent();
            word = wordtoDefine;
            wordLabel.Content = word;
            defineWord(word);
            
        }


        public async void defineWord(string word)
        {
            Console.WriteLine("Getting to define word Yay!!!");
            using (var client = new HttpClient())
            {
                try
                {
                    //var stringContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");
                    string urlToGetTo = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
                    urlToGetTo += word;
                    urlToGetTo += "?key=8e6c9461-437f-4dea-93f7-9b0cd218d5a0";
                    var response = await client.GetAsync(urlToGetTo);
                    var result = await response.Content.ReadAsStringAsync();
                    
                    parseDefinition(result);
                    playAudio(result);
                    defLabel.Content = result;
                }
                catch (Exception ex)
                {
                    //TODO: popup an error message
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }


        //This methhod will parse the API response into 3 objects
        //  1. Definition
        //  2. Syllable layout
        public void parseDefinition(string word_response)
        {

            //2.
            int indexOfSyllable = word_response.IndexOf("\"hw\":");
            indexOfSyllable = indexOfSyllable + 5;
            int indexOfSyllableEnd = word_response.IndexOf(',', indexOfSyllable);
            int syllableLength = indexOfSyllableEnd - indexOfSyllable;
            string syllable = word_response.Substring(indexOfSyllable, syllableLength);
            syllableLabel.Content = syllable;
            //syllableLabel.FontFamily = System.Windows.Media.FontFamily("Arial");
            syllableLabel.FontSize = 24;
        }
        
        public void playAudio(string word_response)
        {
            int indexOfAudio = word_response.IndexOf("\"audio\":");
            indexOfAudio = indexOfAudio + 9;
            int indexOfAudioEnd = word_response.IndexOf(",", indexOfAudio);
            indexOfAudioEnd = indexOfAudioEnd - 1;
            int audioLength = indexOfAudioEnd - indexOfAudio;
            string audiofile = word_response.Substring(indexOfAudio, audioLength);
            string audioAccessPoint = "https://media.merriam-webster.com/audio/prons/en/us/wav/";

            string subDir = getSubDir(audiofile);
            

            audioAccessPoint += subDir + "/" + audiofile + ".wav";

            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = audioAccessPoint;
            player.Play();
        }

        public string getSubDir(string audio)
        {
            string subDir = "";
            if (32 < audio[0] && audio[0] < 60)
            {
                subDir = "number";
                return subDir;
            }
            else
            {
                if (audio.Length > 2)
                {
                    if (audio[0] == 98 && audio[1] == 105 && audio[2] == 120)
                    {
                        subDir = "bix";
                        return subDir;
                    }
                    else
                    {
                        if (audio[0] == 103 && audio[1] == 103)
                        {
                            subDir = "gg";
                            return subDir;
                        }
                    }
                }
            }
            return audio[0].ToString();
        }
      
    }
}
