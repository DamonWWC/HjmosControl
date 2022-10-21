using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
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
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// ExcelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExcelWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern int MessageBeep(uint uType);

        public ExcelWindow()
        {
            InitializeComponent();

            //this.Loaded += ExcelWindow_Loaded;
            

        }
        SpeechSynthesizer synth;
        private void ExcelWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
            synth = new SpeechSynthesizer(); 

            //synth.SelectVoice("Microsoft Lili");
            //synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Child);
            synth.Volume = 100;
            synth.Rate = 2;
            //synth.SetOutputToDefaultAudioDevice();
            synth.SpeakAsync("你好，当前有一条应急防汛报警信息，请及时处理！");
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //uint beep = 0x00000030;
            //MessageBeep(beep);
            //System.Media.SystemSounds.Exclamation.Play();
            //System.Media.SystemSounds.Beep.Play();
            //SoundPlayer player = new SoundPlayer(@"C:\Windows\media\Windows Proximity Notification.wav");
            //player.Play();

            //player.PlayLooping();
            var bb = Application.Current.MainWindow.Width;
            var ss = this.Width;
            //synth = new SpeechSynthesizer();
            ////synth.SelectVoice("Microsoft Lili");
            ////synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Child);
            //synth.Volume = 100;
            //synth.Rate = 1;
            //synth.SpeakCompleted += Synth_SpeakCompleted;
            ////synth.SetOutputToDefaultAudioDevice();
            
            //Speak();

        }

        private void Speak()
        {
            synth.SpeakAsync("你好，当前有一条应急防汛报警信息，请及时处理！");
        }

        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            Speak();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            synth.Dispose();
            
        }
    }
}
