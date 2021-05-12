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


        //Puts file from fileExplorer into textBlock to read from
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


        //M2 (someone clicked on a word)
        public async void textBlock_SelectionChanged(object sender, MouseButtonEventArgs e)
        { 
            RichTextBox txtBox = (RichTextBox)sender;      
            List<string> listForDefineWindow = new List<string>();
            listForDefineWindow = await define(txtBox.Selection.Text);
            var defineWindow = new Define2(listForDefineWindow);
            defineWindow.Show();
        }


        //example uses "Tuesday" as word to define
        public async Task<List<string>> define(string word)
        {
            Console.WriteLine("Getting to define word Yay!!!");
            List<string> toReturn = new List<string>();
            using (var client = new HttpClient())
            { 
                try
                {
                    //send API call
                    string urlToGetTo = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
                    urlToGetTo += word;
                    urlToGetTo += "?key=8e6c9461-437f-4dea-93f7-9b0cd218d5a0";
                    var response = await client.GetAsync(urlToGetTo);
                    var result = await response.Content.ReadAsStringAsync(); 

                    //fill our return value with results
                    toReturn = parseDefinition(result);
                    //by now, toRetun would look like this :  ["definition of Tuesday", "Tues*day", "noun"]
                    //  We still need to get audio
                    toReturn.Add(getAudioAccessPoint(result));
                    //by now, toRetun would look like this :  ["definition of Tuesday", "Tues*day", "noun", "audioAccessPointString"]
                    //  We still need to add the orignal word lookup to the beginning of our list
                    toReturn.Insert(0, word);
                    //by now, toRetun would look like this :  ["Tuesday", "definition of Tuesday", "Tues*day", "noun", "audioAccessPointString"]
                }
                catch (Exception ex)
                {
                    //TODO: popup an error message
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    return null;
                }
            }
            return toReturn;
        }


        //This methhod will parse the API response into 3 objects
        //  1. Definition
        //  2. Syllable layout
        //  3. Part of Speech

        // So the return value would be something like: ["definition of Tuesday", "Tues*day", "noun"]
        public List<string> parseDefinition(string word_response)
        {
            List<string> retval = new List<string>();
            //1.
            int indexOfDef = word_response.IndexOf("shortdef\":[");
            indexOfDef = indexOfDef + 11;
            int indexOfDefEnd = word_response.IndexOf("]", indexOfDef);
            int defLength = indexOfDefEnd - indexOfDef;
            string shortDef = word_response.Substring(indexOfDef, defLength);
            retval.Add(shortDef);
            //2.
            int indexOfSyllable = word_response.IndexOf("\"hw\":");
            indexOfSyllable = indexOfSyllable + 5;
            int indexOfSyllableEnd = word_response.IndexOf(',', indexOfSyllable);
            int syllableLength = indexOfSyllableEnd - indexOfSyllable;
            string syllable = word_response.Substring(indexOfSyllable, syllableLength);
            retval.Add(syllable);
            //3.
            int indexOfPartOfSpeech = word_response.IndexOf("\"fl\":");
            indexOfPartOfSpeech = indexOfPartOfSpeech + 6;
            int indexOfPartOfSpeechEnd = word_response.IndexOf(",", indexOfPartOfSpeech);
            indexOfPartOfSpeechEnd = indexOfPartOfSpeechEnd - 1;
            int partOfSpeechLength = indexOfPartOfSpeechEnd - indexOfPartOfSpeech;
            string partOfSpeech = word_response.Substring(indexOfPartOfSpeech, partOfSpeechLength);
            retval.Add(partOfSpeech);
            return retval;
        }

        public string getAudioAccessPoint(string word_response)
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
            return audioAccessPoint;

            //System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            //player.SoundLocation = audioAccessPoint;
            //player.Play();
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
