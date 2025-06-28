using System;
using System.Collections.Generic;

namespace QuizWeb.Infrastructure.Persistence.Models;

public partial class Test
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TopicId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Topic Topic { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
