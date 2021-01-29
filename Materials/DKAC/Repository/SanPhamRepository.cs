using System;
using System.Collections.Generic;
using System.Linq;
using DKAC.IRepository;
using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;

namespace DKAC.Repository
{
    public class SanPhamRepository: ISanPhamRepository
    {
        private MaterialsContext db = new MaterialsContext();
        public int Add(SanPham sanPham)
        {
            try
            {
                sanPham.created_date = DateTime.Now;
                sanPham.last_updated = DateTime.Now;
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public int Delete(int id)
        {
            var data = db.SanPham.FirstOrDefault(x => x.id == id);
            if (data == null) { return 0; }

            db.SanPham.Remove(data);
            db.SaveChanges();
            return 1;
        }

        public SanPham GetById(int? id)
        {
            var data = db.SanPham.Where(x=>  x.id == id).FirstOrDefault();
            if (data == null) { return new SanPham(); }
            return data;
        }

        public int Update(SanPham sanPham)
        {
            var data = db.SanPham.FirstOrDefault(x => x.id == sanPham.id);
            if (data == null) { return 0; }

            data.ten_san_pham = sanPham.ten_san_pham;
            data.last_updated = sanPham.last_updated;
            return db.SaveChanges();
        }

        public SanPhamRequestModel GetBySanPham(string KeySearch, int page, int pageSize)
        {
            SanPhamRequestModel request = new SanPhamRequestModel();
            List<SanPham> lst = new List<SanPham>();
            request.page = page;
            request.pageSize = pageSize;
            lst = db.SanPham.ToList();
            int startRow = (page - 1) * pageSize;
            if (!string.IsNullOrEmpty(KeySearch))
            {
                KeySearch = KeySearch.ToLowerInvariant();
                lst = lst.Where(x => x.ten_san_pham.ToLowerInvariant().Contains(KeySearch) || 
                                     x.ma_san_pham.ToLowerInvariant().Contains(KeySearch)).ToList();
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

        public SanPham GetByTenSanPham(string ten_san_pham)
        {
            var data = db.SanPham.Where(x => x.ten_san_pham.ToLower() == ten_san_pham.ToLower()).FirstOrDefault();
            return data;
        }

        public SanPham GetByMaSanPham(string ma_san_pham)
        {
            var data = db.SanPham.Where(x => x.ma_san_pham.ToLower() == ma_san_pham.ToLower()).FirstOrDefault();
            return data;
        }
    }
}