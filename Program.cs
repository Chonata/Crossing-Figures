using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string fig1 = Console.ReadLine();
            string fig2 = Console.ReadLine();
            if (fig2[0] == 'r')
            {
                string temp = fig1;
                fig1 = fig2;
                fig2 = temp;
            }
            double[] param = new double[4];
            string substring = Regex.Replace(fig1.Substring(10, fig1.Length - 11), @"\s+", "");
            string[] arr = substring.Split(',');
            for (int j = 0; j < param.Length; j++)
            {
                param[j] = double.Parse(arr[j].Replace('.', ','));
            }
            Rectangle rect = new Rectangle(param[0], param[1], param[2], param[3]);
            param = new double[3];
            substring = Regex.Replace(fig2.Substring(7, fig2.Length - 8), @"\s+", "");
            arr = substring.Split(',');
            for (int j = 0; j < param.Length; j++)
            {
                param[j] = double.Parse(arr[j].Replace('.', ','));
            }
            Circle circ = new Circle(param[0], param[1], param[2]);
            bool inside = centerInside(rect, circ);
            if (calcDistance(rect.lowerLeft, circ.center) - circ.radius < 0.01
            && calcDistance(rect.lowerRight, circ.center) - circ.radius < 0.01
            && calcDistance(rect.upperLeft, circ.center) - circ.radius < 0.01
            && calcDistance(rect.upperRight, circ.center) - circ.radius < 0.01) { Console.WriteLine("Rectangle inside circle"); }
            else if (circ.center.x - circ.radius >= rect.lowerLeft.x - 0.01 &&
            circ.center.x + circ.radius <= rect.lowerRight.x - 0.01 &&
            circ.center.y - circ.radius >= rect.lowerLeft.y - 0.01 &&
            circ.center.y + circ.radius <= rect.upperLeft.y - 0.01) { Console.WriteLine("Circle inside rectangle"); }
            else if (calcDistance(rect.center, circ.center) > circ.radius + calcDistance(rect.upperLeft, rect.lowerRight) / 2)
            {
                Console.WriteLine("Rectangle and circle do not cross");
            }
            else { Console.WriteLine("Rectangle and circle cross"); }
        }
    }
    public static double calcDistance(Coords a, Coords b)
    {
        return Math.Sqrt(Math.Pow(Math.Abs(a.x - b.x), 2) + Math.Pow(Math.Abs(a.y - b.y), 2));
    }
    public static bool centerInside(Rectangle rectum, Circle circum) {
        if (rectum.upperLeft.x < circum.center.x
            && circum.center.x < rectum.upperRight.x
            && rectum.lowerLeft.y < circum.center.y
            && rectum.upperLeft.y > circum.center.y)
        { return true; }
        else { return false;  }
    }
}
struct Coords {
   public double x;
   public double y;
    public Coords(double X, double Y) {
        x = X;
        y = Y;
    }
}
class Rectangle {
   public Coords upperLeft;
    public Coords upperRight;
    public Coords lowerLeft;
    public Coords lowerRight;
    public Coords center;
    public Rectangle(double Ax, double Ay, double Bx, double By) {
        upperLeft = new Coords(Ax, Ay);
        lowerRight = new Coords(Bx, By);
        upperRight = new Coords(Bx, Ay);
        lowerLeft = new Coords(Ax, By);
        center = new Coords((Bx + Ax) / 2, (By + Ay) / 2);
    }
}
class Circle {
    public Coords center;
    public double radius;
    public Circle(double Ax, double Ay, double Radius) {
        center = new Coords(Ax, Ay);
        radius = Radius;
    }
}