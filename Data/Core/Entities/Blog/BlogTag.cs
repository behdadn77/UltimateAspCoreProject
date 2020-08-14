using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Core.Entities.Blog
{
    /// <summary>
    /// کلاس برچسب  
    /// </summary>
    public class BlogTag
    {
        public BlogTag()
        {
            this.BlogArticleBlogTag = new List<BlogArticleBlogTag>();
        }
        [Key]
        public int Id { get; set; }

        #region ForeignKeys

        /// <summary>
        /// شناسه تنظیمات سایت
        /// </summary>
        [Display(Name = "شناسه تنظیمات سایت")]
        public int? SiteSettingId { get; set; }
        /// <summary>
        /// شناسه تنظیمات سایت
        /// </summary>
        //[Display(Name = "شناسه تنظیمات سایت")]
        //[ForeignKey("SiteSettingId")]
        //public SiteSetting SiteSetting { get; set; }

        #endregion

        /// <summary>
        /// نام برچسب
        /// </summary>
        [Display(Name = "نام برچسب")]
        [MaxLength(500, ErrorMessage = "نام  نمی تواند بیشتر از 500 کاراکتر باشد.")]
        public string Title { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "آدرس")]
        [RegularExpression(@"^[a-zA-Z0-9-\(\)]+$", ErrorMessage = "آدرس مطالب باید به صورت انگلیسی باشد (از خط تیره نیز می توانید استفاده کنید)")]
        [MaxLength(100, ErrorMessage = "آدرس  نمی تواند بیشتر از 100 کاراکتر باشد.")]
        public string BlogTagUrl { get; set; }
        /// <summary>
        /// لیست مطالب
        /// </summary>
        [Display(Name = "لیست مطالب")]
        public ICollection<BlogArticleBlogTag> BlogArticleBlogTag { get; set; }
        //public Nullable<int> SiteId { get; set; }
        //public  ICollection<Document> Documents { get; set; }
    }
}
