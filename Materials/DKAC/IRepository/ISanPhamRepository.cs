using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;

namespace DKAC.IRepository
{
    public interface ISanPhamRepository
    {
        int Add(SanPham sanPham);
        int Delete(int id);
        SanPham GetById(int? id);
        int Update(SanPham sanPham);
        SanPhamRequestModel GetBySanPham(string KeySearch, int page, int pageSize);
        SanPham GetByTenSanPham(string ten_san_pham);
        SanPham GetByMaSanPham(string ma_san_pham);
    }
}