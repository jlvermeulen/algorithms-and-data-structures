using System;

namespace Utility
{
    namespace Algorithms
    {
        namespace ComputationalGeometry
        {
            /// <summary>
            /// Defines a two-dimensional vector.
            /// </summary>
            public struct Vector2D
            {
                /// <summary>
                /// The x-coordinate of this vector.
                /// </summary>
                public double X;

                /// <summary>
                /// The y-coordinate of this vector.
                /// </summary>
                public double Y;

                /// <summary>
                /// Instantiates a new vector with the given coordinates.
                /// </summary>
                /// <param name="x">The x-coordinate of the new vector.</param>
                /// <param name="y">The y-coordinate of the new vector.</param>
                public Vector2D(double x, double y) { this.X = x; this.Y = y; }

                /// <summary>
                /// Returns the length of the vector.
                /// </summary>
                /// <returns>The length of the vector.</returns>
                public double Length() { return Math.Sqrt(Vector2D.Dot(this, this)); }

                /// <summary>
                /// Returns a string that represents the vector.
                /// </summary>
                /// <returns>A string representation of the vector.</returns>
                public override string ToString() { return "(" + this.X + ", " + this.Y + ")"; }

                /// <summary>
                /// Returns the cross product of two vectors.
                /// </summary>
                /// <param name="vector1">The first vector.</param>
                /// <param name="vector2">The second vector.</param>
                /// <returns>The cross product of the two argument vectors.</returns>
                public static double Cross(Vector2D vector1, Vector2D vector2) { return vector1.X * vector2.Y - vector2.X * vector1.Y; }

                /// <summary>
                /// Returns the distance between two points represented by vectors.
                /// </summary>
                /// <param name="vector1">The first vector.</param>
                /// <param name="vector2">The second vector.</param>
                /// <returns></returns>
                public static double Distance(Vector2D vector1, Vector2D vector2) { return (vector1 - vector2).Length(); }

                /// <summary>
                /// Returns the dot product of two vectors.
                /// </summary>
                /// <param name="vector1">The first vector.</param>
                /// <param name="vector2">The second vector.</param>
                /// <returns>The dot product of the two argument vectors.</returns>
                public static double Dot(Vector2D vector1, Vector2D vector2) { return vector1.X * vector2.X + vector1.Y * vector2.Y; }

                public static Vector2D operator *(Vector2D vector, double scalar) { return new Vector2D(vector.X * scalar, vector.Y * scalar); }

                public static Vector2D operator *(Matrix left, Vector2D right)
                {
                    if (!left.IsSquare || left.Columns != 2)
                        throw new ArgumentException("Matrix must be 2x2.");

                    return new Vector2D(left[0, 0] * right.X + left[0, 1] * right.Y,
                                        left[1, 0] * right.X + left[1, 1] * right.Y);
                }

                public static Vector2D operator *(double scalar, Vector2D vector) { return vector * scalar; }

                public static Vector2D operator +(Vector2D left, Vector2D right) { return new Vector2D(left.X + right.X, left.Y + right.Y); }

                public static Vector2D operator -(Vector2D left, Vector2D right) { return new Vector2D(left.X - right.X, left.Y - right.Y); }

                public static Vector2D operator /(Vector2D vector, double scalar) { return new Vector2D(vector.X / scalar, vector.Y / scalar); }

                public static bool operator ==(Vector2D vector1, Vector2D vector2) { return vector1.X == vector2.X && vector1.Y == vector2.Y; }

                public static bool operator !=(Vector2D vector1, Vector2D vector2) { return !(vector1 == vector2); }

                /// <summary>
                /// Returns a vector with both coordinates set to zero.
                /// </summary>
                public static Vector2D Zero { get { return new Vector2D(0, 0); } }

                public override bool Equals(object obj)
                {
                    try { return this == (Vector2D)obj; }
                    catch { return base.Equals(obj); }
                }

                public override int GetHashCode() { return this.X.GetHashCode() ^ this.Y.GetHashCode(); }
            }

            /// <summary>
            /// Defines a three-dimensional vector.
            /// </summary>
            public struct Vector3D
            {
                /// <summary>
                /// The x-coordinate of this vector.
                /// </summary>
                public double X;

