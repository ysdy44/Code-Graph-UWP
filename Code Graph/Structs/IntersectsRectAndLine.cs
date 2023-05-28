using Windows.Foundation;

namespace Code_Graph
{
    public readonly struct IntersectsRectAndLine
    {
        public readonly Point Arrow;
        public IntersectsRectAndLine(double w, double h, double x2, double y2, double x1, double y1)
        {
            double w1 = x2 - w / 2;
            double w2 = x2 + w / 2;
            double h1 = y2 - h / 2;
            double h2 = y2 + (h / 2);

            double k = (y1 - y2) / (x1 - x2);
            double b = y2 - k * x2;

            double xInt, yInt;

            // Bottom
            yInt = h2;
            xInt = (yInt - b) / k;
            if (xInt >= w1 && xInt <= w2)
            {
                this.Arrow = new Point(xInt, yInt);
                return;
            }

            // Right
            xInt = w2;
            yInt = k * xInt + b;
            if (yInt >= h1 && yInt <= h2)
            {
                this.Arrow = new Point(xInt, yInt);
                return;
            }

            // Top
            yInt = h1;
            xInt = (yInt - b) / k;
            if (xInt >= w1 && xInt <= w2)
            {
                this.Arrow = new Point(xInt, yInt);
                return;
            }

            // Left
            xInt = w1;
            yInt = k * xInt + b;
            if (yInt >= h1 && yInt <= h2)
            {
                this.Arrow = new Point(xInt, yInt);
                return;
            }
        }
    }
}