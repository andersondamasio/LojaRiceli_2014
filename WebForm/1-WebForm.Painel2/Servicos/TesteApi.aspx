<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TesteApi.aspx.cs" Inherits="Loja.Servicos.TesteApi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../Painel/js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
      $(document).ready(function () {

        //Get
        $('#get').click(function (e) {
          e.preventDefault();
          $.getJSON("api/Servicing");
        });

        //Get Single
        $('#getsingle').click(function (e) {
          e.preventDefault();
          $.getJSON("api/Servicing/" + 1);
        });

        //Post
        $('#post').click(function (e) {
          e.preventDefault();
          var data = JSON.stringify({
            stuff: {
              id: 1,
              hello: "World"
            }
          });
          $.ajax({
            url: "api/Servicing",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            data: data
          });
        });

        //Put
        $('#put').click(function (e) {
          e.preventDefault();
          var data = JSON.stringify({
            stuff: {
              id: 1,
              hello: "World"
            }
          });
          $.ajax({
            url: "api/Servicing/" + 1,
            type: "PUT",
            contentType: "application/json;charset=UTF-8",
            data: data
          });
        });

        //Delete
        $('#delete').click(function (e) {
          e.preventDefault();
          $.ajax({
            url: "api/Servicing/" + 1,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
          });
        });
      });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <button id="get">Get Hello</button>
      <button id="getsingle">Get a Hello</button>
      <button id="post">Post Hello</button>
      <button id="put">Put Hello</button>
      <button id="delete">Delete Hello</button>
    </div>
    </form>
</body>
</html>