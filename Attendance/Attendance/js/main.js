var lateTime = false;

function dragFun(){
    $("#origin .user").draggable({ 
			cursor: "Move", 
			revert: "invalid",
			drag: function() {
				$('.dummyPopup').fadeIn(100);
				$('.boxC1, .boxC2').addClass('activeDiv');
				$('.boxC1').css('z-index','32');
				$('.boxC2').css('z-index','31');
			},
			stop: function(){
				$('.dummyPopup').fadeOut(50);
				$('.boxC1, .boxC2').removeClass('activeDiv');
			}
				
	});	
	
	$("#drop1 .user").draggable({ 
			cursor: "Move", 
			revert: "invalid",
			drag: function() {
				$('.dummyPopup').fadeIn(100);
				$('.boxC2, .boxC3').addClass('activeDiv');
				$('.boxC2').css('z-index','31');
				$('.boxC3').css('z-index','30');
			},
			stop: function(){
				$('.dummyPopup').fadeOut(50);
				$('.boxC2, .boxC3').removeClass('activeDiv');				
			}	
	});	
	
	//$("#origin").sortable();
	//$("#drop1").sortable();
	//$("#drop2").sortable();

}

$(window).load(function(){
    $('.user img').each(function(){
        if($(this).attr('src').length <= 5){
            $(this).hide();            
            $(this).parent().append('<div class="wrapper"><span class="name">'+$(this).parent().children('input:eq(0)').val()+'</span></div>')
        }
    });
});

