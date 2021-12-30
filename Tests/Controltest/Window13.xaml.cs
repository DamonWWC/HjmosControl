﻿using Hjmos.BaseControls;
using Hjmos.BaseControls.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// Window13.xaml 的交互逻辑
    /// </summary>
    public partial class Window13 : Window, INotifyPropertyChanged
    {
        public Window13()
        {
            InitializeComponent();

            RadarModels = new ObservableCollection<RadarModel>();
            Player theShy = new Player()
            {
                姓名 = "The Shy",
                击杀 = 80,
                助攻 = 50,
                物理 = 90,
                生存 = 10,
                金钱 = 60,
                防御 = 30,
                魔法 = 30
            };
            Type t = theShy.GetType();
            PropertyInfo[] pArray = t.GetProperties();
            pArray = pArray.Where(it => it.PropertyType == typeof(int)).ToArray();

            var collectionpPayer = new ObservableCollection<RadarModel>();
            Array.ForEach(pArray, p =>
            {
                collectionpPayer.Add(new RadarModel { Text = $"{p.Name}（{(int)p.GetValue(theShy, null)}分）", ValueMax = (int)p.GetValue(theShy, null) });
            });
            RadarModels = collectionpPayer;

            Indicators = new List<Indicator> { new Indicator { Name="A组"  }, new Indicator { Name = "B组" },
            new Indicator { Name="C组" },new Indicator { Name="D组" },new Indicator { Name="E组" },new Indicator { Name="F组" }};

            RadarSeries = new List<RadarSeries>
            {
                new RadarSeries{ SeriesName="11",Values=new List<double>{ 30,44,55,66,33,22} }
            };

            DataContext = this;
        }

        public List<Indicator> Indicators { get; set; }

        // private ObservableCollection<RadarModel> _RadarModels;
        public ObservableCollection<RadarModel> RadarModels
        {
            get;
            set;
        }

        private List<RadarSeries> _RadarSeries;

        public List<RadarSeries> RadarSeries
        {
            get { return _RadarSeries; }
            set { _RadarSeries = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RadarSeries[0].Values = new List<double> { 70, 34, 95, 26, 33, 72 };
        }
    }

    public class Player
    {
        public string 姓名 { get; set; }
        public int 击杀 { get; set; }
        public int 生存 { get; set; }
        public int 助攻 { get; set; }
        public int 物理 { get; set; }
        public int 魔法 { get; set; }
        public int 防御 { get; set; }
        public int 金钱 { get; set; }
    }
}