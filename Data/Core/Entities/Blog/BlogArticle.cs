using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Data.Core.Entities.Blog
{
    /// <summary>
    /// کلاس وبلاگ و مقالات
    /// </summary>
    public class BlogArticle 
    {

        public BlogArticle()
        {
            this.BlogComments = new List<BlogComment>();
            this.BlogArticleBlogCategory = new List<BlogArticleBlogCategory>();
            this.BlogArticleBlogTag = new List<BlogArticleBlogTag>();
        }
        [Key]
        public int Id { get; set; }

        #region ForeignKeys
        /// <summary>
        /// شناسه نویسنده
        /// </summary>
        [Display(Name = "شناسه نویسنده")]
        // [MaxLength(100, ErrorMessage = "شناسه نویسنده نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string ApplicationUserId { get; set; }
        /// <summary>
        /// شناسه نویسنده
        /// </summary>
        [ForeignKey("ApplicationUserId")]
        [Display(Name = "شناسه نویسنده")]
        public ApplicationUser ApplicationUser { get; set; }
        #endregion

        /// <summary>
        /// تاریخ پست
        /// </summary>
        [Display(Name = "تاریخ پست")]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "عنوان نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string Title { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "آدرس را وارد نمایید")]
        //[RegularExpression(@"^[a-zA-Z0-9-\(\)]+$", ErrorMessage = "آدرس مطالب باید به صورت انگلیسی باشد (از خط تیره نیز می توانید استفاده کنید)")]
        [MaxLength(100, ErrorMessage = "آدرس نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string BlogArticleURL { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "آدرس دوم")]
        [Url(ErrorMessage = "آدرس مطالب باید وارد شود (از خط تیره نیز می توانید استفاده کنید)")]
        //[RegularExpression(@"^[a-zA-Z0-9-\(\)]+$", ErrorMessage = "آدرس مطالب باید به صورت انگلیسی باشد (از خط تیره نیز می توانید استفاده کنید)")]
        //[MaxLength(100, ErrorMessage = "آدرس نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string ArticleURLSecound { get; set; }

        /// <summary>
        /// عنوان منبع
        /// </summary>
        [Display(Name = "عنوان منبع")]
        [MaxLength(50, ErrorMessage = "عنوان منبع نمی تواند بیشتر از 50 کاراکتر باشد")]
        public string RefrenceTitle { get; set; }
        /// <summary>
        /// آدرس منبع
        /// </summary>
        [Display(Name = "آدرس منبع")]
        [Url(ErrorMessage = "آدرس نامعتبر است")]
        [MaxLength(100, ErrorMessage = "آدرس منبع نمی تواند بیشتر از 100 کاراکتر باشد")]
        public string RefrenceUrl { get; set; }
        /// <summary>
        /// نمایش؟
        /// </summary>
        [Display(Name = "نمایش؟")]
        public bool IsVisible { get; set; }
        /// <summary>
        /// چکیده
        /// </summary>
        [Display(Name = "چکیده")]
        [Required(ErrorMessage = "چکیده را وارد نمایید")]
        // [AllowHtml]
        public string Intro { get; set; }
        /// <summary>
        /// متن کامل
        /// </summary>
        [Display(Name = "متن کامل")]
        // [AllowHtml]
        public string Description { get; set; }
        /// <summary>
        /// تصویر اصلی مطلب
        /// </summary>
        [Display(Name = "تصویر اصلی مطلب")]
        [MaxLength(100, ErrorMessage = "آدرس نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string ImageURL { get; set; }
        /// <summary>
        /// ویدیو مطلب
        /// </summary>
        [Display(Name = "ویدیو مطلب")]
        [MaxLength(100, ErrorMessage = "آدرس نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string VideoURL { get; set; }
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "تاریخ ثبت را وارد نمایید")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        /// <summary>
        /// بازدید
        /// </summary>
        [Display(Name = "بازدید")]
        public Nullable<long> VisitCounter { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        [Display(Name = "ترتیب")]
        public int SortOrder { get; set; }
        /// <summary>
        /// وضعیت انتشار
        /// </summary>
        [Display(Name = "وضعیت انتشار")]
        public Nullable<byte> Status { get; set; }
        /// <summary>
        /// متا دسکریپشن
        /// </summary>
        [Display(Name = "متا دسکریپشن")]
        [MaxLength(300, ErrorMessage = "فایل نمی تواند بیشتر از 300 کاراکتر باشد.")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
        /// <summary>
        /// اضافه شدن به نقشه سایت
        /// </summary>
        //[Display(Name = "متا کیبورد")]
        //public string MetaKeyword { get; set; }
        [Display(Name = "اضافه شدن به نقشه سایت")]
        public bool IsIncudeSiteMap { get; set; }
        /// <summary>
        /// اسکریپت هدر
        /// </summary>
       // [AllowHtml]
        [Display(Name = "اسکریپت هدر")]
        [MaxLength(300, ErrorMessage = "فایل نمی تواند بیشتر از 300 کاراکتر باشد.")]
        public string HeaderScript { get; set; }
        /// <summary>
        /// اسکریپت فوتر
        /// </summary>
       // [AllowHtml]
        [Display(Name = "اسکریپت فوتر")]
        [MaxLength(300, ErrorMessage = "اسکریپت فوتر نمی تواند بیشتر از 300 کاراکتر باشد.")]
        public string FooterScript { get; set; }
        /// <summary>
        /// شمارنده لایک
        /// </summary>
        [Display(Name = "شمارنده لایک")]
        public Nullable<long> LikeCounter { get; set; }
        /// <summary>
        /// شمارنده دیسلایک
        /// </summary>
        [Display(Name = "شمارنده دیسلایک")]
        public Nullable<long> UnlikeCounter { get; set; }
        /// <summary>
        /// تنظیمات سایت
        /// </summary>
        [Display(Name = "تنظیمات سایت")]
        public int? SiteSettingId { get; set; }
        /// <summary>
        /// تنظیمات سایت
        /// </summary>
        //[Display(Name = "تنظیمات سایت")]
        //[ForeignKey("SiteSettingId")]
        //public  SiteSetting SiteSetting { get; set; }
        /// <summary>
        /// نظرات
        /// </summary>
        [Display(Name = "نظرات")]
        public ICollection<BlogComment> BlogComments { get; set; }
        /// <summary>
        /// دسته بندی مقالات
        /// </summary>
        [Display(Name = "دسته بندی مقالات")]
        public ICollection<BlogArticleBlogCategory> BlogArticleBlogCategory { get; set; }
        /// <summary>
        /// برچسب
        /// </summary>
        [Display(Name = "برچسب")]
        [DataType(DataType.MultilineText)]
        public ICollection<BlogArticleBlogTag> BlogArticleBlogTag { get; set; }
    }
}
