using System;

namespace Utility
{
    /// <summary>
    /// Represents a Bloom filter.
    /// </summary>
    /// <typeparam name="T">The type of values the Bloom filter should work with.</typeparam>
    public class BloomFilter<T>
    {
        /// <summary>
        /// Represents a function that takes an object of type <typeparamref name="T"/> and returns a hash of this object.
        /// </summary>
        /// <param name="item">The object to hash.</param>
        /// <returns>An unsigned integer containing the hash value of <paramref name="item"/>.</returns>
        public delegate uint HashFunction(T item);

        private uint[] filter;
        private uint size, hashCount;
        private HashFunction primaryHash, secondaryHash;

        /// <summary>
        /// Initialises a new BloomFilter&ltT> with an optimal size and number of hash functions given the desired capacity and error rate.
        /// </summary>
        /// <param name="capacity">The expected number of items that will be stored in the BloomFilter&ltT>.</param>
        /// <param name="errorRate">The desired probability of false positives occurring when the capacity has been filled.</param>
        /// <param name="primaryHash">The first hash function.</param>
        /// <param name="secondaryHash">The second hash function.</param>
        public BloomFilter(uint capacity, double errorRate, HashFunction primaryHash, HashFunction secondaryHash)
        {
            this.size = OptimalSize(capacity, errorRate);
            this.hashCount = OptimalHashCount(capacity, this.size);
            this.primaryHash = primaryHash;
            this.secondaryHash = secondaryHash;

            this.filter = new uint[this.size];
            this.filter.Initialize();
        }

        /// <summary>
        /// Initialises a new BloomFilter&ltT> with the given size and number of hash functions.
        /// </summary>
        /// <param name="arraySize">The size of the BloomFilter&ltT>.</param>
        /// <param name="numberOfHashFunctions">The number of hash functions to use.</param>
        /// <param name="primaryHash">The first hash function.</param>
        /// <param name="secondaryHash">The second hash function.</param>
        public BloomFilter(uint arraySize, uint numberOfHashFunctions, HashFunction primaryHash, HashFunction secondaryHash)
        {
            this.size = arraySize;
            this.hashCount = numberOfHashFunctions;
            this.primaryHash = primaryHash;
            this.secondaryHash = secondaryHash;

            this.filter = new uint[this.size];
            this.filter.Initialize();
        }

        /// <summary>
        /// Adds a new item to the BloomFilter&lt;T>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            uint[] hashes = this.GetHashes(item);
            foreach (uint u in hashes)
                this.filter[u]++;
            this.Count++;
        }

        /// <summary>
        /// Tries to delete an item from the BloomFilter&lt;T>.
        /// </summary>
        /// <param name="item">The item that should be deleted.</param>
        /// <returns><code>true</code> if the item might have been in the BloomFilter&lt;T> and was deleted; <code>false</code> if the item was definitely not in the BloomFilter&lt;T>.</returns>
        public bool Delete(T item)
        {
            double conf;
            return this.Delete(item, out conf);
        }

        /// <summary>
        /// Tries to delete an item from the BloomFilter&lt;T>.
        /// </summary>
        /// <param name="item">The item that should be deleted.</param>
        /// <param name="confidence">The confidence that <paramref name="item"/> was in the BloomFilter&lt;T> before deletion.</param>
        /// <returns><code>true</code> if the item might have been in the BloomFilter&lt;T> and was deleted; <code>false</code> if the item was definitely not in the BloomFilter&lt;T>.</returns>
        public bool Delete(T item, out double confidence)
        {
            uint[] hashes;
            confidence = 1;

            if (!this.Contains(item, out hashes))
                return false;

            for (int i = 0; i < hashCount; i++)
                this.filter[hashes[i]]--;

            this.Count--;

            return true;
        }

