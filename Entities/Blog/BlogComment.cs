using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Entities.Blog
{
    /// <summary>
    /// کلاس بخش نظرات
    /// </summary>
    public class BlogComment
    {
        public BlogComment()
        {
            //this.ShopProducts = new List<ShopProduct>();
        }
        [Key]
        public int Id { get; set; }

        #region ForeignKeys
        /// <summary>
        /// شناسه نویسنده
        /// </summary>
        [Display(Name = "شناسه نویسنده")]
        //[MaxLength(50, ErrorMessage = "فایل نمی تواند بیشتر از 50 کاراکتر باشد.")]
        public string AuthorId { get; set; }
        /// <summary>
        /// شناسه نویسنده
        /// </summary>
        [Display(Name = "شناسه نویسنده")]
        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }
        /// <summary>
        /// مطلب
        /// </summary>
        [Display(Name = "مطلب")]
        public int BlogArticleId { get; set; }
        /// <summary>
        /// مطلب
        /// </summary>
        [ForeignKey("BlogArticleId")]
        [Display(Name = "مطلب")]
        public BlogArticle BlogArticle { get; set; }
        /// <summary>
        /// شناسه پدر
        /// </summary>
        [Display(Name = "شناسه پدر")]
        public Nullable<int> ParentId { get; set; }
        /// <summary>
        /// شناسه پدر
        /// </summary>
        [Display(Name = "شناسه پدر")]
        [ForeignKey("ParentId")]
        public BlogComment Parent { get; set; }
        /// <summary>
        /// شناسه تنظیمات سایت
        /// </summary>
        [Display(Name = "شناسه تنظیمات سایت")]
        public Nullable<int> SiteSettingId { get; set; }
        /// <summary>
        /// شناسه تنظیمات سایت
        /// </summary>
        //[Display(Name = "شناسه تنظیمات سایت")]
        //[ForeignKey("SiteSettingId")]
        //public SiteSetting SiteSetting { get; set; }
        #endregion

        /// <summary>
        /// وضعیت
        /// </summary
        [Display(Name = "وضعیت")]
        public BlogCommentStatus Status { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        [Display(Name = "توضیح")]
        [Required(ErrorMessage = "وارد کردن متن نظر اجباری می باشد.")]
        // [AllowHtml]
        //[MaxLength(300, ErrorMessage = "توضیحات نمی تواند بیشتر از 300 کاراکتر باشد.")]
        public string Description { get; set; }
        /// <summary>
        /// تاریخ ارسال
        /// </summary>
        [Display(Name = "تاریخ ارسال")]
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        [Display(Name = "شماره تلفن")]
        [MaxLength(13, ErrorMessage = "تلفن همراه  نمی تواند بیشتر از 13 کاراکتر باشد.")]
        public string CellPhoneNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [MaxLength(50, ErrorMessage = "ایمیل  نمی تواند بیشتر از 50 کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبری را وارد نمایید")]
        [Display(Name = "ایمیل")]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        [Display(Name = "نام")]
        [MaxLength(50, ErrorMessage = "نام نمی تواند بیشتر از 50 کاراکتر باشد.")]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "نام خانوادگی نمی تواند بیشتر از 50 کاراکتر باشد.")]
        public string LastName { get; set; }
        /// <summary>
        /// نمایش؟
        /// </summary>
        [Display(Name = "نمایش؟")]
        public bool IsVisible { get; set; }
        /// <summary>
        /// شمارنده دیسلایک
        /// </summary>
        [Display(Name = "موافق")]
        public Nullable<long> UnlikeCounter { get; set; }
        /// <summary>
        /// شمارنده لایک
        /// </summary>        
        [Display(Name = "مخالف")]
        public Nullable<long> LikeCounter { get; set; }
        ///// <summary>
        ///// لیست محصولات فروشگاه
        ///// </summary>
        //[Display(Name = "لیست محصولات فروشگاه")]
        //public  ICollection<ShopProduct> ShopProducts { get; set; }
    }

    public enum BlogCommentStatus : byte
    {
        در_حال_بررسی,
        اسپم,
        تایید_شده,
        تایید_نشده,
    }
}
