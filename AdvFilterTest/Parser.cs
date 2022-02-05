using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvFilterTest
{
    class Parser
    {
        private string str;

        public Parser(string str)
        {
            this.str = str;
        }

        public void Parse()
        {

            var n = ParseValue(str);
            //Print(n);

        }

        private void Print(params object[] en)
        {
            foreach(var e in en)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private static IFunction ParseValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Empty value");

            Stack<int> parenthesis = new Stack<int>();

            Dictionary<int, List<Function>> levelChildren = new Dictionary<int, List<Function>>();

            Function rootFunction = null;

            for (int i = 0; i < value.Length; ++i)
            {
                char ch = value[i];

                if (ch == '(')
                    parenthesis.Push(i);
                else if (ch == ')')
                {
                    if (!parenthesis.Any()) throw new Exception("Parenthèses manquante");
                    int level = parenthesis.Count;
                    int openBracket = parenthesis.Pop();
                   
                    
                    var expressi = value.Substring(openBracket +1, i - openBracket -1);
                    var funcName = GetFunctionName(value.Substring(0, openBracket));
                    //System.Diagnostics.Debug.WriteLine($"[~{funcName}~]");


                    levelChildren.TryGetValue(level +1, out var children);
                    var newFunc = Functions.GetFunction(funcName, level, expressi.Split(','), children != null ? children.ToArray() : null);
                    if(level == 1) rootFunction = newFunc; // On garde la référence de l'élement root, qui contient tous les enfants

                    if(!levelChildren.ContainsKey(level))
                    {
                        levelChildren[level] = new List<Function>();
                    }

                    levelChildren[level].Add(newFunc);

                    if (children != null)
                    {
                        levelChildren[level + 1].Clear();
                    }
                }
            }

            if (parenthesis.Any()) throw new Exception("Parenthèses en trop");

            return rootFunction;
        }


        /// <summary>
        /// Récupère le nom de la fonction
        /// ex:
        ///     "and(or" retourne "or",
        ///     "or" retourne "or",
        ///     "and(or(a,b),eq" retourne "eq",
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private static string GetFunctionName(string expr)
        {
            int index = -1;
            for (int i = expr.Length-1; i >= 0 ; --i)
            {
                char ch = expr[i];

                if (!char.IsLetter(ch))
                {
                    index = i + 1;
                    break;
                }else if (i==0) // Si le nom de la fonction est à la racine
                {
                    index = i;
                    break;
                }
                    
            }
            if (index < 0) return string.Empty;
            return expr.Substring(index, expr.Length - index);
        }
    }


}
