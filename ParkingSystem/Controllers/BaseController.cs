using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;

namespace ParkingSystem.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            try
            {
                string Lang = "";
                HttpCookie LangCookie = Request.Cookies["culture"];
                if (LangCookie != null)
                {
                    Lang = LangCookie.Value;
                }
                else
                {
                    Lang = "en";
                }
                var CultureInfo = new CultureInfo(Lang);
                Thread.CurrentThread.CurrentUICulture = CultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CultureInfo.Name);
                LangCookie = new HttpCookie("culture", Lang);
                LangCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Response.Cookies.Add(LangCookie);
                var Language = AvailableLanguage.Where(x => x.LanguageFullName == Lang).FirstOrDefault();
                if (Language != null)
                {
                    ViewBag.dir = Language.LanguageDirection;
                    ViewBag.lang = Language.LanguageCultureName;
                }
                return base.BeginExecuteCore(callback, state);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<Language> AvailableLanguage = new List<Language> {
            new Language {LanguageFullName = "English" ,LanguageCultureName = "en" , LanguageDirection = "ltr"},
            new Language { LanguageFullName = "Arabic" , LanguageCultureName = "ar" , LanguageDirection = "rtl"},
        };
        public class Language
        {
            public string LanguageFullName { get; set; }
            public string LanguageCultureName { get; set; }
            public string LanguageDirection { get; set; }

        }
    }
}