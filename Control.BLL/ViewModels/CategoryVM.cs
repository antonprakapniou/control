﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Control.BLL.ViewModels
{
    public sealed class CategoryVM:BaseViewModel
    {

        [Required]
        [DisplayName("Category")]
        public string? Name { get; set; }
    }
}
