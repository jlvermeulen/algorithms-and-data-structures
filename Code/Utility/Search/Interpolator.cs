using System;

namespace Utility
{
    namespace Algorithms
    {
        namespace Search
        {
            /// <summary>
            /// Defines a function that gives the position of <paramref name="item"/>, interpolated between <paramref name="lower"/> and <paramref name="upper"/>.
            /// </summary>
            /// <typeparam name="T">The type of value to interpolate.</typeparam>
            /// <param name="item">The item of which the position within the range of <paramref name="lower"/> and <paramref name="upper"/> is to be calculated.</param>
            /// <param name="lower">The lower bound of the interpolation.</param>
            /// <param name="upper">The upper bound of the interpolation.</param>
            /// <returns>A value between 0 and 1, indicating the position of <paramref name="item"/> between <paramref name="lower"/> and <paramref name="upper"/></returns>
            public delegate double Interpolation<in T>(T item, T lower, T upper);

            /// <summary>
            /// Represents a class that can be used to calculate the position of one value between two other values.
            /// </summary>
            /// <typeparam name="T">The type of value to interpolate.</typeparam>
            public abstract class Interpolator<T> : IInterpolator<T>
            {
                private Interpolation<T> interpolate;

                protected Interpolator() { }

                /// <summary>
                /// Creates an instance of Interpolator&lt;T&gt; with the specified interpolation method.
                /// </summary>
                /// <param name="interpolation">The function to use for interpolation.</param>
                /// <returns>An instance of Interpolator&lt;T&gt; with the specified interpolation method.</returns>
                public static Interpolator<T> Create(Interpolation<T> interpolation)
                {
                    return new InterpolatorImplementation(interpolation);
                }

                /// <summary>
                /// Calculates the relative position of <paramref name="item"/> between <paramref name="lower"/> and <paramref name="upper"/>.
                /// </summary>
                /// <param name="item">The item of which the position within the range of <paramref name="lower"/> and <paramref name="upper"/> is to be calculated.</param>
                /// <param name="lower">The lower bound of the interpolation.</param>
                /// <param name="upper">The upper bound of the interpolation.</param>
                /// <returns></returns>
                public virtual double Interpolate(T item, T lower, T upper)
                {
                    return interpolate(item, lower, upper);
                }

                private class InterpolatorImplementation : Interpolator<T>
                {
                    public InterpolatorImplementation(Interpolation<T> interpolate) { base.interpolate = interpolate; }
                }
            }

            /// <summary>
            /// Defines an interface for classes that can perform interpolation.
            /// </summary>
            /// <typeparam name="T">The type of value to interpolate.</typeparam>
            public interface IInterpolator<in T>
            {
                double Interpolate(T item, T lower, T upper);
            }
        }
    }
}