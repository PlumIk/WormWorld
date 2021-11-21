using System;
using System.Collections.Generic;
using System.IO;

namespace WormTwo
{
    public class Work
    {
        private string FileName = "/home/alex/Prog/Reshotka/WormTwo/WormTwo/out.txt";
        private FoodList AllFood;
        private NameGive namer ;
        private WormList AllWorms ;

        public void Do()
        {
            namer = new NameGive();
            AllFood = new FoodList();
            AllWorms = new WormList(namer.GetName());
            DoSame();
        }
        
        void DoSame()
        {
            
            int one=1;
            int[] w;
            
            File.WriteAllText(FileName, "Start:\n");
            AllFood.AddFood();
            while (AllFood.Eat(AllWorms))
            {
                AllFood.AddFood();
            }
            File.AppendAllText(FileName, AllWorms.Info()+","+AllFood.Info()+"\n");
            AllFood.Eat(AllWorms);
            while (one<100)
            {
                one++;
                File.AppendAllText(FileName,"more");
                //Проверка на съедение
                
                
                //Добавляем еду и, если мгновенно съели, повторяем пока не появится на пустом месте
                AllFood.AddFood();
                while (AllFood.Eat(AllWorms))
                {
                    AllFood.AddFood();
                }
                
                //Убираем мёртвых и обновляем листы
                AllWorms.IsDie();
                AllFood.IsDie();

                List<Worm> stepl = new List<Worm>();
                List<Worm> prevList = AllWorms.GetList();
                foreach (var Test in prevList)
                {
                    w = Test.DoMyStep(AllFood.GetList(), prevList);
                    bool can = true;
                    foreach (var WormOne in prevList)
                    {
                        if (w[0] == WormOne.getX() && w[1] == WormOne.getY())
                        {
                            can = false;
                        }
                    }

                    if (can)
                    {
                        if (w.Length == 2)
                        {

                            if (can)
                            {
                                Test.AcStep(w);
                                
                            }
                        }
                        else
                        {
                           stepl.Add( AllWorms.AddOne(namer.GetName(), Test, w[0], w[1]));
                        }
                    }
                    stepl.Add(Test);
                    

                }
                AllWorms.accnewlist(stepl);
                
                //Проверка на съедение
                if( AllFood.Eat(AllWorms))
                {
                    Console.WriteLine(AllWorms.Info());
                }
                File.AppendAllText(FileName, AllWorms.Info()+","+AllFood.Info()+"\n");
            }
        }

        void PrintField(int[] inMas)
        {
            for (int i = 0; i < 12; i++)
            {
                string outw = "";
                for (int j = 0; j < 12; j++)
                {
                    if (inMas[0] + 5 == j && inMas[1] + 5 == i)
                    {
                        outw += "1";
                    }
                    else
                    {
                        outw += "0";
                    }
                }

                Console.WriteLine(outw);
            }
       
        }
    }
}