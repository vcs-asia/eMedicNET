<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_Items" Codebehind="Items.aspx.cs" %>
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
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <h4>Items List</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="MED_NAME">Drug Name</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onclick="javascript:openSub('0');">New</button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="med_id, med_flag" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small" OnRowCommand="OnRowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Batch" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view Batch stock" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlBatch" style="display:none">
                        <asp:GridView runat="server" ID="LstBatch" AutoGenerateColumns="false" DataKeyNames="batch_id" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="Batch No" DataField="BATCH_NO" ItemStyle-Width="15%" />
                                <asp:BoundField HeaderText="Packing" DataField="PACKING" ItemStyle-Width="15%" />
                                <asp:BoundField HeaderText="Expiry Date" DataField="EXP_DATE" ItemStyle-Width="20%" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Balance" DataField="BAL" ItemStyle-Width="50%" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="btnMovement" NavigateUrl="~/Inventory/Movement.aspx"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Outlet" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view Outlet stock" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlOutlet" style="display:none">
                        <asp:GridView runat="server" DataKeyNames="OUTLET_ID, OUTLET_MED_ID" ID="LstOutlet" AutoGenerateColumns="false" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="Outlet" DataField="OUTLET_NAME" ItemStyle-Width="50%" />
                                <asp:BoundField HeaderText="Balance" DataField="BAL" ItemStyle-Width="40%" />
                                <asp:TemplateField ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:viewOMovement({0},{1})",HttpUtility.UrlEncode(Eval("OUTLET_ID").ToString()),HttpUtility.UrlEncode(Eval("OUTLET_MED_ID").ToString())) %>' ToolTip="View Movement">Movement</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Balance" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view Stock Movement" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlMovement" style="display:none">
                        <asp:GridView runat="server" ID="LstMovement" AutoGenerateColumns="false" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="Packing" DataField="PACKING" ItemStyle-Width="40%"/>
                                <asp:BoundField HeaderText="Balance" DataField="BAL" ItemStyle-Width="40%"/>
                                <asp:TemplateField ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Text="Movement" NavigateUrl='<%# String.Format("~/report-preview.aspx?param={0}&mode={1}",Eval("MED_ID") + "." + Eval("MED_PACK"), "MOVE") %>' Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <img title="Expand to view Purchase Orders" alt="" style="cursor:pointer;width:14px;height:14px" src="<%:ResolveUrl("~/Content/img/icon-expand.gif") %>" />
                    <asp:Panel runat="server" ID="pnlPO" style="display:none">
                        <asp:GridView runat="server" ID="LstPO" AutoGenerateColumns="false" DataKeyNames="PO_ID, PO_MED_ID" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
                            <Columns>
                                <asp:BoundField HeaderText="PO No" DataField="PO_NO" ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Date" DataField="PO_DATE" ItemStyle-Width="10%"  DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField HeaderText="Supplier" DataField="SUPPLIER_NAME" ItemStyle-Width="50%"/>
                                <asp:BoundField HeaderText="Ordered" DataField="MED_ORD_QTY_P" ItemStyle-Width="20%" />
                                <asp:BoundField HeaderText="Cost" DataField="PO_MED_COST" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="med_name" SortExpression="med_name" ItemStyle-Width="26%" HeaderText="Name of Drug" />
            <asp:BoundField DataField="med_generic_name" SortExpression="med_generic_name" ItemStyle-Width="16%"  HeaderText="Generic Name"/>
            <asp:BoundField DataField="med_cost_price" SortExpression="med_cost_price" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"  HeaderText="Cost[RM]" DataFormatString="{0:0.00}"/>
            <asp:BoundField DataField="med_unit_cost" SortExpression="med_unit_cost" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"  HeaderText="Unit[RM]" DataFormatString="{0:0.00}"/>
            <asp:BoundField DataField="med_out_selling_cost" SortExpression="med_out_selling_cost" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right"  HeaderText="Sell O[RM]" DataFormatString="{0:0.00}"/>
            <asp:BoundField DataField="med_mark_up" SortExpression="med_mark_up" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"  HeaderText="%" DataFormatString="{0:0.00}"/>
            <asp:BoundField DataField="med_selling_price" SortExpression="med_selling_price" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right" HeaderText="Sell I[RM]" DataFormatString="{0:0.00}"/>
            <asp:BoundField DataField="med_out_mark_up" SortExpression="med_out_mark_up" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"  HeaderText="%" DataFormatString="{0:0.00}"/>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openSub({0})",HttpUtility.UrlEncode(Eval("MED_ID").ToString()))%>' ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="2%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%# Eval("MED_ID") %>' CommandName="delete" ToolTip="Delete drug"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="2%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%# Eval("MED_ID") %>' CommandName="bActivate" ToolTip="Deactivate drug"><i class='icon-ban-circle'></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--Outlet Movement Reception label printing starts-->
    <div class="modal hide fade" id="myOutletPeriod" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myLblLabel">Option</h3>
        </div>
        <div class="modal-body">
            <div class="control-group">
                <label class="control-label">Days</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtDays" ClientIDMode="Static" Text="30"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hdnOutletID" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hdnDrugID" ClientIDMode="Static"/>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <Button type="button" class="btn btn-inverse" onclick="processOMovement()">Print</Button>
        </div>
    </div>
    <!--Reception label printing ends-->
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
            var myWindow = window.open('<%:ResolveUrl("~/Inventory/Item.aspx")%>' + '?id=' + _id, '_blank');
            return false;
        }
        function viewOMovement(_outlet, _medid) {
            $('#hdnOutletID').val(_outlet);
            $('#hdnDrugID').val(_medid);
            $('#myOutletPeriod').modal('show');
        }
        function processOMovement() {
            var _days = document.getElementById("txtDays").value;
            var _outlet = document.getElementById("hdnOutletID").value;
            var _medid = document.getElementById("hdnDrugID").value;
            var winO = window.open("<%:ResolveUrl("~/report-preview.aspx")%>?_p=" + _days + '.' + _outlet + "." + _medid + "&_n=OMOVE", "_blank");
            return false;
        }
    </script>
</asp:Content>