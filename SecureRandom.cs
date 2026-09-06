using System;
using System.Security.Cryptography;

namespace EzPass
{
    /// <summary>
    /// Cryptographically secure replacement for System.Random, used anywhere password-related
    /// randomness is required. System.Random is a predictable, non-cryptographic PRNG and is
    /// not suitable for generating security-sensitive values such as passwords.
    /// </summary>
    static class SecureRandom
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

        /// <summary>
        /// Returns a cryptographically secure random integer in the range [minInclusive, maxExclusive).
        /// </summary>
        /// <param name="minInclusive"></param>
        /// <param name="maxExclusive"></param>
        /// <returns></returns>
        public static int Next(int minInclusive, int maxExclusive)
        {
            if (maxExclusive <= minInclusive)
                throw new ArgumentOutOfRangeException(nameof(maxExclusive), "maxExclusive must be greater than minInclusive");

            uint range = (uint)(maxExclusive - minInclusive);

            // Reject values that would fall in the "leftover" partial range at the top of a uint,
            // so the result stays uniformly distributed instead of being biased towards low values
            // (the classic modulo-bias problem with `randomUint % range`).
            uint limit = uint.MaxValue - (uint.MaxValue % range);

            byte[] buffer = new byte[4];
            uint value;

            do
            {
                rng.GetBytes(buffer);
                value = BitConverter.ToUInt32(buffer, 0);
            } while (value >= limit);

            return minInclusive + (int)(value % range);
        }
    }
}
