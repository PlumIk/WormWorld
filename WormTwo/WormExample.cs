using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WormTwo;

public class WormList
{
    private List<Worm> WormsList = new List<Worm>();

    
    public WormList(string name)
    {
        WormsList.Add(new Worm().DoOne(name));
    }

    public void accnewlist(List<Worm> inl)
    {
        WormsList = inl;
    }

    public List<Worm> GetList()
    {
        List<Worm> ret = new List<Worm>();
        
        foreach (var one in WormsList)
        {
            ret.Add(one.Copy());
        }
        
        return ret;
    }

    public Worm AddOne(string name, Worm father,int StepX, int StepY)
    {
        return father.OneMore(StepX,StepY,name);
    }

    public void IsDie()
    {
        List<Worm> a = new List<Worm>();
        foreach (var worm in WormsList)
        {
            if (worm.life <= 0)
            {
                a.Add(worm);
            }
        }
        foreach (var worm in a)
        {
            if (worm.life <= 0)
            {
                WormsList.Remove(worm);
            }
        }
        
    }
    
    public string Info()
    {
        StringBuilder ret = new StringBuilder("");
        ret.Append("Worms:[");
        foreach (var worm in WormsList)
        {
            ret.Append(worm.getName()+"-"+worm.life+ "(" + worm.getX() + "," + worm.getY()+"),");
        }
        ret.Append("]");
        return ret.ToString();
    }
}

public class Worm
{
    public enum MyEnum
    {
        Patrol=0,
        IGoToThePoint=2,
        IGoToTheFood=3
    }

    public MyEnum task = MyEnum.Patrol;
    public int[] point = new int[2];
    private int x, y;
    public int life = 10;
    
    private string Name { get; set; }

    public string getName()
    {
        return Name;
    }
    
    public Worm(){}

    private Worm(int x, int y, string name)
    {
        this.x = x;
        this.y = y;
        Name = name;
    }
    
    public Worm DoOne(string name)
    {
        Name = name;
        x = 0;
        y = 0;
        return this;
    }

    void SetCoord(int a, int b)
    {
        x = a;
        y = b;
    }
        
    public Worm OneMore(int StepX, int StepY, string name)
    {
        Worm child = new Worm(StepX,StepY,name);
        life -= 10;
        return child;
    }

    public int getX()
    {
        return x;
    }
        
