using System;
using System.Collections.Generic;
using Countries_Lab11;

namespace Lab13
{
    public class TestCollecions //<Country, Republic> //where TValue: TKey where TKey: ICloneable,IComparable
    {
        public static Random R = new Random();

        public LinkedList<Country> c1 = new LinkedList<Country>();
        public LinkedList<string> c2 = new LinkedList<string>();

        public SortedDictionary<Country, Republic> c3 = new SortedDictionary<Country, Republic>();
        public SortedDictionary<string, Republic> c4 = new SortedDictionary<string, Republic>();

        public TestCollecions ()
        {
            
        }
        
        public bool Remove(Republic item)
        {
            if (!c1.Contains(item.Downgrade()))
                return false;
            c1.Remove(item.Downgrade());
            c2.Remove(item.ToString());
            c3.Remove(item.Downgrade());
            c4.Remove(item.ToString());
            return true;
        }

        public void Add()
        {
            Add(Generate(R.Next()));
        }

        public void Add(Republic item)
        {
            if (c3.ContainsKey(item.Downgrade()))
                throw new KeyException();

            c1.AddLast(item.Downgrade());
            c2.AddLast(item.ToString());
            c3.Add(item.Downgrade(), item);
            c4.Add(item.ToString(), item);
        }

        public static Republic Generate(long seed)
        {
            var result = new Republic("", 0, "", "", new []{""})
            {
                Name = seed.GetHashCode().ToString(),
                Continent = (seed % 10).ToString(),
                Population = (ulong)Math.Abs(seed / 100),
                Ruler = (seed % 99).ToString(),
                Parlament = new string[5]
            };

            for (int i = 0; i < 5; i++)
            {
                seed /= 10;
                result.Parlament[i] = (seed%10).ToString();
            }
                
            return result;
        }
    }
}