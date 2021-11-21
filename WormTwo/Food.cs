using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WormTwo
{
    public class FoodList
    {
        private Random r;
        private List<Food> FoodCont;

        public FoodList()
        {
            r = new Random();
            FoodCont = new List<Food>();
        }

        private List<Food> CopyLisy()
        {
            List<Food> ret = new List<Food>();
            foreach (var one in FoodCont)
            {
                ret.Add(one.Copy());
                
            }

            return ret;
        }
        public List<Food> GetList()
        {
            return CopyLisy();
        }

        public void AddFood()
        {
            Food a = new Food(r);
            while (!Isempty(a))
            {
                a = new Food(r);
            }
            FoodCont.Add(a);
        }

        private bool Isempty(Food a)
        {
            foreach (Food VARIABLE in FoodCont)
            {
                if (VARIABLE.x == a.x && VARIABLE.y == a.y)
                {
                    return false;
                }
            }

            return true;
        }
        public bool Eat(WormList AllWorms)
        {
            bool ret = false;
            List<Food> eat = new List<Food>();
            List<Worm> slist = AllWorms.GetList();
            foreach (var worm in slist)
            {
                foreach (var food in FoodCont)
                {
                    if (worm.getX() == food.x && worm.getY() == food.y)
                    {
                        worm.Eat();
                        eat.Add(food);
                        ret = true;
                    }
                }
                    
            }
            AllWorms.accnewlist(slist);

            foreach (var food in eat)
            {
                FoodCont.Remove(food);
            }

            return ret;
        }
        public void IsDie()
        {
            foreach (var food in FoodCont)
            {
                if (food.life <= 0)
                {
                    FoodCont.Remove(food);
                }
            }
        }

        public string Info()
        {
            StringBuilder ret = new StringBuilder("");
            ret.Append("Food:[");
            foreach (var food in FoodCont)
            {
                ret.Append("(" + food.x + "," + food.y+"),");
            }

            ret.Append("]");
            return ret.ToString();
        }
    }
    
    public class Food
    {
        private int[] coord = new int[2] ;

        public int x
        {
            get
            {
                return coord[0];
            }
        }
        public int y
        {
            get
            {
                return coord[1];
            }
        }

        public int life = 10;
        public Food(Random r)
        {
            coord[0] = NextNormal(r);
            coord[1] = NextNormal(r);
        }

        public Food(int inx,int iny)
        {
            coord[0]= inx;
            coord[1] = iny;
        }
        
        public int NextNormal(Random r, double mu = 0, double sigma = 5)
        {

            var u1 = r.NextDouble();

            var u2 = r.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); 

            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);

        }
        

        public Food Copy()
        {
            Food ret = new Food(x, y);
            return ret;
        }
        
        
    }
}