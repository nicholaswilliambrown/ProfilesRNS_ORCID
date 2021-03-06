﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvideORCIDConfirmation.ascx.cs"
    Inherits="Profiles.ORCID.Modules.ProvideORCIDConfirmation.ProvideORCIDConfirmation" %>
<asp:Label ID="lblErrors" runat="server" EnableViewState="false" CssClass="uierror" />
<h3>
    Profiles &harr; ORCID Synchronization</h3>
<p id="pSuccess" runat="server" visible="false">
    Thank you, your ORCID has been associated with your user account.
</p>
<p id="pHasProfile" runat="server" visible="false">
    Please <a href="UploadInfoToORCID.aspx">click here</a> if you would like to upload
    data from your
    <asp:HyperLink ID="hlProfile" runat="server" Text="BU Profile" Target="_blank" />
    to ORCID.
</p>
