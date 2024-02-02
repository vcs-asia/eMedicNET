﻿<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Change Password" AutoEventWireup="true" Inherits="Account_changePwd" Codebehind="changePwd.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Change Password</h3>
    <div id="divWarning" class="alert alert-block">
        <strong>Note: </strong> Should be at least 8 characters, 1 Captial Letter and Numbers.
    </div>
    <div id="divError" class="alert alert-error error-icon" style="display:none">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <label id="lblError"></label>
    </div>
    <div id="divSuccess" class="alert alert-success success-icon" style="display:none">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Old Password"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtOPassword" Required="true" ClientIDMode="Static" CssClass="input-xsmall" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="New Password"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtNPassword" Required="true" ClientIDMode="Static" CssClass="input-xsmall" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Confirm Password"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtCPassword" Required="true" ClientIDMode="Static" CssClass="input-xsmall" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <p>
        <button type="button" class="btn btn-success" onclick="changePassword()">Apply</button>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function changePassword() {
            $.ajax({
                type: 'POST',
                url: 'changePwd.aspx/verifyPassword',
                data: '{pwd:"' + document.getElementById('txtOPassword').value + '",npwd:"' + document.getElementById('txtNPassword').value + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    if (respond.d.substring(0, 5) == 'ERROR') {
                        $('#lblError').text(respond.d);
                        $('#divError').removeAttr('style');
                    }
                    else {
                        if (document.getElementById("txtNPassword").value != document.getElementById("txtCPassword").value) {
                            $("#lblError").text("The new password and confirm password do not match. Please try again.");
                            $('#divError').removeAttr('style');
                        }
                        else {
                            $.ajax({
                                type: 'POST',
                                url: 'changePwd.aspx/saveInfo',
                                data: '{npwd:"' + $('#txtCPassword').val() + '"}',
                                dataType: 'json',
                                contentType: 'application/json;charset=utf-8',
                                success: function (respond) {
                                    if (respond.d.substring(0, 5) == "ERROR") {
                                        $("#lblError").text(respond.d);
                                        $('#divError').removeAttr('style');
                                    }
                                    else {
                                        $('#divError').attr('style', 'display:none');
                                        $('#divSuccess').removeAttr('style');
                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownerror) {
                                    $("#lblError").text(xhr.responseText);
                                    $('#divError').removeAttr('style');
                                }
                            });
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    $('#lblError').text(xhr.responseText);
                    $('#divError').removeAttr('style');
                }
            });
        }
    </script>
</asp:Content>
