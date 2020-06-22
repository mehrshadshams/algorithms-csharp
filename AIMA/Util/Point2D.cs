using System;

namespace AIMA.Util
{
    public struct Point2D : ICloneable
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Distance(Point2D other)
        {
            return Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
        }

        /**
	 * Adds a vector onto a point.<br/>
	 * This moves the point by {@code vector.getX()} in X direction and {@code vector.getY()} in Y direction.
	 * @param op2 the {@link Vector2D} to be added.
	 * @return the moved point.
	 */
        public Point2D Add(Vector2D op2)
        {
            return new Point2D(this.X + op2.X, this.Y + op2.Y);
        }

        /**
	 * Subtracts a vector from a point.<br/>
	 * This moves the point by {@code vector.getX()} in negative X direction and {@code vector.getY()} in negative Y direction.
	 * @param op2 the {@link Vector2D} to be subtracted.
	 * @return the moved point.
	 */
        public Point2D Sub(Vector2D op2)
        {
            return new Point2D(this.X - op2.X, this.Y - op2.Y);
        }

        /**
	 * Calculates the vector between this point and the target point.
	 * @param target the point that describes the end of the vector.
	 * @return the vector between this point and the target point.
	 */
        public Vector2D Vec(Point2D target)
        {
            return new Vector2D(target.X - this.X, target.Y - this.Y);
        }

        public object Clone()
        {
            return new Point2D(X, Y);
        }
    }
}