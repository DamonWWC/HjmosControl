using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hjmos.BaseControls.Tools
{
    public class ResourceHelper
    {

        public static T GetResource<T>(string key)
        {
            if(Application.Current.TryFindResource(key) is T resource)
            {
                return resource;
            }
            return default;
        }
    }
}
