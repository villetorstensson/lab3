<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default</title>
    </head>
    <body style="height: 435px">
    <form id="form1" runat="server">
       
        <h2>Travel Reservation Service</h2>
    
    <p>Click continue for Flight reservation only, check the box for hotel reservation aswell</p>

    <div>
            <asp:CheckBoxList ID="CheckAdditionalBox" runat="server">
            <asp:ListItem Text="Hotel Service" Value="hrsbox"></asp:ListItem>
        </asp:CheckBoxList>

        <asp:Button ID="DefaultContinue" runat="server" Text="Continue" Width="306px" Height="45px" OnClick="BtnContinue"></asp:Button> 
        <asp:Button ID="ReportButton" runat="server" Text="View The Reports" Width="306px" Height="45px" OnClick="BtnReport"></asp:Button> 
        <br />
    
    </div>
    </form>
</body>
</html>