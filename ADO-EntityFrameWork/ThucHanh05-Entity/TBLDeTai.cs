//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThucHanh05_Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLDeTai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLDeTai()
        {
            this.TBLHuongDans = new HashSet<TBLHuongDan>();
        }
    
        public string Madt { get; set; }
        public string Tendt { get; set; }
        public Nullable<int> Kinhphi { get; set; }
        public string Noithuctap { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLHuongDan> TBLHuongDans { get; set; }
    }
}