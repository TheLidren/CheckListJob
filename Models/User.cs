using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckListJob.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        [RegularExpression(@"^([А-ЯЁ]{1}[а-яё]{1,50})$", ErrorMessage = "Текст должен содержать только латинские буквы и нижний регистр")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [RegularExpression(@"^([А-ЯЁ]{1}[а-яё]{1,50})$", ErrorMessage = "Текст должен содержать только латинские буквы и нижний регистр")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [RegularExpression(@"^([А-ЯЁ]{1}[а-яё]{1,50})$", ErrorMessage = "Текст должен содержать только латинские буквы и нижний регистр")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Не указан email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]{1,50}$", ErrorMessage = "Некорректный адрес")]
        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(255, ErrorMessage = "Пароль должен содержать как от 8 символов", MinimumLength = 8)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public bool Status { get; set; }

        public ICollection<ListLog>? ListLogs { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
