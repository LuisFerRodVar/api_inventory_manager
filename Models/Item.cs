using System;
using System.Collections.Generic;

namespace inventory_manager.Models;

public partial class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Price { get; set; }
}
