<%@ Page Language="C#" Title="PO Noted" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_PONotes" Codebehind="PONotes.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-warning" Visible="false">
        Oh!.. Error
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success" Visible="false">
        OK.Success
        <asp:Label runat="server" ID="Label1"></asp:Label>
    </asp:Panel>
    <div class="control-group">
        <label class="control-label">PO Notes</label>
        <div class="controls">
            <asp:TextBox runat="server" ID="templateEditor" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hCharges" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hType" ClientIDMode="Static"/>
    <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save"/>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(document).ready(function () { $("#templateEditor").cleditor(); });
    </script>
</asp:Content>