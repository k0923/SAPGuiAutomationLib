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

        //public void DataBinding()
        //{
        //    if (Global.DataSet != null && Global.DataSet.Tables.Count > 0)
        //    {
        //        Type me = this.GetType();
        //        DataTable dt = null;

        //        var tableAt = me.GetCustomAttributes(typeof(TableBindingAttribute), true).FirstOrDefault() as TableBindingAttribute;

        //        if (tableAt != null)
        //        {
        //            tableAt.TableName = string.IsNullOrEmpty(tableAt.TableName) ? me.Name : tableAt.TableName;


        //            if (Global.DataSet.Tables.Contains(tableAt.TableName))
        //            {
        //                if (Global.TypeCounts.ContainsKey(me))
        //                {
        //                    Global.TypeCounts[me] += 1;
        //                }
        //                else
        //                {
        //                    Global.TypeCounts.Add(me, 0);
        //                }
        //                dt = Global.DataSet.Tables[tableAt.TableName];
        //            }


        //            var atMiPairs = from m in me.GetMembers()
        //                            where (m.MemberType == MemberTypes.Property
        //                            || m.MemberType == MemberTypes.Method)
        //                            && m.GetCustomAttributes(typeof(OrderAttribute), true).FirstOrDefault() != null
        //                            orderby (m.GetCustomAttributes(typeof(OrderAttribute), true).First() as OrderAttribute).Order
        //                            select new { Order = m.GetCustomAttributes(typeof(OrderAttribute), true).FirstOrDefault(), MemberInfo = m };

        //            foreach (var pair in atMiPairs)
        //            {
        //                if (pair.MemberInfo is PropertyInfo)
        //                {
        //                    var property = pair.MemberInfo as PropertyInfo;
        //                    var propertyType = property.PropertyType;
        //                    if (propertyType.IsClass && propertyType != typeof(string) && propertyType.IsSubclassOf(typeof(DataInitial)))
        //                    {
        //                        if (propertyType.GetConstructor(Type.EmptyTypes) != null)
        //                        {
        //                            dynamic newInstance = Activator.CreateInstance(propertyType);
        //                            property.SetValue(this, newInstance, null);
        //                            newInstance.DataBinding();
        //                        }
        //                    }
        //                    else if (dt != null)
        //                    {
        //                        if (pair.Order is ColumnBindingAttribute)
        //                        {
        //                            ColumnBindingAttribute colAt = pair.Order as ColumnBindingAttribute;
        //                            colAt.ColName = string.IsNullOrEmpty(colAt.ColName) ? pair.MemberInfo.Name : colAt.ColName;

        //                            if (dt != null && dt.Columns.Contains(colAt.ColName))
        //                            {
        //                                DataRow dr = null;
        //                                if (Global.Cycle == CycleType.Default)
        //                                    dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId).FirstOrDefault();
        //                                else
        //                                    dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId)[Global.TypeCounts[me]];

        //                                if (dr != null && dr[colAt.ColName].ToString() != "")
        //                                {
        //                                    if (colAt.Directory == DataDirectory.Input)
        //                                    {
        //                                        var value = Convert.ChangeType(dr[colAt.ColName], propertyType);
        //                                        property.SetValue(this, value, null);
        //                                    }
        //                                    if (colAt.Directory == DataDirectory.Output)
        //                                    {
        //                                        dr[colAt.ColName] = property.GetValue(this, null);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else if (pair.Order is MultiColumnBindingAttribute)
        //                        {
        //                            MultiColumnBindingAttribute mulColAt = pair.Order as MultiColumnBindingAttribute;
        //                            bool isAllColContains = true;
        //                            foreach (var col in mulColAt.ColNames)
        //                            {
        //                                if (!dt.Columns.Contains(col))
        //                                {
        //                                    isAllColContains = false;
        //                                    break;
        //                                }
        //                            }
        //                            if (isAllColContains)
        //                            {


        //                                DataRow[] drs = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId);
        //                                if (drs != null)
        //                                {
        //                                    if (property.PropertyType == typeof(DataRow[]))
        //                                        property.SetValue(this, drs, null);
        //                                    else
        //                                    {
        //                                        var method = Delegate.CreateDelegate(typeof(ConvertMethod), this, mulColAt.MethodName) as ConvertMethod;
        //                                        property.SetValue(this, method(drs), null);
        //                                    }

        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //                else if (pair.MemberInfo is MethodInfo)
        //                {
        //                    var method = pair.MemberInfo as MethodInfo;
        //                    if (method.GetParameters().Count() == 0)
        //                    {
        //                        dynamic returnObj = method.Invoke(this, null);
        //                        if (returnObj != null && returnObj.GetType().IsSubclassOf(typeof(DataInitial)))
        //                        {
        //                            returnObj.DataBinding();
        //                        }

        //                    }
        //                }

        //            }
        //        }
        //    }

        //}


        //public void DataBindingV2()
        //{
        //    if (Global.DataSet != null && Global.DataSet.Tables.Count > 0)
        //    {
        //        Type me = this.GetType();
        //        DataTable dt = null;

        //        var tableAt = me.GetCustomAttributes(typeof(TableBindingAttribute), true).FirstOrDefault() as TableBindingAttribute;

        //        if (tableAt != null)
        //        {
        //            tableAt.TableName = string.IsNullOrEmpty(tableAt.TableName) ? me.Name : tableAt.TableName;


        //            if (Global.DataSet.Tables.Contains(tableAt.TableName))
        //            {
        //                if (Global.TypeCounts.ContainsKey(me))
        //                {
        //                    Global.TypeCounts[me] += 1;
        //                }
        //                else
        //                {
        //                    Global.TypeCounts.Add(me, 0);
        //                }
        //                dt = Global.DataSet.Tables[tableAt.TableName];
        //            }
                        

        //            var atMiPairs = from m in me.GetMembers()
        //                            where (m.MemberType == MemberTypes.Property
        //                            || m.MemberType == MemberTypes.Method)
        //                            && m.GetCustomAttributes(typeof(OrderAttribute), true).FirstOrDefault() != null
        //                            orderby (m.GetCustomAttributes(typeof(OrderAttribute), true).First() as OrderAttribute).Order
        //                            select new { Order = m.GetCustomAttributes(typeof(OrderAttribute), true).FirstOrDefault(), MemberInfo = m };

        //            foreach (var pair in atMiPairs)
        //            {
        //                if (pair.MemberInfo is PropertyInfo)
        //                {
        //                    var property = pair.MemberInfo as PropertyInfo;
        //                    var propertyType = property.PropertyType;
        //                    if (propertyType.IsClass && propertyType != typeof(string) && propertyType.IsSubclassOf(typeof(DataInitial)))
        //                    {
        //                        if (propertyType.GetConstructor(Type.EmptyTypes) != null)
        //                        {
        //                            dynamic newInstance = Activator.CreateInstance(propertyType);
        //                            property.SetValue(this, newInstance, null);
        //                            newInstance.DataBinding();
        //                        }
        //                    }
        //                    else if (dt != null)
        //                    {
        //                        if (pair.Order is ColumnBindingAttribute)
        //                        {
        //                            ColumnBindingAttribute colAt = pair.Order as ColumnBindingAttribute;
        //                            colAt.ColName = string.IsNullOrEmpty(colAt.ColName) ? pair.MemberInfo.Name : colAt.ColName;

        //                            if (dt != null && dt.Columns.Contains(colAt.ColName))
        //                            {
        //                                DataRow dr = null;
        //                                if (Global.Cycle == CycleType.Default)
        //                                    dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId).FirstOrDefault();
        //                                else
        //                                    dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId)[Global.TypeCounts[me]];

        //                                if (dr != null && dr[colAt.ColName].ToString() != "")
        //                                {
        //                                    if (colAt.Directory == DataDirectory.Input)
        //                                    {
        //                                        var value = Convert.ChangeType(dr[colAt.ColName], propertyType);
        //                                        property.SetValue(this, value, null);
        //                                    }
        //                                    if (colAt.Directory == DataDirectory.Output)
        //                                    {
        //                                        dr[colAt.ColName] = property.GetValue(this, null);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else if (pair.Order is MultiColumnBindingAttribute)
        //                        {
        //                            MultiColumnBindingAttribute mulColAt = pair.Order as MultiColumnBindingAttribute;
        //                            bool isAllColContains = true;
        //                            foreach (var col in mulColAt.ColNames)
        //                            {
        //                                if (!dt.Columns.Contains(col))
        //                                {
        //                                    isAllColContains = false;
        //                                    break;
        //                                }
        //                            }
        //                            if (isAllColContains)
        //                            {
                                        

        //                                DataRow[] drs = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId);
        //                                if (drs != null)
        //                                {
        //                                    if (property.PropertyType == typeof(DataRow[]))
        //                                        property.SetValue(this, drs, null);
        //                                    else
        //                                    {
        //                                        var method = Delegate.CreateDelegate(typeof(ConvertMethod), this, mulColAt.MethodName) as ConvertMethod;
        //                                        property.SetValue(this, method(drs), null);
        //                                    }
                                            
        //                                }
        //                            }
        //                        }
        //                    }
                            
        //                }
        //                else if (pair.MemberInfo is MethodInfo)
        //                {
        //                    var method = pair.MemberInfo as MethodInfo;
        //                    if (method.GetParameters().Count() == 0)
        //                    {
        //                        dynamic returnObj = method.Invoke(this, null);
        //                        if(returnObj != null && returnObj.GetType().IsSubclassOf(typeof(DataInitial)))
        //                        {
        //                            returnObj.DataBinding();
        //                        }
                                
        //                    }
        //                }

        //            }
        //        }
        //    }
            
        //}

        //public void DataBindingV2()
        //{
        //    if (Global.DataSet != null && Global.DataSet.Tables.Count > 0)
        //    {
        //        Type me = this.GetType();
        //        var tableAt = me.GetCustomAttributes(typeof(TableBindingAttribute), true).FirstOrDefault() as TableBindingAttribute;
        //        if (tableAt != null)
        //        {
        //            string tableName = string.IsNullOrEmpty(tableAt.TableName) ? me.Name : tableAt.TableName;
        //            if (Global.DataSet.Tables.Contains(tableName))
        //            {
        //                DataTable dt = Global.DataSet.Tables[tableName];

        //                if(Global.TypeCounts.ContainsKey(me))
        //                {
        //                    Global.TypeCounts[me] += 1;
        //                }
        //                else
        //                {
        //                    Global.TypeCounts.Add(me, 0);
        //                }

        //                Dictionary<MemberInfo, OrderAttribute> orderDic = new Dictionary<MemberInfo, OrderAttribute>();

        //                foreach (var mi in me.GetMembers().Where(t=>t.MemberType == MemberTypes.Property || t.MemberType == MemberTypes.Method))
        //                {
        //                    var orderAt = mi.GetCustomAttributes(typeof(OrderAttribute), true).FirstOrDefault() as OrderAttribute;
                            
        //                    if(orderAt != null)
        //                    {
        //                        orderDic.Add(mi, orderAt);
        //                    }
        //                }
        //                foreach (var keyValue in orderDic.OrderBy(v=>v.Value.Order))
        //                {
        //                    if(keyValue.Key is PropertyInfo)
        //                    {
        //                        var property = keyValue.Key as PropertyInfo;
        //                        var propertyType = property.PropertyType;
        //                        if(propertyType.IsClass && propertyType != typeof(string) && propertyType.IsSubclassOf(typeof(DataInitial)))
        //                        {
        //                            if(propertyType.GetConstructor(Type.EmptyTypes)!=null)
        //                            {
        //                                dynamic newInstance = Activator.CreateInstance(propertyType);
        //                                property.SetValue(this, newInstance, null);
        //                                newInstance.DataBinding();
        //                            }
        //                        }
        //                        else if (keyValue.Value is ColumnBindingAttribute)
        //                        {
        //                            ColumnBindingAttribute colAt = keyValue.Value as ColumnBindingAttribute;
        //                            colAt.ColName = string.IsNullOrEmpty(colAt.ColName) ? keyValue.Key.Name : colAt.ColName;
        //                            if (dt.Columns.Contains(colAt.ColName))
        //                            {
        //                                DataRow dr = null;
        //                                if (Global.Cycle == CycleType.Default)
        //                                    dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId).FirstOrDefault();
        //                                else
        //                                    dr = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId)[Global.TypeCounts[me]];

        //                                if (dr != null && dr[colAt.ColName].ToString() != "")
        //                                {
        //                                    if (colAt.Directory == DataDirectory.Input)
        //                                    {
        //                                        var value = Convert.ChangeType(dr[colAt.ColName], propertyType);
        //                                        property.SetValue(this, value, null);
        //                                    }
        //                                    if (colAt.Directory == DataDirectory.Output)
        //                                    {
        //                                        dr[colAt.ColName] = property.GetValue(this, null);
        //                                    }
        //                                }
        //                            }

        //                        }
        //                        else if (keyValue.Value is MultiColumnBindingAttribute)
        //                        {
        //                            MultiColumnBindingAttribute mulColAt = keyValue.Value as MultiColumnBindingAttribute;
        //                            bool isAllColContains = true;
        //                            foreach (var col in mulColAt.ColNames)
        //                            {
        //                                if (!dt.Columns.Contains(col))
        //                                {
        //                                    isAllColContains = false;
        //                                    break;
        //                                }
        //                            }
        //                            if (isAllColContains)
        //                            {
        //                                var method = Delegate.CreateDelegate(typeof(ConvertMethod), this, mulColAt.MethodName) as ConvertMethod;

        //                                DataRow[] drs = dt.Select(tableAt.IdColumnName + "=" + Global.CurrentId);
        //                                if (drs != null)
        //                                {
        //                                    property.SetValue(this, method(drs), null);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if(keyValue.Key is MethodInfo)
        //                    {
        //                        var method = keyValue.Key as MethodInfo;
        //                        if(method.GetParameters().Count()==0)
        //                        {
        //                            method.Invoke(this, null);
        //                        }
        //                    }

                            
                            
        //                }
        //            }
        //        }
        //    }
        //}

       
        
    }

   
}
