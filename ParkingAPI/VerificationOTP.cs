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
    
    public partial class VerificationOTP
    {
        public VerificationOTP()
        {
            this.APIUsers = new HashSet<APIUser>();
        }
    
        public int VerificationOTPId { get; set; }
        public string username { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> OTPExpDateTime { get; set; }
        public Nullable<int> UserId { get; set; }
        public string OTP { get; set; }
    
        public virtual UserMaster UserMaster { get; set; }
        public virtual ICollection<APIUser> APIUsers { get; set; }
    }
}
