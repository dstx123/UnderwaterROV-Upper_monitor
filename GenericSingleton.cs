using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnderwaterROV
{
    public class GenericSingleton<T> where T : Form, new()
    {
        private static T t = null;
        public static T CreateInstrance()
        {
            if (null == t || t.IsDisposed)
            {
                t = new T();
            }
            return t;
        }
    }
}
