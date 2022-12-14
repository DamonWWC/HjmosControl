using LiteDB;
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

namespace WpfApp3
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
            //using var db = new LiteDatabase(@"MyData.db");

            using var db = new LiteDatabase(new ConnectionString("Filename=MyData1.db;Password=1234"));
            // 获取 Customers 集合
            var col = db.GetCollection<Customer>("customers1");

            // 创建一个对象
            var customer = new Customer
            {
                Name = "John Doe",
                //Phones = new string[] { "8000-0000", "9000-0000" },
                //Age = 39,
                //IsActive = true
            };
           // Customer us=col.FindOne(x => x.Name == "John Doe");
            // 在 Name 字段上创建唯一索引
            //col.EnsureIndex(x => x.Name, true);

            // 数据插入
            col.Insert(customer);

            //// 数据查询 
            //List<Customer> list = col.Find(x => x.Age > 20).ToList();
            //Customer user = col.FindOne(x => x.Age > 20);

            //// 数据删除 
            //col.Delete(user.Id);
        }
    }


    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string[] Phones { get; set; }
        //public bool IsActive { get; set; }
    }
}
