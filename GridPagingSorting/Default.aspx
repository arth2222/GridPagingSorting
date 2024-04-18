<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridPagingSorting.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>GridView med sorting og paging</h1>
            <p>Bruker databasen Arthurs, tabell Person, på serveren glemmen.bergersen.dk,4729</p>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
            </asp:GridView>
            <br />
            <br />
            BubbleSortoppgaven<br />
            <br />
            <asp:GridView ID="GridViewBubble" runat="server">
            </asp:GridView>
            <br />
            <br />
            <asp:Button ID="ButtonBubble" runat="server" Text="BubbleSort" OnClick="ButtonBubble_Click" />
        </div>
    </form>
</body>
</html>
