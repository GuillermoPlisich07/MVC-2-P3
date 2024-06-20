using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MVC_2_P3.DTOs
{
    public class DTOLogin
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El email no tiene un formato válido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El email no tiene un formato válido")]
        [MaxLength(100)] // Longitud maxima de 100
        public string email {  get; set; }
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;¡!])[A-Za-z\d.,;¡!]+$", ErrorMessage = "La contraseña no cumple con los requisitos mínimos")]

        public string password { get; set; }
    }
}
