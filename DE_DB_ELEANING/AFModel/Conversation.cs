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
    
    public partial class Conversation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Conversation()
        {
            this.ConversationSentences = new HashSet<ConversationSentence>();
        }
    
        public System.Guid Id { get; set; }
        public string NameVi { get; set; }
        public string NameDe { get; set; }
        public Nullable<System.Guid> SubjectId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Location { get; set; }
        public Nullable<int> Index { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConversationSentence> ConversationSentences { get; set; }
    }
}
