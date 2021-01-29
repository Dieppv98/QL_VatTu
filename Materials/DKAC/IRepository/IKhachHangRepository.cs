using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;

namespace DKAC.IRepository
{
    public interface IKhachHangRepository
    {
        int Add(KhachHang khachHang);
        int Delete(int id);
        KhachHang GetById(int? id);
        int Update(KhachHang khachHang);
        KhachHangRequestModel GetByKhachHang(string KeySearch, int page, int pageSize);
        KhachHang GetByTenKhachHang(string ten_khach_hang);
        KhachHang GetByMaKhachHang(string ma_khach_hang);
    }
}