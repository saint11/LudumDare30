using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    static public class Calc
    {
        

        static public float LerpSnap(float value1, float value2, float amount, float snapThreshold = .1f)
        {
            float ret = (float)MathHelper.Lerp(value1, value2, amount);
            if (Math.Abs(ret - value2) < snapThreshold)
                return value2;
            else
                return ret;
        }

        static public float DistanceSq(float x, float y, float x2, float y2) {
            return (x2 - x) * (x2 - x) + (y2 - y) * (y2 - y);
        }
        /*
        static public float GetCircleLineIntersectionPosition(LineCollider line, CircleCollider circle)
        {
            List<Vector2> points = GetCircleLineIntersectionPoints(line, circle);

            if (points.Count == 0) return 0;

            Vector2 centralPoint;
            if (points.Count == 1)
            {
                centralPoint = points[0];
            }
            else
            {
                centralPoint = Vector2.Lerp(points[0], points[1], 0.5f);
            }

            float min = Math.Min(line.X, line.X2);
            float max = Math.Max(line.X, line.X2);
            float mid = (float)centralPoint.X;
            max -= min;
            mid -= min;

            return mid / max;
        }

        static public List<Vector2> GetCircleLineIntersectionPoints(LineCollider line, CircleCollider circle)
        {
            return GetCircleLineIntersectionPoints(line.X, line.Y, line.X2, line.Y2, circle.CenterX, circle.CenterY + 1, circle.Radius);
        }


        static public List<Vector2> GetCircleLineIntersectionPoints(float x1, float y1, float x2, float y2,
            float centerX, float centerY, float radius)
        {
            double baX = x2 - x1;
            double baY = y2 - y1;
            double caX = centerX - x1;
            double caY = centerY - y1;

            double a = baX * baX + baY * baY;
            double bBy2 = baX * caX + baY * caY;
            double c = caX * caX + caY * caY - radius * radius;

            double pBy2 = bBy2 / a;
            double q = c / a;

            double disc = pBy2 * pBy2 - q;
            if (disc < 0)
            {
                return new List<Vector2>();
            }
            // if disc == 0 ... dealt with later
            double tmpSqrt = Math.Sqrt(disc);
            double abScalingFactor1 = -pBy2 + tmpSqrt;
            double abScalingFactor2 = -pBy2 - tmpSqrt;

            List<Vector2> ret = new List<Vector2>();
            Vector2 p1 = new Vector2(x1 - baX * abScalingFactor1, y1
                    - baY * abScalingFactor1);
            ret.Add(p1);

            if (disc == 0)
            {
                return ret;
            }
            Vector2 p2 = new Vector2(x1 - baX * abScalingFactor2, y1
                    - baY * abScalingFactor2);
            ret.Add(p2);
            return ret;
        }*/
    }
}
