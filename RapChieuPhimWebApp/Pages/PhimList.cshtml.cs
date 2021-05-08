using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RapChieuPhimWebApp.Models;

namespace RapChieuPhimWebApp.Pages
{
    public class PhimListModel : PageModel
    {

        private readonly QLRapChieuPhimContext _context;

        public PhimListModel(QLRapChieuPhimContext context)
        {
            _context = context;
        }

        public IEnumerable<Phim> ListPhim { get; private set; }

        public IEnumerable<TheLoaiPhim> ListTheLoai { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;
        public int PageCount = 1;
        private int PageSize = 5;

        [BindProperty(SupportsGet = true)]
        public int TheLoaiId { get; set; }
        //dropdown list
        public SelectList TheLoaiSelectList { get; private set; }
        public void OnGet()
        {
            ListTheLoai = _context.TheLoaiPhims
                                  .AsNoTracking() //Không theo dõi trạng thái, bao gồm CRUD => đỡ cồng kềnh, nhanh hơn
                                  .ToList();
            TheLoaiSelectList = new SelectList(ListTheLoai, "Id", "Ten", selectedValue: TheLoaiId);
            IQueryable<Phim> phimQuery = _context.Phims;

            if (TheLoaiId != 0) phimQuery = phimQuery.Where(p => p.DanhSachTheLoaiId.Contains($",{TheLoaiId},"));
            // Kết nối csdl thực hiện thống kê
            float pageCount = (float)phimQuery.Count() / PageSize;
            PageCount = (int)Math.Ceiling(pageCount);
            if (PageNumber < 1) PageNumber = 1;
            if (PageNumber > PageCount) PageNumber = PageCount;
            int n = (PageNumber - 1) * PageSize; //số mẫu tin cần bỏ quả

            // Kết nối csdl thực hiện kéo dữ liệu Phim ứng với một trang, bao gồm XepHangPhim

            ListPhim = phimQuery.Include(p => p.XepHangPhim)
                                .Skip(n)
                                .Take(PageSize)
                                .AsNoTracking()
                                .ToList();
        }
    }
}
