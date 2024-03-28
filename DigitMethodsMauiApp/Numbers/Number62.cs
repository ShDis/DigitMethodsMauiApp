namespace DigitMethodsMauiApp.Numbers
{
    public class Number62 : Number6
    {
        public double a { get; } = 1.0;
        public double b { get; } = 2.0;
        public double y0 { get; } = 1;
        public double f(double x, double y)
        {
            return -(1 + x * y) / Math.Pow(x, 2);
        }
    }
}
