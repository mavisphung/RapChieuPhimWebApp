using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapChieuPhimWebApp.Models;

namespace RapChieuPhimWebApp.Pages
{
    public class TestModelModel : PageModel
    {

        private readonly QLRapChieuPhimContext _context;

        public TestModelModel(QLRapChieuPhimContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var items = _context.TheLoaiPhims.ToList();
        }
    }
}
