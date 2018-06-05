using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using InventoryManagement.Services.Interface.Utility;

namespace InventoryManagement.Services.Utility
{
    public class PasswordGeneratorService : IPasswordGeneratorService
    {
        public int MinimumLengthPassword { get; }
        public int MaximumLengthPassword { get; }
        public int MinimumLowerCaseChars { get; }
        public int MinimumUpperCaseChars { get; }
        public int MinimumNumericChars { get; }
        public int MinimumSpecialChars { get; }

        public static string AllLowerCaseChars { get; }
        public static string AllUpperCaseChars { get; }
        public static string AllNumericChars { get; }
        public static string AllSpecialChars { get; }
        private readonly string _allAvailableChars;

        private readonly RandomSecureVersion _randomSecure = new RandomSecureVersion();
        private readonly int _minimumNumberOfChars;

        static PasswordGeneratorService()
        {
            AllLowerCaseChars = GetCharRange('a', 'z', "ilo");
            AllUpperCaseChars = GetCharRange('A', 'Z', "IO");
            AllNumericChars = GetCharRange('0', '9');
            AllSpecialChars = "!@#%*$?";
        }

        public PasswordGeneratorService(int minimumSpecialChars = 1, int minimumNumericChars = 2, int minimumUpperCaseChars = 2, int minimumLowerCaseChars = 2, int maximumLengthPassword = 8, int minimumLengthPassword = 8)
        {
            _minimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars + minimumNumericChars + minimumSpecialChars;

            MinimumLengthPassword = minimumLengthPassword;
            MaximumLengthPassword = maximumLengthPassword;

            MinimumLowerCaseChars = minimumLowerCaseChars;
            MinimumUpperCaseChars = minimumUpperCaseChars;
            MinimumNumericChars = minimumNumericChars;
            MinimumSpecialChars = minimumSpecialChars;

            _allAvailableChars = OnlyIfOneCharIsRequired(minimumLowerCaseChars, AllLowerCaseChars) + OnlyIfOneCharIsRequired(minimumUpperCaseChars, AllUpperCaseChars) + OnlyIfOneCharIsRequired(minimumNumericChars, AllNumericChars) + OnlyIfOneCharIsRequired(minimumSpecialChars, AllSpecialChars);
        }

        private string OnlyIfOneCharIsRequired(int minimum, string allChars)
        {
            return minimum > 0 || _minimumNumberOfChars == 0 ? allChars : string.Empty;
        }

        public string Random()
        {
            var lengthOfPassword = _randomSecure.Next(MinimumLengthPassword, MaximumLengthPassword);

            var minimumChars = GetRandomString(AllLowerCaseChars, MinimumLowerCaseChars) + GetRandomString(AllUpperCaseChars, MinimumUpperCaseChars) + GetRandomString(AllNumericChars, MinimumNumericChars) + GetRandomString(AllSpecialChars, MinimumSpecialChars);
            var rest = GetRandomString(_allAvailableChars, lengthOfPassword - minimumChars.Length);
            var unshuffeledResult = minimumChars + rest;

            var result = unshuffeledResult.ShuffleTextSecure();
            return result;
        }

        private string GetRandomString(string possibleChars, int lenght)
        {
            var result = string.Empty;
            for (var position = 0; position < lenght; position++)
            {
                var index = _randomSecure.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        private static string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (var value = minimum; value <= maximum; value++)
            {
                result += value;
            }
            if (string.IsNullOrEmpty(exclusiveChars)) return result;
            var inclusiveChars = result.Except(exclusiveChars).ToArray();
            result = new string(inclusiveChars);
            return result;
        }
    }

    internal static class Extensions
    {
        private static readonly Lazy<RandomSecureVersion> RandomSecure =
            new Lazy<RandomSecureVersion>(() => new RandomSecureVersion());
        public static IEnumerable<T> ShuffleSecure<T>(this IEnumerable<T> source)
        {
            var sourceArray = source.ToArray();
            for (var counter = 0; counter < sourceArray.Length; counter++)
            {
                var randomIndex = RandomSecure.Value.Next(counter, sourceArray.Length);
                yield return sourceArray[randomIndex];

                sourceArray[randomIndex] = sourceArray[counter];
            }
        }

        public static string ShuffleTextSecure(this string source)
        {
            var shuffeldChars = source.ShuffleSecure().ToArray();
            return new string(shuffeldChars);
        }
    }

    internal class RandomSecureVersion
    {
        private readonly RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();

        public int Next()
        {
            var randomBuffer = new byte[4];
            _rngProvider.GetBytes(randomBuffer);
            var result = BitConverter.ToInt32(randomBuffer, 0);
            return result;
        }

        public int Next(int maximumValue)
        {
            return Next(0, maximumValue);
        }

        public int Next(int minimumValue, int maximumValue)
        {
            var seed = Next();

            return new Random(seed).Next(minimumValue, maximumValue);
        }
    }
}
