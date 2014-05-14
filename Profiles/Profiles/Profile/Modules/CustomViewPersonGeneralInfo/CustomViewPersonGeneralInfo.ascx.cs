using System;
using System.Collections.Generic;
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

namespace Profiles.Profile.Modules.CustomViewPersonGeneralInfo
{
    public partial class CustomViewPersonGeneralInfo : BaseModule
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DrawProfilesModule();
        }

        public CustomViewPersonGeneralInfo() : base() { }
        public CustomViewPersonGeneralInfo(XmlDocument pagedata, List<ModuleParams> moduleparams, XmlNamespaceManager pagenamespaces)
            : base(pagedata, moduleparams, pagenamespaces)
        {
            base.RDFTriple = new RDFTriple(Convert.ToInt64(Request.QueryString["Subject"]));

        }
        private void DrawProfilesModule()
        {

            XsltArgumentList args = new XsltArgumentList();
            XslCompiledTransform xslt = new XslCompiledTransform();
            SessionManagement sm = new SessionManagement();

            Utilities.DataIO data = new Profiles.Profile.Utilities.DataIO();
            string email = string.Empty;
            string imageemailurl = string.Empty;
            if (this.BaseData.SelectSingleNode("rdf:RDF[1]/rdf:Description[1]/prns:emailEncrypted", this.Namespaces) != null &&
                this.BaseData.SelectSingleNode("rdf:RDF[1]/rdf:Description[1]/vivo:email", this.Namespaces) == null)
            {
                email = this.BaseData.SelectSingleNode("rdf:RDF[1]/rdf:Description[1]/prns:emailEncrypted", this.Namespaces).InnerText;
                imageemailurl = string.Format(Root.Domain + "/profile/modules/CustomViewPersonGeneralInfo/" + "EmailHandler.ashx?msg={0}", HttpUtility.UrlEncode(email));
            }
            
            args.AddParam("root", "", Root.Domain);
            if (email != string.Empty)
            {
                args.AddParam("email", "", imageemailurl);
            }
            args.AddParam("imgguid", "", Guid.NewGuid().ToString());

            // Check for an ORCID
            string internalUsername = new Profiles.ORCID.Utilities.ProfilesRNSDLL.BLL.Profile.Data.Person().GetInternalUsername(Convert.ToInt64(Request.QueryString["Subject"]));
            Profiles.ORCID.Utilities.ProfilesRNSDLL.BO.ORCID.Person orcidPerson = new Profiles.ORCID.Utilities.ProfilesRNSDLL.BLL.ORCID.Person().GetByInternalUsername(internalUsername);
            if (orcidPerson.Exists && !orcidPerson.ORCIDIsNull)
            {
                args.AddParam("orcid", "", orcidPerson.ORCID);
                args.AddParam("orcidurl", "", Profiles.ORCID.Utilities.config.ORCID_URL + "/" + orcidPerson.ORCID);
                args.AddParam("orcidinfourl", "", Profiles.ORCID.Utilities.config.InfoSite);
                args.AddParam("orcidimage", "", Root.Domain + "/Framework/Images/orcid_16x16(1).gif");
                args.AddParam("orcidimageguid", "", Guid.NewGuid().ToString());
            }
            else if (Profiles.ORCID.Utilities.config.ShowNoORCIDMessage && Profiles.ORCID.Utilities.config.Enabled)
            {
                args.AddParam("orcid", "", "No ORCID id has been created for this user");
                args.AddParam("orcidinfosite", "", Profiles.ORCID.Utilities.config.InfoSite);
                //args.AddParam("orcidurl", "", Profiles.ORCID.Utilities.config.ORCID_URL + "/" + orcidPerson.ORCID);
                args.AddParam("orcidimage", "", Root.Domain + "/Framework/Images/orcid_16x16(1).gif");
                args.AddParam("orcidimageguid", "", Guid.NewGuid().ToString());
            }


            litPersonalInfo.Text = XslHelper.TransformInMemory(Server.MapPath("~/Profile/Modules/CustomViewPersonGeneralInfo/CustomViewPersonGeneralInfo.xslt"), args, base.BaseData.OuterXml);

            if (base.BaseData.SelectSingleNode("rdf:RDF/rdf:Description[1]/prns:mainImage/@rdf:resource", base.Namespaces) != null)
            {
                string imageurl = base.BaseData.SelectSingleNode("//rdf:RDF/rdf:Description[1]/prns:mainImage/@rdf:resource", base.Namespaces).Value;
                imgPhoto.ImageUrl = imageurl + "&cachekey=" + Guid.NewGuid().ToString();
            }
            else
            {
                imgPhoto.Visible = false;
            }



        }

    }
}