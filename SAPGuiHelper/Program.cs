using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace SAPGuiHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(args[0]);
                SetAccess(args[0]);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
            
        }

        static void SetAccess(string windowName)
        {

            bool isPress = false;
            int i = 0;
            while (!isPress)
            {
                try
                {
                    var e = TreeWalker.ControlViewWalker.GetFirstChild(AutomationElement.RootElement);

                    while (e != null)
                    {
                        if (e.Current.Name == windowName)
                            break;
                        var tempE = TreeWalker.ControlViewWalker.GetNextSibling(e);
                        e = tempE;


                    }

                    if (e != null && e.Current.Name == windowName)
                    {
                        var condition1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox);
                        var checkboxElement = e.FindFirst(TreeScope.Descendants, condition1);
                        if (checkboxElement != null)
                        {
                            var checkbox = checkboxElement.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
                            if (checkbox.Current.ToggleState == ToggleState.Off)
                            {
                                checkbox.Toggle();
                            }
                        }


                        condition1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
                        var condition2 = new PropertyCondition(AutomationElement.AccessKeyProperty, "Alt+A");
                        var andCondition = new AndCondition(condition1, condition2);
                        var btnElement = e.FindFirst(TreeScope.Descendants, andCondition);
                        if (btnElement != null)
                        {
                            var btn = btnElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

                            btn.Invoke();
                            isPress = true;
                        }
                    }
                   
                }
                catch(Exception ex)
                {
                    Task.Delay(1000).Wait();
                    Console.Clear();
                    Console.WriteLine(windowName);
                    Console.Write(ex.Message);
                    Console.WriteLine(i++);
                }
               
            }

        }
    }
}
