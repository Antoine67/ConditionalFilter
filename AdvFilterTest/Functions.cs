using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvFilterTest
{

    interface IFunction
    {

    }


    public class Function : IFunction
    {
        protected object[] parameters;
        int level;
        private Function[] children;

        public Function(int level, object[] parameters, Function[] children)
        {
            this.parameters = parameters;
            this.level = level;
            this.children = children;
        }

        public override string ToString()
        {
            string tabs = "";

            for(int i = 0; i<level; i++)
            {
                tabs += "\t";
            }

            string childrenSeparator = $"\n{tabs}- " ;

            return 
                $"[{this.GetType().Name}][LV:{level}] " +
                $"params : {string.Join(" / ", parameters)} " +
                $"{(children != null ? "children :"+ childrenSeparator + string.Join(childrenSeparator, children.Select(x=>x.ToString())) : null)}";
        }
    }

    class OR : Function
    {
        public OR(int level, object[] parameters, Function[] children) : base(level, parameters, children)
        {
        }
    }

    class AND : Function
    {
        public AND(int level, object[] parameters, Function[] children) : base(level, parameters, children)
        {
        }
    }

    class EQ : Function
    {
        public EQ(int level, object[] parameters, Function[] children) : base(level, parameters, children)
        {
        }
    }

    static class Functions
    {
        public static Function GetFunction(string name, int level, object[] parameters, Function[] children)
        {
            switch(name)
            {
                case "or":
                    return new OR(level,parameters, children);
                case "and":
                    return new AND(level,parameters, children);
                case "eq":
                    return new EQ(level,parameters, children);
                default:
                    throw new Exception("Fonction non trouvée");
            }
        }

    }
}
