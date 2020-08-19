using HjmosControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HjmosControl.Controls
{

    [TemplatePart(Name =ElementPasswordBox,Type =typeof(PasswordBox))]
    public class PasswordBox : Control, IDataInput
    {
        private const string ElementPasswordBox = "PART_PasswordBox";




        public Func<string, OperationResult<bool>> VerifyFunc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsError { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ErrorStr { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TextType TextType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ShowClearButton { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool VerifyData()
        {
            throw new NotImplementedException();
        }
    }
}
