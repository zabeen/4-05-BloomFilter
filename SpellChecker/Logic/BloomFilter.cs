using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SpellChecker.Logic
{
    public interface IBloomFilter
    {
        bool[] Bitmap { get; }
        IEnumerable<byte> Write(string value);
        bool Verify(string value);
    }

    public class BloomFilter : IBloomFilter
    {
        public bool[] Bitmap { get; }
        private readonly int _hashCount;
        private const int MaxHashCount = 16;

        public BloomFilter(int bitmapSize = byte.MaxValue, int hashCount = 5)
        {
            if (bitmapSize < byte.MaxValue)
                throw new ArgumentException($"{nameof(bitmapSize)} size must be >= {byte.MaxValue}.");

            if (hashCount < 1 || hashCount > MaxHashCount)
                throw new ArgumentException($"{nameof(hashCount)} must be between 1 and {MaxHashCount}.");

            Bitmap = new bool[bitmapSize];
            _hashCount = hashCount;
        }

        public IEnumerable<byte> Write(string value)
        {
            var hashes = GetHashIndexes(value).ToList();
            hashes.ForEach(h => Bitmap[h] = true);
            return hashes;
        }

        public bool Verify(string value)
        {
            var hashes = GetHashIndexes(value);
            return hashes.All(i => Bitmap[i]);
        }

        private IEnumerable<byte> GetHashIndexes(string value)
        {
            var byteArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value)).ToList();
            return byteArray.Where(i => i < Bitmap.Length).Take(_hashCount);
        }
    }
}
