using System;
using System.Collections.Generic;

namespace QuizWeb.Infrastructure.Persistence.Models;

public partial class vw_UserTestResult
{
    public string DisplayName { get; set; } = null!;

    public int TestId { get; set; }

    public int TopicId { get; set; }

    public double? Score { get; set; }

    public int? TotalQuestions { get; set; }

    public int? CorrectAnswers { get; set; }
}
