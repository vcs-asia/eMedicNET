<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugsIssueList" Codebehind="DrugsIssueList.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Stock Issue</h4>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="ISSUE_REF_NO">REF NO</asp:ListItem>
                <asp:ListItem Value="OUTLET_NAME">OUTLET</asp:ListItem>
                <asp:ListItem Value="getStatus('STOCK_ISSUE_INFO',ISSUE_REF_ID)">STATUS</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onclick="javascript:openSub('0');">New</button>
        </div>
    </div>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="ISSUE_REF_ID, ISSUE_REF_NO, ISSUE_POST_FLAG" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnRowCommand="OnRowCommand" OnRowDeleting="OnRowDeleting" OnSorting="Sorting" AllowSorting="true" GridLines="None" AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small">
        <Columns>
            <asp:TemplateField ItemStyle-Width="2%" HeaderText="Status" SortExpression="FLAG">
                <ItemTemplate>
                    <%# getStatus(Eval("ISSUE_POST_FLAG").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Drugs" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view PO Drugs" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlPODrugs" style="display:none">
                        <asp:GridView runat="server" ID="LstPODrugs" AutoGenerateColumns="false" DataKeyNames="TRANS_ID" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="Drug Name" DataField="MED_NAME" ItemStyle-Width="30%" />
                                <asp:BoundField HeaderText="Issued" DataField="RQTY" ItemStyle-Width="10%" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ISSUE_REF_NO" SortExpression="ISSUE_REF_NO" ItemStyle-Width="10%" HeaderText="REF No." />
            <asp:BoundField DataField="ISSUE_DATE" SortExpression="ISSUE_DATE" ItemStyle-Width="10%" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="OUTLET_NAME" SortExpression="OUTLET_NAME" ItemStyle-Width="30%"  HeaderText="Outlet"/>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openSub({0})",HttpUtility.UrlEncode(Eval("ISSUE_REF_ID").ToString()))%>' ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ISSUE_REF_ID") %>' CommandName="delete" ToolTip="Delete Issue"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ISSUE_REF_ID") %>' CommandName="post" ToolTip="Post Issue"><i class="icon-ok"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ISSUE_REF_ID") %>' CommandName="print" ToolTip="Print Issue"><i class="icon-print"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $("[src*=expand]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "<%:ResolveUrl("~/Content/img/icon-collapse.gif")%>");
        });
        $("[src*=collapse]").live("click", function () {
            $(this).attr("src", "<%:ResolveUrl("~/Content/img/icon-expand.gif")%>");
            $(this).closest("tr").next().remove();
        });
        function openSub(_id) {
            var myWindow = window.open('<%:ResolveUrl("~/Inventory/DrugsIssue.aspx")%>' + '?n=' + _id, '_blank');
            return false;
        }
    </script>
</asp:Content>