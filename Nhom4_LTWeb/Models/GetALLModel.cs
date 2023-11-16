using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom4_LTWeb.Models
{
    public class GetALLModel
    {
        public IEnumerable<Models.ADMIN> GetADMINsModels { get; set; }
        public IEnumerable<Models.BINH_LUAN> GetBINHLUANModels { get; set; }
        public IEnumerable<Models.CHITIETDATHANG> GetCHITIETDATHANGModels { get; set; }
        public IEnumerable<Models.CHITIET_SP> GetCHITIET_SPModels { get; set; }
        public IEnumerable<Models.DONHANG> GetDONHANGModels { get; set; }
        public IEnumerable<Models.HANG> GetHANGModels { get; set; }
        public IEnumerable<Models.HANG_LOAISP> GetHANG_LOAISPModels { get; set; }
        public IEnumerable<Models.KHACHHANG> GetKHACHHANGModels { get; set; }
        public IEnumerable<Models.LOAISP> GetsLOAISPModels { get; set; }
        public IEnumerable<Models.SANPHAM> GetSANPHAMModels { get; set; }
        public IEnumerable<Models.THE> GetTHEModels { get; set; }
        public IEnumerable<Models.THE_CHITIET> GetTHE_CHITIETModels { get; set; }
        public IEnumerable<Models.WISHLIST> GetWISHLISTModels { get; set; }
        public IEnumerable<Models.ChiTietSP> GetChiTietSPModels { get; set; }
        public IEnumerable<Models.RECOMMENT> GetRECOMMENTModels { get; set;}
    }
}