using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RapChieuPhimWebApp.Models;

namespace RapChieuPhimWebApp.Pages
{
    /*
     * Vì trong trung tâm lưu hình ảnh dưới dạng nhị phân mã hóa chứ không phải đường dẫn
     * nên xử lý như sau
     *  <img src="data:image/png;base64, @Model.Phim.Poster" alt="@Model.Phim.TenPhim">
     *  
     * Cách xử lí quan hệ nhiều nhiều bằng cách khác ví dụ như quan hệ giữa phim và thể loại phim
     *      Tạo 1 partial class với tên file là PhimExt nhưng nội dung bên trong vẫn là class Phim
     *      
     * Partial: cùng 1 class nhưng chia ra 2 file vật lý
     */
    public class PhimDetailsModel : PageModel
    {
        private readonly QLRapChieuPhimContext _context;

        public PhimDetailsModel(QLRapChieuPhimContext context)
        {
            _context = context;
        }

        //Chỉ show thông tin nên có thể là field hoặc property đều được
        public Phim Phim { get; private set; }

        //Lấy id từ url
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; } = 0;

        public IEnumerable<TheLoaiPhim> DanhSachTheLoaiPhim { get; private set; }

        //Điều hướng thì sẽ đổi thành IActionResult chứ không dùng void nữa
        public IActionResult OnGet()
        {
            DanhSachTheLoaiPhim = _context.TheLoaiPhims.ToList();
            //Nếu không truyền id vào thì lấy phim đầu tiên trong danh sách
            if (Id <= 0)
            {
                Phim = _context.Phims
                               .Include(p => p.XepHangPhim)
                               .FirstOrDefault();
            } else //lọc theo id
            {
                Phim = _context.Phims
                               .Include(p => p.XepHangPhim)
                               .FirstOrDefault(p => p.Id == Id);
            }
            
            //Chuyển hướng sang PhimList nếu không tìm thấy phim
            if (Phim == null)
            {
                return RedirectToPage("PhimList");
            }

            //trở lại trang hiện hành
            return Page();
        }
    }
}
