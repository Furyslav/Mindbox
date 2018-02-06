using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMindbox
{
    public static class CountSquare
    {      
        //какая точность? float || double?
        //что на счёт обработки исключительных ситуаций?
        private static string ErrorText = "Error!\nLibraryMindbox-->CountSquare-->{0}:\n{1}";

        public static bool HideError = false;

        /// <summary>
        /// Площадь круга по радиусу
        /// </summary>
        /// <param name="R">Радиус</param>
        /// <returns>PI * R * R</returns>
        public static float CircleRadius(float R)
        {
            return Convert.ToSingle(CircleRadius(Convert.ToDouble(R)));
        }

        /// <summary>
        /// Площадь круга по радиусу
        /// </summary>
        /// <param name="R">Радиус</param>
        /// <returns>PI * R * R</returns>
        public static double CircleRadius(double R)
        {
            if (!HideError && R == 0)
                throw new ArgumentException(string.Format(ErrorText, "StraightRectangle", "Radius is 0"));
            return Math.PI * R * R;
        }

        /// <summary>
        /// Площадь прямоугольного треугольника
        /// </summary>
        /// <param name="b">Катет 1</param>
        /// <param name="b">Катет 2</param>
        /// <returns>S = 0.5 * a * b</returns>
        public static float StraightRectangle(float a, float b)
        {
            return Convert.ToSingle(StraightRectangle(Convert.ToDouble(a), Convert.ToDouble(b)));
        }

        /// <summary>
        /// Площадь прямоугольного треугольника
        /// </summary>
        /// <param name="a">Катет 1</param>
        /// <param name="b">Катет 2</param>
        /// <returns>S = 0.5 * a * b</returns>
        public static double StraightRectangle(double a, double b)
        {
            if (!HideError && (a==0 || b==0))
                throw new ArgumentException(string.Format(ErrorText, "StraightRectangle", "One of arguments is 0"));
            return (double)(0.5 * a * b);
        }
                
    }

    public static class CountFormula
    {
        //что на счёт обработки исключительных ситуаций?
        private static string ErrorText = "Error!\nLibraryMindbox-->CountFormula-->{0}:\n{1}";
        public static bool HideError = false;

        private static string[] _operators = { "-", "+", "/", "*", "^" };
        private static Func<double, double, double>[] _operations = {
                                            (a1, a2) => a1 - a2,
                                            (a1, a2) => a1 + a2,
                                            (a1, a2) => a1 / a2,
                                            (a1, a2) => a1 * a2,
                                            (a1, a2) => Math.Pow(a1, a2)
                                            };


        private static List<string> getExpressions(string formula)
        {
            string operators = "()^*/+-";
            List<string> exp = new List<string>();
            string digit = "";

            foreach (char c in formula.Replace(" ", "").Replace(".", ","))
            {
                if (operators.IndexOf(c) != -1)
                {
                    if (digit.Length > 0)
                    {
                        exp.Add(digit);
                    }

                    exp.Add(c.ToString());
                    digit = "";
                    continue;
                }

                if (!(c <= '9' && c >= '0') && c != ',')
                {
                    throw new ArgumentException("Incorrect symobol " + c.ToString());
                }

                digit += c.ToString();
            }
            if (digit.Length > 0)
                exp.Add(digit);

            return exp;
        }

        private static string getSubExpression(List<string> expression, ref int index)
        {
            string result = "";
            int parentLevel = 1;
            index++;

            while (parentLevel > 0 && expression.Count > index)
            {
                if (expression[index] == "(")
                {
                    parentLevel++;
                }

                if (expression[index] == ")")
                {
                    parentLevel--;
                }

                if (parentLevel > 0)
                {
                    result += expression[index];
                }
                index++;
            }

            if (parentLevel > 0)
            {
                throw new ArgumentException("Dont matched parentheses quality");
            }
            return result;

        }

        private static double count(Stack<string> operat, Stack<double> operand)
        {
            while (operat.Count > 0)
            {
                string op = operat.Pop();
                double arg2 = operand.Pop();
                double arg1 = operand.Pop();
                operand.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
            }
            return operand.Pop();
        }

        private static double Eval(string formula)
        {
            List<double> result = new List<double>();
            List<string> exp = getExpressions(formula);
            Stack<double> operand = new Stack<double>();
            Stack<string> operat = new Stack<string>();
            int indexExp = 0;
            double res = 0;

            while (indexExp < exp.Count)
            {
                if (exp[indexExp] == "(")
                {
                    string subExp = getSubExpression(exp, ref indexExp);
                    operand.Push(Eval(subExp));
                    continue;
                }


                if (Array.IndexOf(_operators, exp[indexExp]) == -1)
                {
                    operand.Push(double.Parse(exp[indexExp]));
                }
                else
                {
                    while (operat.Count > 0 && Array.IndexOf(_operators, exp[indexExp]) <= Array.IndexOf(_operators, operat.Peek()))
                    {
                        string op = operat.Pop();
                        double arg2 = operand.Pop();
                        double arg1 = operand.Pop();
                        operand.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                    }
                    operat.Push(exp[indexExp]);
                }


                indexExp++;
            }

            res += count(operat, operand);

            return res;
        }


        /// <summary>
        /// Вычисляет значение из заданной формулы. Поддерживаемые операторы: +-/*^()
        /// </summary>
        /// <example>
        /// <code>
        /// double r = Evaluate("3+2*4+7");
        /// r = Evaluate("3 + (2/4)*4 + 7");
        /// r = Evaluate("2 * (2 + 2)^2-5");
        /// r = Evaluate("2 + 2 * 2");
        /// </code>
        /// </example> 
        /// <param name="formula">Формула</param>
        /// <returns>double</returns>
        public static double Evaluate(string formula)
        {
            try
            {
                double result = Eval(formula);
                if (double.IsInfinity(result))
                {
                    if (!HideError)
                        throw new ArgumentException(string.Format(ErrorText, "Evaluate", "divine by 0"));
                }
                if (double.IsNaN(result))
                {
                    if (!HideError)
                        throw new ArgumentException(string.Format(ErrorText, "Evaluate", "result is Nan"));
                }
                return result;
            }
            catch(Exception e)
            {
                if (!HideError)
                    throw new ArgumentException(string.Format(ErrorText, "Evaluate", "incorrect formula\n"+ e.Message ));
                return double.NaN;
            }

        }

        //public static double EvaluateByScript(string formula)
        //{
        //    //Install-Package Roslyn.Services.CSharp -Version 1.2.20906.2
        //    var engine = new Roslyn.Scripting.CSharp.ScriptEngine();
        //    var session = engine.CreateSession();
        //    return session.Execute<double>("(1*2)+(3+7)-(12^2)-3");
        //    //return Microsoft.CodeAnalysis.Scripting.CSharp.CSharpScript.Eval("(1*2)+(3+7)-(12^2)-3");
        //}

    }
}
