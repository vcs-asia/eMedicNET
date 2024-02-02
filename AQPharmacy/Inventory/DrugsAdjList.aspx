<%@ Page Language="C#" Title="Adjustment List" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugsAdjList" Codebehind="DrugsAdjList.aspx.cs" %>
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
            <asp:HiddenField runat="server" ID="hdnID" Value="" />
        </p>
    </asp:Panel>
    <h4>Stock Adjustment</h4>
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
                <asp:ListItem Value="ADJ_REF_NO">REF NO</asp:ListItem>
                <asp:ListItem Value="getStatus('STOCK_ADJUSTMENT_INFO',ADJ_REF_ID)">STATUS</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onclick="javascript:openSub('0');">New</button>
        </div>
    </div>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="ADJ_REF_ID, ADJ_REF_NO, ADJ_POST_FLAG" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnRowCommand="OnRowCommand" OnSorting="Sorting" AllowSorting="true" GridLines="None" AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small" OnRowDeleting="RowDeleting">
        <Columns>
            <asp:TemplateField ItemStyle-Width="2%" HeaderText="Status" SortExpression="FLAG">
                <ItemTemplate>
                    <%# getStatus(Eval("ADJ_POST_FLAG").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Drugs" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view PO Drugs" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlPODrugs" style="display:none">
                        <asp:GridView runat="server" ID="LstPODrugs" AutoGenerateColumns="false" DataKeyNames="TRANS_ID" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="Drug Name" DataField="MED_NAME" ItemStyle-Width="30%" />
                                <asp:BoundField HeaderText="Adjust" DataField="PQTY" ItemStyle-Width="10%" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ADJ_REF_NO" SortExpression="ADJ_REF_NO" ItemStyle-Width="10%" HeaderText="REF No." />
            <asp:BoundField DataField="ADJ_DATE" SortExpression="ADJ_DATE" ItemStyle-Width="10%" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:TemplateField ItemStyle-Width="60%">
                <ItemTemplate>
                    <asp:Label runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openSub({0})",HttpUtility.UrlEncode(Eval("ADJ_REF_ID").ToString()))%>' Target="_blank" ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ADJ_REF_ID") %>' CommandName="delete"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ADJ_REF_ID") %>' CommandName="post"><i class="icon-ok"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ADJ_REF_ID") %>' CommandName="bPrint" ToolTip="Print"><i class="icon-print"></i></asp:LinkButton>
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
            var myWindow = window.open('<%:ResolveUrl("~/Inventory/DrugsADJ.aspx")%>' + '?n=' + _id, '_blank');
            return false;
        }
    </script>
</asp:Content>