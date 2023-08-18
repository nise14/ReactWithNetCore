using System;
using System.Collections.Generic;

namespace ReactProject.Models;

public partial class Task
{
    public int IdTask { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }
}
