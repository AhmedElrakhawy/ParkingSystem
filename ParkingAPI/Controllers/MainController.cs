using ParkingAPI.Models;
using ParkingAPI.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingAPI.Controllers
{
    public class MainController : ApiController
    {

        [HttpPost]
        [Route("Main/SendingOTP")]
        public HttpResponseMessage SendingOTP(string UserName , string Password)
        {
            try
            {
                int OTP = 0; 
                using (ParkingSystemEntities DbContext = new ParkingSystemEntities())
                {
                    bool IsExist = DbContext.UserMasters.Any(x => x.UserName == UserName && x.Password == Password);

                    if (IsExist)
                    {
                        UserMaster User = DbContext.UserMasters.FirstOrDefault(x => x.UserName == UserName);
                        if (SendOTPFunction(UserName, "", User.Email, out OTP))
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK, new ResponseObj()
                            {
                                Response = "Success",
                                ResponseCode = "101",
                                ResponseMessageAr = "تـــم أرسال الكود بنــجاح ",
                                ResponseMessage = "OTP Sent Successfully",
                            });
                        }
                        else
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK, new ResponseObj()
                            {
                                Response = "Faild",
                                ResponseCode = "102",
                                ResponseMessageAr = "فشل أرسال الكود  ",
                                ResponseMessage = "Fail To send OTP Code",
                            });
                        }

                    }
                    else
                    {
                        return this.Request.CreateResponse(HttpStatusCode.OK, new ResponseObj()
                        {
                            Response = "UnAuthorized",
                            ResponseCode = "103",
                            ResponseMessage = "User is not Exist",
                            ResponseMessageAr = "مستخدم غير موجود",
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool SendOTPFunction(string UserName , string MobileNumber , string ContactEmail , out int OTP)
        {
            try
            {
                string PathURL = CommonFunction.GetAppCanKeyValue("PathURL");
                string Path = PathURL + "Template/OTPTemplate.html";
                string Htmlbody2 = File.ReadAllText(new Uri(Path).LocalPath);
                int OTPExpTime = Convert.ToInt32(CommonFunction.GetAppCanKeyValue("OTPExpTime"));
                Random random = new Random();
                OTP = random.Next(1000, 9999);
                using (ParkingSystemEntities DbContext = new ParkingSystemEntities())
                {
                    var OtpObj = DbContext.VerificationOTPs.Where(x => x.username == UserName).FirstOrDefault();
                    if (OtpObj == null)
                    {
                        VerificationOTP UserOtp = new VerificationOTP()
                        {
                            username = UserName,
                            OTP = OTP.ToString(),
                            DateCreated = DateTime.Now,
                            OTPExpDateTime = DateTime.Now.AddMinutes(OTPExpTime)
                        };
                        DbContext.VerificationOTPs.Add(UserOtp);
                    }
                    else
                    {
                        OtpObj.OTP = OTP.ToString();
                        OtpObj.DateCreated = DateTime.Now;
                        OtpObj.OTPExpDateTime = DateTime.Now.AddMinutes(OTPExpTime);
                    }
                    if (DbContext.SaveChanges() > 0)
                    {
                        Htmlbody2.Replace("userName", UserName);
                        Htmlbody2.Replace("otp", OTP.ToString());
                        CommonFunction.SendEmail(Htmlbody2, ContactEmail, "OTP Verification");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
