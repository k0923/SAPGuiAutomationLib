using SAPAutomation.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SAPAutomation.Framework
{
    
    public class DataInitial
    {
        

        public void DataBinding()
        {
            if (Global.DataSet != null && Global.DataSet.Tables.Count > 0)
            {
                Type me = this.GetType();
                var tableAt = me.GetCustomAttributes(typeof(TableBindingAttribute), true).FirstOrDefault() as TableBindingAttribute;
                if (tableAt != null)
                {
                    string tableName = tableAt.TableName;
                    if (Global.DataSet.Tables.Contains(tableName))
                    {
                        DataTable dt = Global.DataSet.Tables[tableName];

                        if(Global.TypeCounts.ContainsKey(me))
                        {
                            Global.TypeCounts[me] += 1;
                        }
                        else
                        {
                            Global.TypeCounts.Add(me, 0);
                        }

                        Dictionary<PropertyInfo, OrderAttribute> orderDic = new Dictionary<PropertyInfo, OrderAttribute>();

                        foreach (var property in me.GetProperties())
                        {
                            var columnAt = property.GetCustomAttributes(typeof(OrderAttribute), true).FirstOrDefault() as OrderAttribute;
                            if(columnAt != null)
                            {
                                orderDic.Add(property, columnAt);
                            }
                        }
                        foreach (var keyValue in orderDic.OrderBy(v=>v.Value.Order))
                        {

                            if (keyValue.Key.PropertyType.IsClass && keyValue.Key.PropertyType != typeof(string) && keyValue.Key.PropertyType.IsSubclassOf(typeof(DataInitial)))
                            {
                                if(keyValue.Key.PropertyType.GetConstructor(Type.EmptyTypes)!=null)
                                {
                                    dynamic newInstance  = Activator.CreateInstance(keyValue.Key.PropertyType);
                                    keyValue.Key.SetValue(this, newInstance, null);
                                    newInstance.DataBinding();
                                }
                            }
                            else if(keyValue.Value is ColumnBindingAttribute)
                            {
                                ColumnBindingAttribute colAt = keyValue.Value as ColumnBindingAttribute;
                                if(dt.Columns.Contains(colAt.ColName))
                                {
                                    DataRow dr = null;
                                    if (Global.Cycle == CycleType.Default)
                                        dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId).FirstOrDefault();
                                    else
                                        dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId)[Global.TypeCounts[me]];

                                    if (dr != null)
                                    {
                                        if(colAt.Directory == DataDirectory.Input)
                                        {
                                            var value = Convert.ChangeType(dr[colAt.ColName], keyValue.Key.PropertyType);
                                            keyValue.Key.SetValue(this, value, null);
                                        }
                                        if(colAt.Directory == DataDirectory.Output)
                                        {
                                            dr[colAt.ColName] = keyValue.Key.GetValue(this, null);
                                        }
                                    }
                                }

                            }
                            else if(keyValue.Value is MultiColumnBindingAttribute)
                            {
                                MultiColumnBindingAttribute mulColAt = keyValue.Value as MultiColumnBindingAttribute;
                                bool isAllColContains = true;
                                foreach (var col in mulColAt.ColNames)
                                {
                                    if (!dt.Columns.Contains(col))
                                    {
                                        isAllColContains = false;
                                        break;
                                    }
                                }
                                if (isAllColContains)
                                {
                                    var method = Delegate.CreateDelegate(typeof(ConvertMethod), this, mulColAt.MethodName) as ConvertMethod;

                                    DataRow[] drs = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId);
                                    if (drs != null)
                                    {
                                        keyValue.Key.SetValue(this, method(drs), null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //public void DataBinding()
        //{
        //    if (Global.DataSet != null && Global.DataSet.Tables.Count > 0)
        //    {
        //        Type me = this.GetType();
        //        var tableAt = me.GetCustomAttributes(typeof(TableBindingAttribute), true).FirstOrDefault() as TableBindingAttribute;
        //        if (tableAt != null)
        //        {
        //            string tableName = tableAt.TableName;
        //            if (Global.DataSet.Tables.Contains(tableName))
        //            {
        //                DataTable dt = Global.DataSet.Tables[tableName];
        //                foreach (var property in me.GetProperties())
        //                {
        //                    var columnAt = property.GetCustomAttributes(typeof(ColumnBindingAttribute), true).FirstOrDefault() as ColumnBindingAttribute;
        //                    if (columnAt != null && dt.Columns.Contains(columnAt.ColName))
        //                    {
        //                        DataRow dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId).FirstOrDefault();
        //                        if (dr != null)
        //                        {
        //                            property.SetValue(this, dr[columnAt.ColName], null);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var multiColumnAt = property.GetCustomAttributes(typeof(MultiColumnBindingAttribute), true).FirstOrDefault() as MultiColumnBindingAttribute;
        //                        if (multiColumnAt != null)
        //                        {
        //                            bool isAllColContains = true;
        //                            foreach (var col in multiColumnAt.ColNames)
        //                            {
        //                                if (!dt.Columns.Contains(col))
        //                                {
        //                                    isAllColContains = false;
        //                                    break;
        //                                }
        //                            }
        //                            if (isAllColContains)
        //                            {
        //                                var method = Delegate.CreateDelegate(typeof(ConvertMethod), this, multiColumnAt.MethodName) as ConvertMethod;

        //                                DataRow[] drs = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId);
        //                                if (drs != null)
        //                                {
        //                                    property.SetValue(this, method(drs), null);
        //                                }
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        
    }

   
}
