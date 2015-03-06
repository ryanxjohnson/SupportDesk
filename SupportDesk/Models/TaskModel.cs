using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    [MetadataType(typeof(TaskMetadata))]
    public partial class Task : HasRoute, HasID, Selectable
    {
        public SelectListItem toSelectListItem(long? s_id)
        {
            return buildSelectListItem(Summary.Substring(0, Summary.Length < 20 ? Summary.Length : 20), ID, s_id);
        }

        public override String getController() { return "Tasks"; }

        public override String getAction() { return "RemoveTask"; }

        public override long? getID() { return ID; }

        public Assignment AssignEmployee { get; set; }
        public Assignment AssignEquipment { get; set; }
        public List<SelectListItem> chooseSubmitter_Employee_ID { get; set; }
        public List<SelectListItem> chooseAssigned_Group_ID { get; set; }
    }

    public class TaskMetadata
    {
        //[Required
        //public long ID { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Summary { get; set; }

        [DisplayName("Date Due")]
        public Nullable<System.DateTime> Due { get; set; }

        [Required]
        public string Priority { get; set; }

        //[Required]
        //public System.DateTime Submitted { get; set; }

        public string Notes { get; set; }

        [DisplayName("Submitter")]
        public Nullable<long> Submitter_Employee_ID { get; set; }

        [DisplayName("Assigned Group")]
        public Nullable<long> Assigned_Group_ID { get; set; }
    }
}