<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Contact Support" AutoEventWireup="true" Inherits="Manage_ContactSupport" Codebehind="ContactSupport.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Contact Support</h4>
    <p>
        Please contact us for support by email that is registered with us. Our email address is <strong>support@emedicnet.com</strong>. Any un-registered email will not be entertained.
    </p>
    <table class="table">
        <thead>
            <tr><th>#</th><th>Problem Category</th><th>Max. Responding Time</th><th>Max. Resolution Time</th></tr>
        </thead>
        <tbody>
            <tr><td>1.</td><td>System Down</td><td>1 Hour(s)</td><td>4 Hour(s)</td></tr>
            <tr><td>2.</td><td>A sub-module that is very important having a severe issue</td><td>2 Hour(s)</td><td>6 Hour(s)</td></tr>
            <tr><td>3.</td><td>A Module cannot be used at all</td><td>6 Hour(s)</td><td>48 Hour(s)</td></tr>
            <tr><td>4.</td><td>Any small issue with any module</td><td>8 Hour(s)</td><td>72 Hour(s)</td></tr>
        </tbody>
    </table>
    <p>
        <u>Note:</u><br /><br />
        1. Problem categories will be detected by technical team basing on received email.<br />
        2. Above mentioned services are applied for only who are under Annual Maintenance Contract unless it is a defect.<br />
        3. The immediate attempt of solving the issue would be handled online always online. Onsite services are in a special case.<br />
        4. User Desktop PC or Laptop PC or Printers or Any Other Devices or LAN/Communicatoin problems are strictly not covered.<br />
        5. Any problems with Server in terms of hardware are strictly not covered. Please contact hardware vendor.<br />
        6. No phone assistance unless it is a special case.<br />
        7. <em>If you are seeking for help in training or on current updates we recommend our clients to <strong>Follow us on Facebook and Subscribe to our Youtube channel</strong>.</em>
    </p>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>