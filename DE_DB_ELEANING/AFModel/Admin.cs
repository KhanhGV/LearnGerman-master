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
    
    public partial class Admin
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }
    
        public virtual Account Account { get; set; }
    }
}