    public int getY()
    {
        return y;
    }

/*
    private int[][] PGaff;

    private void Rec(int inX, int inY, int step)
    {
        //Console.WriteLine(inX+" "+inY);
        if (PGaff[inX][inY] > step||PGaff[inX][inY]==0)
        {
            
            PGaff[inX][inY] = step;
            step++;
            if (step <= life)
            {
                Rec(inX + 1,inY,step);
                Rec(inX - 1,inY,step);
                Rec(inX ,inY+1,step);
                Rec(inX ,inY-1,step);
            }
        }
        
    }

    private int min(int a, int b, int c, int d)
    {
        if (a>b)
        {
            a = b;
        }

        if (c > d)
        {
            c = d;
        }

        if (a < c)
        {
            return a;
        }

        return c;
    }

    private int RecFound(int inX, int inY, int step)
    {
        if (PGaff[inX][inY] < 0)
        {
            return step;
        }

        if (PGaff[inX][inY] < life)
        {
            step++;
            if (step <= life)
            {
                return min(RecFound(inX + 1, inY , step),
                    RecFound(inX-1 , inY , step),
                    RecFound(inX , inY + 1, step),
                    RecFound(inX , inY - 1, step));
            }
        }

        return life + 1;
    }

    private void PrintPG()
    {
        Console.WriteLine("----");
        for(int i=0;i<2*life+1;i++)
        {
            Console.Write(PGaff[i][0] + " ");
            for(int j=1;j<2*life+1;j++)
            {
                Console.Write(PGaff[i][j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("----");
    }

    private int InitPGraf(List<Food> FoodAround, List<Worm> Worms)
    {
        PGaff=new int[(2*life+1)][];
        for(int i=0;i<2*life+1;i++)
        {
            PGaff[i] = new int[life * 2 + 1];
        }

        foreach (var one in Worms)
        {
            if (Math.Abs(one.getX() - x) <= life && Math.Abs(one.getY() - y)<=life)
            {
                PGaff[life + one.getX() - x][life + one.getY() - y] = life + 1;
            }
        }


        Rec(life,life,0);
        for(int i=0;i<2*life+1;i++)
        {
            for(int j=0;j<2*life+1;j++)
            {
                if (PGaff[i][j] == 0)
                {
                    PGaff[i][j] = life + 1;
                }
            }
        }
        
        
        
        PGaff[life][life] = 0;
        
        foreach (var one in FoodAround)
        {
            if (Math.Abs(one.x - x) <= life && Math.Abs(one.y - y)<=life)
            {
                PGaff[life + one.x - x][life + one.y - y] *=-1 ;
            }
        }

        int[] vec = new int[4];
        vec[0] = RecFound(life, life + 1, 1);
        vec[1] = RecFound(life+1, life , 1);
        vec[2] = RecFound(life, life - 1, 1);
        vec[3] = RecFound(life-1, life , 1);

        if (vec[0] <= vec[1] && vec[0] <= vec[2] && vec[0] <= vec[3])
        {
            if (vec[0] <= life)
            {
                return 0;
            }

            return -1;
        }
        if (vec[1] <= vec[2] && vec[1] <= vec[3] )
        {
            if (vec[1] <= life)
            {
                return 1;
            }

            return -1;
        }
        if(vec[2] <= vec[3])
        {
            if (vec[2] <= life)
            {
                return 2;
            }

            return -1;
        }
        if (vec[3] <= life)
        {
            return 3;
        }

        return -1;
    }

*/
    public int[] DoMyStep(List<Food> FoodAround, List<Worm> Worms)
    {
        int[] ret={x,y} ;
        
        /*
        int ch= InitPGraf(FoodAround,Worms);

        if (ch == 0)
        {
            ret[1] += 1;
        }
        else if (ch==1)
        {
            ret[0] += 1;
        }
        else if(ch==2)
        {
            ret[1] -= 1;
        }else if(ch==3)
        {
            ret[0] -= 1;
        }
       
*/

        if (life >= 14 && (new Random().Next()) % 5 <=2)
        {
            bool[] canplace = new bool[] {true, true, true, true};
            foreach (var one in Worms)
            {
                int i = 0;
                if (one.getX() == (x + 1) && one.getY() == y)
                {
                    canplace[i] = false;
                }

                if (one.getX() == (x - 1) && one.getY() == y)
                {
                    canplace[i] = false;
                }

                if (one.getX() == (x) && one.getY() == y + 1)
                {
                    canplace[i] = false;
                }

                if (one.getX() == (x + 1) && one.getY() == y - 1)
                {
                    canplace[i] = false;
                }
            }

            if (canplace[0] || canplace[1] || canplace[2] || canplace[3])
            {
                ret = new[] {x, y, 0};
                if (canplace[0])
                {
                    ret[0] += 1;
                }
                else if (canplace[1])
                {
                    ret[0] -= 1;
                }
                else if (canplace[2])
                {
                    ret[1] += 1;
                }
                else if (canplace[3])
                {
                    ret[1] -= 1;
                }

                return ret;
            }
        }

    

        life--;
        foreach (var FoodEx in FoodAround)
        {
            
            int path = Math.Abs(x - FoodEx.x) + Math.Abs(y - FoodEx.y);
            if (path <= life&&path<=FoodEx.life)
            {
                if (task < MyEnum.IGoToThePoint)
                {
                    task = MyEnum.IGoToTheFood;
                    point = new[] {FoodEx.x,  FoodEx.y};
                }
                else if (task==MyEnum.IGoToTheFood)
                {
                    if (Math.Abs (point[0] - x) + Math.Abs(point[1] - y) > path)
                    {
                        point = new[] {FoodEx.x,  FoodEx.y};
                    }
                }
            }
        }

        if (task == MyEnum.IGoToTheFood)
        {
            if (x != point[0])
            {
                if (point[0] > x)
                {
                    ret = new[] {x + 1, y};
                }
                else
                {
                    ret = new[] {x - 1, y};
                }
            }
            else if (y != point[1])
            {
                if (point[1] > y)
                {
                    ret = new[] {x , y+1};
                }
                else
                {
                    ret = new[] {x , y-1};
                }
            }
            else
            {
                task = MyEnum.Patrol;
            }
        }

        if (task ==MyEnum.Patrol)
        {
            ret = new[] {x, y};
        }

        return ret;
    }

    public void Eat()
    {
        life += 10;
        task = MyEnum.Patrol;
    }
    

    public void AcStep(int[] ac)
    {
        SetCoord(ac[0],ac[1]);
    }

    public Worm Copy()
    {
        Worm ret = new Worm(x,y,Name);
        ret.life = life;
        return ret;
    }
}