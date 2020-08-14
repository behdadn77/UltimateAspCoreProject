using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Core.Entities.Blog
{
    /// <summary>
    /// کلاس دسته بندی مطالب
    /// </summary>
    public class BlogCategory
    {
        public BlogCategory()
        {
            this.BlogCategories = new List<BlogCategory>();
            this.BlogArticleBlogCategory = new List<BlogArticleBlogCategory>();
        }
        [Key]
        public int Id { get; set; }

        #region ForeignKeys
        /// <summary>
        /// شناسه پدر
        /// </summary>
        [Display(Name = "شناسه پدر")]
        public int? ParentId { get; set; }
        /// <summary>
        /// شناسه پدر
        /// </summary>
        [Display(Name = "شناسه پدر")]
        [ForeignKey("ParentId")]
        public BlogCategory Parent { get; set; }
        /// <summary>
        /// دسته بندی مطالب
        /// </summary>
        [Display(Name = "دسته بندی مطالب")]
        public  ICollection<BlogArticleBlogCategory> BlogArticleBlogCategory { get; set; }
        //public  Category Category { get; set; }

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
        #endregion

        /// <summary>
        /// نمایش؟
        /// </summary>
        [Display(Name = "نمایش؟")]
        public bool IsVisible { get; set; }       
        /// <summary>
        /// لیست گروه
        /// </summary>
        [Display(Name = "لیست گروه")]
        public  ICollection<BlogCategory> BlogCategories { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید.")]
        [MaxLength(50, ErrorMessage = "عنوان نمی تواند بیشتر از 50 کاراکتر باشد.")]
        public string Title { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "آدرس")]
        [MaxLength(100, ErrorMessage = "آدرس مطلب نمی تواند بیشتر از 100 کاراکتر باشد.")]
        //[RegularExpression(@"^[a-zA-Z0-9-\(\)]+$", ErrorMessage = "آدرس مطالب باید به صورت انگلیسی باشد (از خط تیره نیز می توانید استفاده کنید)")]
        public string BlogCategoryURL { get; set; }       
    }
}
