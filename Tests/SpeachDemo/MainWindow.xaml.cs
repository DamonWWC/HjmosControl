using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
using System.Xml;

namespace SpeachDemo
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
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            Task.Run(async () =>
            {
                SpeechSynthesizer synth;
                int n = 3;
                while (n-- > 0)
                {
                    synth = new SpeechSynthesizer();
                    //synth.SelectVoice("Microsoft Lili");
                    //synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Child);
                    synth.Volume = 100;
                    synth.Rate = 1;
                    synth.SetOutputToDefaultAudioDevice();

                    synth.SpeakAsync("你好,你好你好你好你好你好你好你好你好你好你好你好你好");

                    synth.Speak("你好");
                   await Task.Delay(5000);
                }
  
            });
          
        }
        

        private void Speech(string text,int num)
        {

        }
    }
}
