//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataProcessorAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EOPF_DP_MAPPING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EOPF_DP_MAPPING()
        {
            this.EOPF_DP_SUB_MAPPING = new HashSet<EOPF_DP_SUB_MAPPING>();
            this.EOPF_DP_QUERY = new HashSet<EOPF_DP_QUERY>();
        }
    
        public decimal ID { get; set; }
        public string TABLE_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EOPF_DP_SUB_MAPPING> EOPF_DP_SUB_MAPPING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EOPF_DP_QUERY> EOPF_DP_QUERY { get; set; }
    }
}