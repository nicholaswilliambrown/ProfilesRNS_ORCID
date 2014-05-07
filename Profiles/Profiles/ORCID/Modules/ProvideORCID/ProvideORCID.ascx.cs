﻿/*  
 
    Copyright (c) 2008-2012 by the President and Fellows of Harvard College. All rights reserved.  
    Profiles Research Networking Software was developed under the supervision of Griffin M Weber, MD, PhD.,
    and Harvard Catalyst: The Harvard Clinical and Translational Science Center, with support from the 
    National Center for Research Resources and Harvard University.


    Code licensed under a BSD License. 
    For details, see: LICENSE.txt 
  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using Profiles.Login.Utilities;
using Profiles.Framework.Utilities;
using Profiles.ORNG.Utilities;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml.Xsl;

using Profiles.Profile.Utilities;

namespace Profiles.ORCID.Modules.ProvideORCID
{
    public partial class ProvideORCID : ORCIDBaseModule
    {
        public ProvideORCID() {
            base.RDFTriple = new RDFTriple(Convert.ToInt64(sm.Session().NodeID.ToString()));             
        }
        //public ProvideORCID(XmlDocument pagedata, List<ModuleParams> moduleparams, XmlNamespaceManager pagenamespaces)
        //    : base(pagedata, moduleparams, pagenamespaces)
        //{
        //    base.RDFTriple = new RDFTriple(Convert.ToInt64(sm.Session().NodeID.ToString()));
        //}
        public override Label Errors
        {
            get { return this.lblErrors; }
        }
        public void Initialize(XmlDocument basedata, XmlNamespaceManager namespaces, RDFTriple rdftriple)
        {
            BaseData = basedata;
            Namespaces = namespaces;
            RDFTriple = rdftriple;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProfilesRNSDLL.BO.ORCID.Person person = GetPerson();
                divORCIDAlreadyProvided.Visible = person.Exists && !person.ORCIDIsNull;
                divProvideORCID.Visible = !divORCIDAlreadyProvided.Visible;
                //lblOrganizationName.Text = Profiles.ORCID.Utilities.config.OrganizationName;
                //lblOrganizationName2.Text = Profiles.ORCID.Utilities.config.OrganizationName;
                this.lblButtonLabel.Text = this.btnLoginToORCID.Text;
            }
        }
        protected void btnLoginToORCID_Click(object sender, EventArgs e)
        {
            // Have the user log into ORCID and Authenticate.  Upon return from ORCID, redirect to ProvideORCIDConfirmation.aspx.   
            // If successful, ProvideORCIDConfirmation.aspx should have a query string parameter named code, that will allow us to get the ORCID ID.
            ProfilesRNSDLL.BO.ORCID.REFPermission refPermission = new ProfilesRNSDLL.BLL.ORCID.REFPermission().Get((int)ProfilesRNSDLL.BO.ORCID.REFPermission.REFPermissions.authenticate);
            string orcidLoginPath = ProfilesRNSDLL.BLL.ORCID.OAuth.GetUserPermissionURL(refPermission.PermissionScope, "ProvideORCIDConfirmation.aspx");
            Response.Redirect(orcidLoginPath, true);
        }
    }
}