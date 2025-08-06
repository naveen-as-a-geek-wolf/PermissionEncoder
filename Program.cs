using System.Collections;
using System.IO.Compression;
using System.Numerics;
using System.Text;

namespace PermissionEncoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Permission Encoder / Decoder ===");

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Encode Permission IDs");
                Console.WriteLine("2. Decode Base62 String");
                Console.WriteLine("0. Exit");
                Console.Write("> ");
                string? choice = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(choice)) continue;

                switch (choice.Trim())
                {
                    case "1":
                        EncodePermissionsFlow();
                        break;
                    case "2":
                        DecodePermissionsFlow();
                        break;
                    case "0":
                        Console.WriteLine("👋 Exiting...");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option. Please enter 1, 2, or 0.");
                        break;
                }

                Console.WriteLine("\nDo you want to continue? (y/n)");
                Console.Write("> ");
                string? again = Console.ReadLine();
                if (!string.Equals(again?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("👋 Goodbye!");
                    break;
                }
            }
        }

        static void EncodePermissionsFlow()
        {
            Console.WriteLine("\nEnter permission IDs (e.g., 1,5,999,26000), separated by commas:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var permissions = input.Split(',')
                    .Select(p => int.Parse(p.Trim()))
                    .Where(p => p > 0)
                    .Distinct()
                    .ToList();

                string encoded = PermissionCodeEncoder.EncodePermissions(permissions);
                Console.WriteLine("\n✅ Encoded Base62: " + encoded);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DecodePermissionsFlow()
        {
            Console.WriteLine("\nEnter Base62 string to decode:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var decoded = PermissionCodeEncoder.DecodePermissions(input.Trim());
                Console.WriteLine("\n🔁 Decoded Permission IDs: " + string.Join(", ", decoded));
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }
    }

    public static class PermissionCodeEncoder
    {
        private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string EncodePermissions(List<int> permissions)
        {
            if (permissions == null || permissions.Count == 0)
                throw new ArgumentException("No permission IDs provided.");

            int maxId = permissions.Max();
            var bits = new BitArray(maxId);

            foreach (var id in permissions)
            {
                if (id < 1)
                    throw new ArgumentOutOfRangeException(nameof(id), "Permission ID must be a positive integer.");
                bits[id - 1] = true;
            }

            byte[] bytes = new byte[(maxId + 7) / 8];
            bits.CopyTo(bytes, 0);

            byte[] compressed = Compress(bytes);

            BigInteger bigInt = new BigInteger(compressed.Append((byte)0).ToArray());
            return ToBase62(bigInt);
        }

        public static List<int> DecodePermissions(string base62)
        {
            BigInteger bigInt = FromBase62(base62);
            byte[] compressed = bigInt.ToByteArray();

            // Remove the appended 0 byte if present
            if (compressed[^1] == 0)
                compressed = compressed[..^1];

            byte[] bytes = Decompress(compressed);
            var bits = new BitArray(bytes);

            var result = new List<int>();
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                    result.Add(i + 1);
            }

            return result;
        }

        private static byte[] Compress(byte[] input)
        {
            using var output = new MemoryStream();
            using (var gzip = new GZipStream(output, CompressionLevel.Optimal))
                gzip.Write(input, 0, input.Length);
            return output.ToArray();
        }

        private static byte[] Decompress(byte[] input)
        {
            using var inputStream = new MemoryStream(input);
            using var gzip = new GZipStream(inputStream, CompressionMode.Decompress);
            using var output = new MemoryStream();
            gzip.CopyTo(output);
            return output.ToArray();
        }

        private static string ToBase62(BigInteger value)
        {
            if (value == 0) return "0";

            var sb = new StringBuilder();
            while (value > 0)
            {
                var remainder = (int)(value % 62);
                sb.Insert(0, Base62Chars[remainder]);
                value /= 62;
            }
            return sb.ToString();
        }

        private static BigInteger FromBase62(string input)
        {
            BigInteger value = 0;
            foreach (char c in input)
            {
                int index = Base62Chars.IndexOf(c);
                if (index == -1)
                    throw new FormatException($"Invalid Base62 character: {c}");
                value = value * 62 + index;
            }
            return value;
        }
    }
}
