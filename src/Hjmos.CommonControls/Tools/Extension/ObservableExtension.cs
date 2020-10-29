using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hjmos.CommonControls.Tools.Extension
{
    public static class ObservableExtension
    {
        public static void Descend<T>(this ObservableCollection<T> collection) where T:IComparable<T>
        {
            List<T> DescendList = collection.OrderBy(x => x).ToList();//升序
            for(int i=0;i<DescendList.Count();i++)
            {
                collection.Move(collection.IndexOf(DescendList[i]), i);
            }
        }
    }
}
