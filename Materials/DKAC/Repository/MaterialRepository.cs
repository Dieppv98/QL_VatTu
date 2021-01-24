using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Repository
{
    public class MaterialRepository
    {
        MaterialsContext db = new MaterialsContext();

        public List<MaterialType> GetAllMaterial()
        {
            return db.MaterialTypes.AsNoTracking().ToList();
        }

        public List<MaterialType> Search(MaterialTypeRequestModel request, int pageIndex, int recordPerPage, out int totalRecord)
        {
            pageIndex = pageIndex - 1;
            var query = db.MaterialTypes.Where(t => (string.IsNullOrEmpty(request.Keywords) || t.MaterialTypeName.Contains(request.Keywords)));
            totalRecord = query.Count();
            return query.OrderByDescending(x => x.Id).Skip(pageIndex * recordPerPage).Take(recordPerPage).ToList();
        }

        public int UpdateMaterialType(MaterialType model)
        {
            try
            {
                var material = new MaterialType()
                {
                    MaterialTypeName = model.MaterialTypeName,
                };
                db.MaterialTypes.Add(material);
                db.SaveChanges();
                return 1;
            }
            catch
            {
            }
            return 0;
        }

        public MaterialType CheckDupplicateMaterial(string MaterialTypeName, int? id)
        {
            var data = db.MaterialTypes.Where(x => x.Id != id && x.MaterialTypeName == MaterialTypeName).FirstOrDefault();
            return data;
        }

        public List<MaterialType> GetAllMaterialTest(string name)
        {
            return db.MaterialTypes.AsNoTracking().Where(x => x.MaterialTypeName.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}