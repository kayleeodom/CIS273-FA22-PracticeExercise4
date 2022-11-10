using System;

namespace PracticeExercise4
{
	public class HashTableLinearProbing<K,V>: IHashTable<K,V>
	{

        private Bucket<K, V>[] buckets;
        private int initialCapacity = 16;


		public HashTableLinearProbing()
		{
            buckets = new Bucket<K, V>[initialCapacity];

            for(int i= 0; i < buckets.Length; i++)
            {
                buckets[i] = new Bucket<K, V>();
            }

		}

        private int count;
        private readonly double MAX_LOAD_FACTOR = 0.6;

        public int Count => count;

        // percentage of buckets that are full filled/number
        public double LoadFactor => count / (double)buckets.Length;

        // O(1) - average case
        // O(N) - worst case
        public bool Add(K key, V value)
        {
            // more than 60% full we need to resize
            if(LoadFactor > MAX_LOAD_FACTOR)
            {
                Resize();
            }

            // find the hash
            int hash = Hash(key);

            // find the starting index
            int startingIndex = hash % buckets.Length;
            int bucketIndex = startingIndex;

            while(buckets[bucketIndex].State == BucketState.Full)
            {
                // if the key already exists, then update it
                if(buckets[bucketIndex].Key.Equals(key))
                {
                    // didn't add a value just updated it
                    buckets[bucketIndex].Value = value;
                    return true;
                }

                bucketIndex = (bucketIndex + 1) % buckets.Length;

                if(bucketIndex == startingIndex)
                {
                    throw new OutOfMemoryException();
                }
            }

            // if the key doesn't exist (and there it room), then add it
            buckets[bucketIndex].Key = key;
            buckets[bucketIndex].Value = value;
            buckets[bucketIndex].State = BucketState.Full;
            count++;
            return false;
        }

        // TODO
        // O(1) - average case
        // O(N) - worst case
        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        // TODO
        // O(N) - average case
        // O(N) - worst case
        public bool ContainsValue(V value)
        {
            throw new NotImplementedException();
        }

        // TODO
        // O(1) - average case
        // O(N) - worst case
        public V Get(K key)
        {
            throw new NotImplementedException();
        }

        // TODO
        // O(N) - average case
        // O(N) - worst case
        public List<K> GetKeys()
        {
            throw new NotImplementedException();
        }

        // TODO
        // O(N) - average case
        // O(N) - worst case
        public List<V> GetValues()
        {
            throw new NotImplementedException();
        }

        // TODO
        // O(1) - average case
        // O(N) - worst case
        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        private void Resize() 
        {
            var newBuckets = new Bucket<K, V>[2 * buckets.Length];
            var oldbuckets = buckets;

            buckets = newBuckets;
            for(int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new Bucket<K, V>();
            }

            count = 0;

            // rehash all the old/existing buckets into the new array/hashtable
            foreach(var bucket in oldbuckets)
            {
                Add(bucket.Key, bucket.Value);
            }
        }

        private int Hash(K key)
        {
            int hash = key.GetHashCode();

            // This makes sure it doesn't return a negative number
            return hash < 0 ? -hash : hash;
        }
    }
}

