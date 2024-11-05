﻿using System;
using System.Collections.Generic;

namespace Wsc2020Day2Session2Api.Models;

public partial class CheckIn
{
    public int Id { get; set; }

    public string CompetitorId { get; set; } = null!;

    public virtual User Competitor { get; set; } = null!;
}
