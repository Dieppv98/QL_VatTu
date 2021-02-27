using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKAC.IRepository
{
    public interface IChiTietThongKeRepository
    {
        int Add(ChiTietThongKe chiTietThongKe);
        int Delete(int id);
    }
}
