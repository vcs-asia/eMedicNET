<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/eMedicNET.master" Inherits="Patient_Visits" Codebehind="Visits.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Patients Visits</h4>
    <asp:GridView runat="server" ID="LstVisit" AutoGenerateColumns="false" DataKeyNames="VISIT_ID, PAT_ID" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige">
        <Columns>
            <asp:BoundField HeaderText="Visit Date" DataField="VISIT_DATE" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField HeaderText="Visit Time" DataField="VISIT_TIME" ItemStyle-Width="10%" DataFormatString="{0:hh:mm tt}" />
            <asp:BoundField HeaderText="Doctor" DataField="DOC_NAME" ItemStyle-Width="25%" />
            <asp:BoundField HeaderText="Discipline" DataField="DISC" ItemStyle-Width="25%" />
            <asp:BoundField HeaderText="Amount" DataField="VISIT_TOT_AMT" ItemStyle-Width="5%" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right" />
            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkdel" NavigateUrl='<%# String.Format("~/Patient/PatientVisit.aspx?_vD={0}", HttpUtility.UrlEncode(Eval("VISIT_ID").ToString())) %>' Target= "_blank" ToolTip="Edit/View Visit Details" runat="server"><i class="icon-pencil"></i>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkdel" NavigateUrl='<%# String.Format("~/Patient/ReceiptList.aspx?_vD={0}", HttpUtility.UrlEncode(Eval("VISIT_ID").ToString())) %>' Target= "_blank" ToolTip="List of Receipts" runat="server"><i class="icon-tasks"></i>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>