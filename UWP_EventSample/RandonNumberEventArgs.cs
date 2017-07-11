using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_EventSample
{
     public class RandonNumberEventArgs: EventArgs
    {
        private Random r = new Random();
        public double Value { get; private set; }

        public RandonNumberEventArgs()
        {
            Value = r.NextDouble();
        }
    }
}
