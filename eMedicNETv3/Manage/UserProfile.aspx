<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="My Profile" AutoEventWireup="true" Inherits="Account_UserProfile" Codebehind="UserProfile.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">User Profile</h3>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="User Name"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtUserName" Required="true" ClientIDMode="Static" CssClass="input-xxlarge" placeholder="NAME"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdnID" Value="0" />
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Login ID"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtLoginID" Required="true" ClientIDMode="Static" CssClass="input-xlarge" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Sex"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstSex" ClientIDMode="Static">
                <asp:ListItem Value="0">MALE</asp:ListItem>
                <asp:ListItem Value="1">FEMALE</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="User Type"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstUType" Readonly="true">
                <asp:ListItem Value="1">Administrator</asp:ListItem>
                <asp:ListItem Value="2">Doctor</asp:ListItem>
                <asp:ListItem Value="3">Cashier</asp:ListItem>
                <asp:ListItem Value="4">Pharmacy</asp:ListItem>
                <asp:ListItem Value="5">Reception</asp:ListItem>
                <asp:ListItem Value="6">Office</asp:ListItem>
                <asp:ListItem Value="7">Inventory</asp:ListItem>
                <asp:ListItem Value="8">Billing</asp:ListItem>
                <asp:ListItem Value="9">Patient</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Email"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtUEmail" Required="true" ClientIDMode="Static" CssClass="input-xxlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Doctor"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstDoctor">
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Status"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstStatus" ClientIDMode="Static" Readonly="true">
                <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                <asp:ListItem Value="0">IN-ACTIVE</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <p>
        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Save" OnClick="saveInfo"/>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
</asp:Content>
