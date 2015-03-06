using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(Name, ID, s_id);
        }

        public override String getController() { return "Departments"; }

        public override String getAction() { return "RemoveDepartment"; }

        public override long? getID() { return ID; }
    }

    public class DepartmentMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZIP { get; set; }

        [DisplayName("Floor/Suite")]
        public string Floor_Suite { get; set; }
    }
}