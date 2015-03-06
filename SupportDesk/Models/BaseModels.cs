using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportDesk.Models
{
    public interface HasID
    {
        long ID { get; set; }
    }

    public interface Selectable
    {
        SelectListItem toSelectListItem(long? s_id);
    }

    public abstract class HasRoute
    {
        public abstract String getController();
        public abstract String getAction();
        public abstract long? getID();

        public String S_ID { get; set; }

        protected SelectListItem buildSelectListItem(String text, long id, long? s_id)
        {
            return new SelectListItem()
            {
                Text = text,
                Value = id.ToString(),
                Selected = id.Equals(s_id)
            };
        }
    }

    public class Assignment
    {
        public String Controller { get; set; }
        public String Action { get; set; }
        public long? ID { get; set; }
        public String S_ID { get; set; }
        public List<SelectListItem> selectList { get; set; }
    }
}