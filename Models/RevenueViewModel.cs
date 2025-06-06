namespace TMDTFINAL.Models
{
    public class RevenueViewModel
    {
        public Dictionary<string, double> MonthlyRevenue { get; set; }
        public Dictionary<string, double> ProductRevenue { get; set; }
        public Dictionary<string, Dictionary<string, double>> ProductsSoldInMonth { get; set; }
        public List<string> AllProducts { get; set; }
        public double TotalRevenue { get; set; }
        public Dictionary<string, double> ProductPrices { get; set; }
        public List<string> TopProducts { get; set; }
    }
}
