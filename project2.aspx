<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="project2.aspx.cs" Inherits="Project_1.project2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SignUp Page</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            background-image: url(/Images/signin.jpg); /* put your image in the project folder */
            background-size: cover;
            background-position: center;
            font-family: Arial;
        }
        .form-container {
            background-color: rgba(255, 255, 255, 0.9);
            padding: 30px;
            margin: 50px auto;
            width: 400px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px 2px #00000033;
        }
        .form-container h2 {
            text-align: center;
        }
        .form-container table {
            width: 100%;
        }
        .form-container input[type="text"],
        .form-container input[type="password"],
        .form-container input[type="date"] {
            width: 100%;
            padding: 8px;
            margin: 5px 0;
        }
        .form-container input[type="submit"] {
            padding: 8px 20px;
            margin: 5px;
        }
        .gridview-container {
            margin: 20px auto;
            width: 90%;
            background-color: #fff;
            padding: 10px;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>SignUp Details</h2>
            <table>
                <tr>
                    <td>UserName :</td>
                    <td><asp:TextBox ID="txtname" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>PassWord :</td>
                    <td><asp:TextBox ID="txtpwd" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email :</td>
                    <td><asp:TextBox ID="txtemail" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Date of Birth :</td>
                    <td><asp:TextBox ID="txtcld" runat="server" TextMode="Date"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button ID="btnsignin" runat="server" Text="Sign In" OnClick="btnsignin_Click" />
                        <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="gridview-container" align="center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="sno" 
                OnRowDeleting="GridView1_RowDeleting"
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit">
                <Columns>
                    
                    
                    <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                   <ItemTemplate>
                   <%# Eval("UserName") %>
                   </ItemTemplate>
                   <EditItemTemplate>
                   <asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("UserName") %>' />
                   </EditItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password">
                   <ItemTemplate>
                   <%# Eval("Password") %>
                   </ItemTemplate>
                   <EditItemTemplate>
                   <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("Password") %>' />
                   </EditItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                   <%# Eval("Email") %>
                   </ItemTemplate>
                   <EditItemTemplate>
                   <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                   </EditItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOB">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtDOB" runat="server" Text='<%# Bind("DOB") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                    <%# Eval("DOB") %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
