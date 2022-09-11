using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBB.Models.ViewModels
{
    public class CommentsViewModels
    {
        public int CommentId { get; set; }
        public Nullable<int> MaKH { get; set; }
        public string DanhGia { get; set; }
        public Nullable<int> MaMenu { get; set; }
        public Nullable<System.DateTime> Createon { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual Menu Menu { get; set; }
        public IEnumerable<Menu> ListMenu { get; set; }
    }
}