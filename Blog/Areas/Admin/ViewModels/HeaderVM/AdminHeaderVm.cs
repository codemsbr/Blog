using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.ViewModels.HeaderVM
{
    public class AdminHeaderVm
    {
        public int Id { get; set; }
        [MinLength(5), MaxLength(25)]
        public string Text { get; set; }
        [MinLength(5), MaxLength(60)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
