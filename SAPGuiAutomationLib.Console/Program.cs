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
            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.SAPGuiSession.Hit += SAPGuiSession_Hit;
            for (int i = 0; i < SAPTestHelper.Current.SAPGuiSession.Children.Count;i++ )
            {
                var fWin = SAPTestHelper.Current.SAPGuiSession.Children.ElementAt(i) as GuiFrameWindow;
                if(fWin!=null)
                {
                    fWin.ElementVisualizationMode = true;
                }
            }
                ShowInfo("GuiTree");
            
            Console.WriteLine(SAPTestHelper.Current.SAPGuiConnection.Children.Count);
            Console.ReadLine();
        }

        static void SAPGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            SAPTestHelper.Current.SAPGuiSession.Hit -= SAPGuiSession_Hit;
            for (int i = 0; i < SAPTestHelper.Current.SAPGuiSession.Children.Count; i++)
            {
                var fWin = SAPTestHelper.Current.SAPGuiSession.Children.ElementAt(i) as GuiFrameWindow;
                if (fWin != null)
                {
                    fWin.ElementVisualizationMode = false;
                }
            }
            Console.WriteLine("abc");
            for (int i = 0; i < SAPTestHelper.Current.SAPGuiSession.Children.Count; i++)
            {
                var fWin = SAPTestHelper.Current.SAPGuiSession.Children.ElementAt(i) as GuiFrameWindow;
                if (fWin != null)
                {
                    Console.WriteLine(fWin.ElementVisualizationMode);
                }
            }
        }

        static void ShowInfo(string typeName)
        {
            SAPTestHelper.Current.SetSAPApiAssembly();
            var ms = SAPTestHelper.Current.GetSAPTypeInfoes<MethodInfo>(typeName, t => t.GetMethods().Where(m => m.IsSpecialName == false));
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

            var props = SAPTestHelper.Current.GetSAPTypeInfoes<PropertyInfo>(typeName, t => t.GetProperties().Where(p => p.IsSpecialName == false));
            foreach (var p in props)
            {
                Console.WriteLine(p.Name);



                
            }
        }
    }
}
