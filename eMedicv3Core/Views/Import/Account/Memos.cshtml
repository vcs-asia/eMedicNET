<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Memos" AutoEventWireup="true" Inherits="Account_Memos" Codebehind="Memos.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Memos</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="USER_NAME">User Name</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <Button type="button" class="btn btn-inverse" onclick="javascript:openRecord(0)">New</Button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="MEMO_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="MEMO_DATE" SortExpression="MEMO_DATE" ItemStyle-Width="20%" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="MEMO_SUBJECT" SortExpression="MEMO_SUBJECT" ItemStyle-Width="28%" HeaderText="Subject" />
            <asp:BoundField DataField="MEMO_TUSER" SortExpression="MEMO_TUSER" ItemStyle-Width="10%" HeaderText="To" />
            <asp:BoundField DataField="MEMO_CUSER" SortExpression="MEMO_CUSER" ItemStyle-Width="20%" HeaderText="CC List" />
            <asp:BoundField DataField="MEMO_BUSER" SortExpression="MEMO_BUSER" ItemStyle-Width="20%" HeaderText="BCC List" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" ToolTip="Edit Memo" NavigateUrl='<%# String.Format("javascript:openRecord({0})",HttpUtility.UrlEncode(Eval("MEMO_ID").ToString())) %>'><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ToolTip="Delete Memo" CommandArgument="" CommandName="delete"><i class="icon-ban-circle"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function openRecord(_id) {
            var win = window.open("<%:ResolveUrl("~/Account/Memo.aspx?_id=")%>" + _id, "_blank");
            return false;
        }
    </script>
</asp:Content>