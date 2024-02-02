<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Appointments_Contacts" Codebehind="Contacts.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Contacts</h4>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>Oh snap!</strong> Error saving. Change a few things up and try submitting again.
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <div class="control-group">
        <label class="control-label" for="txtName">Name</label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtName" placeholder="Dato. Dr. Subra" required="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtHP">H/P No.</label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtHP" placeholder="0123456789" required="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtEmail">Email</label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtEmail" placeholder="someone@domain.com"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="lstGroup">Group</label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstGroup">
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Button runat="server" CssClass="btn btn-success" Text="Save" OnCommand="saveInfo" />
    </div>
    <asp:GridView runat="server" ID="Lst" DataKeyNames="contact_id" AutoGenerateColumns="false" GridLines="Horizontal" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnSorting="Sorting" AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="contact_name" SortExpression="contact_name" ItemStyle-Width="20%" HeaderText="Contact Name" />
            <asp:BoundField DataField="contact_hp" SortExpression="contact_hp" ItemStyle-Width="10%" HeaderText="Handphone" />
            <asp:BoundField DataField="contact_email" SortExpression="contact_email" ItemStyle-Width="20%" HeaderText="Email" />
            <asp:BoundField DataField="group_name" SortExpression="group_name" ItemStyle-Width="20%" HeaderText="Group Name" />
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server"></asp:Content>
