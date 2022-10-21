using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.IO;
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
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows.Threading;
using System.Windows.Markup;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Spire.Pdf;

namespace PrintWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private delegate void LoadXpsMethod();//委托事件，相当于函数指针
        public  FlowDocument m_doc;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //m_doc = (FlowDocument)Application.LoadComponent(new Uri("FlowDocument1.xaml", UriKind.RelativeOrAbsolute));

            //Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
             Loadxps1();

        
            //using (FileStream fs = File.Open("FlowDocument1.xaml", FileMode.Open))
            //{
            //    FlowDocument doc = (FlowDocument)XamlReader.Load(fs);
            //    docViewer.Document = doc;
            //}

        }


        //public  byte[] ConvertDoc(FlowDocument document)
        //{

        //    var xpsFile = XpsConverter.ConverterDoc(document);
        //    var tmpXpsFileName = Path.Combine(Path.GetTempPath(), DateTime.Now.Ticks + ".xps");
        //    File.WriteAllBytes(tmpXpsFileName, xpsFile);

        //    var doc = new PdfDocument();
        //    doc.LoadFromXPS(tmpXpsFileName);
        //    byte[] pdfFile;
        //    using (var stream = new MemoryStream())
        //    {
        //        doc.SaveToStream(stream);
        //        pdfFile = stream.ToArray();
        //    }

        //    File.Delete(tmpXpsFileName);
        //    return pdfFile;
        //}
        //public void LoadXps()
        //{
        //    //构造一个基于内存的xps document
        //    MemoryStream ms = new MemoryStream();
        //    Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
        //    Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
        //    PackageStore.RemovePackage(DocumentUri);
        //    PackageStore.AddPackage(DocumentUri, package);
        //    XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

        //    //将flow document写入基于内存的xps document中去
        //    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
        //    writer.Write(((IDocumentPaginatorSource)m_doc).DocumentPaginator);

        //    //获取这个基于内存的xps document的fixed document
        //    docViewer.Document = xpsDocument.GetFixedDocumentSequence();

        //    //关闭基于内存的xps document
        //    xpsDocument.Close();
        //    ms.Dispose();
        //}

        public void Loadxps1()
        {
            // Load the XPS content into memory.
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);
            PackageStore.AddPackage(DocumentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast,
                DocumentUri.AbsoluteUri);

            // Load the XPS content into a temporary file (alternative approach).
            //if (File.Exists("test2.xps")) File.Delete("test2.xps");
            //    XpsDocument xpsDocument = new XpsDocument("test2.xps", FileAccess.ReadWrite);

            using (FileStream fs = File.Open("FlowDocument1.xaml", FileMode.Open))
            {
                FlowDocument doc = (FlowDocument)XamlReader.Load(fs);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator);

                // Display the new XPS document in a viewer.
                //docViewer.Document = xpsDocument.GetFixedDocumentSequence();
                var bytes = ms.ToArray();

                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromStream(fs);
                //pdf.LoadFromBytes(bytes);
                pdf.SaveToFile("12345");

                xpsDocument.Close();

               
                ms.Dispose();


                
                //string root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                //string dir = root + "Data\\";
                //if (!System.IO.Directory.Exists(dir))
                //{
                //    System.IO.Directory.CreateDirectory(dir);
                //}
                //var outputFilePath = dir + "\\" + "123" + ".pdf";
                //PdfFilePrinter.PrintXpsToPdf(bytes, outputFilePath, "pdf");
            }
        }
    
    }
}
