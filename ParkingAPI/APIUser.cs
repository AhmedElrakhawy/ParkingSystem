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
    
    public partial class APIUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public Nullable<int> VerificationOTPId { get; set; }
        public Nullable<int> APITokenId { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ApiToken ApiToken { get; set; }
        public virtual VerificationOTP VerificationOTP { get; set; }
    }
}
