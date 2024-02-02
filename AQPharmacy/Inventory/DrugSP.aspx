<%@ Page Title="Drugs Selling Price" MasterPageFile="~/eMedicNET.master"  Language ="C#" AutoEventWireup="true" Inherits="Inventory_DrugSP" Codebehind="DrugSP.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <h4>Drugs List</h4>
    <div class="control-group">
        <label class="control-label">Page Selection</label>
        <div  class="controls">
            <asp:DropDownList runat="server" ID="pages" AutoPostBack="true" OnSelectedIndexChanged="onChange">
                <asp:ListItem Value="1.100">1</asp:ListItem>
                <asp:ListItem Value="101.200">2</asp:ListItem>
                <asp:ListItem Value="201.300">3</asp:ListItem>
                <asp:ListItem Value="301.400">4</asp:ListItem>
                <asp:ListItem Value="401.500">5</asp:ListItem>
                <asp:ListItem Value="501.600">6</asp:ListItem>
                <asp:ListItem Value="601.700">7</asp:ListItem>
                <asp:ListItem Value="701.800">8</asp:ListItem>
                <asp:ListItem Value="801.900">9</asp:ListItem>
                <asp:ListItem Value="901.1000">10</asp:ListItem>
                <asp:ListItem Value="1001.1100">11</asp:ListItem>
                <asp:ListItem Value="1101.1200">12</asp:ListItem>
                <asp:ListItem Value="1201.1300">13</asp:ListItem>
                <asp:ListItem Value="1301.1400">14</asp:ListItem>
                <asp:ListItem Value="1401.1500">15</asp:ListItem>
                <asp:ListItem Value="1501.1600">16</asp:ListItem>
                <asp:ListItem Value="1601.1700">17</asp:ListItem>
                <asp:ListItem Value="1701.1800">18</asp:ListItem>
                <asp:ListItem Value="1801.1900">19</asp:ListItem>
                <asp:ListItem Value="1901.2000">20</asp:ListItem>
                <asp:ListItem Value="2001.2100">21</asp:ListItem>
                <asp:ListItem Value="2101.2200">22</asp:ListItem>
                <asp:ListItem Value="2201.2300">23</asp:ListItem>
                <asp:ListItem Value="2301.2400">24</asp:ListItem>
                <asp:ListItem Value="2301.2400">25</asp:ListItem>
            </asp:DropDownList>
            <asp:Button runat="server" ID="getPage" OnClick="onChange" CssClass="btn" Text="Go" />
        </div>
    </div>
    <div class="control-group">
        <table id="tblDrugs" border="1" style="width:100%;border-spacing:0">
            <tbody>
                <tr>
                    <th style="width:2%;padding:5px;">#</th>
                    <th style="width:38%;padding:5px;">Description</th>
                    <th style="width:10%;padding:5px;text-align:right">Unit Price[RM]</th>
                    <th style="width:10%;padding:5px;text-align:right">&nbsp;</th>
                    <th style="width:10%;padding:5px;text-align:right">Sell In[RM]</th>
                    <th style="width:10%;padding:5px;text-align:right">%</th>
                    <th style="width:10%;padding:5px;text-align:right">Sell Out[RM]</th>
                    <th style="width:10%;padding:5px;text-align:right">%</th>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>