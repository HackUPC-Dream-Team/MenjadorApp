using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menjadora
{
    class ClassFeed
    {
            private int amount;
            private int hour;
            private int min;

            public ClassFeed(int Amount, int Hour, int Min)
            {
                hour = Hour;
                amount = Amount;
                min = Min;
            }



            public int Amount
            {
                get
                {
                    return amount;
                }
                set
                {
                    amount = value;
                }
            }


        public int Hour
        {
            get
            {
                return hour;
            }
            set
            {
                hour = value;
            }
        }

        public int Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }



    }

    
}
