//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParkingAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApiToken 
    {
        public ApiToken()
        {
            this.APIUsers = new HashSet<APIUser>();
        }
    
        public int TokenId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string authToken { get; set; }
        public string IssuedOn { get; set; }
        public string ExpiresOn { get; set; }
    
        public virtual UserMaster UserMaster { get; set; }
        public virtual ICollection<APIUser> APIUsers { get; set; }
    }
}
