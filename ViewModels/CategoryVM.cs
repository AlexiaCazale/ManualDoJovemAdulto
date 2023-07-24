using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manualdojovem.Models;

namespace manualdojovem.ViewModels
{
    public class CategoryVM
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Article> Articles { get; set; } = new List<Article>();
        
    }
}