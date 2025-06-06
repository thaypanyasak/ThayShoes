using TMDTFINAL.Data;

namespace TMDTFINAL.Services
{
    public interface ITheLoaiService
    {
        int GetTheLoaiId();
    }
    public class TheLoaiService : ITheLoaiService
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiService(ApplicationDbContext db)
        {
            _db = db;
        }

        public int GetTheLoaiId()
        {
            // Thực hiện logic để lấy giá trị TheLoaiId từ database
            // Ví dụ:
            var firstTheLoai = _db.TheLoai.FirstOrDefault();
            return firstTheLoai != null ? firstTheLoai.Id : 0;
        }
    }
}
