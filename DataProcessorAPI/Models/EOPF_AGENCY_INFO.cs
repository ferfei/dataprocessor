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
    
    public partial class EOPF_AGENCY_INFO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EOPF_AGENCY_INFO()
        {
            this.EOPF_AGENCY_DETAILS = new HashSet<EOPF_AGENCY_DETAILS>();
        }
    
        public decimal EAI_ID { get; set; }
        public string EAI_CODE { get; set; }
        public string EAI_DESCRIPTION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EOPF_AGENCY_DETAILS> EOPF_AGENCY_DETAILS { get; set; }
    }
}
