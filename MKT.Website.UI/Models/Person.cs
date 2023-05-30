using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MKT.Website.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("الاسم*")]
        [Required(ErrorMessage = "يرجى كتابة الاسم")]
        [MinLength(3, ErrorMessage = "يرجى كتابة الاسم الكامل")]
        [MaxLength(50, ErrorMessage = "يرجى التحقق من الاسم")]
        public string? PersonName { get; set; }

        [DisplayName("الهاتف*")]
        [Required(ErrorMessage = "يرجى كتابة الهاتف")]
        [MinLength(6, ErrorMessage = "يرجى كتابة رقم الهاتف بشكل صحيح")]
        [MaxLength(12, ErrorMessage = "يرجى التحقق من الرقم")]
        [Phone(ErrorMessage = "يرجى كتابة رقم الهاتف بشكل صحيح")]
        public string? PersonPhone { get; set; }

        [DisplayName("البريد الإلكتروني*")]
        [Required(ErrorMessage = "يرجى كتابة البريد الإلكتروني")]
        [MinLength(5, ErrorMessage = "يرجى كتابة بريدك الإلكتروني بشكل صحيح")]
        [MaxLength(50, ErrorMessage = "تحقق من بريدك الإلكتروني")]
        [EmailAddress(ErrorMessage = "يرجى ادخال بريد إلكتروني صحيح")]
        public string? PersonEmail { get; set; }

        [DisplayName("الإشتراك برسائل البريد الإلكتروني")]
        public string? PersonSubscription { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


    }
}

