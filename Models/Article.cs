using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace manualdojovem.Models
{
    [Table("Article")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o Nome")]
        [StringLength(60, ErrorMessage = "O Nome deve possuir no máximo 60 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(2500, ErrorMessage = "O Artigo deve possuir no máximo 2500 caracteres")]
        public string Description { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe a Categoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Display(Name = "Foto")]
        [StringLength(200)]
        public string Image { get; set; }
    }
}