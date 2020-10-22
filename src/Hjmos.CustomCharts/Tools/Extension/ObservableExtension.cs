using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.CustomCharts.Tools.Extension
{
    public static class ObservableExtension
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable<T>
        {
            List<T> sortedList = collection.OrderByDescending(x => x).ToList();//降序
            for (int i = 0; i < sortedList.Count(); i++)
            {
                collection.Move(collection.IndexOf(sortedList[i]), i);
            }
        }
    }
}
