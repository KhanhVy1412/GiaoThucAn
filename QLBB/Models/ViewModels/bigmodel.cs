using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBB.Models.ViewModels
{
    public class bigmodel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<int> MaPhanLoai { get; set; }
        public string HinhSP { get; set; }
        public bool Deleted { get; set; }
        public string GhiChu { get; set; }
        public Nullable<int> MaMenu { get; set; }
      
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CTHD> CTHDs { get; set; }
        public IEnumerable<Menu> ListMenu { get; set; }
        public IEnumerable<PhanLoai> ListPhanLoai { get; set; }
        public string TenMenu { get; set; }
        public string Logo { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> MaPhanLoaiMN { get; set; }
        public Nullable<bool> NoiBat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoiTac> DoiTacs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual PhanLoaiMN PhanLoaiMN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}