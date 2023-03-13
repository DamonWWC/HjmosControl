using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controltest
{
    public interface Interface1
    {
        [Resource("aaa")]
        void Call(int b,int a=1);
    }

    public interface Interface2
    {
        int Sum([Required] int a, [Range(1, 10)] int b);
    }



    [AttributeUsage(AttributeTargets.Method , AllowMultiple = false)]
    public class ResourceAttribute : System.Attribute
    {
        public ResourceAttribute( string name)
        {
     
            Name = name;
           
        }

        public ResourceAttribute()
        {
        }

      
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

    }



}