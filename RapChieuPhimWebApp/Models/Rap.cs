﻿using System;
using System.Collections.Generic;

#nullable disable

namespace RapChieuPhimWebApp.Models
{
    public partial class Rap
    {
        public Rap()
        {
            NhanViens = new HashSet<NhanVien>();
            Phongs = new HashSet<Phong>();
        }

        public int Id { get; set; }
        public string TenRap { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public int NguoiQuanLyId { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
