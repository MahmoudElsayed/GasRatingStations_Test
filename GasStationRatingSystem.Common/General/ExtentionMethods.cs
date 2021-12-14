using GasStationRatingSystem.Common;
using GasStationRatingSystem.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GasStationRatingSystem
{
    /// <summary>
    /// This Class Is Resposible for all extention method in the app
    /// we removed Common from Electricity.Common namespace to make this class public 
    /// for the all application
    /// </summary>
    public static class ExtentionMethods
    {
      
        #region String Helpers
        public static string GetUpperLetters(this string word)
        {
            return word.Where(p => char.IsUpper(p)).Select(p => p.ToString()).Aggregate((a, b) => a + b);
        }
        #endregion
        #region User

        public static bool IsAuthorize(this HttpContext httpContext)
        {
            return httpContext.Request.Cookies.ContainsKey(AppConstants._UserIdCookie);
        }

        public static Guid UserId(this IHttpContextAccessor httpContext)
        {
            return (httpContext?.HttpContext.User.Identity.IsAuthenticated ?? false) ? Guid.Parse(httpContext?.HttpContext.User.Identity.Name) : Guid.TryParse(httpContext?.HttpContext?.Request?.Cookies[AppConstants._UserIdCookie], out Guid guid) ? guid : Guid.Empty;
        }
        public static Guid UserId(this HttpContext httpContext)
        {
            return (httpContext?.User.Identity.IsAuthenticated ?? false) ? Guid.Parse(httpContext?.User.Identity.Name) : Guid.TryParse(httpContext?.Request?.Cookies[AppConstants._UserIdCookie], out Guid guid) ? guid : Guid.Empty;
        }

        public static Guid LanguageId(this HttpContext httpContext)
        {
            if (httpContext == null) return Guid.Empty;
            // default ar 
            return Guid.TryParse(httpContext.Request.Cookies[AppConstants.LanguageIdCookie], out Guid id) ? id : Guid.Parse("1B9F3C2F-1F73-4321-8D5D-66D1E7D923F0");
        }


        public static void ChangeLanguage(this HttpResponse Response, Guid LangId, string code, bool isRtl)
        {
            Response.Cookies.Delete(AppConstants.LanguageCodeCookie);
            Response.Cookies.Delete(AppConstants.LanguageIdCookie);

            Response.Cookies.AppendCookie(AppConstants.LanguageCodeCookie, code);
            Response.Cookies.AppendCookie(AppConstants.LanguageIdCookie, LangId + "");
            Response.Cookies.AppendCookie(AppConstants.LanguageRtlCookie, isRtl.ToString());
        }

        /// <summary>
        /// Get Language Code From Cookie If Not Exists Return Default
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string LanguageCode(this HttpContext httpContext)
        {
            return httpContext.Request.Cookies[AppConstants.LanguageCodeCookie]?.ToLower() ?? "ar";
        }
        public static string GetLocalizedString(this HttpContext httpContext, string key)
        {
            var _StringLocalizer = httpContext.RequestServices.GetService(typeof(IStringLocalizer<GasStationRatingSystemResources>)) as IStringLocalizer<GasStationRatingSystemResources>;
            var LangugaeCode = httpContext.LanguageCode();
            if (string.IsNullOrWhiteSpace(key))
                return "";
            else
            {
                CultureInfo cult = new CultureInfo(LangugaeCode);

                return _StringLocalizer.WithCulture(cult)[key].ToString();
            }
        }
        public static bool LanguageIsArabic(this HttpContext httpContext)
        {
            return httpContext.LanguageCode()=="ar";
        }


        public static void AppendCookie(this IResponseCookies responseCookies, string key, string value, bool IsExpire = false)
        {
            var cOption = new CookieOptions()
            {
                HttpOnly = true,
                Path = "/",
                //TODO please un comment the next line if y will use HTTPS
                // Secure = true
            };

            if (!IsExpire)
            {
                cOption.Expires = AppDateTime.Now.AddYears(5);
            }


            responseCookies.Append(key, value, cOption);
        }

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG",".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public static bool IsImage(this string fileName) => ImageExtensions.Any(p => p.ToLower()== Path.GetExtension(fileName).ToLower());

        /// <summary>
        /// Get Direction
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool IsRtl(this HttpContext httpContext)
        {
            //  return !bool.TryParse(httpContext.Request.Cookies[AppConstants.LanguageRtlCookie], out bool t) || t;
            return true;
        }

        public static string RandomString(this int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray()) + new Random().Next(2, 4);
        }

        #endregion

        #region Random Numbers
        public static int RandomNumber(this int number)
        {
            Random random = new Random((int)AppDateTime.Now.Ticks);
            return random.Next(1111111, 9999999);
        }
        #endregion

        /// <summary>
        /// Check If This Field Is Empty Or Null or "" or white space
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string s) => s == null || string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        #region Get Url , Action ,etc

        /// <summary>
        /// Get The Specified Action Followed By its Controller and Area if exists
        /// </summary>
        /// <param name="html">The Current Html Helper</param>
        /// <param name="action">The Required Action At Same Area</param>
        /// <returns></returns>
        public static string GetAction(this IUrlHelper html, string action)
        {
            string _url = "/";
            if (html.ActionContext.HttpContext.GetRouteValue("area") != null)
                _url += html.ActionContext.HttpContext.GetRouteValue("area").ToString() + "/";
            _url += html.ActionContext.HttpContext.GetRouteValue("controller").ToString() + "/";
            _url += action;
            return _url;
        }
        public static string GetAction(this IUrlHelper html, string action, string controller)
        {
            string _url = "/";
            if (html.ActionContext.HttpContext.GetRouteValue("area") != null)
                _url += html.ActionContext.HttpContext.GetRouteValue("area").ToString() + "/";
            _url += controller + "/";
            _url += action;
            return _url;
        }
        public static string GetAction(this IUrlHelper html, string action, string controller, string area)
        {
            string _url = "/";
            _url += area + "/";
            _url += controller + "/";
            _url += action;
            return _url;
        }

        public static string GetAction(this IUrlHelper html, string action, string controller, string area, string route)
        {
            string _url = "/";
            _url += area + "/";
            _url += controller + "/";
            _url += action + "/";
            _url += route;
            return _url;
        }

    


        public static string GetFullUrl(this IUrlHelper url, string fileName = "")
        {
            var request = url.ActionContext.HttpContext.Request;
            string FullUrl = $"{request.Scheme}://{request.Host}/{fileName}";

            return FullUrl;
        }

        #endregion

        /// <summary>
        /// Serialize Any C# <see cref="object"/> <see cref="class"/> , <see cref="IEnumerable"/> ,... etc to json object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this object obj) => JsonConvert.SerializeObject(obj);

        public static T Desrialize<T>(this string obj) => JsonConvert.DeserializeObject<T>(obj);
        public static string GetEnumDescription(this object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());


            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.FirstOrDefault().Description;
            }

            return value.ToString();
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            return (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att) ? valueSelector(att) : default;
        }

        /// <summary>
        /// Get Table Name With Schema Example=> Guide.Cities
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSchemaWithTableName(this Type type) => type.GetAttributeValue((TableAttribute p) => p.Schema + "." + p.Name);

      

    
        /// <summary>
        /// If All Properties inside this object is null or empty or ....
        /// </summary>
        /// <param name="mdl">any c# <see cref="object"/></param>
        /// <param name="EmptyValues">Default is <see cref="null"/>,<see cref="Guid.Empty"/> <see cref="string.Empty"/> </param>
        /// <returns></returns>
        //public static bool IsValid(this object mdl, object[] EmptyValues = null)
        //{
        //    if (EmptyValues == null) EmptyValues = AppConstants.EmptyValues;
        //    return mdl.GetType().GetProperties().Any(x => EmptyValues.Any(y => y == x.GetValue(mdl)));
        //}
        public static bool IsValid(this object mdl, object[] EmptyValues = null)
        {
            if (EmptyValues == null) EmptyValues = AppConstants.EmptyValues;
            return (mdl.GetType().GetProperties().Where(x => EmptyValues.Where(y => y == x.GetValue(mdl)).Any())?.Count() ?? 0) == 0;
        }

       

        

        public static string RemoveLines(this string s)
        {
            if (s == null) return "";
            return s.Replace("\r", "").Replace("\n", "").Replace(Environment.NewLine, "").TrimStart().TrimEnd();
        }



        public static string GetContentType(this string fileName, string defaultName = "")
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
            {
                contentType = defaultName;
            }
            return contentType;
        }

        public static string GetPercent(this int d, int max)
        {

            return max == 0 ? "0 %" : (d * 100 / max) + " %";
        }
        public static double GetPercentAsNumber(this int max, int d)
        {

            return max == 0 ?0:  (d * 100 / max) ;
        }
        public static int GetSubtract(this int num1, int num2)
        {

            return (num1 - num2).GetInteger();
        }
       


        #region EncryptString_And_DecryptString
        public static string EncryptString(this string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AppConstants.EncryptKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString( this string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AppConstants.EncryptKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// لو سالب رجعه موجب برده
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int GetInteger(this int i)
        {
            return i > 0 ? i : i * -1;
        }




    }
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
    public class FileFormData
    {
        public FileFormData()
        {
            this.Content = (Stream)new MemoryStream();
        }

        public string Name { get; set; }

        public Stream Content { get; set; }

        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        public string Extension
        {
            get
            {
                return Path.GetExtension(this.Name);
            }
        }
    }


        //static class ServicesCollectionExtensions
        //{
        //    public static void ConfigureWritable<T>(
        //        this IServiceCollection services,
        //        IConfigurationRoot configuration,
        //        string sectionName,
        //        string file) where T : class, new()
        //    {
        //        services.Configure<T>(configuration.GetSection(sectionName));

        //        services.AddTransient<IWritableOptions<T>>(provider =>
        //        {
        //            var environment = provider.GetService<IWebHostEnvironment>();
        //            var options = provider.GetService<IOptionsMonitor<T>>();
        //            var writer = new OptionsWriter(environment, configuration, file);
        //            return new WritableOptions<T>(sectionName, writer, options);
        //        });
        //    }
        //}
    }
