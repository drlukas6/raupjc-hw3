using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hw3webapp
{
    public class TodoViewModel
    {
        [Required]
        public string Label { get; set; }

        [Required]
        public string Category { get; set; }

    }
}
