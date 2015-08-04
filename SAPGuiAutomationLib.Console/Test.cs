

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;

using System.Reflection;

namespace Test
{
    public class DataTable<T>:IEnumerable<T> where T : class,new()
    {
        private DataTable _dt;
        
        private List<T> _dataList;
        public DataTable()
        {
            _dt = new DataTable() { TableName = typeof(T).Name };
            _dataList = new List<T>();
            entityAction(new T(), (s, p, d) =>
            {
                _dt.Columns.Add(new DataColumn(s, p.PropertyType));
            });
        }

        [Obsolete]
        public List<T> MyList { get { return _dataList; } }

        public DataTable(DataTable Data)
        {
            checkTableSchema(Data);
            _dt = Data.Copy();
            _dataList = new List<T>();
            createDataEntiry();
        }

        public bool Add(T data)
        {
            bool result = false;
            DataRow dr = _dt.NewRow();
            entityAction(data, (s, p, d) =>
            {
                object obj = p.GetValue(d);
                if (obj != null)
                    dr[s] = obj;
            });
            _dt.Rows.Add(dr);
            _dataList.Add(data);
            return result;
        }

        public bool Remove(T data)
        {
            bool result = false;
            Remove(_dataList.IndexOf(data));
            return result;
        }

        public bool Remove(int Row)
        {
            bool result = false;
            _dt.Rows.RemoveAt(Row);
            _dataList.RemoveAt(Row);
            return result;
        }

        public T Get(int Row)
        {
            return _dataList[Row];
        }

        public bool Update(T data)
        {
            bool result = false;
            DataRow dr = _dt.Rows[_dataList.IndexOf(data)];
            entityAction(data, (s, p, o) =>
            {
                object obj = p.GetValue(o);
                if (obj != null)
                    dr[s] = obj;
            });
            return result;
        }

        private void createDataEntiry()
        {
            foreach (DataRow dr in _dt.Rows)
            {
                T item = new T();
                entityAction(item, (s, p, o) =>
                {
                    p.SetValue(o, dr[s]);
                });
                _dataList.Add(item);
            }
        }

        private void checkTableSchema(DataTable dt)
        {
            entityAction(new T(), (s, p, o) =>
            {
                if (!dt.Columns.Contains(s))
                    throw new ArgumentNullException(s);
            });
        }


        private void entityAction(object data, Action<string, PropertyInfo, object> dataAction, string parentName = "")
        {
            foreach (var p in data.GetType().GetProperties())
            {
                string name = p.Name;
                if (parentName != "")
                    name = parentName + "." + name;
                //if (p.GetCustomAttributes(typeof(TestDataAttribute), true).Count() > 0)
                //{
                //    if (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string))
                //        dataAction(name, p, data);
                //    else if (p.PropertyType.IsClass)
                //    {
                //        var obj = p.GetValue(data);
                //        if (obj == null)
                //        {
                //            obj = Activator.CreateInstance(p.PropertyType);
                //            p.SetValue(data, obj);
                //        }
                //        entityAction(obj, dataAction, name);
                //    }
                //}
            }
        }

        public DataTable GetCopy()
        {
            return _dt.Copy();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    //public class Exchange
    //{
    //    /// Exchange Rate Type
    //    public System.String Rate_Type { get; set; }
    //    /// From currency
    //    public System.String CurFrom { get; set; }
    //    /// To-currency
    //    public System.String CurTo { get; set; }

    //    public static void Main1()
    //    {
    //        Exchange data = new Exchange();
    //        data.Rate_Type = "M";
    //        data.CurFrom = "USD";
    //        data.CurTo = "CNY";

    //        Exchange.RunAction(data);
    //    }

    //    public static void RunAction(Exchange Data)
    //    {
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").ResizeWorkingPane(216, 24, false);
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "/n";
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "se16";
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtDATABROWSE-TABLENAME").Text = "TCURR";
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI1-LOW").Text = Data.Rate_Type;
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI2-LOW").Text = Data.CurFrom;
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").Text = Data.CurTo;
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").SetFocus();
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").CaretPosition = 3;
    //        SAPTestHelper.Current.SAPGuiSession.FindById<GuiButton>("wnd[0]/tbar[1]/btn[8]").Press();
    //    }
    //}







}
