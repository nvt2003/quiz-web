using System;
using System.Collections.Generic;

namespace QuizWeb.Infrastructure.Persistence.Models;

public partial class Topic
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public virtual User User { get; set; } = null!;
}
