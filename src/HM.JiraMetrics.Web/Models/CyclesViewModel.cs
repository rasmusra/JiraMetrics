using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM.JiraMetrics.Web.Models
{
    public class CyclesViewModel
    {
        public IEnumerable<string> PreCycleStatuses { get; set; }

        public IEnumerable<string> CycleStatuses { get; set; }

        public IEnumerable<string> PostCycleStatuses { get; set; }

        public IEnumerable<SelectListItem> GetListItems(IEnumerable<string> texts)
        {
            return texts.Select(text => new SelectListItem { Text = text });
        }
    }
}