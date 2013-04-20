using System;

namespace Utility
{
    public class BloomFilter<T>
    {
        public delegate uint HashFunction(T item);

        uint[] filter;
        uint size, hashCount;
        HashFunction primaryHash, secondaryHash;

        public BloomFilter(uint capacity, double errorRate, HashFunction primaryHash, HashFunction secondaryHash)
        {
            this.size = OptimalSize(capacity, errorRate);
            this.hashCount = OptimalHashCount(capacity, this.size);
            this.primaryHash = primaryHash;
            this.secondaryHash = secondaryHash;

            this.filter = new uint[this.size];
            this.filter.Initialize();
        }

        public BloomFilter(uint arraySize, uint numberOfHashFunctions, HashFunction primaryHash, HashFunction secondaryHash)
        {
            this.size = arraySize;
            this.hashCount = numberOfHashFunctions;
            this.primaryHash = primaryHash;
            this.secondaryHash = secondaryHash;

            this.filter = new uint[this.size];
            this.filter.Initialize();
        }

        public void Add(T item)
        {
            uint[] hashes = this.GetHashes(item);
            foreach (uint u in hashes)
                this.filter[u]++;
            this.Count++;
        }

        public bool Delete(T item)
        {
            double conf;
            return this.Delete(item, out conf);
        }

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

        public bool Contains(T item)
        {
            double conf;
            return this.Contains(item, out conf);
        }

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

        public double Confidence { get { return 1 - Math.Pow(1 - Math.Pow(1 - 1.0 / size, hashCount * this.Count), hashCount); } }

        public int Count { get; private set; }

        private uint[] GetHashes(T item)
        {
            uint[] hashes = new uint[hashCount];
            uint hash1 = this.primaryHash(item), hash2 = this.secondaryHash(item);

            for (uint i = 0; i < hashCount; i++)
                hashes[i] = (hash1 + i * hash2 + i * i) % size;

            return hashes;
        }

        public static uint OptimalHashCount(uint capacity, uint size)
        {
            return (uint)(((double)size / capacity) * Math.Log(2));
        }

        public static uint OptimalSize(uint capacity, double errorRate)
        {
            return (uint)(-capacity * Math.Log(errorRate) / (Math.Log(2) * Math.Log(2)));
        }

        public static double ErrorRate(uint capacity, uint size, uint hashCount)
        {
            return Math.Pow(1 - Math.Pow(1 - 1.0 / size, hashCount * capacity), hashCount);
        }
    }
}