using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manualdojovem.Models;

namespace manualdojovem.ViewModels
{
    public class ArticleVM
    {
        public List<Article> Articles { get; set; } = new List<Article>();
        public Category Category { get; set; }
    }
}