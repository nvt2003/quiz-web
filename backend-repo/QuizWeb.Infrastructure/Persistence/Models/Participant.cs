using System;
using System.Collections.Generic;

namespace QuizWeb.Infrastructure.Persistence.Models;

public partial class Participant
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public int UserId { get; set; }

    public double? Score { get; set; }

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
