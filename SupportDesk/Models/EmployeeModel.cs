using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(F_Name + " " + L_Name, ID, s_id);
        }

        public override String getController() { return "Employees"; }

        public override String getAction() { return "RemoveEmployee"; }

        public override long? getID() { return ID; }

        public Assignment AssignSupportGroup { get; set; }
        public Assignment AssignTask { get; set; }
        public List<SelectListItem> chooseDepartment_ID { get; set; }
    }

    public class EmployeeMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string F_Name { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string L_Name { get; set; }

        [Range(typeof(long), "0", "9999999999")]
        [DisplayName("Phone Number")]
        public Nullable<long> Phone { get; set; }

        [DisplayName("Office Number")]
        public string Office { get; set; }

        [DisplayName("Department")]
        public Nullable<long> Department_ID { get; set; }
    }
}