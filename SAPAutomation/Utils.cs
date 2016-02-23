using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAPAutomation
{
    public static class Utils
    {
      

        public static float GetAmount(string val)
        {
            val.Replace(",", "");
            var pos = val.IndexOf('-');
            if (val.Contains("-") && val.IndexOf('-') == val.Length - 1)
            {
                val = "-" + val.Substring(0, val.Length - 1);
            }
            return float.Parse(val);
        }

        public static string FillNumber(string val, int length = 10)
        {
            if (val.Length < length)
            {
                while (val.Length < length)
                {
                    val = "0" + val;
                }

            }
            return val;
        }

        //public static void SetAccess(string windowName, CancellationToken token)
        //{
            
        //    bool isPress = false;
        //    while (!isPress)
        //    {
        //        if (token.IsCancellationRequested)
        //            break;

        //        var e = TreeWalker.ControlViewWalker.GetFirstChild(AutomationElement.RootElement);

        //        while (e != null)
        //        {
        //            if (e.Current.Name == windowName)
        //                break;
        //            var tempE = TreeWalker.ControlViewWalker.GetNextSibling(e);
        //            e = tempE;


        //        }

        //        if(e!=null && e.Current.Name == windowName)
        //        {
        //            var condition1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox);
        //            var checkboxElement = e.FindFirst(TreeScope.Descendants, condition1);
        //            if (checkboxElement != null)
        //            {
        //                var checkbox = checkboxElement.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
        //                if (checkbox.Current.ToggleState == ToggleState.Off)
        //                {
        //                    checkbox.Toggle();
        //                }
        //            }


        //            condition1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
        //            var condition2 = new PropertyCondition(AutomationElement.AccessKeyProperty, "Alt+A");
        //            var andCondition = new AndCondition(condition1, condition2);
        //            var btnElement = e.FindFirst(TreeScope.Descendants, andCondition);
        //            if (btnElement != null)
        //            {
        //                var btn = btnElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

        //                btn.Invoke();
        //                isPress = true;
        //            }
        //        }
        //    }

        //}
    }
}
