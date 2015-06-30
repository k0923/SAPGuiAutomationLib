using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SAPAutomation.Extension
{
    public static class TypeExtension
    {
        public static void ForeachAttributes<T>(this Type type,Func<Type,IEnumerable<T>> methodFunc,Action<IEnumerable,T> AttributesAction) where T:MemberInfo
        {
            foreach(var member in methodFunc(type))
            {
                var attributes = member.GetCustomAttributes(true);
                AttributesAction(attributes, member);
            }
        }

        public static void ForeachAttributes(this Type type, Action<IEnumerable, MemberInfo> AttributesAction)
        {
            foreach (var member in type.GetMembers())
            {
                var attributes = member.GetCustomAttributes(true);
                AttributesAction(attributes, member);
            }
        }

        public static void ForeachPrimitiveProperty(this Type type,Action<PropertyInfo,string> PropertyAction,string propName = "")
        {
            foreach(var prop in type.GetProperties())
            {
                string name = prop.Name;
                if(propName != "")
                {
                    name = propName + "." + name;
                }
                if(prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
                {
                    PropertyAction(prop, name);
                }
                else if(prop.PropertyType.IsClass)
                {
                    ForeachPrimitiveProperty(prop.PropertyType, PropertyAction, name);
                }
            }
        }

        
    }
}
