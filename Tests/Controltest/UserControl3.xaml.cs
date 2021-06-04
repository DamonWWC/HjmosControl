﻿using System;
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

namespace Controltest
{
    /// <summary>
    /// UserControl3.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
            Type = new List<string> { "PSD", "CCTV", "AFC", "PSD", "CCTV", "AFC", "PSD", "CCTV", "AFC", "PSD", "CCTV", "AFC", "PSD", "CCTV", "AFC" };
            DataContext = this;
        }

        private List<string> _Type;

        public List<string> Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }
}
