using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSB.FRC
{
    public abstract class FRCObject
    {
        public virtual string value { get; set;}
		public  object objectValue { get { return this; } }
       

    }
}
