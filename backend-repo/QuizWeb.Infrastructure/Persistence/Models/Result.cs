using System;
using System.Collections.Generic;

namespace QuizWeb.Infrastructure.Persistence.Models;

public partial class Result
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public bool? IsTrue { get; set; }

    public DateTime AttemptTime { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
