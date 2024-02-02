<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="My Settings" AutoEventWireup="true" Inherits="Account_MySettings" Codebehind="MySettings.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">My Settings</h3>
    <asp:Panel runat="server" ID="pnlWarning" CssClass="alert alert-block" ClientIDMode="Static">
        <strong>Note: </strong> The settings will be applied after your next Login.
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Startup Page"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat ="server" ID="lstModule">
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Idle Time(Minutes)"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtIMinutes" Required="true" ClientIDMode="Static" CssClass="input-small"></asp:TextBox>
        </div>
    </div>
    <p>
        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Apply" OnClick="saveInfo"/>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
</asp:Content>
