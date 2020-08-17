using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Blog
{
    /// <summary>
    /// کلاس دسته بندی مطالب
    /// </summary>
    public class BlogArticleBlogCategory
    {

        public BlogArticleBlogCategory()
        {

        }
        [Key]
        public int Id { get; set; }

        #region ForeignKeys
        /// <summary>
        /// شناسه دسته بندی
        /// </summary>
        [Display(Name = "شناسه دسته بندی")]
        public int BlogCategoryId { get; set; }
        /// <summary>
        /// شناسه مطلب
        /// </summary>
        [Display(Name = "شناسه مطلب")]
        public int BlogArticleId { get; set; }
        /// <summary>
        /// شناسه دسته بندی
        /// </summary>
        [Display(Name = "شناسه دسته بندی")]
        [ForeignKey("BlogCategoryId")]
        public  BlogCategory BlogCategory { get; set; }
        /// <summary>
        /// شناسه مطلب
        /// </summary>
        [Display(Name = "شناسه مطلب")]
        [ForeignKey("BlogArticleId")]       
        public  BlogArticle BlogArticle { get; set; }
        ////public  Category Category { get; set; }
        ///// <summary>
        ///// شناسه CRMCategory
        ///// </summary>
        //[Display(Name = "شناسه CRMCategory")]
        //public int? CRMCategoryId { get; set; }
        ///// <summary>
        ///// شناسه CRMCategory
        ///// </summary>
        //[Display(Name = "شناسه CRMCategory")]
        //[ForeignKey("CRMCategoryId")]
        //public  CRMCategory CRMCategory { get; set; }

        #endregion
    }
}
