<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebformsSampleApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ChartJS Helper Sample Application</title>
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
	<link rel="stylesheet" href="CustomStyles/sample-app.css" type="text/css" />

	<script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
	<script src="Scripts/Chart.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<div class="panel panel-default" style="height:auto;">
			<div class="panel-heading">
				<h4>ChartJS Helper Sample Application</h4>
			</div>
			<div class="panel-body" style="height:auto;">
				<div runat="server" id="DivCharts">

				</div>
			</div>
			<div class="panel-footer text-center">
				<span>&copy;2018 - Leonel Fraga de Oliveira - <a href="http://leonelfraga.com/neomatrixtech" target="_blank">http://leonelfraga.com/neomatrixtech</a></span>
			</div>
		</div>
    </form>
</body>
</html>
