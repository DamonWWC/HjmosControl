﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = ElementHourList, Type = typeof(System.Windows.Controls.ListBox))]
    [TemplatePart(Name = ElementMinuteList, Type = typeof(System.Windows.Controls.ListBox))]
    [TemplatePart(Name = ElementSecondList, Type = typeof(System.Windows.Controls.ListBox))]
    [TemplatePart(Name = ElementTimeStr, Type = typeof(TextBlock))]

    public class ListClock : ClockBase
    {
        #region Constants
       
        private const string ElementHourList = "PART_HourList";
        private const string ElementMinuteList = "PART_MinuteList";
        private const string ElementSecondList = "PART_SecondList";
        private const string ElementTimeStr = "PART_TimeStr";

        #endregion Constants

        #region Data

        private System.Windows.Controls.ListBox _hourList;

        private System.Windows.Controls.ListBox _minuteList;

        private System.Windows.Controls.ListBox _secondList;
        private TextBlock _blockTime;

        #endregion Data

        public override void OnApplyTemplate()
        {
            AppliedTemplate = false;
            if (ButtonConfirm != null)
            {
                ButtonConfirm.Click -= ButtonConfirm_OnClick;
            }

            if (_hourList != null)
            {
                _hourList.SelectionChanged -= HourList_SelectionChanged;
            }

            if (_minuteList != null)
            {
                _minuteList.SelectionChanged -= MinuteList_SelectionChanged;
            }

            if (_secondList != null)
            {
                _secondList.SelectionChanged -= SecondList_SelectionChanged;
            }

            base.OnApplyTemplate();

            

            _hourList = GetTemplateChild(ElementHourList) as System.Windows.Controls.ListBox;
            if (_hourList != null)
            {
                CreateItemsSource(_hourList, 24);
                _hourList.SelectionChanged += HourList_SelectionChanged;
            }

            _minuteList = GetTemplateChild(ElementMinuteList) as System.Windows.Controls.ListBox;
            if (_minuteList != null)
            {
                CreateItemsSource(_minuteList, 60);
                _minuteList.SelectionChanged += MinuteList_SelectionChanged;
            }

            _secondList = GetTemplateChild(ElementSecondList) as System.Windows.Controls.ListBox;
            if (_secondList != null)
            {
                CreateItemsSource(_secondList, 60);
                _secondList.SelectionChanged += SecondList_SelectionChanged;
            }

            ButtonConfirm = GetTemplateChild(ElementButtonConfirm) as Button;
            _blockTime = GetTemplateChild(ElementTimeStr) as TextBlock;

            if (ButtonConfirm != null)
            {
                ButtonConfirm.Click += ButtonConfirm_OnClick;
            }

            AppliedTemplate = true;
            if (SelectedTime.HasValue)
            {
                Update(SelectedTime.Value);
            }
            else
            {
                DisplayTime = DateTime.Now;
                Update(DisplayTime);
            }
        }

        private void HourList_SelectionChanged(object sender, SelectionChangedEventArgs e) => Update();

        private void MinuteList_SelectionChanged(object sender, SelectionChangedEventArgs e) => Update();

        private void SecondList_SelectionChanged(object sender, SelectionChangedEventArgs e) => Update();

        private void CreateItemsSource(ItemsControl selector, int count)
        {
            var list = new List<string>();
            for (var i = 0; i < count; i++)
            {
                list.Add(i.ToString("#00"));
            }

            selector.ItemsSource = list;
        }

        private void Update()
        {
            if (_hourList.SelectedIndex >= 0 && _hourList.SelectedIndex < 24 &&
                _minuteList.SelectedIndex >= 0 && _minuteList.SelectedIndex < 60 &&
                _secondList.SelectedIndex >= 0 && _secondList.SelectedIndex < 60)
            {
                var now = DateTime.Now;
                DisplayTime = new DateTime(now.Year, now.Month, now.Day, _hourList.SelectedIndex,
                    _minuteList.SelectedIndex, _secondList.SelectedIndex);
                _blockTime.Text = DisplayTime.ToString(TimeFormat);
            }
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="time"></param>
        internal override void Update(DateTime time)
        {
            if (!AppliedTemplate) return;

            var h = time.Hour;
            var m = time.Minute;
            var s = time.Second;

            _hourList.SelectedIndex = h;
            _minuteList.SelectedIndex = m;
            _secondList.SelectedIndex = s;

            _hourList.ScrollIntoView(_hourList.SelectedItem);
            _minuteList.ScrollIntoView(_minuteList.SelectedItem);
            _secondList.ScrollIntoView(_secondList.SelectedItem);

            DisplayTime = time;
        }
    }
}
