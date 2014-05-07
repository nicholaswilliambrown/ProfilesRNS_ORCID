﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateMyORCID.ascx.cs"
    Inherits="Profiles.ORCID.Modules.CreateMyORCID.CreateMyORCID" %>
<%@ Register Src="~/ORCID/Modules/UploadInfoToORCID/UploadInfoToORCID.ascx" TagName="UploadInfoToORCID"
    TagPrefix="uc1" %>
<asp:Label ID="lblErrors" runat="server" EnableViewState="false" CssClass="uierror" />
<h3>
    Create My ORCID</h3>
<div id="divEntryForm" runat="server">
    <h3>
        Please complete the form below to create your ORCID.
    </h3>
    <div class="bumc">
        <div class="directions">
            <span class="uierror">Note: </span>For more information on the name fields below
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://support.orcid.org/knowledgebase/articles/142948-names-in-the-orcid-registry"
                Target="_blank">click here</asp:HyperLink>
            (opens in new window)
        </div>
        <table class="data">
            <tr>
                <td class="uierror">
                    *
                </td>
                <td>
                    First Name
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" Width="166px" MaxLength="150" />
                </td>
                <td class="style7">
                    Your given name, or the name you most commonly go by. If you only have one name
                    you should record it in the First Name field. This is the only required name field
                    and is limited to 150 characters.
                </td>
                <td>
                    <asp:Label ID="lblFirstNameErrors" runat="server" EnableViewState="false" CssClass="uierror" />
                </td>
            </tr>
            <tr>
                <td class="uierror">
                    *
                </td>
                <td>
                    Last Name
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" Width="165px" MaxLength="150" />
                </td>
                <td class="style7">
                    Your family name or surname. This field is limited to 150 characters.
                </td>
                <td>
                    <asp:Label ID="lblLastNameErrors" runat="server" EnableViewState="false" CssClass="uierror" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="nowrap">
                    Published Name:
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtPublishedName" runat="server" Width="165px" />
                </td>
                <td>
                    How you prefer your name to appear when credited. This is also the name that appears
                    at the top of your ORCID profile.
                </td>
                <td>
                    <asp:Label ID="lblPublishedNameErrors" runat="server" EnableViewState="false" CssClass="uierror" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="nowrap">
                    Other Names:
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtOtherNames" runat="server" TextMode="MultiLine" CssClass="multilinesmall"
                        Height="61px" />
                </td>
                <td>
                    Additional names you may be known by, such as with an abbreviated first name, maiden
                    name, or name including a suffix or title. For each other name, record the complete
                    name in the order it typically appears. You can add as many other names as needed,
                    (one per line).
                </td>
                <td>
                    <asp:Label ID="lblOtherNames" runat="server" EnableViewState="false" CssClass="uierror" />
                </td>
            </tr>
        </table>
        <br />
        <div class="directions">
            <span class="uierror">Note: </span>
            <asp:Label ID="lblOrgEmailRequired" runat="server" />
        </div>
        <table class="data">
            <tr>
                <td class="uierror">
                    *
                </td>
                <td>
                    Primary Email
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmailDecisionID" runat="server" Width="75px">
                        <asp:ListItem Value="2" Selected="True">Public</asp:ListItem>
                        <asp:ListItem Value="3">Limited</asp:ListItem>
                        <asp:ListItem Value="5">Private</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtEmailAddress" runat="server" Width="320px" />
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblEmailAddressErrors" runat="server" EnableViewState="false" CssClass="uierror" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Alternate Email(s)
                </td>
                <td>
                    <asp:DropDownList ID="ddlAlternateEmailDecisionID" runat="server" Width="75px">
                        <asp:ListItem Value="1" Selected="True">Public</asp:ListItem>
                        <asp:ListItem Value="2">Limited</asp:ListItem>
                        <asp:ListItem Value="3">Private</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtAlternateEmail" runat="server" Height="61px" TextMode="MultiLine"
                        CssClass="multilinesmall" />
                </td>
                <td>
                    (one per line)
                </td>
                <td>
                    <asp:Label ID="lblAlternateEmailDecisionIDErrors" runat="server" EnableViewState="false"
                        CssClass="uierror" />
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="vertical-align: top;"><asp:CheckBox ID="chkUploadInfoNow" runat="server" AutoPostBack="true" OnCheckedChanged="chkUploadInfoNow_CheckedChanged"
                Checked="false" /></td>
                <td>
                            I would like to select Profiles data to send to ORCID now (you can do this at a
            later time by using the 'Upload Info To ORCID' link which will appear after you create an ORCID.)

                </td>
            </tr>
        </table>
        <p>
            
            
        </p>
        <uc1:UploadInfoToORCID ID="UploadInfoToORCID1" runat="server" Visible="false" />
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblErrorsCreate" runat="server" EnableViewState="false" CssClass="uierror" />
                <p id="pAcknowledge" runat="server">
                    <asp:CheckBox ID="chkIAgree" runat="server" AutoPostBack="true" OnCheckedChanged="chkIAgree_CheckedChanged" />
                    I acknowledge that I have read and agree with the
                    <asp:HyperLink ID="hlORCIDAckAndConsent" runat="server" Target="_blank"></asp:HyperLink>.
                </p>
                <asp:Button ID="btnNewORCID" runat="server" OnClick="btnNewORCID_Click" Text="Create ORCID"
                    Enabled="false" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNewORCID" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
