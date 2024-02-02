<%@ Page Title="Patient Parameters" MasterPageFile="~/eMedicNET.master" Language="C#" AutoEventWireup="true" Inherits="Patient_Parameters" Codebehind="Parameters.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Patient Module - Parameters</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="PARAM_NAME">Parameter</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <asp:Button runat="server" ID="btnNParameter" CssClass="btn" Text="New" OnCommand="newParameter" />
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="PARAM_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="PARAM_NAME" SortExpression="PARAM_NAME" ItemStyle-Width="50%" HeaderText="Parameter" />
            <asp:BoundField DataField="PARAM_TYPE" SortExpression="PARAM_TYPE" ItemStyle-Width="45%" HeaderText="Parameter Type" />
            <asp:ButtonField ButtonType="Link" CommandName="bEdit" ItemStyle-Width="5%" Text="Edit" />
            <asp:ButtonField ButtonType="Link" CommandName="bDelte" ItemStyle-Width="5%" Text="Delete" />
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
    </script>
</asp:Content>