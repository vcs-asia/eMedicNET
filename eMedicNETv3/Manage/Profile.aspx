<%@ Page Title="Profile" MasterPageFile="~/eMedicNET.master" Language="C#" AutoEventWireup="true" Inherits="Manage_Profile" Codebehind="Profile.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $('#txtCompany').change(function () {
            $('#txtOutlet').val($('#txtCompany').val());
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Profile</h4>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false" ClientIDMode="Static">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved.
    </asp:Panel>
    <p>
        <asp:Button runat="server" ID="btnSaveTop" CssClass="btn btn-success" Text="Save" OnCommand="SaveInfo"/>
    </p>
    <div class="well">
        <h5>Profile</h5>
        <div class="control-group">
            <label class="control-label" for="txtName">Name</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtName" CssClass="input-xxlarge" Required="true" ClientIDMode="Static" oninvalid="setCustomValidity('Please enter the Patient Name')"></asp:TextBox>
                <asp:HiddenField runat="server" ID="hdnPatID" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtRegNo">Regn. No.</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtRegNo" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtAdd1">Address</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAdd1" CssClass="input-xxlarge" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtAdd2">&nbsp;</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAdd2" CssClass="input-xxlarge" Required="true"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtAdd3">&nbsp;</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAdd3" CssClass="input-xxlarge" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtPhone">Phone</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtPhone" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtFax">Fax</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtFax" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtWeb">Web</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtWeb" CssClass="input-xxlarge" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtEmail">Email</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="input-xxlarge" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
    </div>
    <p>
        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Save" OnCommand="SaveInfo"/>
    </p>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>
