using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{   
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public bool IsPublish { get; set; }
        public Sort Sort { get; set; }
        public int IdofSort { get; set; }
    }
}
