using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RapChieuPhimWebApp.Models
{
    public partial class Phim
    {
        [NotMapped] //khi có lệnh update database trên mô hình thực thể thì thuộc tính này không bị rewrite
        public List<int> TheLoadIds
        {
            get
            {
                //,5,2,12,11,
                string[] arrayIds = DanhSachTheLoaiId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                //chuyển kiểu string thành int bằng câu lệnh select
                List<int> listIds = arrayIds.Select(p => int.Parse(p)).ToList();
                return listIds;
            }
        }

        public IEnumerable<TheLoaiPhim> GetTheLoais(List<TheLoaiPhim> items)
        {
            var lstTL = items.Where(item => TheLoadIds.Contains(item.Id)).ToList();
            return lstTL;
        }

        public string GetTenTheLoais(IEnumerable<TheLoaiPhim> items)
        {
            var lstTen = items.Where(p => TheLoadIds.Contains(p.Id))
                                 .Select(p => p.Ten)
                                 .ToList();
            string strTen = string.Join(", ", lstTen);
            return strTen;
        }

    }
}
