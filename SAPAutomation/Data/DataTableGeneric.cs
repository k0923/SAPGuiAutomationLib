using SAPAutomation.Framework.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SAPAutomation.Data
{
    public class DataTable<T> : IEnumerable<T> where T : class,new()
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
                object obj = p.GetValue(d,null);
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
                object obj = p.GetValue(o,null);
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
                    p.SetValue(o, Convert.ChangeType(dr[s], p.PropertyType), null);
                   
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
                if (p.GetCustomAttributes(typeof(TestDataAttribute), true).Count() > 0)
                {
                    TestDataAttribute bizData = p.GetCustomAttributes(typeof(TestDataAttribute), true).First() as TestDataAttribute;
                    if (bizData.FriendlyName != null && bizData.FriendlyName != "")
                        name = bizData.FriendlyName;
                    if (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string))
                        dataAction(name, p, data);
                    else if (p.PropertyType.IsClass)
                    {
                        var obj = p.GetValue(data,null);
                        if (obj == null)
                        {
                            obj = Activator.CreateInstance(p.PropertyType);
                            p.SetValue(data, obj,null);
                        }
                        entityAction(obj, dataAction, name);
                    }
                }
            }
        }

        public DataTable GetCopy()
        {
            return _dt.Copy();
        }

        public int Count
        {
            get { return _dataList.Count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }
    }
}
