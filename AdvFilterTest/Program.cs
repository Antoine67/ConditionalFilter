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
            //Expected to work
            Test("and(eq('a','b'),eq('c','d'))");
            Test("or(and(eq('a','b'),eq('c','d')),and(eq('a','b'),eq('c','d')))");
            Test("and(or(and(eq('a','b'),eq('c','d')),and(eq('e','f'),eq('g','h'))),or(and(eq('i','j'),eq('k','l')),and(eq('m','n'),eq('o','p'))))");

            Test("and(eq(a,b),eq(c,d))");
            Test("and(eq(1,2),eq(3,4))");

            Test(@"and(eq('\aa','b'),eq('c','d'))");
            Test(@"and(eq('\(a','b'),eq('c','d'))");

            //Expected to fail
            Test("and(eq('a','b'), eq('(c','d'))", shouldFail:true);
            Test("and(eq('a','b'), eq('c','d')))", shouldFail: true);

            Test("and(eq('a','b'), ne('c','d'))", shouldFail: true);
        }

        static void Test(string str, bool shouldFail = false)
        {
            //System.Diagnostics.Debug.WriteLine($"\n[STARTING]  {str}\n ");
            try
            {
                var af = new AdvancedFilter(str);
                af.ParseAndBuild();
            }catch(Exception e) {
                if (!shouldFail)
                    System.Diagnostics.Debug.WriteLine($"[ERROR] {str} ~ {e}");
                else
                    System.Diagnostics.Debug.WriteLine($"[OK] {str} ~ {e.Message}");
                return;
            }

            if(!shouldFail)
            {
                System.Diagnostics.Debug.WriteLine($"[OK] {str}");
            }else
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] {str} ~ {shouldFail}");
            }



        }
    }
}
