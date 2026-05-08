namespace TODO_API_WEEK11.DTOs
{
    public class TodoResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Priority { get; set; }
    }
}
