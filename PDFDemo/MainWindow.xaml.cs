using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PDFDemo
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

            //            Document.Create(container =>
            //            {
            //                container.Page(page =>
            //                {
            //                    page.Size(PageSizes.A4);
            //                    page.Margin(2, Unit.Centimetre);
            //                    page.PageColor(Colors.White);
            //                    page.DefaultTextStyle(x => x.FontSize(20));

            //                    page.Header()
            //                    .AlignCenter()
            //                        .Text("Hello PDF!")
            //                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

            //                    page.Content()
            //                        .PaddingVertical(1, Unit.Centimetre)
            //                        .Column(x =>
            //                        {
            //                            x.Spacing(20);

            //                            x.Item().Text(Placeholders.LoremIpsum());
            //                            x.Item().Image(Placeholders.Image(200, 100));
            //                        });

            //                    //page.Footer()
            //                    //    .AlignCenter()
            //                    //    .Text(x =>
            //                    //    {
            //                    //        x.Span("Page ");
            //                    //        x.CurrentPageNumber();
            //                    //    });
            //                });
            //            })
            //.GeneratePdf("hello.pdf");


            var filePath = "invoice.pdf";
            var document = new InvoiceDocument();
            document.GeneratePdf(filePath);
            Process.Start("explorer.exe", filePath);
        }
    }

    public class InvoiceDocument : IDocument
    {
        public InvoiceDocument()
        {

        }
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                //page.PageColor(QuestPDF.Helpers.Colors.Red.Lighten1);
                page.Size(PageSizes.A4);
                page.Margin(10);
                page.Header().Element(ComposeHeader);
                page.Content().AlignCenter().Element(ComposeContent);
               
            });
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(10).SemiBold().FontFamily("simhei");
            container.AlignCenter().AlignMiddle().Text("长沙地铁6号线汛情巡检报告 2022/11/12 10:20:08").Style(titleStyle);
            //container.Row(row =>
            //{
            //    row.RelativeItem().Column(column =>
            //    {
            //        column.Item().AlignCenter().Text("长沙地铁6号线汛情巡检报告 2022/11/12 10:20:08").FontFamily("simhei").Style(titleStyle);

            //    })
            //    ;

            //});

        }
        List<int> result = new List<int>
        {
            1,2,3,4,5
        };
        void ComposeContent(IContainer container)
        {

            container.PaddingVertical(10).DefaultTextStyle(x=>x.FontFamily("simhei")).Column(column =>
            {
                column.Spacing(5);
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text("巡检情况").FontFamily("simhei");

                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>                
                    {                    
                        columns.RelativeColumn(1);                    
                        columns.RelativeColumn(2);                   
                        columns.RelativeColumn(1);                    
                        columns.RelativeColumn(2);                   
                        columns.RelativeColumn(1);                    
                        columns.RelativeColumn(2);                
                    });
               
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());

                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());
                    table.Cell().LabelCell("巡检日期");
                    table.Cell().ValueCell(Placeholders.ShortDate());

                    static IContainer CellStyle(IContainer container)
                    {
                        //.Background("#FF0B3A5A")
                        return container.MaxHeight(50).Border(1).BorderColor(Colors.Grey.Lighten2).AlignMiddle().Padding(5);
                    }
                });


                column.Item().PaddingTop(10).Text("视频加载失败项");

                column.Item().Table(table =>
                {
                    IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                    {
                        return container
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(backgroundColor)
                            .PaddingVertical(5)
                            .PaddingHorizontal(10)
                            .AlignLeft()
                            .AlignMiddle();
                    }

                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("车站");
                        header.Cell().Element(CellStyle).Text("位置");

                        header.Cell().Element(CellStyle).Text("摄像头名称");
                        header.Cell().Element(CellStyle).Text("摄像头编码");
                        header.Cell().Element(CellStyle).Text("记录时间");
                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                    });

                    for(int i=0;i<10;i++)
                    {

                        table.Cell().Element(CellStyle).Text(i);
                        table.Cell().Element(CellStyle).Text(i);
                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                    }
                });
                column.Item().PaddingTop(10).Text("汛情详情");
                column.Item().Border(0).Padding(10).BorderColor(Colors.Grey.Lighten2).Column(column2 =>
                {
                    int n = 2;
                    int m = 5;
                    for (int j=0;j<n;j++)
                    {
                        column2.Item().Padding(5).Text("谢家桥");
                        for (int i = 0; i < m; i+=2)
                        {                       
                            column2.Item().Row(row =>
                            {
                                row.Spacing(20);

                                //var dd = i + 2 > result.Count ? result.Count - i : 2;

                                //var aaa = result.GetRange(i, dd);

                                for(int ii=i;ii<i+2;ii++)
                                {
                                    if(ii<result.Count)
                                    {
                                        row.RelativeItem().Column(column =>
                                        {
                                            column.Spacing(2);

                                            //column.Item().AlignCenter().AlignMiddle().Width(150).Image("河流.png", ImageScaling.FitArea);
                                            column.Item().PaddingVertical(5).PaddingHorizontal(10).Height(200).Placeholder();
                                            column.Item().Padding(10).Row(row1 =>
                                            {
                                                row1.RelativeItem().Table(table =>
                                                {
                                                    table.ColumnsDefinition(columns =>
                                                    {
                                                        columns.RelativeColumn(1);
                                                        columns.RelativeColumn(2.5f);
                                                    });

                                                    table.Cell().ValueCell("标记位置:");
                                                    table.Cell().ValueCell("谢家桥");
                                                    table.Cell().ValueCell("水位状态:");
                                                    table.Cell().ValueCell("正常");
                                                    table.Cell().ValueCell("截图时间:");
                                                    table.Cell().ValueCell(Placeholders.ShortDate());
                                                    table.Cell().ValueCell("监控名称:");
                                                    table.Cell().ValueCell("谢家桥站1号口部");
                                                    table.Cell().ValueCell("监控编号:");
                                                    table.Cell().ValueCell("1234567890");
                                                });

                                            });

                                        });
                                    }
                                    else
                                    {
                                        row.RelativeItem().Container();
                                    }
                                  
                                }                                                                                                                                                                          
                                //row.ConstantItem(10);

                                //row.RelativeItem().Column(column =>
                                //{
                                //    column.Spacing(2);

                                //    column.Item().AlignCenter().AlignMiddle().Width(150).Image("河流.png");
                                //    column.Item().Padding(10).Row(row1 =>
                                //    {
                                //        row1.RelativeItem().Table(table =>
                                //        {
                                //            table.ColumnsDefinition(columns =>
                                //            {
                                //                columns.RelativeColumn(1);
                                //                columns.RelativeColumn(2.5f);
                                //            });

                                //            table.Cell().ValueCell("标记位置:");
                                //            table.Cell().ValueCell("谢家桥");
                                //            table.Cell().ValueCell("水位状态:");
                                //            table.Cell().ValueCell("正常");
                                //            table.Cell().ValueCell("截图时间:");
                                //            table.Cell().ValueCell(Placeholders.ShortDate());
                                //            table.Cell().ValueCell("监控名称:");
                                //            table.Cell().ValueCell("谢家桥站1号口部");
                                //            table.Cell().ValueCell("监控编号:");
                                //            table.Cell().ValueCell("1234567890");
                                //        });

                                //    });

                                //});

                            });
                        }

                        column2.Spacing(20);
                    }
             
                });

              
            });

          
        }
    }
    static class SimpleExtension
    {
        private static IContainer Cell(this IContainer container, bool dark)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Grey.Medium)
                .Background(dark ? Colors.Grey.Lighten3 : Colors.White)
                .Padding(5);
        }

        public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).SemiBold();
        public static IContainer ValueCell(this IContainer container) => container.Cell(false);
        public static void ValueCell(this IContainer container, string text) => container.ValueCell().Text(text);
    }
}
