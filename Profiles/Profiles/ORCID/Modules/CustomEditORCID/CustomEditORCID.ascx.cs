/*  
 
    Copyright (c) 2008-2012 by the President and Fellows of Harvard College. All rights reserved.  
    Profiles Research Networking Software was developed under the supervision of Griffin M Weber, MD, PhD.,
    and Harvard Catalyst: The Harvard Clinical and Translational Science Center, with support from the 
    National Center for Research Resources and Harvard University.


    Code licensed under a BSD License. 
    For details, see: LICENSE.txt 
  
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

using Profiles.Framework.Utilities;
using Profiles.Profile.Utilities;
using Profiles.Edit.Utilities;


namespace Profiles.ORCID.Modules.CustomEditORCID
{
    public partial class CustomEditORCID : BaseModule
    {

        Edit.Utilities.DataIO data;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session["pnlInsertAward.Visible"] = null;
        }

        public CustomEditORCID() : base() { }
        public CustomEditORCID(XmlDocument pagedata, List<ModuleParams> moduleparams, XmlNamespaceManager pagenamespaces)
            : base(pagedata, moduleparams, pagenamespaces)
        {
            SessionManagement sm = new SessionManagement();
            this.XMLData = pagedata;

            data = new Edit.Utilities.DataIO();
            Profiles.Profile.Utilities.DataIO propdata = new Profiles.Profile.Utilities.DataIO();

            if (Request.QueryString["subject"] != null)
                this.SubjectID = Convert.ToInt64(Request.QueryString["subject"]);
            else if (base.GetRawQueryStringItem("subject") != null)
                this.SubjectID = Convert.ToInt64(base.GetRawQueryStringItem("subject"));
            else
                Response.Redirect("~/search");

            string predicateuri = Request.QueryString["predicateuri"].Replace("!", "#");
            this.PropertyListXML = propdata.GetPropertyList(this.BaseData, base.PresentationXML, predicateuri, false, true, false);

            this.PredicateID = data.GetStoreNode(predicateuri);

            base.GetNetworkProfile(this.SubjectID, this.PredicateID);

            litBackLink.Text = "<a href='" + Root.Domain + "/edit/" + this.SubjectID.ToString() + "'>Edit Menu</a> &gt; <b>" + PropertyListXML.SelectSingleNode("PropertyList/PropertyGroup/Property/@Label").Value + "</b>";


            securityOptions.Subject = this.SubjectID;
            securityOptions.PredicateURI = predicateuri;
            securityOptions.PrivacyCode = Convert.ToInt32(this.PropertyListXML.SelectSingleNode("PropertyList/PropertyGroup/Property/@ViewSecurityGroup").Value);
            securityOptions.SecurityGroups = new XmlDataDocument();
            securityOptions.SecurityGroups.LoadXml(base.PresentationXML.DocumentElement.LastChild.OuterXml);

            securityOptions.BubbleClick += SecurityDisplayed;
            string orcidHelpLink = string.Empty;
            string orcidInfoSite = Profiles.ORCID.Utilities.config.InfoSite;
            if (!string.IsNullOrEmpty(orcidInfoSite))
            {
                orcidHelpLink = "&nbsp;<a style='border: none;' href='" + orcidInfoSite + "' target='_blank'><img style='border-style: none' src='" + Root.Domain + "/Framework/Images/info.png'  border='0'></a>";
            }
            litCreateProvideORCID.Text = "<img src='" + Root.Domain + "/framework/images/icon_squareArrow.gif' border='0'/>&nbsp;<a href='" + Root.Domain + "/ORCID/CreateMyORCID.aspx'>Create My ORCID</a>" + orcidHelpLink + "<br><img src='" + Root.Domain + "/framework/images/icon_squareArrow.gif' border='0'/>&nbsp;<a href='" + Root.Domain + "/ORCID/ProvideORCID.aspx'>Provide My ORCID</a>" + orcidHelpLink;
            litUploatInfoToORCID.Text = "<img src='" + Root.Domain + "/framework/images/icon_squareArrow.gif' border='0'/>&nbsp;<a href='" + Root.Domain + "/ORCID/UploadInfoToORCID.aspx'>Upload Info To ORCID</a>";
                                string loggedInInternalUsername = new Profiles.ORCID.Utilities.DataIO().GetInternalUserID();
                    Profiles.ORCID.Utilities.ProfilesRNSDLL.BO.ORCID.Person person = new Profiles.ORCID.Utilities.ProfilesRNSDLL.BLL.ORCID.Person().GetByInternalUsername(loggedInInternalUsername);

                    if (!person.Exists || person.ORCIDIsNull)
                    {
                        litCreateProvideORCID.Visible = true;
                        litUploatInfoToORCID.Visible = false;
                        orcidtable.Visible = false;
                    }
                    else
                    {
                        litORCIDID.Text = "<a href='" + Profiles.ORCID.Utilities.config.ORCID_URL + "/" + person.ORCID + "'>" + person.ORCID + "</a>";
                        litCreateProvideORCID.Visible = false;
                        litUploatInfoToORCID.Visible = true;
                        orcidtable.Visible = true;
                    }
        }

        #region Awards

        private void SecurityDisplayed(object sender, EventArgs e)
        {
        }

 
        #endregion

        private Int64 SubjectID { get; set; }
        private Int64 PredicateID { get; set; }
        private XmlDocument XMLData { get; set; }
        private XmlDocument PropertyListXML { get; set; }




    }
}