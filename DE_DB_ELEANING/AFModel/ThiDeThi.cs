//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DE_DB_ELEANING.AFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThiDeThi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThiDeThi()
        {
            this.ThiBaiTapThis = new HashSet<ThiBaiTapThi>();
            this.ThiResults = new HashSet<ThiResult>();
        }
    
        public System.Guid ID { get; set; }
        public Nullable<bool> Status { get; set; }
        public string NameVi { get; set; }
        public string NameDe { get; set; }
        public string Name { get; set; }
        public Nullable<int> Index { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string NgheVi { get; set; }
        public string NgheDe { get; set; }
        public string NoiVi { get; set; }
        public string NoiDe { get; set; }
        public string DocVi { get; set; }
        public string DocDe { get; set; }
        public string VietVi { get; set; }
        public string VietDe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThiBaiTapThi> ThiBaiTapThis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThiResult> ThiResults { get; set; }
    }
}
