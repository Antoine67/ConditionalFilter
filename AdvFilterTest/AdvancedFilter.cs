using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvFilterTest
{
    class AdvancedFilter
    {
        private string str;
        private Parser parser;
        private Function rootElement;

        public AdvancedFilter(string str)
        {
            this.str = str;
            this.parser = new Parser(str);
        }

        private void Validate()
        {
            string strWithoutEscapedChar = RemoveEscaped(str);
            
            if(strWithoutEscapedChar.Count(f => (f == '(')) != strWithoutEscapedChar.Count(f => (f == ')')))
            {
                throw new Exception("Malformation de l'input : parenthèses incohérentes");
            }

        }

        private string RemoveEscaped(string st)
        {
            StringBuilder withoutEscaped = new StringBuilder();
            for(int i = 0; i< st.Length; i ++)
            {
                if(st[i] == '\\')
                {
                    i++;
                    continue;
                }else
                {
                    withoutEscaped.Append(st[i]);
                }
            }
            return withoutEscaped.ToString();
        }

        internal void Parse()
        {
            Validate();
            rootElement = parser.Parse();
        }

        internal void Build()
        {
            string build = rootElement.DefaultBuild();
            //System.Diagnostics.Debug.WriteLine("Après build : " + build);
        }


        internal void ParseAndBuild()
        {
            Parse();
            Build();
        }
    }
}
