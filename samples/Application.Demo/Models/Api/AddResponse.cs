using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;

namespace Applicaion.Demo.Models
{
    public class AddResponse

    {
        public KeyValueDto Item { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
