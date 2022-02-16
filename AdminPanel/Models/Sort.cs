using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class Sort
    {
        [Key]
        public int SortId { get; set; }
        public string PageName { get; set; }
    }
}
