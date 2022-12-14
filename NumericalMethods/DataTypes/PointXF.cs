namespace NumericalMethods
{
    public class PointXF
    {
        public double X { get; internal set; } = double.NaN;
        public double F { get; internal set; } = double.NaN;

        public PointXF(double x, double f) { X = x; F = f; }

        public string ToPrint()
        {
            return $"    ({X,16:F10},{F,16:F10} )";
        }
    }
}