        /// <summary>
        /// Determines if an element possibly is or definitely is not contained in the BloomFilter&lt;T>.
        /// </summary>
        /// <param name="item">The item to locate in the BloomFilter&lt;T>.</param>
        /// <returns><code>true</code> if the BloomFilter&lt;T> possibly contains <paramref name="item"/>, <code>false</code> if it definitely does not.</returns>
        public bool Contains(T item)
        {
            double conf;
            return this.Contains(item, out conf);
        }

        /// <summary>
        /// Determines if an element possibly is or definitely is not contained in the BloomFilter&lt;T>.
        /// </summary>
        /// <param name="item">The item to locate in the BloomFilter&lt;T>.</param>
        /// <param name="confidence">The confidence that <paramref name="item"/> is in the BloomFilter&lt;T>.</param>
        /// <returns><code>true</code> if the BloomFilter&lt;T> possibly contains <paramref name="item"/>, <code>false</code> if it definitely does not.</returns>
        public bool Contains(T item, out double confidence)
        {
            uint[] hashes;
            confidence = 1;
            return this.Contains(item, out hashes);
        }

        private bool Contains(T item, out uint[] hashes)
        {
            hashes = new uint[hashCount];
            uint[] h = this.GetHashes(item);

            for (int i = 0; i < hashCount; i++)
                if (this.filter[h[i]] == 0)
                    return false;

            Buffer.BlockCopy(h, 0, hashes, 0, (int)hashCount);
            return true;
        }

        /// <summary>
        /// The confidence that when an item is found, it is actually contained in the BloomFilter&lt;T>.
        /// </summary>
        public double Confidence { get { return 1 - Math.Pow(1 - Math.Pow(1 - 1.0 / size, hashCount * this.Count), hashCount); } }

        /// <summary>
        /// The number of items in the BloomFilter&lt;T>.
        /// </summary>
        public int Count { get; private set; }

        private uint[] GetHashes(T item)
        {
            uint[] hashes = new uint[hashCount];
            uint hash1 = this.primaryHash(item), hash2 = this.secondaryHash(item);

            for (uint i = 0; i < hashCount; i++)
                hashes[i] = (hash1 + i * hash2 + i * i) % size;

            return hashes;
        }

        /// <summary>
        /// Returns the optimal number of hash functions for the given capacity and size.
        /// </summary>
        /// <param name="capacity">The expected number of items that will be stored in the BloomFilter&ltT>.</param>
        /// <param name="size">The size of the BloomFilter&lt;T>.</param>
        /// <returns>The number of hash functions that minimise the probability of false positives.</returns>
        public static uint OptimalHashCount(uint capacity, uint size)
        {
            return (uint)(((double)size / capacity) * Math.Log(2));
        }

        /// <summary>
        /// Returns the optimal size of the BloomFilter&lt;T> for the given capacity and error rate.
        /// </summary>
        /// <param name="capacity">The expected number of items that will be stored in the BloomFilter&ltT>.</param>
        /// <param name="errorRate">The probability of false positives occurring when the capacity has been filled.</param>
        /// <returns>The smallest size for which the desired error rate can be achieved when the capacity has been filled.</returns>
        public static uint OptimalSize(uint capacity, double errorRate)
        {
            return (uint)(-capacity * Math.Log(errorRate) / (Math.Log(2) * Math.Log(2)));
        }

        /// <summary>
        /// Returns the probability of false positives occurring for a BloomFilter&lt;T> with a given size and number of hash functions when the capacity has been filled.
        /// </summary>
        /// <param name="capacity">The expected number of items that will be stored in the BloomFilter&ltT>.</param>
        /// <param name="size">The size of the BloomFilter&lt;T>.</param>
        /// <param name="hashCount">The number of hash functions the BloomFilter&lt;T> uses.</param>
        /// <returns>The probability of false positives occurring when the capacity has been filled.</returns>
        public static double ErrorRate(uint capacity, uint size, uint hashCount)
        {
            return Math.Pow(1 - Math.Pow(1 - 1.0 / size, hashCount * capacity), hashCount);
        }
    }
}