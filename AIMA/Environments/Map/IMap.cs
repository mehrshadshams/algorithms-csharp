using System;
using System.Collections.Generic;
using AIMA.Util;

namespace AIMA.Environments.Map
{
    public interface IMap
    {
        /** Returns a list of all locations. */
        IList<String> Locations { get; }

        /**
	 * Answers to the question: Where can I get, following one of the
	 * connections starting at the specified location?
	 */
        IList<String> GetPossibleNextLocations(string location);

        /**
	 * Answers to the question: From where can I reach a specified location,
	 * following one of the map connections?
	 */
        IList<String> GetPossiblePrevLocations(string location);

        /**
	 * Returns the travel distance between the two specified locations if they
	 * are linked by a connection and null otherwise.
	 */
        double? GetDistance(string fromLocation, string toLocation);

        /**
	 * Returns the position of the specified location. The position is
	 * represented by two coordinates, e.g. latitude and longitude values.
	 */
        Point2D GetPosition(String loc);

        /**
	 * Returns a location which is selected by random.
	 */
        String RandomlyGenerateDestination();
    }
}