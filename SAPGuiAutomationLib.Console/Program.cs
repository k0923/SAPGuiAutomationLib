using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Reflection;
using System.Reflection.Emit;
using SAPTestRunTime;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Threading;
using System.Data;
//using Young.DAL;

namespace SAPGuiAutomationLib.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            SAPGuiAutomationLib.Console.TestClass test = new Console.TestClass();
            System.Console.ReadLine();


            Test.Exchange ex = new Test.Exchange();
            
            
        }
        

        

        static string GetCode<T>(T item, Func<CodeDomProvider, Action<T, TextWriter, CodeGeneratorOptions>> action)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "c";
            StringBuilder sb = new StringBuilder();

            using (TextWriter sourceWriter = new StringWriter(sb))
            {
                action(provider)(item, sourceWriter, options);
            }
            return sb.ToString();
        }

       


        static void SAPGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            
        }


        static void ShowProp(string typeName,object obj)
        {
            
            
        }

        static void DataClassTest()
        {
            List<SAPDataParameter> ps = new List<SAPDataParameter>();
            ps.Add(new SAPDataParameter() { Name = "Type", Type = typeof(int), Comment = "Type of the currency." });
            ps.Add(new SAPDataParameter() { Name = "CurrencyFrom", Type = typeof(string), Comment = "From Currency." });
            ps.Add(new SAPDataParameter() { Name = "CurrencyTo", Type = typeof(string), Comment = "To Currency." });
            ps.Add(new SAPDataParameter() { Name = "ValidDate", Type = typeof(string), Comment = "Valid Date." });

            //SAPModuleAttribute attribute = new SAPModuleAttribute();
            //attribute.ModuleName = "Get Currency";
            //attribute.ModuleVersion = "1.0.0.0";
            //attribute.ScreenNumber = "1000";
            //attribute.TCode = "SE16";
            //attribute.Author = "Zhou Yang";
            //attribute.Email = "yang.zhou4@hp.com";
           

            //var tp = SAPAutomationExtension.GetDataClass("Screen_GetCurrency", ps,attribute);
            //string code = CodeHelper.GetCode<CodeTypeMember>(tp, p => p.GenerateCodeFromMember).ToString();
           
          

        }

        static void DynamicEmit()
        {
            //ModuleBuilder builder = new ModuleBuilder();
            //TypeBuilder tb = builder.DefineType("Student", TypeAttributes.Class);
            //PropertyBuilder pb = tb.DefineProperty("Name", PropertyAttributes.None, typeof(string), null);
            
        }
    }

    public class RefTest
    {
        private bool _isSet;
        public RefTest(ref bool IsSet)
        {
            _isSet = IsSet;
        }

        public void Set()
        {
            _isSet = true;
        }
    }

    class CodeHelper
    {
        private static CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
        public static StringBuilder GetCode<T>(T item, Func<CodeDomProvider, Action<T, TextWriter, CodeGeneratorOptions>> action)
        {
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            StringBuilder sb = new StringBuilder();

            using (TextWriter sourceWriter = new StringWriter(sb))
            {
                action(provider)(item, sourceWriter, options);
            }
            return sb;
        }

        public static bool IsValidVariable(string VariableName)
        {
            return provider.IsValidIdentifier(VariableName);
        }

    }
}
