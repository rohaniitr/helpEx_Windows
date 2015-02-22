<?php
//@params='msg' , 'roll' , 'to'  -via POST
//message
//$message = $_POST['msg'];
//mail body - image position, background, font color, font size...
$body ='<html>
<head>
<style>
body
{
background: #fff;
font-family: "lucida grande", tahoma, verdana;
font-size:16px;
font-weight: bold;
color: #fff;
}
.content{
overflow:hidden;
background-color: #336699;
margin: 10px;
padding:10px;
}
</style>
</head>
<body>
<div class="content">
<h1>My Observations</h1>
<br />
'.$_FILE['msg'].'
</div>
</body>';
$headers='MIME-Version: 1.0' . "\r\n";
$headers .= 'Content-type: text/html;charset=iso-8859-1' . "\r\n";
$headers .= 'From: Student  <noreply@example.com>';
$to = $_POST['to'];
$subject = 'Experiment Submission for Roll no: '.$_POST["roll"];
$mail = mail($to, $subject, $body, $headers);
if(!$mail) {   
     echo "Error sending email";   
} else {
    echo "Your email was sent successfully.";
}
?>