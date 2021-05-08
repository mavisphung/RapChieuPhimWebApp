using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public void OnGet()
        {
        }
    }
}
