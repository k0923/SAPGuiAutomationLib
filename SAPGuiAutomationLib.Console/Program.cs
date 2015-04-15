using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Reflection;
using System.Reflection.Emit;

namespace SAPGuiAutomationLib.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMethod();
            //GuiButton btn = new GuiButton();
            
            Console.ReadLine();
        }

        static void ShowMethod()
        {
            SAPTestHelper.Current.SetSAPApiAssembly();
            var ms = SAPTestHelper.Current.GetSAPTypeInfoes<MethodInfo>("GuiButton", t => t.GetMethods().Where(m=>m.IsSpecialName == false));
            foreach(var m in ms)
            {
                string method = string.Empty;
                method += m.ReturnType.Name + " " + m.Name;

                ParameterInfo[] paInfoes = m.GetParameters();
                if(paInfoes.Count()>0)
                {
                    method += "(";
                    foreach (var p in paInfoes)
                    {
                        if(p.IsOptional)
                        {
                            method += "[Optional]";
                        }
                        method += p.ParameterType.Name + " " + p.Name + ",";
                    }
                    method = method.Substring(0, method.Length - 1);
                    method += ")";
                }
                else
                {
                    method += "()";
                }



                Console.WriteLine(method);
            }
        }
    }
}
