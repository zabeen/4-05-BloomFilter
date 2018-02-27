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
        IEnumerable<int> Write(string value);
        bool Verify(string value);
    }

    public class BloomFilter : IBloomFilter
    {
        public bool[] Bitmap { get; }
        private readonly int _bitmapSize;
        private readonly int _hashCount;        
        private const int MaxHashCount = 4;

        public BloomFilter(int bitmapSize = int.MaxValue / 100, int hashCount = MaxHashCount)
        {
            if (hashCount < 1 || hashCount > MaxHashCount)
                throw new ArgumentException($"{nameof(hashCount)} must be between 1 and {MaxHashCount}.");

            Bitmap = new bool[bitmapSize];
            _bitmapSize = bitmapSize;
            _hashCount = hashCount;
        }

        public IEnumerable<int> Write(string value)
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

        private IEnumerable<int> GetHashIndexes(string value)
        {
            var byteArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value));

            // source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-byte-array-to-an-int
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            var arraySize = byteArray.Length / sizeof(int);
            var intList = new List<int>();
            for (var i = 0; i < byteArray.Length; i += arraySize)
            {
                var bytes = new byte[arraySize];
                Array.Copy(byteArray, i, bytes, 0, arraySize);
                intList.Add(Math.Abs(BitConverter.ToInt32(bytes, 0) % _bitmapSize));
            }

            return intList.Take(_hashCount);
        }
    }
}
