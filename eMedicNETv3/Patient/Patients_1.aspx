<%@ Page Language="C#" Title="Patient" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Patient_Patients_1" Codebehind="Patients_1.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Patients List</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="PAT_NAME">Patient Name</asp:ListItem>
                <asp:ListItem Value="PAT_REG_NO">Folder No</asp:ListItem>
                <asp:ListItem Value="REMARKS">Remarks</asp:ListItem>
                <asp:ListItem Value="PAT_IC_NO">IC No</asp:ListItem>
                <asp:ListItem Value="PAT_HANDPHONE">H/P No.</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="btnSearch_Command" />
            <asp:HyperLink runat="server" NavigateUrl="~/Patient/Registration.aspx?PatID=0" Text="New" CssClass="btn" Target="_blank"></asp:HyperLink>
        </div>
    </div>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="PAT_ID" GridLines="None" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="20"
        OnPageIndexChanging="PageIndexChanging" OnRowCommand="OnRowCommand" OnSorting="Sorting" AllowSorting="true" AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small">
        <Columns>
            <asp:BoundField DataField="PAT_REG_NO" SortExpression="PAT_REG_NO" ItemStyle-Width="10%" HeaderText="Folder No." />
            <asp:BoundField DataField="PAT_NAME" SortExpression="PAT_NAME" ItemStyle-Width="24%" HeaderText="Name of Patient" />
            <asp:BoundField DataField="PAT_IC_NO" SortExpression="PAT_IC_NO" ItemStyle-Width="10%"  HeaderText="IC No"/>
            <asp:BoundField DataField="PAT_PREV_HISTORY" SortExpression="PAT_PREV_HISTORY" ItemStyle-Width="24%" HeaderText="Remarks"/>
            <asp:BoundField DataField="PAT_REG_DATE" SortExpression="PAT_REG_DATE" ItemStyle-Width="10%" HeaderText="Regn. Date"  DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="PAT_BIRTH_DATE" SortExpression="PAT_BIRTH_DATE" ItemStyle-Width="10%" HeaderText="Birth Date"  DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:TemplateField ItemStyle-Width="10%">
                <ItemTemplate>
                    <div class="btn-group">
                        <button class="btn btn-primary">Visits</button>
                        <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></button>
                        <ul class="dropdown-menu">
                            <%#getVisitLinks(Eval("PAT_ID").ToString()) %>
                        </ul>
	                </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format(ResolveUrl("~/Patient/Registration.aspx?PatID={0}").ToString(),HttpUtility.UrlEncode(Eval("PAT_ID").ToString()))%>' Target="_blank" ToolTip="Edit/View Details"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:sendToQueue(" + Eval("PAT_ID") + ")") %>' ToolTip="Send to Queue"><i class="icon-plus-sign"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="modal hide fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myModalLabel">Patient Queue</h3>
        </div>
        <div class="modal-body">
            <div class="control-group">
                <label class="control-label">Select Discipline</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="drpDiscipline">
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="hdnPatID" ClientIDMode="Static" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Date</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="txtMonth" CssClass="input-mini" ClientIDMode="Static">
                        <asp:ListItem Value="01">JAN</asp:ListItem>
                        <asp:ListItem Value="02">FEB</asp:ListItem>
                        <asp:ListItem Value="03">MAR</asp:ListItem>
                        <asp:ListItem Value="04">APR</asp:ListItem>
                        <asp:ListItem Value="05">MAY</asp:ListItem>
                        <asp:ListItem Value="06">JUN</asp:ListItem>
                        <asp:ListItem Value="07">JUL</asp:ListItem>
                        <asp:ListItem Value="08">AUG</asp:ListItem>
                        <asp:ListItem Value="09">SEP</asp:ListItem>
                        <asp:ListItem Value="10">OCT</asp:ListItem>
                        <asp:ListItem Value="11">NOV</asp:ListItem>
                        <asp:ListItem Value="12">DEC</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="txtDay" CssClass="input-mini" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtYear" CssClass="input-mini" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <asp:Button runat="server" CssClass="btn btn-primary" Text="Send to Queue" OnClick="SendToQueue" />
        </div>
    </div>
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

        function callMe(hyperLink) {
            var myWindow = window.open(hyperLink, "_blank", "toolbar=yes, scrollbar=yes, resizable=yes, fullscreen=yes");
            return false;
        }
        function sendToQueue(patID) {
            $('#hdnPatID').val(patID);
            $('#myModal').modal('show');
        }
    </script>
</asp:Content>