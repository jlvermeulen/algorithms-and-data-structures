using System;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// A class containing methods for computational geometry.
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        /// Returns the smallest polygon that contains all the points in the input.
        /// </summary>
        /// <param name="input">The set of points to calculate the convex hull of.</param>
        /// <returns>A list of points describing the convex hull in clockwise order.</returns>
        public static List<Vector2D> ConvexHull(IEnumerable<Vector2D> input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            List<Vector2D> points = new List<Vector2D>(input);

            Vector2D start = new Vector2D(double.MaxValue, double.MaxValue);
            List<int> indexes = new List<int>();
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < start.Y || points[i].Y == start.Y && points[i].X < start.X)
                {
                    start = points[i];
                    indexes.Clear();
                    indexes.Add(i);
                }
                else if (points[i] == start)
                    indexes.Add(i);
            }
            for (int i = 0; i < indexes.Count; i++)
                points.RemoveAt(indexes[i] - i);

            AngleComparer comp = new AngleComparer(start);
            points.Sort(comp);

            List<Vector2D> temp = new List<Vector2D>();
            for (int i = 0; i < points.Count; i++)
            {
                while (i < points.Count - 1 && comp.Compare(points[i], points[i + 1]) == 0 && Vector2D.Distance(points[i], start) < Vector2D.Distance(points[i + 1], start))
                        i++;
                temp.Add(points[i]);
            }
            points = temp;

            if (points.Count < 2)
                return null;

            Stack<Vector2D> hull = new Stack<Vector2D>();
            hull.Push(start);
            hull.Push(points[0]);
            hull.Push(points[1]);

            Vector2D top, belowTop;
            for (int i = 2; i < points.Count; i++)
            {
                top = hull.Pop();
                belowTop = hull.Pop();
                while (!LeftTurn(belowTop, top, points[i]))
                {
                    top = belowTop;
                    belowTop = hull.Pop();
                }
                hull.Push(belowTop);
                hull.Push(top);
                hull.Push(points[i]);
            }

            points.Clear();
            while (hull.Count > 0)
                points.Add(hull.Pop());
            return points;
        }

        private static bool LeftTurn(Vector2D p1, Vector2D p2, Vector2D p3)
        {
            return Vector2D.Cross(p2 - p1, p3 - p1) < 0; 
        }

        private class AngleComparer : IComparer<Vector2D>
        {
            Vector2D origin;

            public AngleComparer(Vector2D origin) { this.origin = origin; }

            public int Compare(Vector2D vector1, Vector2D vector2)
            {
                double c = Vector2D.Cross(vector1 - this.origin, vector2 - this.origin);
                if (c < 0)
                    return -1;
                if (c > 0)
                    return 1;
                return 0;
            }
        }
    }
}