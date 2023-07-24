using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace manualdojovem.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o Nome")]
        [StringLength(30, ErrorMessage = 
        "Máximo de 30 caracteres")]
        public string Name { get; set; }
        
    }
}