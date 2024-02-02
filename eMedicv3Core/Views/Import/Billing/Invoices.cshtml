<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Billing_Invoices" Codebehind="Invoices.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Invoicing</h4>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
         Error in generating invoice. Please check and generate again.
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The invoice has been generated Successfully.
    </asp:Panel>
    <div class="well">
        <div class="control-group">
            <label class="control-label" for="txtMonth">Month</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtMonth">
                    <asp:ListItem Value="01">JANUARY</asp:ListItem>
                    <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                    <asp:ListItem Value="03">MARCH</asp:ListItem>
                    <asp:ListItem Value="04">APRIL</asp:ListItem>
                    <asp:ListItem Value="05">MAY</asp:ListItem>
                    <asp:ListItem Value="06">JUNE</asp:ListItem>
                    <asp:ListItem Value="07">JULY</asp:ListItem>
                    <asp:ListItem Value="08">AUGUST</asp:ListItem>
                    <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                    <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                    <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                    <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtYear">Year</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtYear" ClientIDMode="Static" Required="true"></asp:TextBox>
            </div>
        </div>
    </div>
    <p>
        <asp:Button runat="server" Text="Generate" CssClass="btn btn-danger" onclick="generateInvoices"/>
        <asp:Button runat="server" Text="View" CssClass="btn btn-warning" onclick="viewInvoices"/>
        <button class="btn" onclick='javascript:var inv = window.open("<%:ResolveUrl("~/Billing/invoice_1.aspx?id=0&type=0&_d=" + txtMonth.SelectedValue + "." + txtYear.Text) + "&_t=0"%>","_blank"); return false;'><i class="icon-print"></i>&nbsp;Print All</button>
        <button class="btn" onclick='javascript:var sum = window.open("<%:ResolveUrl("~/Billing/invoice_1.aspx?id=0&type=0&_d=" + txtMonth.SelectedValue + "." + txtYear.Text) + "&_t=1"%>","_blank"); return false;'><i class="icon-print"></i>&nbsp;Summary</button>
    </p>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="INV_COMPANY_ID, INV_NO" GridLines="None" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="20"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true" AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small">
        <Columns>
            <asp:BoundField DataField="INV_NO" SortExpression="INV_NO" ItemStyle-Width="10%" HeaderText="Invoice No." />
            <asp:BoundField DataField="COMPANY_NAME" SortExpression="COMPANY_NAME" ItemStyle-Width="25%" HeaderText="Name of Company" />
            <asp:BoundField DataField="INV_AMT" SortExpression="INV_AMT" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" DataFormatString="{0:0.00}"/>
            <asp:BoundField DataField="INV_FDAT" SortExpression="INV_DATE" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="From" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="INV_TDAT" SortExpression="INV_TDAT" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="To" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format(ResolveUrl("~/Billing/invoice_1.aspx?id={0}&type={1}&_d={2}").ToString(),HttpUtility.UrlEncode(Eval("INV_NO").ToString()),"0",HttpUtility.UrlEncode(txtMonth.SelectedValue + "." + txtYear.Text))%>' Target="_blank" Text="F"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format(ResolveUrl("~/Billing/invoice_1.aspx?id={0}&type={1}&_d={2}").ToString(),HttpUtility.UrlEncode(Eval("INV_NO").ToString()),"1",HttpUtility.UrlEncode(txtMonth.SelectedValue + "." + txtYear.Text))%>' Target="_blank" Text="E"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format(ResolveUrl("~/Billing/invoice_1.aspx?id={0}&type={1}&_d={2}").ToString(),HttpUtility.UrlEncode(Eval("INV_NO").ToString()),"2",HttpUtility.UrlEncode(txtMonth.SelectedValue + "." + txtYear.Text))%>' Target="_blank" Text="D"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>
