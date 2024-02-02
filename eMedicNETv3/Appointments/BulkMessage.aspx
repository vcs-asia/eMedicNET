<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Appointments_BulkMessage" Codebehind="BulkMessage.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>SMS - Group Sending</h4>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>Oh snap!</strong> Error saving. Change a few things up and try submitting again.
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <div class="control-group">
        <asp:TextBox runat="server" ID="txtSMS" placeholder="Enter the SMS templete to be sent." required="true" TextMode="MultiLine" Rows="5" Width="50%" ClientIDMode="Static"></asp:TextBox>
        <label>1 to 160 characters = 1 SMS. The 8 special characters {}[]|\~^ of each calculated as 2 characters.</label>
        <asp:TextBox runat="server" ID="txtCount" ClientIDMode="Static" Enabled="false" Text="0 Characters"></asp:TextBox>
    </div>
    <div class="cotrol-group">
        <asp:DropDownList runat="server" ID="lstGroup">
        </asp:DropDownList>
    </div>
    <br />
    <div class="control-group">
        <asp:Button runat="server" CssClass="btn btn-success" Text="Assign SMS to Group" OnCommand="AssignSMS"/>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $("#txtSMS").live("keyup", function () {
            $("#txtCount").val($(this).val().length + " Characters");
        });
    </script>
</asp:Content>

