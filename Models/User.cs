using System;
using System.Collections.Generic;

namespace inventory_manager.Models;

public partial class User
{
    public int? Id { get; set; }

    public string? Pass { get; set; }

    public string? Email { get; set; }
}
