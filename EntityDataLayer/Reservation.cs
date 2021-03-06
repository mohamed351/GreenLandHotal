//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public string UserID { get; set; }
        public int RoomID { get; set; }
        public System.DateTime ReservationDate { get; set; }
        public Nullable<System.DateTime> ComingDate { get; set; }
        public System.DateTime DepatureDate { get; set; }
        public bool Checkout { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> LeavingDate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual Room Room { get; set; }
    }
}
