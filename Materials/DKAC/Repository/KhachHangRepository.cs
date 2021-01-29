using DKAC.IRepository;
using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Repository
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private MaterialsContext db = new MaterialsContext();
        public int Add(KhachHang khachHang)
        {
            try
            {
                khachHang.created_date = DateTime.Now;
                khachHang.last_updated = DateTime.Now;
                db.KhachHang.Add(khachHang);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public int Delete(int id)
        {
            var data = db.KhachHang.FirstOrDefault(x => x.id == id);
            if (data == null) { return 0; }

            db.KhachHang.Remove(data);
            db.SaveChanges();
            return 1;
        }

        public KhachHang GetById(int? id)
        {
            var data = db.KhachHang.Where(x => x.id == id).FirstOrDefault();
            if (data == null) { return new KhachHang(); }
            return data;
        }

        public KhachHangRequestModel GetByKhachHang(string KeySearch, int page, int pageSize)
        {
            KhachHangRequestModel request = new KhachHangRequestModel();
            List<KhachHang> lst = new List<KhachHang>();
            request.page = page;
            request.pageSize = pageSize;
            lst = db.KhachHang.ToList();
            int startRow = (page - 1) * pageSize;
            if (!string.IsNullOrEmpty(KeySearch))
            {
                KeySearch = KeySearch.ToLowerInvariant();
                lst = lst.Where(x => x.ten_khach_hang.ToLowerInvariant().Contains(KeySearch) ||
                                     x.ma_khach_hang.ToLowerInvariant().Contains(KeySearch)).ToList();
            }
            request.totalRecord = lst.Count;
            request.data = lst.OrderBy(x => x.id)
                .Skip(startRow)
                .Take(pageSize)
                .ToList();
            int totalPage = 0;
            if (request.totalRecord % pageSize == 0)
            {
                totalPage = request.totalRecord / pageSize;
            }
            else
            {
                totalPage = request.totalRecord / pageSize + 1;
            }
            request.totalPage = totalPage;
            return request;
        }

        public KhachHang GetByMaKhachHang(string ma_khach_hang)
        {
            var data = db.KhachHang.Where(x => x.ma_khach_hang.ToLower() == ma_khach_hang.ToLower()).FirstOrDefault();
            return data;
        }

        public KhachHang GetByTenKhachHang(string ten_khach_hang)
        {
            var data = db.KhachHang.Where(x => x.ten_khach_hang.ToLower() == ten_khach_hang.ToLower()).FirstOrDefault();
            return data;
        }

        public int Update(KhachHang khachHang)
        {
            var data = db.KhachHang.FirstOrDefault(x => x.id == khachHang.id);
            if (data == null) { return 0; }

            data.ten_khach_hang = khachHang.ten_khach_hang;
            data.dia_chi = khachHang.dia_chi;
            data.ma_so_thue = khachHang.ma_so_thue;
            data.dien_thoai = khachHang.dien_thoai;
            data.fax = khachHang.fax;
            data.email = khachHang.email;
            data.nguoi_dai_dien = khachHang.nguoi_dai_dien;
            data.chuc_vu = khachHang.chuc_vu;
            data.last_updated = khachHang.last_updated;
            return db.SaveChanges();
        }
    }
}