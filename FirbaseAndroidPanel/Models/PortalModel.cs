using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirbaseAndroidPanel.Models
{
    public class PortalModel
    {
        public string PortalName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string FirePortalName { get; set; }
    }

    public class CatSubCat
    {
        public string CatCode { get; set; }
        public string SubCatCode { get; set; }
    }
    public class ContentList
    {
        public string PortalCode { get; set; }
        public string CatCode { get; set; }
    }
}