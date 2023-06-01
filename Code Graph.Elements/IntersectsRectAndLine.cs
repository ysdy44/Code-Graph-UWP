using Windows.Foundation;

namespace Code_Graph.Elements
{
    public readonly struct IntersectsRectAndLine
    {
        public readonly Point Arrow;

        public IntersectsRectAndLine(double w, double h, double x2, double y2, double x1, double y1)
        {
            this.Arrow = IntersectsRectAndLine.DirectVector((x2 - x1) / w, (y2 - y1) / h);
            this.Arrow.X *= w;
            this.Arrow.Y *= h;
            this.Arrow.X += x2;
            this.Arrow.Y += y2;
        }

        public static IntersectsDirect GetDirect(double vectorX, double vectorY)
        {
            if (vectorX == 0)
            {
                if (vectorY == 0)
                    return IntersectsDirect.Center;
                else if (vectorY > 0)
                    return IntersectsDirect.Bottom;
                else
                    return IntersectsDirect.Top;
            }
            else if (vectorY == 0)
            {
                if (vectorX > 0)
                    return IntersectsDirect.Right;
                else
                    return IntersectsDirect.Left;
            }
            else
            {
                double absX = System.Math.Abs(vectorX);
                double absY = System.Math.Abs(vectorY);

                // First Quantity
                if (vectorX > 0 && vectorY > 0)
                {
                    if (absX == absY) return IntersectsDirect.RightBottom;
                    else if (absX > absY) return IntersectsDirect.Right2Bottom;
                    else return IntersectsDirect.Bottom2Right;
                }
                // Second Quadrant
                else if (vectorX < 0 && vectorY > 0)
                {
                    if (absX == absY) return IntersectsDirect.LeftBottom;
                    else if (absX > absY) return IntersectsDirect.Left2Bottom;
                    else return IntersectsDirect.Bottom2Left;
                }
                // Third Quadrant  
                else if (vectorX < 0 && vectorY < 0)
                {
                    if (absX == absY) return IntersectsDirect.LeftTop;
                    else if (absX > absY) return IntersectsDirect.Left2Top;
                    else return IntersectsDirect.Top2Left;
                }
                // Fourth Quadrant  
                else
                {
                    if (absX == absY) return IntersectsDirect.RightTop;
                    else if (absX > absY) return IntersectsDirect.Right2Top;
                    else return IntersectsDirect.Top2Right;
                }
            }
        }
        public static Point DirectVector(double vectorX, double vectorY)
        {
            switch (IntersectsRectAndLine.GetDirect(vectorX, vectorY))
            {
                case IntersectsDirect.Center:
                    return new Point(0, 0);
                case IntersectsDirect.Left:
                    return new Point(1, 0);
                case IntersectsDirect.Top:
                    return new Point(0, 1);
                case IntersectsDirect.Right:
                    return new Point(-1, 0);
                case IntersectsDirect.Bottom:
                    return new Point(0, -1);
                case IntersectsDirect.LeftTop:
                    return new Point(1, 1);
                case IntersectsDirect.RightTop:
                    return new Point(-1, 1);
                case IntersectsDirect.RightBottom:
                    return new Point(-1, 1);
                case IntersectsDirect.LeftBottom:
                    return new Point(1, -1);
                case IntersectsDirect.Left2Top:
                    return new Point(1, System.Math.Abs(vectorY / vectorX));
                case IntersectsDirect.Top2Left:
                    return new Point(System.Math.Abs(vectorX / vectorY), 1);
                case IntersectsDirect.Top2Right:
                    return new Point(-System.Math.Abs(vectorX / vectorY), 1);
                case IntersectsDirect.Right2Top:
                    return new Point(-1, System.Math.Abs(vectorY / vectorX));
                case IntersectsDirect.Right2Bottom:
                    return new Point(-1, -System.Math.Abs(vectorY / vectorX));
                case IntersectsDirect.Bottom2Right:
                    return new Point(-System.Math.Abs(vectorX / vectorY), -1);
                case IntersectsDirect.Bottom2Left:
                    return new Point(System.Math.Abs(vectorX / vectorY), -1);
                case IntersectsDirect.Left2Bottom:
                    return new Point(1, -System.Math.Abs(vectorY / vectorX));
                default:
                    return new Point(0, 0);
            }
        }
    }
}