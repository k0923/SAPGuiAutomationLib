using SAPAutomationTools;
using SAPFEWSELib;
using SAPGuiAutomationLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAPUserControl
{
    /// <summary>
    /// SAPScriptRecording.xaml 的交互逻辑
    /// </summary>
    public partial class SAPScriptRecording : UserControl
    {
        ObservableCollection<RecordStep> steps = new ObservableCollection<RecordStep>();
        //private List<RecordStep> mySteps;
        private GuiSession _session;
        private DataTable _parameterTable;


        public SAPScriptRecording()
        {
            InitializeComponent();
            _parameterTable = new DataTable();
            SAPAutomationHelper.Current.OnSetSession += Current_OnSetSession;
            
            dg_Step.DataContext = steps;
            this.IsEnabled = false;
            setRecordStatus(false);

            steps.CollectionChanged += steps_CollectionChanged;
        }

        private void Current_OnSetSession(object sender, GuiSession session)
        {
            _session = session;
            this.IsEnabled = true;
        }

        void steps_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].StepId = i + 1;
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (RecordStep step in e.OldItems)
                {
                    if (step.IsParameterize && step.ActionParams != null)
                    {
                        foreach (var p in step.ActionParams)
                        {
                            for (int i = 0; i < _parameterTable.Columns.Count; i++)
                            {
                                if (_parameterTable.Columns[i].ColumnName == p.Name)
                                {
                                    _parameterTable.Columns.Remove(p.Name);
                                }
                            }
                        }
                        dg_Parameter.ItemsSource = null;
                        dg_Parameter.ItemsSource = _parameterTable.DefaultView;
                    }
                }
            }
        }

       

        private string upperFirstChar(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void setRecordStatus(bool isRecord)
        {
            btn_Record.IsEnabled = !isRecord;
            btn_Stop.IsEnabled = isRecord;
        }

        private void btn_Record_Click(object sender, RoutedEventArgs e)
        {
            setRecordStatus(true);
            SAPAutomationHelper.Current.StartRecording((r) =>
            {
                dg_Step.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //Step s = new Step(r);
                    r.ActionName = upperFirstChar(r.ActionName);
                    r.CompInfo.Id = r.CompInfo.Id.Substring(19);

                    steps.Add(r);


                }));
            });
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            setRecordStatus(false);
            SAPAutomationHelper.Current.StopRecording();
        }

        private void runCase(IEnumerable<RecordStep> steps, bool runWithP = false)
        {
            WorkingEventArgs ae = new WorkingEventArgs();
            ae.IsProcessKnow = true;
            ae.Current = 0;

            int round = 1;
            if (runWithP)
            {
                round = _parameterTable.Rows.Count;
            }
            ae.Max = steps.Count() * round;
            for (int i = 0; i < round; i++)
            {
                foreach (var step in steps)
                {
                    ae.Current += 1;
                    if (runWithP && step.IsParameterize)
                    {
                        DataRow dr = _parameterTable.Rows[i];
                        foreach (var p in step.ActionParams)
                        {
                            p.Value = dr[p.Name];
                        }
                    }
                    SAPAutomationHelper.Current.RunAction(step);
                    if (OnWorking != null)
                    {
                        OnWorking(this, ae);
                    }

                }
            }


        }


        private async void mi_Run_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Step.SelectedItems != null)
            {
                SAPAutomationHelper.Current.StopRecording();
                setRecordStatus(false);
                var runSteps = dg_Step.SelectedItems.Cast<RecordStep>();
                await Task.Run(() =>
                {
                    try
                    {
                        runCase(runSteps, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    finally
                    {
                        if (AfterWorking != null)
                            AfterWorking(this, null);
                    }
                });
            }
        }

        private async void mi_RunAll_Click(object sender, RoutedEventArgs e)
        {

            SAPAutomationHelper.Current.StopRecording();
            setRecordStatus(false);

            await Task.Run(() =>
            {
                try
                {
                    runCase(steps, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                finally
                {
                    if (AfterWorking != null)
                        AfterWorking(this, null);
                }
            });

        }

        private async void mi_RunAllWithP_Click(object sender, RoutedEventArgs e)
        {
            SAPAutomationHelper.Current.StopRecording();
            setRecordStatus(false);

            await Task.Run(() =>
            {
                try
                {
                    runCase(steps, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                finally
                {
                    if (AfterWorking != null)
                        AfterWorking(this, null);
                }
            });
        }

        public event OnWorkingHandler OnWorking;

        public event OnWorkFinishHandler AfterWorking;

        private void mi_CSharp_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Step.SelectedItems != null)
            {
                string code = "";
                foreach (RecordStep step in dg_Step.SelectedItems.Cast<RecordStep>())
                {
                    foreach (var statement in step.GetCodeStatement())
                    {
                        code += CodeHelper.GetCode(statement, p => p.GenerateCodeFromStatement).ToString();
                    }

                }
                Clipboard.SetData(DataFormats.Text, code);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var gridRow = findParentElement<DataGridRow>(sender as DependencyObject);
            var step = dg_Step.ItemContainerGenerator.ItemFromContainer(gridRow) as RecordStep;
            if (step.ActionParams == null || step.ActionParams.Count == 0)
            {
                MessageBox.Show("There is no parameter in the step.");
                step.IsParameterize = false;
                return;
            }
            foreach (var p in step.ActionParams)
            {
                if (!CodeHelper.IsValidVariable(p.Name))
                {
                    MessageBox.Show(string.Format("Parameter named '{0}' is not a valid name", p.Name), "Error");
                    step.IsParameterize = false;
                    return;
                }
                for (int i = 0; i < _parameterTable.Columns.Count; i++)
                {
                    if (_parameterTable.Columns[i].ColumnName == p.Name)
                    {
                        MessageBox.Show(string.Format("Parameter named '{0}' is already exist,pleaese choose another name", p.Name), "Error");
                        step.IsParameterize = false;
                        return;
                    }
                }
            }

            foreach (var p in step.ActionParams)
            {
                DataColumn col = new DataColumn(p.Name, p.Type);
                _parameterTable.Columns.Add(col);
            }
            DataRow dr = null;
            if (_parameterTable.Rows.Count == 0)
            {
                dr = _parameterTable.NewRow();
                _parameterTable.Rows.Add(dr);
            }
            else
            {
                dr = _parameterTable.Rows[0];
            }
            foreach (var p in step.ActionParams)
            {
                dr[p.Name] = p.Value;
            }
            dg_Parameter.ItemsSource = null;
            dg_Parameter.ItemsSource = _parameterTable.DefaultView;

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var gridRow = findParentElement<DataGridRow>(sender as DependencyObject);
            var step = dg_Step.ItemContainerGenerator.ItemFromContainer(gridRow) as RecordStep;
            if (step.ActionParams != null)
            {
                foreach (var p in step.ActionParams)
                {
                    for (int i = 0; i < _parameterTable.Columns.Count; i++)
                    {
                        if (_parameterTable.Columns[i].ColumnName == p.Name)
                        {
                            _parameterTable.Columns.Remove(p.Name);
                        }
                    }
                }
                dg_Parameter.ItemsSource = null;
                dg_Parameter.ItemsSource = _parameterTable.DefaultView;
            }

        }


        private static T findParentElement<T>(DependencyObject source) where T : DependencyObject
        {
            if (source != null)
            {
                while ((source as T) == null)
                {
                    source = VisualTreeHelper.GetParent(source);
                }
            }

            return source as T;
        }

        private void mi_CreateM_Click(object sender, RoutedEventArgs e)
        {
            //CreateModule moduleWin = new CreateModule();
            //moduleWin.Left = App.Current.MainWindow.Left + 50;
            //moduleWin.Top = App.Current.MainWindow.Top + 50;
            //moduleWin.ShowDialog();

            //if (!moduleWin.IsCancel)
            //{
            //    var cls = steps.CreateModule(moduleWin.ModuleName);
            //    var code = CodeHelper.GetCode(cls, p => p.GenerateCodeFromType).ToString();

            //    ScriptWin win = new ScriptWin(code);
            //    win.Left = App.Current.MainWindow.Left + 50;
            //    win.Top = App.Current.MainWindow.Top + 50;
            //    win.ShowDialog();

            //}


        }

        //private void createModule(SAPModuleAttribute attribute)
        //{
        //    //string actionClassName = attribute.Name;
        //    //string dataClassName = actionClassName + "_Data";

        //    //CodeNamespace cns = new CodeNamespace("Test");
        //    //CodeTypeDeclaration moduleClass = new CodeTypeDeclaration(actionClassName);
        //    //moduleClass.IsClass = true;
        //    //moduleClass.Attributes = MemberAttributes.Public;
        //    //CodeMemberMethod method = new CodeMemberMethod();
        //    //method.Name = "RunAction";
        //    //method.Attributes = MemberAttributes.Public;

        //    //bool hasParameter = false;

        //    //List<SAPDataParameter> paras = new List<SAPDataParameter>();
        //    //foreach (var step in steps)
        //    //{
        //    //    if (step.IsParameterize)
        //    //    {
        //    //        hasParameter = true;
        //    //        paras.AddRange(step.ActionParams);
        //    //        method.Statements.AddRange(step.GetCodeStatement("Data").ToArray());

        //    //    }
        //    //    else
        //    //    {
        //    //        method.Statements.AddRange(step.GetCodeStatement().ToArray());
        //    //    }
        //    //}

        //    //if (hasParameter)
        //    //    method.Parameters.Add(new CodeParameterDeclarationExpression() { Type = new CodeTypeReference(dataClassName), Name = "Data" });

        //    //moduleClass.Members.Add(method);
        //    //cns.Types.Add(moduleClass);
        //    //var dataClass = SAPAutomationExtension.GetDataClass(dataClassName, paras, attribute);
        //    //if (dataClass != null)
        //    //    cns.Types.Add(dataClass);

        //    //var code = CodeHelper.GetCode(cns, p => p.GenerateCodeFromNamespace).ToString();

        //}

        private void btn_ShowLog_Click(object sender, RoutedEventArgs e)
        {
            //SAPLogView slv = new SAPLogView();
            //slv.Show();
        }
    }
}
