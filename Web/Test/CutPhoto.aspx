﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CutPhoto.aspx.cs" Inherits="BookShop.Web.Test.CutPhoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/>
    <link href="../Css/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="../SWFUpload/swfupload.js" type="text/javascript"></script>
    <script src="../SWFUpload/handlers.js" type="text/javascript"></script>
    <script src="../js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.2.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var swfu;
        window.onload = function () {
            swfu = new SWFUpload({
                // Backend Settings
                upload_url: "/ashx/cutPhoto.ashx?action=up",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>"
                },

                // File Upload Settings
                file_size_limit: "2 MB",
                file_types: "*.jpg;*.gif",
                file_types_description: "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: ShowData,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "/SWFUpload/images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">请选择文件 <span class="buttonSmall">(2 MB Max)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,

                // Flash Settings
                flash_url: "/SWFUpload/swfupload.swf", // Relative to this file
                flash9_url: "/SWFUpload/swfupload_FP9.swf", // Relative to this file

                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },
                // Debug Settings
                debug: false
            });
        }
        //上传成功以后
        var d;//保存了上传成功图片的路径信息
        function ShowData(file, serverData) {
             d = serverData.split(":");
            if (d[0] == "ok") {
             
                // $("#imgSrc").attr("src", d[1]);
                $("#divContent").css("backgroundImage", "url(" + d[1] + ")").css("width",d[2]+"px").css("height",d[3]+"px");
            }
        }
        $(function () {
            $("#divCut").draggable({ containment: 'parent' }).resizable({
                containment: '#divContent'
            });
            //开始获取要截取头像的范围
            $("#btnCut").click(function () {
                //offset():获取元素绝对坐标 top距浏览器顶端的距离
                var y = $("#divCut").offset().top - $("#divContent").offset().top; //纵坐标
                var x = $("#divCut").offset().left - $("#divContent").offset().left; //横坐标.
                var width = $("#divCut").width(); //宽度
                var height = $("#divCut").height(); //高度
                //将截取图片的访问通过AJAX发送到服务端.
                $.post("/ashx/cutPhoto.ashx", { "action": "cut", "x": parseInt(x), "y": parseInt(y), "width": parseInt(width), "height": parseInt(height), "imgSrc": d[1] }, function (data) {
                    $("#imgsrc").attr("src",data);
                });
            });

        })
	</script>
</head>
<body>
    <form id="form1" runat="server">
  
	<div id="content">
		
	    <div id="swfu_container" style="margin: 0px 10px;">
		    <div>
          
				<span id="spanButtonPlaceholder"></span>
          
		    </div>
       
		    <div id="divFileProgressContainer" style="height: 75px;"></div>
             <div id="divContent" style="width:300px; height:300px">
                <div id="divCut" style="width:100px; height:100px; border:solid 1px red"></div>

             </div>
	    </div>
       <input type="button" value="截取头像" id="btnCut" />
       <img id="imgsrc" />
		</div>
    </form>
</body>
</html>
