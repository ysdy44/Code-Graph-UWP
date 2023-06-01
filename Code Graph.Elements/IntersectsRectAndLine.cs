namespace Code_Graph.Elements
{
    public readonly struct IntersectsRectAndLine
    {
        public readonly double X;
        public readonly double Y;

        public IntersectsRectAndLine(double w, double h, double x2, double y2, double x1, double y1)
        {
            double vectorX = (x2 - x1) / w;
            double vectorY = (y2 - y1) / h;
            IntersectsDirect direct = IntersectsRectAndLine.GetDirect(vectorX, vectorY);

            this.X = IntersectsRectAndLine.DirectX(direct, vectorX, vectorY);
            this.X *= w;
            this.X += x2;

            this.Y = IntersectsRectAndLine.DirectY(direct, vectorX, vectorY);
            this.Y *= h;
            this.Y += y2;
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

        public static double DirectX(IntersectsDirect direct, double vectorX, double vectorY)
        {
            switch (direct)
            {
                case IntersectsDirect.Left:
                case IntersectsDirect.LeftTop:
                case IntersectsDirect.LeftBottom:
                case IntersectsDirect.Left2Top:
                case IntersectsDirect.Left2Bottom:
                    return 1;
                case IntersectsDirect.Right:
                case IntersectsDirect.RightTop:
                case IntersectsDirect.RightBottom:
                case IntersectsDirect.Right2Top:
                case IntersectsDirect.Right2Bottom:
                    return -1;
                case IntersectsDirect.Top2Left:
                case IntersectsDirect.Bottom2Left:
                    return System.Math.Abs(vectorX / vectorY);
                case IntersectsDirect.Top2Right:
                case IntersectsDirect.Bottom2Right:
                    return -System.Math.Abs(vectorX / vectorY);
                default:
                    return 0;
            }
        }
        public static double DirectY(IntersectsDirect direct, double vectorX, double vectorY)
        {
            switch (direct)
            {
                case IntersectsDirect.Top:
                case IntersectsDirect.LeftTop:
                case IntersectsDirect.RightTop:
                case IntersectsDirect.RightBottom:
                case IntersectsDirect.Top2Left:
                case IntersectsDirect.Top2Right:
                    return 1;
                case IntersectsDirect.Bottom:
                case IntersectsDirect.LeftBottom:
                case IntersectsDirect.Bottom2Right:
                case IntersectsDirect.Bottom2Left:
                    return -1;
                case IntersectsDirect.Left2Top:
                case IntersectsDirect.Right2Top:
                    return System.Math.Abs(vectorY / vectorX);
                case IntersectsDirect.Right2Bottom:
                case IntersectsDirect.Left2Bottom:
                    return -System.Math.Abs(vectorY / vectorX);
                default:
                    return 0;
            }
        }
    }
}