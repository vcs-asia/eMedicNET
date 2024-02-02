<%@ Page Language="C#" Title="Outlet Stock Adjustment" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugOAdjList" Codebehind="DrugOAdjList.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Stock Adjustment</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="ADJ_REF_NO">REF NO</asp:ListItem>
                <asp:ListItem Value="getStatus('STOCK_ADJUSTMENT_INFO',ADJ_REF_ID)">STATUS</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <Button type="button" class="btn" onclick="javascript:openSub(0);">New</Button>
        </div>
    </div>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="ADJ_REF_ID, ADJ_REF_NO, ADJ_POST_FLAG" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true" GridLines="None" AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small">
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
            <asp:BoundField DataField="OUTLET_NAME" SortExpression="OUTLET_NAME" ItemStyle-Width="10%" HeaderText="Outlet"/>
            <asp:TemplateField ItemStyle-Width="60%">
                <ItemTemplate>
                    <asp:Label runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openSub({0})", Eval("ADJ_REF_ID")) %>' ToolTip="Edit Record"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:delRec({0})", Eval("ADJ_REF_ID")) %>' ToolTip="Delete Record"><i class="icon-trash"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:posRec({0})", Eval("ADJ_REF_ID")) %>' ToolTip="Post Stock"><i class="icon-ok"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:prnRec({0})", Eval("ADJ_REF_ID")) %>' ToolTip="Print"><i class="icon-print"></i></asp:HyperLink>
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
            var myWindow = window.open('<%:ResolveUrl("~/Inventory/DrugOAdj.aspx")%>' + '?n=' + _id, '_blank');
            return false;
        }
        function prnRec(_id) {
            var myWindow = window.open('<%:ResolveUrl("~/report-preview.aspx")%>' + '?param=' + _id + '&mod=OADJ', '_blank');
            return false;
        }
        function delRec(_id) {
            if (confirm("Are you sure want to delete?")) {
                $.ajax({
                    type: 'POST',
                    url: '<%:ResolveUrl("~/Inventory/DrugOAdjList.aspx/delRec") %>',
                    data:'{id:"' + _id + '"}',
                    dataType: 'json',
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        if (response.d != ''){
                            alert(response.d);
                        }
                        else {
                            location.reload();
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.responseText);
                    }
                });
            }
        }
        function posRec(_id) {
            if (confirm("Are you sure want to post?")) {
                $.ajax({
                    type: 'POST',
                    url: '<%:ResolveUrl("~/Inventory/DrugOAdjList.aspx/posRec") %>',
                    data: '{id:"' + _id + '"}',
                    dataType: 'json',
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        if (response.d != '') {
                            alert(response.d);
                        }
                        else {
                            location.reload();
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.responseText);
                    }
                });
            }
        }
    </script>
</asp:Content>