using Microsoft.VisualBasic.Devices;
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

            this.Loaded += ExcelWindow_Loaded;
            

        }
        SpeechSynthesizer synth;
        private void ExcelWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
            //synth = new SpeechSynthesizer();
            ////synth.SelectVoice("Microsoft Lili");
            ////synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Child);
            //synth.Volume = 100;
            synth.Rate = 2;

            //synth.SetOutputToDefaultAudioDevice();
           // synth.SpeakAsync("你好，");
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //    uint beep = 0x00000030;
            //    MessageBeep(beep);
            //    System.Media.SystemSounds.Exclamation.Play();
            //    System.Media.SystemSounds.Beep.Play();



            var path = "D:\\A佳都工作资料\\在线项目\\HJMosClient\\Client\\CS6\\HJMos_NCC_Client\\Work\\Hjmos_Client\\Files\\Audio\\20221019361931699390464.mp3";

            //SoundPlayer player = new SoundPlayer(@"C:\Windows\media\Windows Proximity Notification.wav");
            SoundPlayer player = new SoundPlayer(path);
            //player.Play();
       
            player.PlayLooping();




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


    /// <summary>
    /// 播放MP3文件
    /// </summary>
    public class Mp3Player
    {
        //定义API函数使用的字符串变量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        private string Name = "";

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        private string durLength = "";

        [MarshalAs(UnmanagedType.LPTStr, SizeConst = 128)]
        private string TemStr = "";

        private int ilong;

        //定义播放状态枚举变量
        public enum State
        {
            mPlaying = 1,
            mPuase = 2,
            mStop = 3
        };

        //结构变量
        public struct structMCI
        {
            public bool bMut;
            public int iDur;
            public int iPos;
            public int iVol;
            public int iBal;
            public string iName;
            public State state;
        };

        public structMCI mc = new structMCI();

        //取得播放文件属性
        public string FileName
        {
            get
            {
                return mc.iName;
            }
            set
            {
                //ASCIIEncoding asc = new ASCIIEncoding();
                try
                {
                    TemStr = "";
                    TemStr = TemStr.PadLeft(127, Convert.ToChar(" "));
                    Name = Name.PadLeft(260, Convert.ToChar(" "));
                    mc.iName = value;
                    ilong = APIClass.GetShortPathName(mc.iName, Name, Name.Length);
                    Name = GetCurrPath(Name);
                    //Name = "open " + Convert.ToChar(34) + Name + Convert.ToChar(34) + " alias media";
                    Name = "open " + Convert.ToChar(34) + Name + Convert.ToChar(34) + " alias media";
                    ilong = APIClass.mciSendString("close all", TemStr, TemStr.Length, 0);
                    ilong = APIClass.mciSendString(Name, TemStr, TemStr.Length, 0);
                    ilong = APIClass.mciSendString("set media time format milliseconds", TemStr, TemStr.Length, 0);
                    mc.state = State.mStop;
                }
                catch
                {
                }
            }
        }

        //播放
        public void play()
        {
            TemStr = "";
            TemStr = TemStr.PadLeft(127, Convert.ToChar(" "));
            APIClass.mciSendString("play media", TemStr, TemStr.Length, 0);
            mc.state = State.mPlaying;
        }

        //停止
        public void StopT()
        {
            TemStr = "";
            TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
            ilong = APIClass.mciSendString("close media", TemStr, 128, 0);
            ilong = APIClass.mciSendString("close all", TemStr, 128, 0);
            mc.state = State.mStop;
        }

        public void Puase()
        {
            TemStr = "";
            TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
            ilong = APIClass.mciSendString("pause media", TemStr, TemStr.Length, 0);
            mc.state = State.mPuase;
        }

        private string GetCurrPath(string name)
        {
            if (name.Length < 1) return "";
            name = name.Trim();
            name = name.Substring(0, name.Length - 1);
            return name;
        }

        //总时间
        public int Duration
        {
            get
            {
                durLength = "";
                durLength = durLength.PadLeft(128, Convert.ToChar(" "));
                APIClass.mciSendString("status media length", durLength, durLength.Length, 0);
                durLength = durLength.Trim();
                if (durLength == "") return 0;
                return (int)(Convert.ToDouble(durLength) / 1000f);
            }
        }

        //当前时间
        public int CurrentPosition
        {
            get
            {
                durLength = "";
                durLength = durLength.PadLeft(128, Convert.ToChar(" "));
                APIClass.mciSendString("status media position", durLength, durLength.Length, 0);
                mc.iPos = (int)(Convert.ToDouble(durLength) / 1000f);
                return mc.iPos;
            }
        }
    }

    public class APIClass
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
        string lpszLongPath,
        string shortFile,
        int cchBuffer
        );

        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
        string lpstrCommand,
        string lpstrReturnString,
        int uReturnLength,
        int hwndCallback
        );
    }
}
