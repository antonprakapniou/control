﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class StatusVM
	{
		public Guid StatusId { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }
	}
}
