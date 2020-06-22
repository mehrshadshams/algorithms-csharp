using System;

namespace AIMA.Util
{
	/**
 * This class represents a vector in a two-dimensional Cartesian plot.<br/>
 * Simple arithmetic operations are supported.
 * 
 * @author Arno von Borries
 * @author Jan Phillip Kretzschmar
 * @author Andreas Walscheid
 *
 */
	public struct Vector2D : IEquatable<Vector2D>
	{
		/**
	 * This is a vector that is parallel to the X axis.
	 */
		public static readonly Vector2D X_VECTOR = new Vector2D(1.0d, 0.0d);

		/**
	 * This is a vector that is parallel to the Y axis.
	 */
		public static readonly Vector2D Y_VECTOR = new Vector2D(0.0d, 1.0d);

		/**
	 * This is the zero vector. It does not have a direction neither a length.
	 */
		public static readonly Vector2D ZERO_VECTOR = new Vector2D(0.0d, 0.0d);

		public double X { get; private set; }
		public double Y { get; private set; }

		/**
	 * @param X the X parameter of the vector.
	 * @param Y the Y parameter of the vector.
	 */
		public Vector2D(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}

		/**
	 * Calculates a vector based on the length and the heading of the vector.
	 * @param length the length of the vector.
	 * @param heading the angle at which the vector points.
	 * @return a new vector derived from its polar representation.
	 */
		public static Vector2D CalculateFromPolar(double length, double heading)
		{
			double X = length * Math.Cos(heading);
			double Y = length * Math.Sin(heading);
			return new Vector2D(X, Y);
		}

		/**
	 * Adds a vector onto this vector.
	 * @param op2 the vector to be added.
	 * @return the new calculated vector.
	 */
		public Vector2D Add(Vector2D op2)
		{
			return new Vector2D(this.X + op2.X, this.Y + op2.Y);
		}

		/**
	 * Subtracts a vector from this vector.
	 * @param op2 the vector to be subtracted.
	 * @return the new calculated vector.
	 */
		public Vector2D Sub(Vector2D op2)
		{
			return new Vector2D(this.X - op2.X, this.Y - op2.Y);
		}

		/**
	 * Multiplies a vector with a double.
	 * @param n the times the vector is to be taken.
	 * @return the new calculated vector.
	 */
		public Vector2D Multiply(double n)
		{
			return new Vector2D(this.X * n, this.Y * n);
		}

		/**
	 * Inverts this vector.
	 * @return the inverted vector.
	 */
		public Vector2D Invert()
		{
			return new Vector2D(-this.X, -this.Y);
		}

		/**
	 * Checks whether this vector and another vector are parallel to each other.<br/>
	 * If one of the vectors is the zero vector this method always returns {@code true}.
	 * @param op2 the second vector.
	 * @return {@code true} if the two vectors are parallel.
	 */
		public bool IsAbsoluteParallel(Vector2D op2)
		{
			return Math.Abs(this.Y * op2.X - this.X * op2.Y) < Double.Epsilon;
		}

		/**
	 * Checks whether this vector and another vector are parallel to each other or rotated by 180 degrees to each other.<br/>
	 * If one of the vectors is the zero vector this method always returns {@code true}.
	 * @param op2 the second vector.
	 * @return {@code true} if the two vectors are parallel.
	 */
		public bool IsParallel(Vector2D op2)
		{
			double angle = AngleTo(op2);

			return Util.CompareDoubles(angle, 0.0d) || Util.CompareDoubles(angle, Math.PI);
		}

		/**
	 * Calculates the angle between two vectors in radians.<br/>
	 * Both vectors must be different from the zero-vector.
	 * @param op2 the second vector.
	 * @return the angle in radians.
	 */
		public double AngleTo(Vector2D op2)
		{
			double result = Math.Atan2(op2.Y, op2.X) - Math.Atan2(this.Y, this.X);
			return result < 0 ? result + 2 * Math.PI : result;
		}

		/**
	 * Calculates the length of the vector.
	 * @return the length of the vector.
	 */
		public double Length
		{
			get { return Math.Sqrt(this.X * this.X + this.Y * this.Y); }
		}

		/**
	 * Checks equality for this vector with another vector.
	 * @param op2 the second vector.
	 * @return true if the vectors are equal in direction and length.
	 */
		public override bool Equals(object obj)
		{
			if (obj is Vector2D) return false;
			return Equals((Vector2D) obj);
		}

		public bool Equals(Vector2D other)
		{
			if ((object) other == null)
			{
				return false;
			}

			return Util.CompareDoubles(X, other.X) && Util.CompareDoubles(Y, other.Y);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X.GetHashCode() * 397) ^ Y.GetHashCode();
			}
		}
	}
}