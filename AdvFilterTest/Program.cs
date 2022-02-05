using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvFilterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("and(eq('a','b'),eq('c','d'))", false);
            Test("or(and(eq('a','b'),eq('c','d')),and(eq('a','b'),eq('c','d')))", false);
            Test("and(or(and(eq('a','b'),eq('c','d')),and(eq('e','f'),eq('g','h'))),or(and(eq('i','j'),eq('k','l')),and(eq('m','n'),eq('o','p'))))", false);
            Test("and(eq('a','b'),eq('c','d'))", true);
        }

        static void Test(string str, bool errorExpected)
        {
            System.Diagnostics.Debug.WriteLine($"[STARTING]  -------------------------{str}------------------------- ");
            bool err = false;
            try
            {
                var af = new AdvancedFilter(str);
                af.Build();
            }catch(Exception e) {
                err = true;
                if (!errorExpected)
                    System.Diagnostics.Debug.WriteLine($"[ERREUR] {str} ~ {e}");
            }

            if(errorExpected == err)
            {
                System.Diagnostics.Debug.WriteLine($"[OK] {str}");
            }else
            {
                System.Diagnostics.Debug.WriteLine($"[ERREUR] {str} ~ {errorExpected}");
            }



        }
    }
}
