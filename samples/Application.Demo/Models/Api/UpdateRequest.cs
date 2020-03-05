using System.ComponentModel.DataAnnotations;

namespace Applicaion.Demo.Models
{
    public class UpdateRequest

    {
		[Required]
		public string Key { get; set; }

		public string Value { get; set; }

	}
}
