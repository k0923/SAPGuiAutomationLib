using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibTest
{
    //public class DataInitial
    //{
    //    public DataInitial(bool Initial = false)
    //    {
    //        if (Initial == false)
    //            return;
    //        if(Global.DataSet != null && Global.DataSet.Tables.Count>0)
    //        {
    //            Type me = this.GetType();
    //            var tableAt = me.GetCustomAttributes(typeof(TableBindingAttribute), true).FirstOrDefault() as TableBindingAttribute;
    //            if(tableAt != null)
    //            {
    //                string tableName = tableAt.TableName;
    //                if(Global.DataSet.Tables.Contains(tableName))
    //                {
    //                    DataTable dt = Global.DataSet.Tables[tableName];
    //                    foreach(var property in me.GetProperties())
    //                    {
    //                        var columnAt = property.GetCustomAttributes(typeof(ColumnBindingAttribute),true).FirstOrDefault() as ColumnBindingAttribute;
    //                        if(columnAt!=null && dt.Columns.Contains(columnAt.ColName))
    //                        {
    //                            DataRow dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId).FirstOrDefault();
    //                            if(dr!=null)
    //                            {
    //                                property.SetValue(this, dr[columnAt.ColName]);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            var multiColumnAt = property.GetCustomAttributes(typeof(MultiColumnBindingAttribute), true).FirstOrDefault() as MultiColumnBindingAttribute;
    //                            if(multiColumnAt != null)
    //                            {
    //                                bool isAllColContains = true;
    //                                foreach (var col in multiColumnAt.ColNames)
    //                                {
    //                                    if (!dt.Columns.Contains(col))
    //                                    {
    //                                        isAllColContains = false;
    //                                        break;
    //                                    }
    //                                }
    //                                if(isAllColContains)
    //                                {
    //                                    var method = Delegate.CreateDelegate(typeof(ConvertMethod), this, multiColumnAt.MethodName) as ConvertMethod;

    //                                    DataRow[] drs = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId);
    //                                    if(drs != null)
    //                                    {
    //                                        property.SetValue(this, method(drs));
    //                                    }
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