$(function() {

    $('#origin .user').each(function() {
    var title = "<b>Scheduled in</b><br><span>Name: </span>" + $(this).children('input:eq(0)').val() + "<br><span>schedule : </span>" + $(this).children('input:eq(2)').val() + " - " + $(this).children('input:eq(3)').val(); //+$(this).children('input:eq(0)').val();
        $(this).attr('title', title);
    });
    $('#drop1 .user').each(function() {
        // var title= "<b>Signed in </b><br><span>Name: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sch_in: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sig_in: </span>"+$(this).children('input:eq(2)').val()+"";
    var title = "<b>Signed in </b><br><span>Name: </span>" + $(this).children('input:eq(0)').val() + "<br><span>schedule: </span>" + $(this).children('input:eq(2)').val() + " - " + $(this).children('input:eq(3)').val() + "<br><span>Sign In: </span>" + $(this).children('input:eq(4)').val() + "";
        $(this).attr('title', title);
    });
    $('#drop2 .user').each(function() {
        //var title= "<b>Signed out</b><br><span>Name: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sch_in: </span>"+$(this).children('input:eq(0)').val()+"<br><span>Sig_in: </span>"+$(this).children('input:eq(2)').val()+"<br><span>Sig_out: </span>"+$(this).children('input:eq(3)').val()+"";
    var title = "<b>Signed out</b><br><span>Name: </span>" + $(this).children('input:eq(0)').val() + "<br><span>schedule: </span>" + $(this).children('input:eq(2)').val() + " - " + $(this).children('input:eq(3)').val() + "<br><span>Sign In: </span>" + $(this).children('input:eq(4)').val() + "<br><span>Sign Out: </span>" + $(this).children('input:eq(5)').val() + "";
        $(this).attr('title', title);
    });


    $.widget("ui.tooltip", $.ui.tooltip, {
        options: {
            content: function() {
                return $(this).prop('title');
            }
        }
    });

    $('[rel=tooltip]').tooltip({
        position: {
            my: "center bottom-20",
            at: "center top",
            using: function(position, feedback) {
                $(this).css(position);
                $("<div>")
                     .addClass("arrow")
                     .addClass(feedback.vertical)
                     .addClass(feedback.horizontal)
                     .appendTo(this);
            }
        }
    });


    // Login popup
    var loginSuc = false;
    var logoutSuc = false;

    $('.one span b').text($('#origin li').length);
    $('.two span b').text($('#drop1 li').length);
    $('.three span b').text($('#drop2 li').length);



    dragFun();

    
    $("#drop1").droppable({ accept: "#origin .user", drop: function(event, ui) {
        //console.log(dragged, origRevertDuration, origRevertValue)			
        var validTime = false;
        var dropped = ui.draggable;
        //console.log(dropped.html())	
        dropped.attr('currentObj', true)
        var droppedOn = $("#drop1");
        $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);

        $('#loginPopup #subm').val('Sign in');
        $('#loginPopup .popContent h2').html('<span>Signing in for </span>' + dropped.find('input:eq(0)').val())
        $('#loginPopup .popContent #txtUserID').val(dropped.find('input:eq(1)').val())
        $('#loginPopup .popContent #txtNpte').text('');
        $('#loginPopup .popContent #userPass').text('');
        var path = $.trim(dropped.find('img').attr('src'));
        if (path && path.length > 5) {
            $('#loginPopup .popContent .userThumb').attr('src', dropped.find('img').attr('src'));
        } else {
            $('#loginPopup .popContent .userThumb').attr('src', 'images/defaultUSer.jpg');
        }

        var tim1 = $.trim(dropped.children('input:eq(2)').val()).split(':')
        tim1[1] = tim1[1].substring(tim1[1].length - 2, tim1[1].length);


        var current_tim = $.trim($('.cTime b').text()).split(':')
        current_tim[2] = current_tim[2].substring(current_tim[2].length - 2, current_tim[2].length);

        if (tim1[0] <= current_tim[0] && tim1[1] == current_tim[2]) { validTime = true }
        if (validTime == false) {
            $('#loginPopup .popContent').addClass('error1')
            lateTime = true;
        } else {
            $('#loginPopup .popContent').removeClass('error1');
            lateTime = true;
        }



        $('#loginPopup').show();
        $('#loginPopup #userPass').val('').focus();
        $('#loginPopup #subm').click(function() {
            if ($.trim($('#userPass').val().length) < 1) {
                alert('Enter passcode');
                $('#subm, #loginPopup #cancel, #userPass, #txtNpte').removeAttr('disabled');
                $('#userPass').focus();
                return false;
            } else {
                $('#loginPopup #cancel, #userPass, #txtNpte').attr('disabled', true);
            }
        })



        $('#loginPopup #cancel').click(function() {
            $('#loginPopup').hide();
            var dropped = $("#drop1").find('li[currentobj=true]');
            //console.log(dropped)
            dropped.removeAttr('currentobj')
            //console.log(dropped)		
            var droppedOn = $('#origin');
            $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
            $('#txtUserID, #txtNpte, #userPass').val('')
            $('#loginPopup .popContent .userThumb').attr('src', 'images/defaultUSer.jpg');

            $('.dummyPopup').fadeOut('fast');
            $('.boxC1, .boxC2').removeClass('activeDiv');

        })


    }
    });


    $("#drop2").droppable({ accept: "#drop1 .user", drop: function(event, ui) {
        //console.log(dragged, origRevertDuration, origRevertValue)			
        var dropped = ui.draggable;

        dropped.attr('currentObj', true)
        var droppedOn = $("#drop2");
        $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);

        $('#logoutPopup #login').val('Sign out');
        $('#logoutPopup .popContent h2').html('<span>Signing out for </span>' + dropped.find('input:eq(0)').val())
        $('#logoutPopup .popContent #txtUserID2').val(dropped.find('input:eq(1)').val())
        $('#logoutPopup .popContent #txtNpte2').text('');
        $('#logoutPopup .popContent #userPass2').text('');
        var path = $.trim(dropped.find('img').attr('src'));
        if (path && path.length > 5) {
            $('#logoutPopup .popContent .userThumb').attr('src', dropped.find('img').attr('src'));
        } else {
            $('#logoutPopup .popContent .userThumb').attr('src', 'images/defaultUSer.jpg');
        }

        $('#logoutPopup #userPass').val('').focus();
        $('#hdnLogoutUserID').val(dropped.find('input:eq(5)').val())
        $('#logoutPopup').show();
        $('#logoutPopup #logout').click(function() {
            if ($.trim($('#userPass2').val().length) < 1) {
                alert('Enter passcode');
                $(' #cancel2, #userPass2, #txtNpte2').removeAttr('disabled');
                $('#userPass2').focus();
                return false;
            } else {
                $('#cancel2, #userPass2, #txtNpte2').attr('disabled', true);
            }


        })

        $('#logoutPopup #cancel2').click(function() {

            var dropped = $("#drop2").find('li[currentobj=true]');
            //console.log(dropped)
            dropped.removeAttr('currentobj');
            var droppedOn = $('#drop1');
            $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
            $('#logoutPopup').hide();
            $('#txtUserID2, #txtNpte2, #userPass2').val('');
            $('#loginPopup .popContent .userThumb').attr('src', 'images/defaultUSer.jpg');

            $('.dummyPopup').fadeOut('fast');
            $('.boxC2, .boxC3').removeClass('activeDiv');

        })


    }
    });


})
    
    //function changeSuccess1a(id){ $('#loginID').val(id) }
    
   // function changeSuccess1b(tim){ alert(tim); $('#logintin1').val(tim); }
    
    
    function changeSuccess1(aa){       
        
        if(aa == "1"){
        var id = $('#loginID').val();
        var tim = $('#logintin1').val();
        
        $('#loginPopup').hide();	
            alert('Successfully signin')
            var dropped = $("#drop1").find('li[currentobj=true]');
            if (lateTime == true) {
                dropped.addClass('lateTime');
            } else {
                dropped.addClass('lateTime');
            }
            lateTime = false;
            //console.log(dropped)
            var id1 = '<input type="hidden" value="' + tim + '">';
            id1 += '<input type="hidden" value="' + id + '">';  
            dropped.append(id1);       
            var title= "<br>Signed in: "+tim+"";
            
            //$(this).attr('title',($(this).attr('title'))+title);
           // var title= "<b>Signed in </b><br><span>Name: </span>"+dropped.children('input:eq(0)').val()+"<br><span>Sch_in: </span><br><span>Sig_in: </span>"+dropped.children('input:eq(3)').val()+"";

            var title = "<b>Signed in </b><br><span>Name: </span>" + dropped.children('input:eq(0)').val() + "<br><span>Sch_Time: </span>" + dropped.children('input:eq(2)').val() + " - " + dropped.children('input:eq(3)').val() + "<br><span>Sig_in: </span>" + dropped.children('input:eq(4)').val() + "";
            dropped.attr('title',title);
            dropped.removeAttr('currentobj');
            $('#txtUserID, #txtNpte, #userPass').val('')
            $('#loginPopup .popContent .userThumb').attr('src','images/defaultUSer.jpg');	

            $('.dummyPopup').fadeOut('fast');
            $('.boxC1, .boxC2').removeClass('activeDiv');  
            
            $('.one span b').text($('#origin li').length);
            $('.two span b').text($('#drop1 li').length);
            //$('.three span b').text($('#drop2 li').length);     
            
        }else{
            alert('Something went wrong please try again later');
        }
        $('#subm').removeAttr('disabled');
        dragFun();
       
        
    }
    
    function changeSuccess2(success){
        //console.log(logintin, logouttime);
        var tim = $('#logintin1').val();
        if(success == "1"){
            $('#logoutPopup').hide();	
            alert('Successfully signout')
            var dropped = $("#drop2").find('li[currentobj=true]');
            console.log(dropped.find('input:eq(5)'));
            dropped.find('input:eq(5)').remove();
            
            var id1 = '<input type="hidden" value="'+tim+'">';  
            dropped.append(id1);       
            var title= "<br>Signed in: "+tim+"";
            
            //$(this).attr('title',($(this).attr('title'))+title);
            //var title= "<b>Signed out</b><br><span>Name: </span>"+dropped.children('input:eq(0)').val()+"<br><span>Sch_in: </span><br><span>Sig_in: </span>"+dropped.children('input:eq(3)').val()+"<br><span>Sig_out: </span>"+dropped.children('input:eq(4)').val()+"";
            var title = "<b>Signed out</b><br><span>Name: </span>" + dropped.children('input:eq(0)').val() + "<br><span>Sch_Time: </span>" + dropped.children('input:eq(2)').val() + " - " + dropped.children('input:eq(3)').val() + "<br><span>Sig_in: </span>" + dropped.children('input:eq(4)').val() + "<br><span>Sig_out: </span>" + dropped.children('input:eq(5)').val() + "";
            
            //var title= "<b>Signed in </b><br><span>Name: </span>"+dropped.children('input:eq(0)').val()+"<br><span>Sch_in: </span><br><span>Sig_in: </span>"+dropped.children('input:eq(3)').val()+"";
            dropped.attr('title',title);
            
            dropped.removeAttr('class').addClass('user');//('ui-draggable');
            dropped.removeAttr('currentobj').removeAttr('ui-draggable').removeAttr('style');	
            $('#txtUserID2, #txtNpte2, #userPass2').val('')	
            $('#loginPopup .popContent .userThumb').attr('src','images/defaultUSer.jpg');

            $('.dummyPopup').fadeOut('fast');
            $('.boxC2, .boxC3').removeClass('activeDiv');
            
            //$('.one span b').text($('#origin li').length);
            $('.two span b').text($('#drop1 li').length);
            $('.three span b').text($('#drop2 li').length);
            
            
            
           /*
             $('#drop2 .user').each(function(){
                var title= "<b>Signout</b><br>Name: "+$(this).children('input:eq(0)').val()+"<br>Signin: "+$(this).children('input:eq(2)').val()+"<br>Signout: "+$(this).children('input:eq(3)').val()+"";
                $(this).attr('title',title);
            });
            */
            
        }else{
            alert('Something went wrong please try again later');
        }
        $('#logout').removeAttr('disabled');
        dragFun();
    }