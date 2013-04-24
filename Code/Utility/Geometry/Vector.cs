﻿using System;

namespace Utility
{
    /// <summary>
    /// Defines a point in 2D space.
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
        public override string ToString() { return "( " + this.X + " , " + this.Y + " )"; }

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
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
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

        public static Vector2D operator *(double scalar, Vector2D vector) { return vector * scalar; }

        public static Vector2D operator +(Vector2D left, Vector2D right) { return new Vector2D(left.X + right.X, left.Y + right.Y); }

        public static Vector2D operator -(Vector2D left, Vector2D right) { return new Vector2D(left.X - right.X, left.Y - right.Y); }

        public static Vector2D operator /(Vector2D vector, double scalar) { return new Vector2D(vector.X / scalar, vector.Y / scalar); }

        public static bool operator ==(Vector2D vector1, Vector2D vector2) { return vector1.Equals(vector2); }

        public static bool operator !=(Vector2D vector1, Vector2D vector2) { return !vector1.Equals(vector2); }

        /// <summary>
        /// Returns a vector with both coordinates set to zero.
        /// </summary>
        public static Vector2D Zero { get { return new Vector2D(0, 0); } }
    }
}