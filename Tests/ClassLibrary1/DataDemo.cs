using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DataDemo
    {
        [DataMember]
        public CmdType Type { get; set; }

        [DataMember]
        public string Argument { get; set; }

        [DataMember]
        public string Parameter { get; set; }

        //[DataMember]
        //public ContextData Data { get; set; }
    }

    public enum CmdType
    {
        StartProcess
    }
}
