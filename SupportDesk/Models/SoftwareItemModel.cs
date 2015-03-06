using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(SoftwareItemMetadata))]
    public partial class SoftwareItem : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(Manufacturer + " " + Name, ID, s_id);
        }

        public override String getController() { return "SoftwareItems"; }

        public override String getAction() { return "RemoveSoftwareItem"; }

        public override long? getID() { return ID; }

        public Assignment AssignEquipment { get; set; }
    }

    public class SoftwareItemMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Manufacturer { get; set; }
    }
}