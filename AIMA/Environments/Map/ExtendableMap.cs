using System;
using System.Collections.Generic;
using System.Linq;
using AIMA.Util;

namespace AIMA.Environments.Map
{
    public class ExtendableMap : IMap
    {
        private readonly LabeledGraph<string, double> links;
        private readonly Dictionary<string, Point2D> locationPositions;
        
        public ExtendableMap()
        {
            links = new LabeledGraph<string, double>();
            locationPositions = new Dictionary<string, Point2D>();
        }

        public IList<string> Locations => links.VertexLabels;

        public void Clear()
        {
            links.Clear();
            locationPositions.Clear();
        }
        
        public void ClearLinks() {
            links.Clear();
        }
        
        public bool IsLocation(string str) {
            return links.IsVertexLabel(str);
        }
        
        public IList<string> GetPossibleNextLocations(string location)
        {
            var result = links.GetSuccessors(location);
            result.Sort();
            return result;
        }

        public IList<string> GetPossiblePrevLocations(string location)
        {
            return GetPossibleNextLocations(location);
        }

        public double? GetDistance(string fromLocation, string toLocation)
        {
            return links.Get(fromLocation, toLocation);
        }

        public Point2D GetPosition(string loc)
        {
            return locationPositions[loc];
        }

        public string RandomlyGenerateDestination()
        {
            return Locations.SelectRandom();
        }
        
        public void AddUnidirectionalLink(string fromLocation, string toLocation, double distance) {
            links.Set(fromLocation, toLocation, distance);
        }
        
        public void AddBidirectionalLink(string fromLocation, string toLocation, double distance) {
            links.Set(fromLocation, toLocation, distance);
            links.Set(toLocation, fromLocation, distance);
        }
        
        public void SetPosition(string loc, double x, double y) {
            locationPositions[loc] = new Point2D(x, y);
        }
        
        public void SetDistAndDirToRefLocation(string loc, double dist, int dir) {
            Point2D coords = new Point2D(-Math.Sin(dir * Math.PI / 180.0) * dist, Math.Cos(dir * Math.PI / 180.0) * dist);
            links.AddVertex(loc);
            locationPositions[loc] = coords;
        }
    }
}