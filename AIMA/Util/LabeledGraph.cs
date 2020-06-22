using System.Collections.Generic;

namespace AIMA.Util
{
	public class LabeledGraph<VertexLabelType, EdgeLabelType>
	{
		/// <summary>
		// Lookup for edge label information. Contains an entry for every vertex
		// label.
		/// </summary>
		private readonly Dictionary<VertexLabelType, Dictionary<VertexLabelType, EdgeLabelType>> globalEdgeLookup;

		/** List of the labels of all vertices within the graph. */
		private List<VertexLabelType> vertexLabels;

		/** Creates a new empty graph. */
		public LabeledGraph()
		{
			globalEdgeLookup = new Dictionary<VertexLabelType, Dictionary<VertexLabelType, EdgeLabelType>>();
			vertexLabels = new List<VertexLabelType>();
		}

		/**
		 * Adds a new vertex to the graph if it is not already present.
		 * 
		 * @param v
		 *            the vertex to add
		 */
		public void AddVertex(VertexLabelType v)
		{
			CheckForNewVertex(v);
		}

		/**
		 * Adds a directed labeled edge to the graph. The end points of the edge are
		 * specified by vertex labels. New vertices are automatically identified and
		 * added to the graph.
		 * 
		 * @param from
		 *            the first vertex of the edge
		 * @param to
		 *            the second vertex of the edge
		 * @param el
		 *            an edge label
		 */
		public void Set(VertexLabelType from, VertexLabelType to, EdgeLabelType el)
		{
			Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = CheckForNewVertex(from);
			localEdgeLookup.Add(to, el);
			CheckForNewVertex(to);
		}

		/** Handles new vertices. */
		private Dictionary<VertexLabelType, EdgeLabelType> CheckForNewVertex(
			VertexLabelType v)
		{
			if (!globalEdgeLookup.TryGetValue(v, out Dictionary<VertexLabelType, EdgeLabelType> result))
			{
				result = new Dictionary<VertexLabelType, EdgeLabelType>();
				globalEdgeLookup[v] = result;
				vertexLabels.Add(v);
			}

			return result;
		}

		/**
	 * Removes an edge from the graph.
	 * 
	 * @param from
	 *            the first vertex of the edge
	 * @param to
	 *            the second vertex of the edge
	 */
		public void Remove(VertexLabelType from, VertexLabelType to)
		{
			if (globalEdgeLookup.ContainsKey(from))
			{
				Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = globalEdgeLookup[from];
				localEdgeLookup.Remove(to);
			}
		}

		/**
	 * Returns the label of the edge between the specified vertices, or null if
	 * there is no edge between them.
	 * 
	 * @param from
	 *            the first vertex of the ege
	 * @param to
	 *            the second vertex of the edge
	 * 
	 * @return the label of the edge between the specified vertices, or null if
	 *         there is no edge between them.
	 */
		public EdgeLabelType Get(VertexLabelType from, VertexLabelType to)
		{
			Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = globalEdgeLookup[from];
			return localEdgeLookup == null ? default : localEdgeLookup[to];
		}

		/**
	 * Returns the labels of those vertices which can be obtained by following
	 * the edges starting at the specified vertex.
	 */
		public List<VertexLabelType> GetSuccessors(VertexLabelType v)
		{
			List<VertexLabelType> result = new List<VertexLabelType>();
			Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = globalEdgeLookup[v];
			if (localEdgeLookup != null)
			{
				result.AddRange(localEdgeLookup.Keys);
			}

			return result;
		}

		/** Returns the labels of all vertices within the graph. */
		public List<VertexLabelType> VertexLabels
		{
			get { return vertexLabels; }
		}

		/** Checks whether the given label is the label of one of the vertices. */
		public bool IsVertexLabel(VertexLabelType v)
		{
			return globalEdgeLookup[v] != null;
		}

		/** Removes all vertices and all edges from the graph. */
		public void Clear()
		{
			vertexLabels.Clear();
			globalEdgeLookup.Clear();
		}
	}
}