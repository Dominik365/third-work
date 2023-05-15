using System;
using System.Collections.Generic;
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

namespace Q4_Kalkulacka
{
    /// <summary>
    /// Interakční logika pro Calculator.xaml
    /// </summary>
    public partial class Calculator : Page
    {
        private string viewstring;
        private List<string> chain;
        private Core core;
        private string curnum;
        private TextBlock viewbox;
        private TextBlock ansbox;
        public Calculator()
        {
            InitializeComponent();
            viewbox = (TextBlock)FindName("_viewbox");
            ansbox = (TextBlock)FindName("_result");
            chain = new List<string>();
            curnum = "";
            core = new Core();
        }
        private void UpdateChain(object sender, RoutedEventArgs e)
        {
            string operation = (string)((Button)sender).Content;
            if (operation.Equals("C"))
            {
               
                chain.Clear();
                curnum = "";
            }
            else if (operation.Equals("CE"))
            {
                curnum = "";   
            }
            else if (operation.Equals("<-"))
            {
                if (curnum.Length > 0)
                {
                    curnum = curnum.Remove(curnum.Length - 1);
                }
            }
            else if (operation.Equals("x^2"))
            {
                if(curnum.Length > 0)
                {
                    chain.Add(curnum);
                    curnum = "";
                    chain.Add("^");
                    chain.Add("2");
                }  
            }
            else if (operation.Equals("√x"))
            {
                if (curnum.Length > 0)
                {
                    chain.Add(curnum);
                    curnum = "";
                    chain.Add("^");
                    chain.Add("0.5");
                }
            }
            else if (operation.Equals("+/-"))
            {
                if(curnum.Length > 0)
                {
                    if (curnum.StartsWith("-"))
                    {
                        curnum = curnum.Replace("-", "");
                    }
                    else
                    {
                        curnum = curnum.Insert(0, "-");
                    }
                }    
            }
            else if (operation.Equals("="))
            {
                if(curnum.Length > 0)
                {
                    chain.Add(curnum);
                    curnum = "";
                }
                for(int i = 0; i < chain.Count - 1; i++)
                {
                    if(chain.ElementAt(i) == "")
                    {
                        chain.RemoveAt(i);
                    }
                }
                
                string output = core.Compute(chain).ToString();
                ansbox.Text = output;
            }
            else if(IsNumeric(operation))
            {
                
                curnum += operation;
            }
            else if(operation == ",")
            {
                if(curnum.Length > 0)
                {
                    curnum += ".";
                }
            }
            else if(operation == "*" || operation == "/" || operation == "+" || operation == "-")
            {
                if(chain.Count > 0)
                {
                    if (IsNumeric(chain.Last()) || curnum.Length > 0 || chain.Last() == ")")
                    {
                        chain.Add(curnum);
                        curnum = "";
                        chain.Add(operation);
                    }
                }
                else if(curnum.Length > 0)
                {
                    chain.Add(curnum);
                    curnum = "";
                    chain.Add(operation);
                }
            }
            else
            {
                if (operation == "(")
                {
                    if (chain.Count > 0)
                    {
                        if ("+-/*(".Contains(chain.Last()))
                        {
                            chain.Add(operation);
                        }
                    }
                    else
                    {
                        chain.Add(operation);
                    }
                }
                else
                {
                    if (chain.Count > 0)
                    {
                        if (IsNumeric(chain.Last()) || curnum.Length > 0)
                        {
                            chain.Add(curnum);
                            curnum = "";
                            chain.Add(operation);
                        }
                    }
                }
            }
            UpdateBox();
        }
        private bool IsNumeric(string token)
        {
            if(token == "ANS")
            {
                return true;
            }
            try
            {
                Double.Parse(token);
                return true;
            }
            catch (Exception e)
            {

            }
            return false;

        }
        private void UpdateBox()
        {
            viewbox.Text = "";
            foreach(string token in chain)
            {
                viewbox.Text += token;
            }
            viewbox.Text += curnum;
        }
    }
}
