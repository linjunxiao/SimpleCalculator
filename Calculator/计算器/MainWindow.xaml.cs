using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 计算器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Stack<double> num = new Stack<double>();
        private Stack<char> op = new Stack<char>();
        private void digit0_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "0";
        }
        private void digit1_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "1";
        }

        private void digit2_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "2";
        }

        private void digit3_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "3";
        }

        private void digit4_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "4";
        }

        private void digit5_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "5";
        }

        private void digit6_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "6";
        }

        private void digit7_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "7";
        }

        private void digit8_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "8";
        }

        private void digit9_Click(object sender, RoutedEventArgs e)
        {
            text.Content = text.Content + "9";
        }
        private void op4_Click(object sender, RoutedEventArgs e)
        {
            ChangeText("%");
        }
        private void op_3_Click(object sender, RoutedEventArgs e)
        {
            ChangeText("/");
        }

        private void op_2_Click(object sender, RoutedEventArgs e)
        {
            ChangeText("*");
        }
        private void op_1_Click(object sender, RoutedEventArgs e)
        {
            ChangeText("-");
        }
        private void op__Click(object sender, RoutedEventArgs e)
        {
            ChangeText("+");
        }
        public void ChangeText(string op)
        {
            string s = (string)text.Content;
            int len = s.Length;
            if (len == 0)
                text.Content += op;
            else
            {
                if (s[len - 1] >= '0' && s[len - 1] <= '9')
                {
                    text.Content += op;
                }
                else
                {
                    s = s.Remove(len - 1);
                    s += op;
                    text.Content = s;
                }
            }
            
        }

        private void point_Click(object sender, RoutedEventArgs e)
        {
            text.Content += ".";
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)text.Content;
            s = s.Insert(0, "-");
            text.Content = s;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)text.Content;
            s = s.Remove(s.Length - 1);
            text.Content = s;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            text.Content = "";
            ans.Content = "";
            num.Clear();
            op.Clear();
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)text.Content;
            int index = 0;
            for(int i=s.Length-1;i>=0;i--)
            {
                if (!(s[i] >= '0' && s[i] <= '9'))
                {
                    index = i+1;
                    break;
                }   
                
            }
            if (index < s.Length)
            {
                s = s.Remove(index);
                text.Content = s;
            }
           
        }

        private void GetAns_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)text.Content + "=";
            int index = 0; double n = 0;
            while (index < s.Length - 1)
            {
                string temp = "";
                while (s[index] >= '0' && s[index] <= '9' || s[index] == '.')
                {
                    temp += s[index++];
                }
                if (temp != "")
                {
                    n = double.Parse(temp);
                    num.Push(n);
                }
                if (s[index] == '+' || s[index] == '-' || s[index] == '*' || s[index] == '/' || s[index] == '%')
                {
                    if (op.Count == 0)
                    {
                        op.Push(s[index++]);
                    }
                    else
                    {
                        char top = op.Peek();
                        switch (cmp(top, s[index]))
                        {
                            case '<':
                                op.Push(s[index++]);
                                break;
                            case '>':
                                double sec = num.Pop();
                                double fir = num.Pop();
                                char c = op.Pop();
                                num.Push(Calculate(fir, sec, c));
                                break;

                        }
                    }

                }
                if (s[index] == '=')
                {
                    while (op.Count != 0)
                    {
                        double sec = num.Pop();
                        double fir = num.Pop();
                        char c = op.Pop();
                        num.Push(Calculate(fir, sec, c));
                    }

                }
            }
            if (ans.Content != "除数不能为0,请重新输入")
                ans.Content = num.Peek();
        }
        
        public char cmp(char op1,char op2)
        {
            char res = ' ';
            switch (op1)
            {
                case '+':
                case '-':
                    if (op2 == '+' || op2 == '-')
                        res = '>';
                    else
                        res = '<';
                    break;
                case '*':
                case '/':
                case '%':
                    res = '>';
                    break;

            }
            return res;
        }
        public double Calculate(double fir,double sec,char op)
        {
            double res = 0;
            switch (op)
            {
                case '+': res = fir + sec; break;
                case '-': res = fir - sec; break;
                case '*': res = fir * sec; break;
                case '/':
                    if (sec == 0)
                    {
                        ans.Content = "除数不能为0,请重新输入";
                    }  
                    else
                        res = fir / sec;
                    break;
                case '%':
                    res = fir % sec;
                    break;
            }
            return res;
        }

       

        private void Reciprocal_Click(object sender, RoutedEventArgs e)
        {
            double item = 0;
            if(double.TryParse((string)text.Content,out item))
            {
                item = double.Parse((string)text.Content);
                ans.Content = (1/item).ToString();
            }
            else
            {
                ans.Content = "请检查是否有非法字符";
            }
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            double item = 0;
            if (double.TryParse((string)text.Content, out item))
            {
                item = double.Parse((string)text.Content);
                ans.Content = (item*item).ToString();
            }
            else
            {
                ans.Content = "请检查是否有非法字符";
            }
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            double item = 0;
            if (double.TryParse((string)text.Content, out item))
            {
                item = double.Parse((string)text.Content);
                ans.Content = Math.Sqrt(item).ToString();
            }
            else
            {
                ans.Content = "请检查是否有非法字符";
            }
        }
    }
}
