namespace ChecksumCalculator.Verification
{
    public static class ChecksumFileParser
    {
        public static Dictionary<string, string> Parse(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Checksums file not found", path);
            }
            
            var map = new Dictionary<string, string>();

            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var parts = line.Split(' ', 2);

                if (parts.Length != 2)
                {
                    continue; 
                }

                string hash = parts[0];
                string fileName = parts[1].Substring(1);

                map[fileName] = hash;
            }

            return map;
        }
    }
}
