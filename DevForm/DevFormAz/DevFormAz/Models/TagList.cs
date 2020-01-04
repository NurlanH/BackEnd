﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class TagList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DailyUseCount { get; set; }
        public int MonthlyUseCount { get; set; }
        public int FormId { get; set; }
    }
}