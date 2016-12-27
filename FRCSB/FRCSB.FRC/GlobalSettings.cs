using System;
using System.Collections.Generic;
using System.Text;

namespace FRCSB.FRC
{
    class GlobalSettings
    {
        public static string year="2016";
        public static string[] years;
        public static string[] getYears()
        {

            List<string> _years = new List<string>();
            for(int i= DateTime.Now.Year;i>=2010;i--)
            {
               _years.Add(i.ToString());
            }
            years = _years.ToArray();
            return _years.ToArray();

        }
    }
}
