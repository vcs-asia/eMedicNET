<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugsGRNList" Codebehind="DrugsGRNList.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <asp:Panel runat="server" ID="pnlDeleteAlert" CssClass="alert alert-block alert-error fade in" Visible="false">
        <a class="close" data-dismiss="alert">x</a>
        <h4 class="alert-heading">Oh! You are trying to delete</h4>
        <p>
            Are you sure want to delete the selected record?
        </p>
        <p>
            <asp:Button runat="server" CssClass="btn btn-danger" Text="Yes. Delete" OnClick="deleteMe" />
            <asp:Button runat="server" CssClass="btn" Text="No. I dont want to Delete" OnClick="cancelDelete"/>
            <asp:HiddenField runat="server" ID="hdnGrnID" Value="" />
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <h4>Goods Receive Note List</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="GRN_ID">GRN NO</asp:ListItem>
                <asp:ListItem Value="SUPPLIER_NAME">SUPPLIER</asp:ListItem>
                <asp:ListItem Value="PO_ID">PO NO.</asp:ListItem>
                <asp:ListItem Value="getStatus('GRN_INFO',GRN_ID)">STATUS</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
        </div>
    </div>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="GRN_ID, POST_FLAG" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true" GridLines="None" AlternatingRowStyle-BackColor="ControlLightLight" OnRowCommand="OnRowCommand" Font-Size="Small">
        <Columns>
            <asp:TemplateField ItemStyle-Width="2%" HeaderText="" SortExpression="FLAG">
                <ItemTemplate>
                    <%# getStatus(Eval("POST_FLAG").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Drugs" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view GRN Drugs" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlGRNDrugs" style="display:none">
                        <asp:GridView runat="server" ID="LstPODrugs" AutoGenerateColumns="false" DataKeyNames="TRANS_ID" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="Drug Name" DataField="MED_NAME" ItemStyle-Width="30%" />
                                <asp:BoundField HeaderText="Cost" DataField="MED_COST" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}"/>
                                <asp:BoundField HeaderText="Quantity" DataField="RQTY" ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Amount" DataField="MED_AMT" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}"/>
                                <asp:BoundField HeaderText="Batch No" DataField="SQTY" ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Expiry Date" DataField="MED_EXP_DATE" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField HeaderText="GST" DataField="MED_GST_AMT" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GRN_ID" SortExpression="GRN_ID" ItemStyle-Width="5%" HeaderText="GRN No." DataFormatString="{0:00000000}" />
            <asp:BoundField DataField="PO_ID" SortExpression="PO_ID" ItemStyle-Width="5%" HeaderText="PO No." DataFormatString="{0:00000000}" />
            <asp:BoundField DataField="INV_NO" SortExpression="INV_NO" ItemStyle-Width="10%" HeaderText="Invoice No." />
            <asp:BoundField DataField="INV_DATE" SortExpression="INV_DATE" ItemStyle-Width="5%" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="SUPPLIER_NAME" SortExpression="SUPPLIER_NAME" ItemStyle-Width="25%"  HeaderText="Supplier"/>
            <asp:BoundField DataField="INV_AMT" SortExpression="INV_AMT" ItemStyle-Width="5%" HeaderText="Amount" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openSub({0})",HttpUtility.UrlEncode(Eval("GRN_ID").ToString()))%>' Target="_blank" ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("GRN_ID") %>' CommandName="bDelete" ToolTip="Delete"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("GRN_ID") %>' CommandName="bPOST" ToolTip="Post GRN"><i class="icon-arrow-down"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("GRN_ID") %>' CommandName="bPrint" ToolTip="Print GRN"><i class="icon-print"></i></asp:LinkButton>
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
            var myWindow = window.open('<%:ResolveUrl("~/Inventory/DrugsGRN.aspx")%>' + '?n=' + _id, '_blank');
            return false;
        }
    </script>
</asp:Content>