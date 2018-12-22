using System;
using System.Collections.Generic;

namespace TokenMod
{
    public class WeightedInt
    {
        public int value = 0;
        public int weight = 0;

        public WeightedInt(int v, int w)
        {
            value = v;
            weight = w;
        }
    }

    // Allows random selection of weighted int
    public class WeightedIntList
    {
        private List<WeightedInt> wints;
        private int totalWeight = 0;
        private Random random;

        public WeightedIntList(Random r)
        {
            wints = new List<WeightedInt>();
            random = r;
        }

        // Add an element to the list
        public void Add(int v, int w)
        {
            WeightedInt wint = new WeightedInt(v, w);
            wints.Add(wint);
            totalWeight += w;
        }

        // Get current total weight of items
        public int GetTotalWeight()
        {
            return totalWeight;
        }

        // Pick a random element from the built list
        public int GetWeightedInt()
        {
            int r = random.Next(totalWeight);

            foreach (WeightedInt wint in wints)
            {
                if (r < wint.weight) return wint.value;
                r -= wint.weight;
            }

            return -1;
        }
    }
}
