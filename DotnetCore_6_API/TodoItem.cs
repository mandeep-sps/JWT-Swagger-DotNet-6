using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetCore_6_API
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [DefaultValue(false)]
        public bool IsComplete { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null;
    }
}
