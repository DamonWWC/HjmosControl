using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
         



            ReflectionTest rt = new ReflectionTest();

            MethodInfo mi = rt.GetType().GetMethod("DisplayType");//先获取到DisplayType<T>的MethodInfo反射对象
            var para = new object[] { "1", "2" };
            mi.MakeGenericMethod(new Type[] { typeof(string) }).Invoke(rt, para);//然后使用MethodInfo反射对象调用ReflectionTest类的DisplayType<T>方法，这时要使用MethodInfo的MakeGenericMethod函数指定函数DisplayType<T>的泛型类型T

            Type myGenericClassType = rt.GetType().GetNestedType("MyGenericClass`1");//这里获取MyGenericClass<T>的Type对象，注意GetNestedType方法的参数要用MyGenericClass`1这种格式才能获得MyGenericClass<T>的Type对象
            myGenericClassType.MakeGenericType(new Type[] { typeof(float) }).GetMethod("DisplayNestedType", BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
            //然后用Type对象的MakeGenericType函数为泛型类MyGenericClass<T>指定泛型T的类型，比如上面我们就用MakeGenericType函数将MyGenericClass<T>指定为了MyGenericClass<float>，然后继续用反射调用MyGenericClass<T>的DisplayNestedType静态方法

          //  Console.ReadLine();
        }
    }


    public class aaa
    {
        public bbb bb = new bbb();

    }


    public class bbb
    {

    }

  

    public class ReflectionTest
    {
        //泛型类MyGenericClass有个静态函数DisplayNestedType
        public class MyGenericClass<T>
        {
            public static void DisplayNestedType()
            {
                MessageBox.Show(typeof(T).ToString());
                //Console.WriteLine(typeof(T).ToString());
            }
        }

        public void DisplayType<T>(string a,string b)
        {
            MessageBox.Show(typeof(T).ToString()+a+b);
            //Console.WriteLine(typeof(T).ToString());
        }
    }

   
}
