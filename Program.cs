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
            Console.WriteLine("=== Encoder Application ===");

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Permission Encoder (Base62)");
                Console.WriteLine("2. Delta Encoder (Base64)");
                Console.WriteLine("3. Permission Encoder Base91");
                Console.WriteLine("4. Delta Encoder Base91");
                Console.WriteLine("5. Delta Encoder Unicode");
                Console.WriteLine("0. Exit");
                Console.Write("> ");
                string? choice = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(choice)) continue;

                switch (choice.Trim())
                {
                    case "1":
                        PermissionEncoderFlow();
                        break;
                    case "2":
                        DeltaEncoderFlow();
                        break;
                    case "3":
                        Base91EncoderFlow();
                        break;
                    case "4":
                        DeltaEncoderBase91Flow();
                        break;
                    case "5":
                        DeltaEncoderUnicodeFlow();
                        break;
                    case "0":
                        Console.WriteLine("👋 Exiting...");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option. Please enter 1, 2, 3, 4, 5, or 0.");
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

        static void PermissionEncoderFlow()
        {
            Console.WriteLine("\n=== Permission Encoder ===");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Encode Permission IDs");
            Console.WriteLine("2. Decode Base62 String");
            Console.Write("> ");
            string? choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice)) return;

            switch (choice.Trim())
            {
                case "1":
                    EncodePermissionsFlow();
                    break;
                case "2":
                    DecodePermissionsFlow();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please enter 1 or 2.");
                    break;
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

        static void DeltaEncoderFlow()
        {
            Console.WriteLine("\n=== Delta Encoder ===");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Compress IDs");
            Console.WriteLine("2. Decompress Base64 String");
            Console.Write("> ");
            string? choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice)) return;

            switch (choice.Trim())
            {
                case "1":
                    CompressIdsFlow();
                    break;
                case "2":
                    DecompressIdsFlow();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please enter 1 or 2.");
                    break;
            }
        }

        static void CompressIdsFlow()
        {
            Console.WriteLine("\nEnter IDs (e.g., 1,5,999,26000), separated by commas:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var ids = input.Split(',')
                    .Select(p => int.Parse(p.Trim()))
                    .Where(p => p > 0)
                    .Distinct()
                    .ToList();

                string compressed = DataCompression.Compress(ids);
                Console.WriteLine("\n✅ Compressed Base64: " + compressed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DecompressIdsFlow()
        {
            Console.WriteLine("\nEnter Base64 string to decompress:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var decompressed = DataCompression.Decompress(input.Trim());
                Console.WriteLine("\n🔁 Decompressed IDs: " + string.Join(", ", decompressed));
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void Base91EncoderFlow()
        {
            Console.WriteLine("\n=== Permission Encoder Base91 ===");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Encode Permission IDs");
            Console.WriteLine("2. Decode Base91 String");
            Console.Write("> ");
            string? choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice)) return;

            switch (choice.Trim())
            {
                case "1":
                    EncodePermissionsBase91Flow();
                    break;
                case "2":
                    DecodePermissionsBase91Flow();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please enter 1 or 2.");
                    break;
            }
        }

        static void EncodePermissionsBase91Flow()
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

                string encoded = PermissionCodeEncoderBase91.EncodePermissions(permissions);
                Console.WriteLine("\n✅ Encoded Base91: " + encoded);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DecodePermissionsBase91Flow()
        {
            Console.WriteLine("\nEnter Base91 string to decode:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var decoded = PermissionCodeEncoderBase91.DecodePermissions(input.Trim());
                Console.WriteLine("\n🔁 Decoded Permission IDs: " + string.Join(", ", decoded));
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DeltaEncoderBase91Flow()
        {
            Console.WriteLine("\n=== Delta Encoder Base91 ===");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Compress IDs");
            Console.WriteLine("2. Decompress Base91 String");
            Console.Write("> ");
            string? choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice)) return;

            switch (choice.Trim())
            {
                case "1":
                    CompressIdsBase91Flow();
                    break;
                case "2":
                    DecompressIdsBase91Flow();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please enter 1 or 2.");
                    break;
            }
        }

        static void CompressIdsBase91Flow()
        {
            Console.WriteLine("\nEnter IDs (e.g., 1,5,999,26000), separated by commas:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var ids = input.Split(',')
                    .Select(p => int.Parse(p.Trim()))
                    .Where(p => p > 0)
                    .Distinct()
                    .ToList();

                string compressed = DeltaCompressionBase91.Compress(ids);
                Console.WriteLine("\n✅ Compressed Base91: " + compressed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DecompressIdsBase91Flow()
        {
            Console.WriteLine("\nEnter Base91 string to decompress:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var decompressed = DeltaCompressionBase91.Decompress(input.Trim());
                Console.WriteLine("\n🔁 Decompressed IDs: " + string.Join(", ", decompressed));
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DeltaEncoderUnicodeFlow()
        {
            Console.WriteLine("\n=== Delta Encoder Unicode ===");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Compress IDs");
            Console.WriteLine("2. Decompress Unicode Characters");
            Console.Write("> ");
            string? choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice)) return;

            switch (choice.Trim())
            {
                case "1":
                    CompressIdsUnicodeFlow();
                    break;
                case "2":
                    DecompressIdsUnicodeFlow();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please enter 1 or 2.");
                    break;
            }
        }

        static void CompressIdsUnicodeFlow()
        {
            Console.WriteLine("\nEnter IDs (e.g., 1,5,999,26000), separated by commas:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var ids = input.Split(',')
                    .Select(p => int.Parse(p.Trim()))
                    .Where(p => p > 0)
                    .Distinct()
                    .ToList();

                string compressed = DeltaCompressionUnicode.Compress(ids);
                Console.WriteLine("\n✅ Compressed Unicode Characters: " + compressed);
                
                // Show the character codes for demonstration
                Console.WriteLine("📊 Character details:");
                foreach (char c in compressed)
                {
                    Console.WriteLine($"   Char: '{c}' => Unicode: {(int)c} (0x{(int)c:X4})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }

        static void DecompressIdsUnicodeFlow()
        {
            Console.WriteLine("\nEnter Unicode characters to decompress:");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("⚠️ No input provided.");
                return;
            }

            try
            {
                var decompressed = DeltaCompressionUnicode.Decompress(input.Trim());
                Console.WriteLine("\n🔁 Decompressed IDs: " + string.Join(", ", decompressed));
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }
    }
      public static class DataCompression
    {
        public static string Compress(IEnumerable<int> ids)
        {
            // Step 1: Sort the IDs
            int[] sortedIds = [.. ids.OrderBy(x => x)];

            // Step 2: Delta encode
            int[] deltas = new int[sortedIds.Length];
            deltas[0] = sortedIds[0];
            for (int i = 1; i < sortedIds.Length; i++)
                deltas[i] = sortedIds[i] - sortedIds[i - 1];

            // Step 3: CSV string
            string csv = string.Join(",", deltas);

            // Step 4: Gzip compress
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            using MemoryStream output = new();
            using (GZipStream gzip = new(output, CompressionLevel.Optimal))
                gzip.Write(bytes, 0, bytes.Length);
            byte[] compressed = output.ToArray();

            // Step 5: Base64 encode
            return Convert.ToBase64String(compressed);
        }

        public static IEnumerable<int> Decompress(string base64)
        {
            // Step 1: Base64 decode
            byte[] compressed = Convert.FromBase64String(base64);

            // Step 2: Gzip decompress
            using MemoryStream input = new(compressed);
            using GZipStream gzip = new(input, CompressionMode.Decompress);
            using StreamReader reader = new(gzip, Encoding.UTF8);
            string csv = reader.ReadToEnd();

            // Step 3: Parse CSV to deltas
            IEnumerable<int> deltas = csv.Split(',').Select(int.Parse);

            // Step 4: Reconstruct original array
            int sum = 0;
            foreach (int delta in deltas)
            {
                sum += delta;
                yield return sum;
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

    public static class DeltaCompressionUnicode
    {
        public static string Compress(IEnumerable<int> ids)
        {
            // Step 1: Sort the IDs
            int[] sortedIds = [.. ids.OrderBy(x => x)];

            // Step 2: Delta encode
            int[] deltas = new int[sortedIds.Length];
            deltas[0] = sortedIds[0];
            for (int i = 1; i < sortedIds.Length; i++)
                deltas[i] = sortedIds[i] - sortedIds[i - 1];

            // Step 3: CSV string
            string csv = string.Join(",", deltas);

            // Step 4: Gzip compress
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            using MemoryStream output = new();
            using (GZipStream gzip = new(output, CompressionLevel.Optimal))
                gzip.Write(bytes, 0, bytes.Length);
            byte[] compressed = output.ToArray();

            // Step 5: Convert bytes to special Unicode characters
            return BytesToSpecialChars(compressed);
        }

        public static IEnumerable<int> Decompress(string specialChars)
        {
            // Step 1: Convert special characters back to bytes
            byte[] compressed = SpecialCharsToBytes(specialChars);

            // Step 2: Gzip decompress
            using MemoryStream input = new(compressed);
            using GZipStream gzip = new(input, CompressionMode.Decompress);
            using StreamReader reader = new(gzip, Encoding.UTF8);
            string csv = reader.ReadToEnd();

            // Step 3: Parse CSV to deltas
            IEnumerable<int> deltas = csv.Split(',').Select(int.Parse);

            // Step 4: Reconstruct original array
            int sum = 0;
            foreach (int delta in deltas)
            {
                sum += delta;
                yield return sum;
            }
        }

        private static string BytesToSpecialChars(byte[] bytes)
        {
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < bytes.Length; i += 2)
            {
                // Combine two bytes to create a 16-bit value for Unicode character
                int charCode;
                if (i + 1 < bytes.Length)
                {
                    // Combine two bytes: high byte and low byte
                    charCode = (bytes[i] << 8) | bytes[i + 1];
                }
                else
                {
                    // If odd number of bytes, use the last byte with padding
                    charCode = bytes[i] << 8;
                }
                
                // Ensure we're in a safe Unicode range (avoid control characters and invalid ranges)
                // Use range 0x0900-0x097F (Devanagari script) for special characters like 'ओ'
                charCode = 0x0900 + (charCode % 0x80);
                
                // Convert to character and append
                char specialChar = (char)charCode;
                result.Append(specialChar);
            }
            
            return result.ToString();
        }

        private static byte[] SpecialCharsToBytes(string specialChars)
        {
            List<byte> bytes = new List<byte>();
            
            foreach (char c in specialChars)
            {
                // Convert back from special character to original value
                int charCode = (int)c;
                int originalValue = charCode - 0x0900;
                
                // Extract the two bytes
                byte highByte = (byte)(originalValue >> 8);
                byte lowByte = (byte)(originalValue & 0xFF);
                
                bytes.Add(highByte);
                bytes.Add(lowByte);
            }
            
            return bytes.ToArray();
        }
    }

    public static class DeltaCompressionBase91
    {
        // Base91 encoding table
        private const string Base91Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&()*+,./:;<=>?@[]^_`{|}~\"";

        public static string Compress(IEnumerable<int> ids)
        {
            // Step 1: Sort the IDs
            int[] sortedIds = [.. ids.OrderBy(x => x)];

            // Step 2: Delta encode
            int[] deltas = new int[sortedIds.Length];
            deltas[0] = sortedIds[0];
            for (int i = 1; i < sortedIds.Length; i++)
                deltas[i] = sortedIds[i] - sortedIds[i - 1];

            // Step 3: CSV string
            string csv = string.Join(",", deltas);

            // Step 4: Gzip compress
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            using MemoryStream output = new();
            using (GZipStream gzip = new(output, CompressionLevel.Optimal))
                gzip.Write(bytes, 0, bytes.Length);
            byte[] compressed = output.ToArray();

            // Step 5: Base91 encode
            return ToBase91(compressed);
        }

        public static IEnumerable<int> Decompress(string base91)
        {
            // Step 1: Base91 decode
            byte[] compressed = FromBase91(base91);

            // Step 2: Gzip decompress
            using MemoryStream input = new(compressed);
            using GZipStream gzip = new(input, CompressionMode.Decompress);
            using StreamReader reader = new(gzip, Encoding.UTF8);
            string csv = reader.ReadToEnd();

            // Step 3: Parse CSV to deltas
            IEnumerable<int> deltas = csv.Split(',').Select(int.Parse);

            // Step 4: Reconstruct original array
            int sum = 0;
            foreach (int delta in deltas)
            {
                sum += delta;
                yield return sum;
            }
        }

        private static string ToBase91(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            int b = 0, n = 0;

            foreach (byte byteVal in data)
            {
                b |= byteVal << n;
                n += 8;

                if (n > 13)
                {
                    int v = b & 8191;
                    if (v > 88)
                    {
                        b >>= 13;
                        n -= 13;
                    }
                    else
                    {
                        v = b & 16383;
                        b >>= 14;
                        n -= 14;
                    }
                    sb.Append(Base91Chars[v % 91]);
                    sb.Append(Base91Chars[v / 91]);
                }
            }

            if (n > 0)
            {
                sb.Append(Base91Chars[b % 91]);
                if (n > 7 || b > 90)
                    sb.Append(Base91Chars[b / 91]);
            }

            return sb.ToString();
        }

        private static byte[] FromBase91(string str)
        {
            List<byte> output = new List<byte>();
            int b = 0, n = 0, v = -1;

            foreach (char c in str)
            {
                int val = Base91Chars.IndexOf(c);
                if (val == -1) continue;

                if (v < 0)
                {
                    v = val;
                }
                else
                {
                    v += val * 91;
                    b |= v << n;
                    n += (v & 8191) > 88 ? 13 : 14;
                    while (n >= 8)
                    {
                        output.Add((byte)(b & 255));
                        b >>= 8;
                        n -= 8;
                    }
                    v = -1;
                }
            }

            if (v >= 0)
            {
                output.Add((byte)((b | v << n) & 255));
            }

            return output.ToArray();
        }
    }

public static class PermissionCodeEncoderBase91
{
    // Base91 encoding table
    private const string Base91Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&()*+,./:;<=>?@[]^_`{|}~\"";

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
        return ToBase91(compressed);
    }

    public static List<int> DecodePermissions(string base91)
    {
        byte[] compressed = FromBase91(base91);
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

    private static string ToBase91(byte[] data)
    {
        StringBuilder sb = new StringBuilder();
        int b = 0, n = 0;

        foreach (byte byteVal in data)
        {
            b |= byteVal << n;
            n += 8;

            if (n > 13)
            {
                int v = b & 8191;
                if (v > 88)
                {
                    b >>= 13;
                    n -= 13;
                }
                else
                {
                    v = b & 16383;
                    b >>= 14;
                    n -= 14;
                }
                sb.Append(Base91Chars[v % 91]);
                sb.Append(Base91Chars[v / 91]);
            }
        }

        if (n > 0)
        {
            sb.Append(Base91Chars[b % 91]);
            if (n > 7 || b > 90)
                sb.Append(Base91Chars[b / 91]);
        }

        return sb.ToString();
    }

    private static byte[] FromBase91(string str)
    {
        List<byte> output = new List<byte>();
        int b = 0, n = 0, v = -1;

        foreach (char c in str)
        {
            int val = Base91Chars.IndexOf(c);
            if (val == -1) continue;

            if (v < 0)
            {
                v = val;
            }
            else
            {
                v += val * 91;
                b |= v << n;
                n += (v & 8191) > 88 ? 13 : 14;
                while (n >= 8)
                {
                    output.Add((byte)(b & 255));
                    b >>= 8;
                    n -= 8;
                }
                v = -1;
            }
        }

        if (v >= 0)
        {
            output.Add((byte)((b | v << n) & 255));
        }

        return output.ToArray();
    }
}

}
