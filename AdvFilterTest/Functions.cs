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


    public abstract class Function : IFunction
    {
        public readonly object[] parameters;
        public readonly int level;
        public readonly Function[] children;

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

            string toStr = $"[{this.GetType().Name}][LV:{level}] ";

            //Si l'object contient des enfants on les affiche
            //Les paramètres ne sont pas utilisés dans ce cas
            if(children != null)
            {
                toStr += $"{"children :" + childrenSeparator + string.Join(childrenSeparator, children.Select(x => x.ToString()))}";
            }
            else
            {
                toStr += $"params : {string.Join(" / ", parameters)} ";
            }

            return toStr;
        }

        //TODO Laisser chaque fonction implémenter sa propre fonction Build
        //public abstract string Build();

        public string DefaultBuild()
        {
            return DefaultBuild(this);
        }


        //Uniquement pour la démo : a remplacer avec des clauses SQL, ...
        private static string DefaultBuild(Function fcEl, string query = "")
        {
            if (fcEl.children != null)
            {
                query += $"{fcEl.GetType().Name} [ ";
                foreach (var chi in fcEl.children)
                {
                    query += DefaultBuild(chi) + " ";
                }
                query += " ] ";
            }
            else
            {
                query += $"{fcEl.GetType().Name} [ { string.Join(", ", fcEl.parameters.Select(x => x.ToString()).ToArray()) } ] ";
            }
            return query.ToString();
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
