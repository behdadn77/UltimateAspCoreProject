using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Blog
{
    /// <summary>
    /// کلاس برچسب مطالب
    /// </summary>

    public class BlogArticleBlogTag
    {
        public BlogArticleBlogTag()
        {

        }

        [Key]
        public int Id { get; set; }

        #region ForeignKeys
        /// <summary>
        /// شناسه برچسب
        /// </summary>
        [Display(Name = "شناسه برچسب")]
        public int BlogTagId { get; set; }
        /// <summary>
        /// شناسه برچسب
        /// </summary>
        [Display(Name = "شناسه برچسب")]
        [ForeignKey("BlogTagId")]
        public BlogTag BlogTag { get; set; }
        /// <summary>
        /// شناسه مطلب
        /// </summary>
        [Display(Name = "شناسه مطلب")]
        public int BlogArticleId { get; set; }
        /// <summary>
        /// شناسه مطلب
        /// </summary>
        [Display(Name = "شناسه مطلب")]
        [ForeignKey("BlogArticleId")]
        public BlogArticle BlogArticle { get; set; }
        #endregion
    }
}
