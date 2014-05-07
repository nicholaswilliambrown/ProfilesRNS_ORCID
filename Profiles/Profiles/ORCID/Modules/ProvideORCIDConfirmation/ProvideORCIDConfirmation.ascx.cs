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

namespace Profiles.ORCID.Modules.ProvideORCIDConfirmation
{
    public partial class ProvideORCIDConfirmation : ORCIDBaseModule
    {
        public override Label Errors
        {
            get { return lblErrors; }
        }
        public void Initialize(XmlDocument basedata, XmlNamespaceManager namespaces, RDFTriple rdftriple)
        {
            BaseData = basedata;
            Namespaces = namespaces;
            RDFTriple = rdftriple;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Utilities.ProfilesRNSDLL.BO.ORCID.Person person = GetPerson();
                    //LoadPageLabels(person);

                    if (Profiles.ORCID.Utilities.DataIO.AssociateORCIDWithOrganizationID(LoggedInInternalUsername, Utilities.ProfilesRNSDLL.BLL.ORCID.OAuth.GetORCID(OAuthCode, "ProvideORCIDConfirmation.aspx", LoggedInInternalUsername)))
                    {
                        pSuccess.Visible = true;
                        //LoadPageLabels(person);
                    }
                    else
                    {
                        pSuccess.Visible = false;
                        lblErrors.Text = "An error occurred while associating your ORCID with your local identifier";
                    }
                    Int64 subjectID = new Utilities.ProfilesRNSDLL.BLL.Profile.Data.Person().GetNodeId(person.InternalUsername);
                    pHasProfile.Visible = !subjectID.Equals(0);
                    hlProfile.NavigateUrl = "~/display/" + subjectID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
//        private void LoadPageLabels(ProfilesRNSDLL.BO.ORCID.Person person)
//        {
            //lblOrganizationName.Text = Profiles.ORCID.Utilities.config.OrganizationName;
//        }
        private bool AssociateORCIDWithOrganizationID(Utilities.ProfilesRNSDLL.BO.ORCID.Person person, string orcid)
        {
            person.ORCID = orcid;
            person.PersonStatusTypeID = (int)Utilities.ProfilesRNSDLL.BO.ORCID.REFPersonStatusType.REFPersonStatusTypes.ORCID_Provided;
            person.ORCIDRecorded = DateTime.Now;
            return PersonBLL.Save(person);
        }
    }
}