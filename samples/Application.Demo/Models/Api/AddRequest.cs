
using System.ComponentModel.DataAnnotations;

namespace Applicaion.Demo.Models
{
    public class AddRequest
    {
        [Required]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
