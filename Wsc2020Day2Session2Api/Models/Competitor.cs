using System;
using System.Collections.Generic;

namespace Wsc2020Day2Session2Api.Models;

public partial class Competitor
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
}
