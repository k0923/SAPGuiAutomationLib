using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Report
{
    public delegate void OnAddHandler<T>(object sender,T item);

    public class EventList<T>:List<T>
    {
        public event OnAddHandler<T> OnAdd;

        public new void Add(T item)
        {
            base.Add(item);
            if(OnAdd!=null)
            {
                OnAdd(this, item);
            }
        }
    }
}
