namespace TMDTFINAL.Models
{
    public class DashBoardViewModel
    {
        public string SelectedMenuItem { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<SanPham> SanPhams { get; set; }
        public List<TheLoai> TheLoais { get; set; }
        public List<NhaCungCap> NhaCungCaps { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<HoaDon> HoaDons { get; set; }
        public List<Room> Rooms { get; set; }
        public List<ProductWarehouse> ProductWarehouses { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalMessages { get; set; }
        public int TotalAccounts { get; set; }

        public double TotalRevenue { get; set; } // Include TotalRevenue property
        public Dictionary<string, (double TotalRevenue, int Top)> ProductRevenue { get; set; }
    }
}
