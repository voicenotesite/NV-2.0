namespace NV2_Project
{
    public class Faction
    {
        public string Name { get; set; } = string.Empty;
        public int PowerLevel { get; set; } = 1;
        public int BotCount { get; set; } = 10;
        public decimal Treasury { get; set; } = 0;

        public void Recruit(int count)
        {
            BotCount += count;
            PowerLevel = BotCount / 10 + 1;
        }

        public void AddBounty(decimal amount)
        {
            Treasury += amount;
        }
    }
}