                /// <summary>
                /// The y-coordinate of this vector.
                /// </summary>
                public double Y;

                /// <summary>
                /// The z-coordinate of this vector.
                /// </summary>
                public double Z;

                /// <summary>
                /// Instantiates a new vector with the given coordinates.
                /// </summary>
                /// <param name="x">The x-coordinate of the new vector.</param>
                /// <param name="y">The y-coordinate of the new vector.</param>
                /// <param name="z">The z-coordinate of the new vector.</param>
                public Vector3D(double x, double y, double z) { this.X = x; this.Y = y; this.Z = z; }

                /// <summary>
                /// Returns the length of the vector.
                /// </summary>
                /// <returns>The length of the vector.</returns>
                public double Length() { return Math.Sqrt(Vector3D.Dot(this, this)); }

                /// <summary>
                /// Returns a string that represents the vector.
                /// </summary>
                /// <returns>A string representation of the vector.</returns>
                public override string ToString() { return "(" + this.X + ", " + this.Y + ", " + this.Z + ")"; }

                /// <summary>
                /// Returns the cross product of two vectors.
                /// </summary>
                /// <param name="vector1">The first vector.</param>
                /// <param name="vector2">The second vector.</param>
                /// <returns>The cross product of the two argument vectors.</returns>
                public static Vector3D Cross(Vector3D vector1, Vector3D vector2) { return new Vector3D(vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                                                                                                       vector1.Z * vector2.X - vector1.X * vector2.Z,
                                                                                                       vector1.X * vector2.Y - vector1.Y * vector2.X); }

                /// <summary>
                /// Returns the distance between two points represented by vectors.
                /// </summary>
                /// <param name="vector1">The first vector.</param>
                /// <param name="vector2">The second vector.</param>
                /// <returns></returns>
                public static double Distance(Vector2D vector1, Vector2D vector2) { return (vector1 - vector2).Length(); }

                /// <summary>
                /// Returns the dot product of two vectors.
                /// </summary>
                /// <param name="vector1">The first vector.</param>
                /// <param name="vector2">The second vector.</param>
                /// <returns>The dot product of the two argument vectors.</returns>
                public static double Dot(Vector3D vector1, Vector3D vector2) { return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z; }

                public static Vector3D operator *(Vector3D vector, double scalar) { return new Vector3D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar); }

                public static Vector3D operator *(double scalar, Vector3D vector) { return vector * scalar; }

                public static Vector3D operator *(Matrix left, Vector3D right)
                {
                    if (!left.IsSquare || left.Columns != 3)
                        throw new ArgumentException("Matrix must be 3x3.");

                    return new Vector3D(left[0, 0] * right.X + left[0, 1] * right.Y + left[0, 2] * right.Z,
                                        left[1, 0] * right.X + left[1, 1] * right.Y + left[1, 2] * right.Z,
                                        left[2, 0] * right.X + left[2, 1] * right.Y + left[2, 2] * right.Z);
                }

                public static Vector3D operator +(Vector3D left, Vector3D right) { return new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z); }

                public static Vector3D operator -(Vector3D left, Vector3D right) { return new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z); }

                public static Vector3D operator /(Vector3D vector, double scalar) { return new Vector3D(vector.X / scalar, vector.Y / scalar, vector.Z / scalar); }

                public static bool operator ==(Vector3D vector1, Vector3D vector2) { return vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z; }

                public static bool operator !=(Vector3D vector1, Vector3D vector2) { return !(vector1 == vector2); }

                /// <summary>
                /// Returns a vector with both coordinates set to zero.
                /// </summary>
                public static Vector3D Zero { get { return new Vector3D(0, 0, 0); } }

                public override bool Equals(object obj)
                {
                    try { return this == (Vector3D)obj; }
                    catch { return base.Equals(obj); }
                }

                public override int GetHashCode() { return (this.X.GetHashCode() * 73856093) ^ (this.Y.GetHashCode() *  19349663) ^ (this.Z.GetHashCode() * 83492791); }
            }
        }
    }
}