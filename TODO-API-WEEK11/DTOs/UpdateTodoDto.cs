using System.ComponentModel.DataAnnotations;
namespace TODO_API_WEEK11.DTOs
{
    public class UpdateTodoDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        [RegularExpression("Low|Medium|High")]
        public string  Priority { get; set; } = "Medium";
    }
}
