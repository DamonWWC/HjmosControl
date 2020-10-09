//The MIT License(MIT)

//Copyright(c) 2016 Alberto Rodriguez & LiveCharts Contributors

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using LiveCharts.Charts;
using LiveCharts.Definitions.Points;
using LiveCharts.Dtos;

namespace LiveCharts.Wpf.Points
{
    internal class RowPointView : PointView, IRectanglePointView
    {
        public Rectangle Rectangle { get; set; }

        public Rectangle RectangleShadow { get; set; }

        /// <summary>
        /// 修改
        /// </summary>
        public ContentControl DataLabelEnd { get; set; }
        public CoreRectangle Data { get; set; }
        public double ZeroReference  { get; set; }
        public BarLabelPosition LabelPosition { get; set; }
        private RotateTransform Transform { get; set; }

        public override void DrawOrMove(ChartPoint previousDrawn, ChartPoint current, int index, ChartCore chart)
        {
            double ShadowWidth=0 ;
            if (IsNew)
            {
                Canvas.SetTop(Rectangle, Data.Top);
                Canvas.SetLeft(Rectangle, ZeroReference);

                Rectangle.Width = 0;
                Rectangle.Height = Data.Height;
                //if(RectangleShadow!=null)
                //{
                //    Canvas.SetTop(RectangleShadow, Data.Top);
                //    Canvas.SetLeft(RectangleShadow, ZeroReference);

                //    RectangleShadow.Width = 0;
                //    RectangleShadow.Height = Data.Height;
                //}
                  
            }

            if (DataLabel != null && double.IsNaN(Canvas.GetLeft(DataLabel)))
            {
                Canvas.SetTop(DataLabel, Data.Top);
                Canvas.SetLeft(DataLabel, ZeroReference);
            }

            Func<double> getY = () =>
            {
                if (LabelPosition == BarLabelPosition.Perpendicular)
                {
                    if (Transform == null)
                        Transform = new RotateTransform(270);

                    DataLabel.RenderTransform = Transform;
                    return Data.Top + Data.Height/2 + DataLabel.ActualWidth*.5;
                }
                //修改
                if(LabelPosition==BarLabelPosition.LeftBottom)
                {
                    var rr= Data.Top + Data.Height;
                    if (rr < 0) rr = 2;
                    //if (rr + DataLabel.ActualHeight > chart.DrawMargin.Height)
                    //    rr -= rr + DataLabel.ActualHeight - chart.DrawMargin.Height + 2;
                    return rr;
                }

                var r = Data.Top + Data.Height / 2 - DataLabel.ActualHeight / 2;

                if (r < 0) r = 2;
                if (r + DataLabel.ActualHeight > chart.DrawMargin.Height)
                    r -= r + DataLabel.ActualHeight - chart.DrawMargin.Height + 2;

                return r;
            };
            //修改
            Func<double> getYend = () =>
            {
                var r = Data.Top + Data.Height / 2 - DataLabelEnd.ActualHeight / 2;

                if (r < 0) r = 2;
                if (r + DataLabelEnd.ActualHeight > chart.DrawMargin.Height)
                    r -= r + DataLabelEnd.ActualHeight - chart.DrawMargin.Height + 2;

                return r;
            };
            Func<double> getX = () =>
            {
                double r;

#pragma warning disable 618
                if (LabelPosition == BarLabelPosition.Parallel || LabelPosition == BarLabelPosition.Merged)
#pragma warning restore 618
                {
                    r = Data.Left + Data.Width/2 - DataLabel.ActualWidth/2;
                }
                else if (LabelPosition == BarLabelPosition.Perpendicular)
                {
                    r = Data.Left + Data.Width/2 - DataLabel.ActualHeight/2;
                }
                //修改
                else if (LabelPosition == BarLabelPosition.EndPosition)
                {
                    r = chart.DrawMargin.Width - DataLabel.ActualWidth;
                }
                else if(LabelPosition==BarLabelPosition.LeftBottom)
                {
                    r = Data.Left+5;
                }
                else
                {
                    if (Data.Left < ZeroReference)
                    {
                        r = Data.Left - DataLabel.ActualWidth - 5;
                        if (r < 0) r = Data.Left + 5;
                    }
                    else
                    {
                        r = Data.Left + Data.Width + 5;
                        if (r + DataLabel.ActualWidth > chart.DrawMargin.Width)
                            r -= DataLabel.ActualWidth + 10;
                    }
                }

                return r;
            };
            //修改设置DataLabel的位置
            Func<double> getXend = () =>
            {
                double r = chart.DrawMargin.Width - 50;
                return r;
            };
            if (chart.View.DisableAnimations)
            {
                Rectangle.Width = Data.Width;
                Rectangle.Height = Data.Height;

                Canvas.SetTop(Rectangle, Data.Top);
                Canvas.SetLeft(Rectangle, Data.Left);

                if(RectangleShadow!=null)
                {
                    ShadowWidth= chart.DrawMargin.Width - ((RowSeries)current.SeriesView).PercentageWith;
                    RectangleShadow.Width = ShadowWidth;
                    RectangleShadow.Height = Data.Height;

                    Canvas.SetTop(RectangleShadow, Data.Top);
                    Canvas.SetLeft(RectangleShadow, Data.Left);
                }
              
                if (DataLabel != null)
                {
                    DataLabel.UpdateLayout();

                    Canvas.SetTop(DataLabel, getY());
                    Canvas.SetLeft(DataLabel, getX());
                }
                //修改
                if (DataLabelEnd != null)
                {
                    DataLabelEnd.UpdateLayout();
                    Canvas.SetTop(DataLabelEnd, getYend());
                    Canvas.SetLeft(DataLabelEnd, getXend());
                }
                if (HoverShape != null)
                {
                    Canvas.SetTop(HoverShape, Data.Top);
                    Canvas.SetLeft(HoverShape, Data.Left);
                    HoverShape.Height = Data.Height;
                    HoverShape.Width = Data.Width;
                }

                return;
            }

            var animSpeed = chart.View.AnimationsSpeed;

            if (DataLabel != null)
            {
                DataLabel.UpdateLayout();

                DataLabel.BeginAnimation(Canvas.LeftProperty, new DoubleAnimation(getX(), animSpeed));
                DataLabel.BeginAnimation(Canvas.TopProperty, new DoubleAnimation(getY(), animSpeed));
            }
            //修改
            if (DataLabelEnd != null)
            {
                DataLabelEnd.UpdateLayout();

                DataLabelEnd.BeginAnimation(Canvas.LeftProperty, new DoubleAnimation(getXend(), animSpeed));
                DataLabelEnd.BeginAnimation(Canvas.TopProperty, new DoubleAnimation(getYend(), animSpeed));
            }
            if(RectangleShadow!=null)
            {
               
                RectangleShadow.BeginAnimation(Canvas.TopProperty,
                    new DoubleAnimation(Data.Top, animSpeed));
                RectangleShadow.BeginAnimation(Canvas.LeftProperty,
                    new DoubleAnimation(Data.Left, animSpeed));

                RectangleShadow.BeginAnimation(FrameworkElement.HeightProperty,
                    new DoubleAnimation(Data.Height, animSpeed));
                RectangleShadow.BeginAnimation(FrameworkElement.WidthProperty,
                    new DoubleAnimation(ShadowWidth, animSpeed));
            }
            Rectangle.BeginAnimation(Canvas.TopProperty, 
                new DoubleAnimation(Data.Top, animSpeed));
            Rectangle.BeginAnimation(Canvas.LeftProperty,
                new DoubleAnimation(Data.Left, animSpeed));

            Rectangle.BeginAnimation(FrameworkElement.HeightProperty, 
                new DoubleAnimation(Data.Height, animSpeed));
            Rectangle.BeginAnimation(FrameworkElement.WidthProperty,
                new DoubleAnimation(Data.Width, animSpeed));



            if (HoverShape != null)
            {
                Canvas.SetTop(HoverShape, Data.Top);
                Canvas.SetLeft(HoverShape, Data.Left);
                HoverShape.Height = Data.Height;
                HoverShape.Width = Data.Width;
            }
        }

        public override void RemoveFromView(ChartCore chart)
        {
            chart.View.RemoveFromDrawMargin(HoverShape);
            chart.View.RemoveFromDrawMargin(Rectangle);
            chart.View.RemoveFromDrawMargin(RectangleShadow);
            chart.View.RemoveFromDrawMargin(DataLabel);
            //修改
            chart.View.RemoveFromDrawMargin(DataLabelEnd);
        }

        public override void OnHover(ChartPoint point)
        {
            var copy = Rectangle.Fill.Clone();
            copy.Opacity -= .15;
            Rectangle.Fill = copy;
        }

        public override void OnHoverLeave(ChartPoint point)
        {
            if (Rectangle == null) return;

            if (point.Fill != null)
            {
                Rectangle.Fill = (Brush)point.Fill;
            }
            else
            {
                Rectangle.Fill = ((Series) point.SeriesView).Fill;
            }
        }
    }
}
