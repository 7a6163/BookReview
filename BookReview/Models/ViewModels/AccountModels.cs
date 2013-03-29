using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using BookReview.App_GlobalResources;
using Domain;

namespace BookReview.Models.ViewModels
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    //[Table("UserProfile")]
    //public class UserProfile
    //{
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //}

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "目前密碼")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認新密碼")]
        [Compare("NewPassword", ErrorMessage = "新密碼與確認密碼不相符。")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {   
        [Required]
        [Display(ResourceType = typeof(Resource), Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "請輸入電子郵件信箱")]
        [Display(ResourceType = typeof(Resource), Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$", ErrorMessage = "請輸入正確的電子郵件格式")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(100, ErrorMessage = "密碼長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請輸入驗證密碼")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "PasswordConfirm")]
        [Compare("Password", ErrorMessage = "密碼驗證不正確")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "請輸入名字")]
        [Display(ResourceType = typeof(Resource), Name = "Nickname")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入驗證碼")]
        [Display(Name = "驗證碼")]
        public string CheckCode { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    public class MemberInfomation
    {
        [Required]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "暱稱")]
        public string Name { get; set; }
    }
}
