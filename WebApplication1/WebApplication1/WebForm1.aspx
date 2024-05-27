<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="StyleSheet1.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="log" runat="server">
        <div id="login">
            <label id="logindane">Podaj imie i nazwisko</label>
            <asp:TextBox ID="dane" runat="server"></asp:TextBox>
            <asp:Button ID="rejestrButton" runat="server" Text="zarajestruj" OnClick="rejestrButton_Click"/>
            <asp:RequiredFieldValidator ID="error1" runat="server" ControlToValidate="dane" ErrorMessage="uzytkownik juz zalogowany">*</asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="error2" runat="server" ControlToValidate="dane" ErrorMessage="pole nie moze byc puste" >*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="error3" runat="server" ControlToValidate="dane" ErrorMessage="nazwa nie spełnia wymgań" ValidationExpression="^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]{2,}\s[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]{2,}$">*</asp:RegularExpressionValidator>
            <asp:ValidationSummary id="errors" runat="server"/>
        </div>

        </asp:Panel>
        <asp:Panel ID="strona" runat="server">
        <div id="sunblocks">
      
<div id="second">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i1" OnClick="ImageButton_Click" CommandArgument="1"></asp:ImageButton>
</div>
<div id="third">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i2" OnClick="ImageButton_Click" CommandArgument="2"></asp:ImageButton>
</div>
<div id="fourth">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i3" OnClick="ImageButton_Click" CommandArgument="3"></asp:ImageButton>
</div>
<div id="five">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i4" OnClick="ImageButton_Click" CommandArgument="4"></asp:ImageButton>
</div>
<div id="six">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i5" OnClick="ImageButton_Click" CommandArgument="5"></asp:ImageButton>
</div>
<div id="seven">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i6" OnClick="ImageButton_Click" CommandArgument="6"></asp:ImageButton>
</div>
<div id="eight">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i7" OnClick="ImageButton_Click" CommandArgument="7"></asp:ImageButton>
</div>
<div id="nine">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i8" OnClick="ImageButton_Click" CommandArgument="8"></asp:ImageButton>
</div>
<div id="ten">
    <asp:ImageButton ImageUrl="zdjecia\moon.jpg" runat="server" ID="i9" OnClick="ImageButton_Click" CommandArgument="9"></asp:ImageButton>
</div>




            </div>

        <div id="restcontent">
     

   <div id="radio">
    <p>Zmień rozmiar obrazka</p>
   <asp:RadioButtonList ID="RadioList" runat="server" CssClass="RadioList" OnSelectedIndexChanged="RadioList_SelectedIndexChanged" AutoPostBack="true">
    <asp:ListItem Text="Mały" Value="mały"></asp:ListItem>
    <asp:ListItem Text="Średni" Value="średni"></asp:ListItem>
    <asp:ListItem Text="Duży" Value="duży"></asp:ListItem>
</asp:RadioButtonList>
</div>
           
            <div id="choice">
              <p id="okr">określ układ rysunku</p>
<asp:DropDownList ID="rozmiarDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rozmiarDropDownList_SelectedIndexChanged">
<asp:ListItem Text="Wybierz rodzaj linii" Value="0"></asp:ListItem>
<asp:ListItem Text="Linia pionowa" Value="1"></asp:ListItem>
<asp:ListItem Text="Linia pozioma" Value="2"></asp:ListItem>
<asp:ListItem Text="Przekątna prawa" Value="3"></asp:ListItem>
<asp:ListItem Text="Przekątna lewa" Value="4"></asp:ListItem>
<asp:ListItem Text="Piątka" Value="5"></asp:ListItem>
</asp:DropDownList>
            </div>
                    
            <div id="MinMax">
                <p>minimalny czas reakcji</p>
                <asp:Label runat="server" ID="time"></asp:Label>
                <asp:Label runat="server" ID="min"></asp:Label>
                <p>masksymalny czas reakcji</p>
                <asp:Label runat="server" ID="max"></asp:Label>
            </div>
            
                <p id="userdata">Użytkownik</p>
                   <asp:label ID="userlab" runat="server"></asp:label> 
           
            
    
              <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
         <asp:Button ID="resetButton" runat="server" OnClick="resetButton_Click" Text="reset" Style="position: absolute; bottom: 5%; z-index: 0;" />
        <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
      
         <asp:DropDownList ID="usersDropDownList" runat="server" OnSelectedIndexChanged="usersDropDownList_SelectedIndexChanged">
        </asp:DropDownList>

        <asp:DetailsView ID="userDetailsView" runat="server" AutoGenerateRows="true">
            <Fields>

            </Fields>
        </asp:DetailsView>
    
    </ContentTemplate>
</asp:UpdatePanel>
        </div>
        </asp:Panel>
        
    </form>
</body>
</html>
