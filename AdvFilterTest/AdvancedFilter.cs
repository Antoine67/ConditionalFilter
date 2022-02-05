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

        public AdvancedFilter(string str)
        {
            this.str = str;
            this.parser = new Parser(str);
        }

        internal void Build()
        {
            parser.Parse();
        }
    }
}
