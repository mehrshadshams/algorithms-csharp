using System;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public class VanEmdeBoas
    {
        private readonly int universe;
        private readonly int numberOfClusters;

        private readonly VanEmdeBoas summary;
        private readonly Dictionary<int, VanEmdeBoas> clusters;

        public VanEmdeBoas(int u)
        {
            universe = u;
            numberOfClusters = (int)Math.Ceiling(Math.Sqrt(u));

            if (u <= 2)
            {
                summary = null;
                clusters = null;
            }
            else
            {
                clusters = new Dictionary<int, VanEmdeBoas>(numberOfClusters);
                summary = new VanEmdeBoas(numberOfClusters);
            }
        }

        /// <summary>
        /// Minimum Value
        /// </summary>
        public int? Minimum { get; private set; }

        /// <summary>
        /// Maximum Value
        /// </summary>
        public int? Maximum { get; private set; }

        public bool Contains(int key)
        {
            if (universe < key)
            {
                return false;
            }

            if ((Minimum.HasValue && Minimum == key) || (Maximum.HasValue && Maximum == key))
            {
                return true;
            }
            else if (universe == 2)
            {
                return false;
            }

            int h = extractHigh(key);
            if (clusters.TryGetValue(h, out var cluster))
            {                
                return cluster.Contains(extractLow(key));
            }

            return false;
        }

        public void Insert(int key)
        {
            // If no key is present in the tree 
            // then set both minimum and maximum 
            if (Minimum == null)
            {
                Minimum = Maximum = key;
            }
            else
            {
                if (key < Minimum)
                {
                    // If the key is less than the current minimum 
                    // then swap it with the current minimum 
                    // because this minimum is actually 
                    // minimum of one of the internal cluster 
                    // so as we go deeper into the Van Emde Boas 
                    // we need to take that minimum to its real position 
                    // This concept is similar to "Lazy Propagation"

                    int temp = Minimum.Value;
                    Minimum = key;
                    key = temp;
                }

                if (universe > 2)
                {
                    int high = extractHigh(key), low = extractLow(key);

                    if (!clusters.ContainsKey(high))
                    {
                        clusters[high] = new VanEmdeBoas(numberOfClusters);
                    }

                    // If no key is present in the cluster then insert key into 
                    // both cluster and summary 
                    if (clusters[high].Minimum == null)
                    {
                        summary.Insert(high);

                        // Sets the minimum and maximum of cluster to the key 
                        // as no other keys are present we will stop at this level 
                        // we are not going deeper into the structure like 
                        // Lazy Propagation 
                        clusters[high].Minimum = low;
                        clusters[high].Maximum = low;
                    }
                    else
                    {
                        // If there are other elements in the tree then recursively 
                        // go deeper into the structure to set attributes accordingly
                        clusters[high].Insert(low);
                    }
                }

                // Sets the key as maximum it is greater than current maximum 
                if (key > Maximum)
                {
                    Maximum = key;
                }
            }
        }

        public int? Successor(int key)
        {
            // Base case: If key is 0 and its successor 
            // is present then return 1 else return null
            if (universe == 2)
            {
                if (key == 0 && Maximum == 1)
                {
                    return 1;
                }

                return null;
            }
            else if (Minimum != null && key < Minimum)
            {
                // If key is less then minimum then return minimum 
                // because it will be successor of the key 
                return Minimum;
            }
            else
            {
                // Find successor inside the cluster of the key 
                // First find the maximum in the cluster

                int high = extractHigh(key), low = extractLow(key);
                int? offset;
                if (clusters.TryGetValue(high, out var cluster))
                {
                    // If there is any key present in the cluster then find 
                    // the successor inside of the cluster

                    if (low < cluster.Maximum)
                    {
                        offset = cluster.Successor(low);
                        if (offset.HasValue)
                        {
                            return generateIndex(high, offset.Value);
                        }
                    }
                }

                // Otherwise look for the next cluster with at least one key present

                int? successorCluster = summary.Successor(high);
                if (successorCluster == null)
                {
                    return successorCluster;
                }

                offset = clusters[successorCluster.Value].Minimum;
                if (offset.HasValue)
                {
                    return generateIndex(successorCluster.Value, offset.Value);
                }
            }

            return null;
        }

        public int? Predecessor(int key)
        {
            if (universe == 2)
            {
                // Base case: If the key is 1 and it's predecessor 
                // is present then return 0 else return null

                if (key == 1 && Minimum == 0)
                {
                    return 0;
                }

                return null;
            }
            else if (Maximum != null && key > Maximum)
            {
                // If the key is greater than maximum of the tree then 
                // return key as it will be the predecessor of the key

                return Maximum;
            }
            else
            {
                int high = extractHigh(key), low = extractLow(key);
                int? offset;
                if (clusters.TryGetValue(high, out var cluster))
                {
                    // Find predecessor in the cluster of the key 
                    // First find minimum in the key to check whether any key 
                    // is present in the cluster

                    if (low > cluster.Minimum)
                    {
                        // If any key is present in the cluster then find predecessor in 
                        // the cluster

                        offset = cluster.Predecessor(low);
                        if (offset.HasValue)
                        {
                            return generateIndex(high, offset.Value);
                        }
                    }                    
                }

                // Otherwise look for predecessor in the summary which 
                // returns the index of predecessor cluster with any key present

                int? clusterPred = summary.Predecessor(high);
                if (clusterPred == null)
                {
                    if (key > Minimum)
                    {
                        return Minimum;
                    }

                    return null;
                }
                else
                {
                    offset = clusters[clusterPred.Value].Maximum;
                    if (offset.HasValue)
                    {
                        return generateIndex(clusterPred.Value, offset.Value);
                    }
                }
            }

            return null;
        }

        public void Delete(int key)
        {
            if (Maximum == Minimum)
            {
                // If only one key is present, it means 
                // that it is the key we want to delete 
                // Same condition as key == max && key == min

                Maximum = Minimum = null;
            }
            else if (universe == 2)
            {
                // Base case: If the above condition is not true 
                // i.e. the tree has more than two keys 
                // and if its size is two than a tree has exactly two keys. 
                // We simply delete it by assigning it to another 
                // present key value

                if (key == 0)
                {
                    Minimum = 1;
                }
                else
                {
                    Minimum = 0;
                }
                Maximum = Minimum;
            }
            else
            {
                // As we are doing something similar to lazy propagation 
                // we will basically find next bigger key 
                // and assign it as minimum

                if (key == Minimum)
                {
                    int firstCluster = summary.Minimum.Value;
                    key = generateIndex(firstCluster, clusters[firstCluster].Minimum.Value);
                    Minimum = key;
                }

                int high = extractHigh(key), low = extractLow(key);

                // Now we delete the key
                clusters[high].Delete(low);

                // After deleting the key, rest of the improvements 

                // If the minimum in the cluster of the key is -1 
                // then we have to delete it from the summary to 
                // eliminate the key completely
                if (clusters[high].Minimum == null)
                {
                    summary.Delete(high);

                    // After the above condition, if the key 
                    // is maximum of the tree then...
                    if (key == Maximum)
                    {
                        // If the max value of the summary is null 
                        // then only one key is present so 
                        // assign min. to max.
                        if (summary.Maximum == null)
                        {
                            Maximum = Minimum;
                        }
                        else
                        {
                            Maximum = generateIndex(summary.Maximum.Value,
                                clusters[summary.Maximum.Value].Maximum.Value);
                        }
                    }
                }
                else if (key == Maximum)
                {
                    // Simply find the new maximum key and 
                    // set the maximum of the tree 
                    // to the new maximum

                    Maximum = generateIndex(high, clusters[high].Maximum.Value);
                }
            }
        }

        /// <summary>
        /// Function to return cluster numbers in which key is present
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int extractHigh(int key)
        {
            return key / numberOfClusters;
        }

        /// <summary>
        /// Function to return position of x in cluster
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int extractLow(int key)
        {
            return key % numberOfClusters;
        }

        /// <summary>
        /// Function to return the index from 
        /// cluster number and position
        /// </summary>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        private int generateIndex(int high, int low)
        {
            return high * numberOfClusters + low;
        }
    }
}
