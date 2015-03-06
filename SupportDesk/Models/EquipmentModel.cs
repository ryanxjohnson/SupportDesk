using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(EquipmentMetadata))]
    public partial class Equipment : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(EquipmentType.Manufacturer + " " + EquipmentType.ModelNumber + " " + SerialNumber, ID, s_id);
        }

        public override String getController() { return "Equipments"; }

        public override String getAction() { return "RemoveEquipment"; }

        public override long? getID() { return ID; }

        public Assignment AssignTask { get; set; }
        public Assignment AssignSoftwareItem { get; set; }
        public List<SelectListItem> chooseDepartment_ID { get; set; }
        public List<SelectListItem> chooseAssignedTo_Employee_ID { get; set; }
        public List<SelectListItem> chooseType_ID { get; set; }
    }

    public class EquipmentMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        [DisplayName("Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        [DisplayName("Warrenty End Date")]
        public System.DateTime War_end_date { get; set; }

        public string Notes { get; set; }

        public string Status { get; set; }

        [Required]
        [DisplayName("Owning Department")]
        public long Department_ID { get; set; }

        [Required]
        [DisplayName("Type")]
        public long Type_ID { get; set; }

        [DisplayName("Currently Assigned to")]
        public Nullable<long> AssignedTo_Employee_ID { get; set; }

        [DisplayName("Date Deployed")]
        public Nullable<System.DateTime> Deployed { get; set; }

        [DisplayName("Date Returned")]
        public Nullable<System.DateTime> Returned { get; set; }
    }
}