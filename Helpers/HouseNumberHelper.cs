namespace MgeniTrack.Helpers
{
    public static class HouseNumberHelper
    {

        public static List<string> GetAllHouseNumbers()
        {
            var numbers = new List<string>();

            foreach (var block in new[] { "A", "B", "C" })
            {
                for (int floor = 1; floor <= 6; floor++)
                {
                    for (int unit = 1; unit <= 10; unit++)
                    {
                        numbers.Add($"{block}{floor}{unit:D2}");
                    }
                }
            }

            return numbers; 
        }

        public static List<string> GetBlockNumbers(string block)
        {
            var numbers = new List<string>();
            for (int floor = 1; floor <= 6; floor++)
                for (int unit = 1; unit <= 10; unit++)
                    numbers.Add($"{block.ToUpper()}{floor}{unit:D2}");
            return numbers;
        }

        public static string GetBlockLabel(string houseNumber)
        {
            if (string.IsNullOrEmpty(houseNumber) || houseNumber.Length < 1)
                return "Unknown";

            return houseNumber[0] switch
            {
                'A' => "Block A — Residential",
                'B' => "Block B — Residential",
                'C' => "Block C — BnB",
                _ => "Unknown"
            };
        }

        public static bool IsBnB(string houseNumber) =>
            !string.IsNullOrEmpty(houseNumber) && houseNumber.StartsWith("C");

        // Grouped for dropdown display: Block A (A101–A610), etc.
        public static Dictionary<string, List<string>> GetGrouped()
        {
            return new Dictionary<string, List<string>>
            {
                { "Block A — Residential (A101–A610)", GetBlockNumbers("A") },
                { "Block B — Residential (B101–B610)", GetBlockNumbers("B") },
                { "Block C — BnB Units  (C101–C610)", GetBlockNumbers("C") }
            };
        }
    }
}