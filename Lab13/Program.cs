using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Countries_Lab11;

using static System.Console;

namespace Lab13
{
    public static class Extension1
    {
        public static Country Downgrade(this Republic m)
        {
            return (Country)((Country)m).Clone();
        }
    }

    class Program
    {
        static Stopwatch TrackT(bool p, Action action)
        {
            var sw = new Stopwatch();
            sw.Restart();

            action();

            sw.Stop();
            return sw;
        }

        static Tuple<string, long> TrackT(string s1, string s2, Action action)
        {
            var sw = TrackT(true, action);
            return new Tuple<string, long>(string.Format(s1, s2, sw.ElapsedTicks), sw.ElapsedTicks);
        }

        static long TrackT(Action action)
        {
            return TrackT(true, action).ElapsedTicks;
        }

        static void Main(string[] args)
        {
            WriteLine("Сколько элементов создать?");

            int n;
            while (!int.TryParse(ReadLine(), out n) || n < 0)
                WriteLine("    Введите целое положительное число");

            var c = new TestCollecions();

            var creation = TrackT(true, delegate
            {
                int ExceptionCount = 0;
                n++;
                int pcount = n/100;
                for (int i = 0; i < n; i++)
                    try
                    {
                        if(c.c1.Count >0 && c.c1.Count % pcount == 0)
                            WriteLine("  "  + i);
                        c.Add();
                    }
                    catch (Exception e)
                    {
                        ExceptionCount++;
                        if (ExceptionCount % 1 == 0)
                            WriteLine("    Словлено {0} исключений ({1})", ExceptionCount, e.Message);
                        i--;
                    }
            });
            WriteLine("Коллекции заполнены за {0} миллисекунд", creation.ElapsedMilliseconds);

            #region findFirst

            bool? found = null;
            string findFirst = "Первый элемент в {0} коллекции {2} за {1} тиков";

            long find11 = TrackT(delegate
            {
                found = c.c1.Contains((Country)c.c1.First.Value.Clone());
            });
            WriteLine(findFirst, "1", find11, found);

            long find12 = TrackT(delegate
            {
                found = c.c2.Contains((string)c.c2.First.Value.Clone());
            });
            WriteLine(findFirst, "2", find12, found);

            long find13 = TrackT(delegate
            {
                found = c.c3.ContainsKey((Country)c.c3.Keys.First().Clone());
            });
            WriteLine(findFirst, "3", find13, found);

            long find14 = TrackT(delegate
            {
                found = c.c4.ContainsKey((string)c.c4.Keys.First().Clone());
            });
            WriteLine(findFirst, "4", find14, found);

            long find15 = TrackT(delegate
            {
                found = c.c3.ContainsValue((Republic)c.c3.Values.First().Clone());
            });
            WriteLine(("(Значение) "+findFirst), "3", find15, found);

            #endregion findFirst

            #region findMid

            string findMid = "Средний элемент в коллекции {0} {2} за {1} тиков";

            long find21 = TrackT(delegate
            {
                found = c.c1.Contains((Country)c.c1.ElementAt(c.c1.Count / 2).Clone());
            });
            WriteLine(findMid, "1", find21, found);

            long find22 = TrackT(delegate
            {
                found = c.c2.Contains((string)c.c2.ElementAt(c.c2.Count / 2).Clone());
            });
            WriteLine(findMid, "2", find22, found);

            long find23 = TrackT(delegate
            {
                found = c.c3.ContainsKey((Country)c.c3.Keys.ElementAt(c.c3.Count / 2).Clone());
            });
            WriteLine(findMid, "3", find23, found);

            long find24 = TrackT(delegate
            {
                found = c.c4.ContainsKey((string)c.c4.Keys.ElementAt(c.c4.Count / 2).Clone());
            });
            WriteLine(findMid, "4", find24, found);

            long find25 = TrackT(delegate
            {
                found = c.c3.ContainsValue((Republic)c.c3.Values.ElementAt(c.c3.Count / 2).Clone());
            });
            WriteLine(("(Значение) " + findMid), "3", find25, found);
            #endregion

            #region findLast

            string findLast = "Последний элемент в коллекции {0} {2} за {1} тиков";

            long find31 = TrackT(delegate
            {
                found = c.c1.Contains((Country)c.c1.Last.Value.Clone());
            });
            WriteLine(findLast,"1",find31, found);

            long find32 = TrackT(delegate
            {
                found = c.c2.Contains((string)c.c2.Last.Value.Clone());
            });
            WriteLine(findLast, "2", find32, found);

            long find33 = TrackT(delegate
            {
                found = c.c3.ContainsKey((Country)c.c3.Keys.Last().Clone());
            });
            WriteLine(findLast, "3", find33, found);

            long find34 = TrackT(delegate
            {
                found = c.c4.ContainsKey((string)c.c4.Keys.Last().Clone());
            });
            WriteLine(findLast, "4", find34, found);

            long find35 = TrackT(delegate
            {
                found = c.c3.ContainsValue((Republic)c.c3.Values.Last().Clone());
            });
            WriteLine(("(Значение) "+findLast), "3", find35, found);

            #endregion

            #region findWrond

            string findWrong = "Элемент не находящийся в коллекции {0} {2} за {1} тиков";

            var item = new Republic("Континент", 987654, "Имя страны", "Прваитель страны", new[]
            {
                "Член парламента 1",
                "Член парламента 2",
                "Член парламента 3",
                "Член парламента 4",
                "Член парламента 5",
                "Член парламента 6"
            });

            long find41 = TrackT(delegate
            {
                found = c.c1.Contains(item.Downgrade());
            });
            WriteLine(findWrong, "1", find41, found);

            long find42 = TrackT(delegate
            {
                found = c.c2.Contains(item.ToString());
            });
            WriteLine(findWrong, "2", find42, found);

            long find43 = TrackT(delegate
            {
                found = c.c3.ContainsKey(item.Downgrade());
            });
            WriteLine(findWrong, "3", find43, found);

            long find44 = TrackT(delegate
            {
                found = c.c4.ContainsKey(item.ToString());
            });
            WriteLine(findWrong, "4", find44, found);

            long find45 = TrackT(delegate
            {
                found = c.c3.ContainsValue(item);
            });
            WriteLine(("(Значение) " + findWrong), "3", find45, found);

            #endregion
            
            ReadKey(true);
        }
    }
}