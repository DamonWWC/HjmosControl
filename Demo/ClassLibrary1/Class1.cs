namespace ClassLibrary1
{
    public class Class1
    {

        private Class1()
        {
                
        }

        public static Class1 Instance=new Class1();



        public string GetData(DataDemo dataDemo)
        {

            return dataDemo.Type.ToString() ;

        }
    }



}