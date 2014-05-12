<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomEditORCID.ascx.cs"
    Inherits="Profiles.ORCID.Modules.CustomEditORCID.CustomEditORCID" %>
<%@ Register TagName="Options" TagPrefix="security" Src="~/Edit/Modules/SecurityOptions/SecurityOptions.ascx" %>
<asp:UpdatePanel ID="upnlEditSection" runat="server" UpdateMode="Conditional">
    <ContentTemplate>   
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100px; width: 100px; top: 0;
                    right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                    <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF;
                        font-size: 25px; left: 40%; top: 40%;"><img alt="Loading..." src="../edit/images/loader.gif" /></span>
                </div>
            </ProgressTemplate>                        
        </asp:UpdateProgress>
        <asp:HiddenField ID="hiddenSubjectID" runat="server" />
        
        <table id="tblEditORCID" width="100%">
            <tr>
                <td>
                    <asp:Literal runat="server" ID="litBackLink"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="padding: 10px 0px;">
                        <asp:Panel runat="server" ID="pnlSecurityOptions">
                            <security:Options runat="server" ID="securityOptions"></security:Options>
                        </asp:Panel>
                        <br />
                        <asp:Panel runat="server" ID="pnlAddORCID">
                            <asp:Literal runat="server" ID="litCreateProvideORCID"></asp:Literal>   
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlEditORCID">
                            <asp:Literal runat="server" ID="litUploatInfoToORCID"></asp:Literal>   
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
