
$( document ).ready(function() {
	//buttonEventHolderLogin();
});
function buttonEventHolderLogin()
{	
	$('#save_btn').click(function(){
		//loginEnter();
	})
}

//function loginEnter()
//{
//	var list = [];
//	$("#info_label").text("Módosítás folyamatban");
//	list.push($("#usr").val());
//	list.push($("#pwd-old").val());
//	list.push($("#pwd-new1").val());
//	list.push($("#pwd-new2").val());
//	$.ajax({
//		type: "POST",
//	    url:  '/Source/',
//        data: {
//        	usr: list[0],
//            pwd_old: list[1],
//            pwd_new1: list[2],
//            pwd_new2: list[3]
//        },
//	    success: function (response) {
//	    	$("#info_label").text(response.split('--')[0]);
//	    	console.log(response.split('--')[1] + "   state: " +response.split('--')[2]);
//	    }
//	});
//}