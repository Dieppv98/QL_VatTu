using DKAC.IRepository;
using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Repository
{
    public class ChiTietThongKeRepository : IChiTietThongKeRepository
    {
        private MaterialsContext db = new MaterialsContext();
        public int Add(ChiTietThongKe chiTietThongKe)
        {
            db.ChiTietThongKes.Add(chiTietThongKe);
            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}