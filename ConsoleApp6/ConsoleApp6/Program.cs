using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;


namespace ConsoleApp6
{
    class Program
    {   
        static void Main(string[] args)
        {
           
            string side = "top";
            Random u = new Random();
            int o = u.Next(1, 9);
            int p = u.Next(1, 9);

            char[,] slide = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((i == 0) || (i == 9) || (j == 0) || (j == 9))
                    {
                        slide[i, j] = '*';
                    }
                    else
                    {
                        slide[i, j] = ' ';
                    }

                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(slide[i, j]);
                    if (j == 9)
                    {
                        Console.Write("\n");
                    }
                }
            }

            List<coordinate> newcoordinate = new List<coordinate>();
            coordinate a = new coordinate
            {
                xi = 4,
                yj = 5
            };
            coordinate b = new coordinate
            {
                xi = 4,
                yj = 6
            };
            coordinate c = new coordinate
            {
                xi = 4,
                yj = 7
            };
            coordinate d = new coordinate
            {
                xi = 4,
                yj = 8
            };
            newcoordinate.Add(a);
            newcoordinate.Add(b);
            newcoordinate.Add(c);
            newcoordinate.Add(d);
            CreateFrog(newcoordinate,slide,o,p,u);
            /////////////////////
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += (sender, e) => ElapsedEventHandler(sender, e, side, newcoordinate, slide,o,p,u);
            t.Interval = 2000;
            t.Enabled = true;
            /////////////////////
            //for (int f = 0; f < newcoordinate.Count; f++)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            if ((newcoordinate.ElementAt(f).xi == i)&& (newcoordinate.ElementAt(f).yj == j))
            //            {
            //                Console.SetCursorPosition(i,j);
            //                Console.Write("q");
            //            }
            //        }
            //    }
            //}


            bool noexit = true;
            while (noexit)
            {
               char q= Console.ReadKey(true).KeyChar;
                if (q == 'a')
                {
                    if (side=="top")
                    {
                        side = "left";
                    }
                    else if (side == "bot")
                    {
                        side = "right";
                    }
                    else if (side == "left")
                    {
                        side = "bot";
                    }
                    else if (side == "right")
                    {
                        side = "top";
                    }
                }
                else if (q == 'd')
                {
                    if (side == "top")
                    {
                        side = "right";
                    }
                    else if (side == "bot")
                    {
                        side = "left";
                    }
                    else if (side == "left")
                    {
                        side = "top";
                    }
                    else if (side == "right")
                    {
                        side = "bot";
                    }
                }

                if (q=='o')
                {
                    noexit = false;
                }
            }

            

            
            Console.Read();
        }
        public static void ResetScreen(List<coordinate> newcoordinate, char[,] slide)
        {
            Console.Clear();
            
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        
                        
                        Console.Write(slide[i, j]);
                        if (j == 9)
                        {
                            Console.Write("\n");
                        }
                    }
                }
            
        }
        public static void Lose(List<coordinate> newcoordinate)
        {
            for (int i = 0; i < newcoordinate.Count; i++)
            {
                if ((newcoordinate.ElementAt(0).xi == newcoordinate.ElementAt(i).xi) && (newcoordinate.ElementAt(0).yj == newcoordinate.ElementAt(i).yj))
                {
                    Console.WriteLine("YOU LOST");
                    Console.Read();
                }
            }
        }
        public static void MoveStraight(string side, List<coordinate> newcoordinate, char[,] slide)
        {   
            coordinate a = new coordinate();
            if (side == "top")
            {
                a.xi =newcoordinate.ElementAt(0).xi;
                a.yj = newcoordinate.ElementAt(0).yj-1;
                newcoordinate.RemoveAt(newcoordinate.Count-1);
                newcoordinate.Insert(0,a);
            }
            else if (side == "bot")
            {
                a.xi = newcoordinate.ElementAt(0).xi;
                a.yj = newcoordinate.ElementAt(0).yj + 1;
                newcoordinate.RemoveAt(newcoordinate.Count - 1);
                newcoordinate.Insert(0, a);
            }
            else if (side == "left")
            {
                a.xi = newcoordinate.ElementAt(0).xi-1;
                a.yj = newcoordinate.ElementAt(0).yj;
                newcoordinate.RemoveAt(newcoordinate.Count - 1);
                newcoordinate.Insert(0, a);
            }
            else if (side == "right")
            {
                a.xi = newcoordinate.ElementAt(0).xi+1;
                a.yj = newcoordinate.ElementAt(0).yj;
                newcoordinate.RemoveAt(newcoordinate.Count - 1);
                newcoordinate.Insert(0, a);
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (slide[i,j]=='q')
                    {
                        slide[i, j] = ' ';
                    }
                }
            }
            for (int i = 0; i < newcoordinate.Count; i++)
            {
                slide[newcoordinate.ElementAt(i).xi, newcoordinate.ElementAt(i).yj]='q';
            }
        }
        public static void EatFrog(string side, List<coordinate> newcoordinate, char[,] slide,int o,int p,Random u )
        {
            if ((newcoordinate.ElementAt(0).xi==o)&&(newcoordinate.ElementAt(0).yj == p))
            {
                slide[o, p] = 'q';
                coordinate a = new coordinate();
                if (side == "top")
                {
                    a.xi = newcoordinate.ElementAt(newcoordinate.Count-1).xi;
                    a.yj = newcoordinate.ElementAt(newcoordinate.Count-1).yj + 1;
                    newcoordinate.Add(a);
                }
                else if (side == "bot")
                {
                    a.xi = newcoordinate.ElementAt(newcoordinate.Count-1).xi;
                    a.yj = newcoordinate.ElementAt(newcoordinate.Count-1).yj - 1;
                    newcoordinate.Add(a);
                }
                else if (side == "left")
                {
                    a.xi = newcoordinate.ElementAt(newcoordinate.Count-1).xi + 1;
                    a.yj = newcoordinate.ElementAt(newcoordinate.Count-1).yj;
                    newcoordinate.Add(a);
                }
                else if (side == "right")
                {
                    a.xi = newcoordinate.ElementAt(newcoordinate.Count-1).xi - 1;
                    a.yj = newcoordinate.ElementAt(newcoordinate.Count-1).yj;
                    newcoordinate.Add(a);
                }
                CreateFrog(newcoordinate,slide,o,p,u);
            }
            

        }
        public static void CreateFrog(List<coordinate> newcoordinate, char[,] slide,int o,int p,Random u)
        {
            bool qwerty = true;
            int count=0;
            while(qwerty)
            {
                o = u.Next(1, 9);
                p = u.Next(1, 9);
                foreach (var d in newcoordinate)
                {
                    if ((d.xi!=o)&&(d.yj!=p))
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                if (count == newcoordinate.Count)
                {
                    slide[o, p] = 'F';
                    qwerty = false;
                }
            }
            
            
        }
        static void ElapsedEventHandler(object sender, ElapsedEventArgs e,string side, List<coordinate> newcoordinate, char[,] slide,int o,int p,Random u)
        {
            MoveStraight(side, newcoordinate,slide);
            //Lose(newcoordinate);
            EatFrog(side, newcoordinate, slide,o,p,u);
            ResetScreen(newcoordinate, slide);

        }
    }
    class coordinate
    {
        public int xi { get; set; }
        public int yj { get; set; }
    }
    
}
