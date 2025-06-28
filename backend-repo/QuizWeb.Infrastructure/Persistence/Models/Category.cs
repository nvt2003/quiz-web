﻿using System;
using System.Collections.Generic;

namespace QuizWeb.Infrastructure.Persistence.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
