using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(SupportGroupMetadata))]
    public partial class SupportGroup : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(Name, ID, s_id);
        }

        public override String getController() { return "SupportGroups"; }

        public override String getAction() { return "RemoveSupportGroup"; }

        public override long? getID() { return ID; }

        public Assignment AssignEmployee { get; set; }
        public List<SelectListItem> chooseOverseer_Employee_ID { get; set; }
    }

    public class SupportGroupMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Group Overseer")]
        public Nullable<long> Overseer_Employee_ID { get; set; }
    }
}