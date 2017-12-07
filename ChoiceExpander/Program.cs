using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoiceExpander
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var items = new string[] { "1", "2", "13" };

            var query = items.Expand<string>();
            query.DisplayElement<string>();

            //var qqq = from s1 in items
            //          from s2 in items
            //          from s3 in items
            //          where s1 != s2 && s2 != s3 && s1 != s3 //重複除去
            //          select new { X1 = s1, X2 = s2, X3 = s3 };

            //qqq.DisplayElement();
        }
    }
}
