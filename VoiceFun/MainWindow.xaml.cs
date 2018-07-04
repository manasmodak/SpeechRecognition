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
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace VoiceFun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(); //for text - specch

        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine(); // recognize the speech

        public MainWindow()
        {
            InitializeComponent();

            Choices choices = new Choices();
            choices.Add(new string[] {"How are you", "How is the weather today", "What is your Name" });
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(choices);

            Grammar grammar = new Grammar(grammarBuilder);

            recognitionEngine.LoadGrammarAsync(grammar);
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.SpeechRecognized += RecognitionEngine_SpeechRecognized;
        }

        private void RecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string answer = string.Empty;
            switch (e.Result.Text)
            {
                case "How are you":
                    answer = "I am good, thanks.";
                    inputText.Text += "\n" + answer; //show in text box
                    speechSynthesizer.Speak(answer); // speak out as well
                    break;

                case "How is the weather today":
                    answer = "Superb to go out, enjoy.";
                    inputText.Text += "\n" + answer;   //show in text box
                    speechSynthesizer.Speak(answer);   // speak out as well
                    break;

                case "What is your Name":
                    answer = "My name is Bond, James Bond.";
                    inputText.Text += "\n" + answer;   //show in text box
                    speechSynthesizer.Speak(answer);   // speak out as well
                    break;

                    //default:
                    //    answer = "Sorry, I am not aware of that.";
                    //    inputText.Text += "\n" + answer;
                    //    speechSynthesizer.Speak(answer);
                    //    break;
            }
        }

        private void btn_Talk_Clicked(object sender, RoutedEventArgs e)
        {
            speechSynthesizer.Speak(inputText.Text);
        }

        private void btn_StartSpeak_Clicked(object sender, RoutedEventArgs e)
        {
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            startSpeak.IsEnabled = false;
            stopSpeak.IsEnabled = true;
        }

        private void btn_StopSpeak_Clicked(object sender, RoutedEventArgs e)
        {
            recognitionEngine.RecognizeAsyncStop();
            Button start = sender as Button;
            stopSpeak.IsEnabled = false;
            startSpeak.IsEnabled = true;
        }
    }
}
