using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Models.Enum
{
    public enum CacheType
    {
        MemoryCache_GetAllSupplier = 1,
        MemoryCache_GetAllDish = 2,
    }

    public enum SeenStatus
    {
        Seen = 1,
    }

    public enum PageId
    {
        QuanLyNguoiDung = 1,
        ThongKeDuToan = 6,
        QuanLyQuyenTruyCap = 12,
        QuanLyVaiTro = 13,
        NhapLieu = 14,
        QuanLyKhachHang = 15,
        QuanLySanPham = 16,
    }

    public enum Actions
    {
        Xem = 1,
        Them = 2,
        Sua = 4,
        Xoa = 8,
    }
}