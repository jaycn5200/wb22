<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CKeditor_Demo.aspx.cs" Inherits="BookShop.Web.Test.CKeditor_Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../SWFUpload/swfupload.js" type="text/javascript"></script>
    <script src="../SWFUpload/handlers.js" type="text/javascript"></script>
    <script src="../js/jquery-1.7.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var swfu;
        window.onload = function () {
            swfu = new SWFUpload({
                // Backend Settings
                upload_url: "/ashx/upload.ashx",
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
        function ShowData(file, serverData) {
            var d = serverData.split(":");
            if (d[0] == "ok") {
                var oEditor = CKEDITOR.instances.editor1; //找到编辑器
                // Check the active editing mode.
	if ( oEditor.mode == 'wysiwyg' ) {//只有在编辑模式下才能上传图片。
		 var img="<img src="+d[1]+"/>"
		 oEditor.insertHtml(img);
	}
	else
		alert( 'You must be in WYSIWYG mode!' );


            }
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
 
    <textarea cols="100" id="editor1" name="editor1" rows="10"></textarea>
       <script type="text/javascript">
   
		//<![CDATA[
           // Replace the <textarea id="editor1"> with an CKEditor instance.
           var editor = CKEDITOR.replace('editor1');
		//]]>
		</script>
        <input type="submit" value="提交" />
    </div>
    <div id="content">
		
	    <div id="swfu_container" style="margin: 0px 10px;">
		    <div>

				<span id="spanButtonPlaceholder"></span>
           
		    </div>
      
		    <div id="divFileProgressContainer" style="height: 75px;"></div>
         
	    </div>
        
		</div>
    </form>
</body>
</html>
