using System;
using System.Collections.Generic;
using System.Text;

namespace LSR_Engine.src.Cache
{
    internal class BlockCache
    {
        private readonly Dictionary<string, byte[][,]> _cache = new Dictionary<string, byte[][,]>();

        public void Register(string shape, byte[][,] rotations)
        {
            _cache[shape] = rotations;
        }

        public byte[][,] GetBlockCache(string shape)
        {
            if (!_cache.TryGetValue(shape, out var cache))
            {
                throw new KeyNotFoundException($"Block shape '{shape}' not found in cache.");
            }
            return cache;
        }
    }
}
