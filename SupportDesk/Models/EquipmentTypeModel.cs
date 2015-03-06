using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(EquipmentTypeMetadata))]
    public partial class EquipmentType : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(Manufacturer + " " + ModelNumber, ID, s_id);
        }

        public override String getController() { return "EquipmentTypes"; }

        public override String getAction() { return "RemoveEquipmentUserOf"; }

        public override long? getID() { return ID; }

        public Assignment AssignEquipmentUserOf { get; set; }
    }

    public class EquipmentTypeMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [DisplayName("Model Number")]
        public string ModelNumber { get; set; }
    }
}