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
            Console.WriteLine(5 / 3);
            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.SetSAPApiAssembly();
            GuiUserArea area = SAPTestHelper.Current.GetElementById<GuiUserArea>("wnd[0]/usr");
            GuiScrollbar scroll = area.VerticalScrollbar;
            GuiScrollbar hScroll = area.HorizontalScrollbar;
            
            //ShowInfo("GuiTree");
            ShowProp("GuiScrollbar", scroll);
            ShowProp("GuiScrollbar", hScroll);
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


        static void ShowProp(string typeName,object obj)
        {
            var props = SAPTestHelper.Current.GetSAPTypeInfoes<PropertyInfo>(typeName, t => t.GetProperties().Where(p => p.IsSpecialName == false));
            foreach (var p in props)
            {
                try
                {
                    Console.WriteLine(p.Name +":"+p.GetValue(obj).ToString());
                }
                catch
                {

                }
                
            }
        }

        static void ShowMethod(string typeName)
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

            
        }
    }
}
