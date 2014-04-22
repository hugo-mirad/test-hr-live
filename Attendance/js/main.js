var lateTime = false;
var timeInt = 300000;  //300000;

var userLoc1 = 0;
var userLoc2 = 0;

var Duplicate = false;

var tt = self.setInterval('pageRefresh()', timeInt);

function pageRefresh() {
    //console.log('Refe')
    window.location.reload();
}


function dragFun() {
    $("#origin .user").draggable({
        cursor: "Move",
        revert: "invalid",
        drag: function() {
            $('.dummyPopup').fadeIn(100);
            $('.boxC1, .boxC2, .boxC4').addClass('activeDiv');
            $('.boxC1').css('z-index', '32');
            $('.boxC2, .boxC4').css('z-index', '31');
        },
        stop: function() {
            $('.dummyPopup').fadeOut(50);
            $('.boxC1, .boxC2, .boxC4').removeClass('activeDiv');
        }

    });
    /*
    $("#drop1 .user").draggable({
        cursor: "Move",
        revert: "invalid",
        drag: function() {
            $('.dummyPopup').fadeIn(100);
            $('.boxC2, .boxC3').addClass('activeDiv');
            $('.boxC2').css('z-index', '31');
            $('.boxC3').css('z-index', '30');
        },
        stop: function() {
            $('.dummyPopup').fadeOut(50);
            $('.boxC2, .boxC3').removeClass('activeDiv');
        }
    });
    */
    $("#drop1 .user").draggable({
        cursor: "Move",
        revert: "invalid",
        drag: function() {
            $('.dummyPopup').fadeIn(100);
            $('.boxC2, .boxC3, .boxC4').addClass('activeDiv');
            $('.boxC2').css('z-index', '31');
            $('.boxC3, .boxC4').css('z-index', '30');
        },
        stop: function() {
            $('.dummyPopup').fadeOut(50);
            $('.boxC2, .boxC3, .boxC4').removeClass('activeDiv');
        }
    });
    
    
   
    $("#drop2 .user").draggable({
        cursor: "Move",
        revert: "invalid",
        drag: function() {
            $('.dummyPopup').fadeIn(100);
            $('.boxC2, .boxC3, .boxC4').addClass('activeDiv');
            $('.boxC2').css('z-index', '30');
            $('.boxC3, .boxC4').css('z-index', '31');
        },
        stop: function() {
            $('.dummyPopup').fadeOut(50);
            $('.boxC2, .boxC3, .boxC4').removeClass('activeDiv');
        }
    });
    
     $("#drop3 .user").draggable({
        cursor: "Move",
        revert: "invalid",
        drag: function() {
            $('.dummyPopup').fadeIn(100);
            $('.boxC2, .boxC4').addClass('activeDiv');
            $('.boxC2').css('z-index', '30');
            $('.boxC4').css('z-index', '31');
        },
        stop: function() {
            $('.dummyPopup').fadeOut(50);
            $('.boxC2,.boxC4').removeClass('activeDiv');
        }
    });
    
    //$("#origin").sortable();
    //$("#drop1").sortable();
    //$("#drop2").sortable();

}

$(window).load(function() {
//    $('.boxC4').height($('.boxC2').height() - ($('.boxC1').height()+10));
    
    $('.user img').each(function() {

        if ($(this).attr('src').length <= 5) {
            $(this).hide();
            if ($(this).parent().children('input:eq(0)').val().length > 14) {
                var Name = $.trim($(this).parent().children('input:eq(0)').val()).substring(0, 13)+'..';
                
            } else {
                var Name = $.trim($(this).parent().children('input:eq(0)').val())
            }

            $(this).parent().append('<div class="wrapper"><span class="name">' + Name + '</span></div>')
        } else {

                if ($(this).parent().children('input:eq(0)').val().length > 14) {
                    var Name = $.trim($(this).parent().children('input:eq(0)').val()).substring(0, 13) + '..';

                } else {
                    var Name = $.trim($(this).parent().children('input:eq(0)').val())
                }

                var strip = '<div class="strip">' + Name + '</div>';
            $(this).parent().append(strip);
        }

        //$(this).children('div.wrapper').remove();
    });
});

$(function() {

// --------------------------------------- Leave form Defaults and events  Start ------------------
        $('#txtFromDt').live('focus', function(){
             if($(this).hasClass('hasDatepicker')){
                $(this).datepicker( "destroy" );
                $(this).removeClass("hasDatepicker")
            }  
            
             $('#txtFromDt').datepicker({	        
	            minDate: leaveSe,
	            maxDate: 90
            });            
            
        })       
        
       $('#txtFromDt').live('change', function(){
            $('#txtToDt').val('');           
       });
       
       
       $('#txtToDt').live('focus', function(){
             if($(this).hasClass('hasDatepicker')){
                $(this).datepicker( "destroy" );
                $(this).removeClass("hasDatepicker")
             }  
            
            $('#txtToDt').datepicker({	        
                minDate: $('#txtFromDt').val(),
                maxDate: 90
            });
            
        }) 
        
        
//         $('input[name=LeaveIdentity]:radio').live('change', function(){
//            $('#leaveEmp #txtLeaveEmpID').val('');
//            
//            if($('#rdSelf').is(':checked')){           
//                $('#leaveEmp').hide();                
//            }else{
//                 $('#leaveEmp').show();                
//            }
//        });
        
        
        // --------------------------------------- Leave form Defaults and events  End ------------------
        
        
        

  
    $('.user, .refresh').live('mouseenter', function() {
        clearInterval(tt);
        //console.log('Stop Timer');
    });

    $('.user, .refresh').live('mouseleave', function() {
        tt = self.setInterval('pageRefresh()', timeInt);
        //console.log('Start Timer');
    })


    $('.user').dblclick(function() {
       // var man = $(this).children('h3').find('span.degi').attr('value');
        //if (man == 'Manager') {
            //alert('Manager');
            var dropped = $(this);          
            $('#managerPopup1 h2').html( dropped.find('input:eq(0)').val())
            $('#managerPopup1 #lblErrorM').text('');
            $('#managerPopup1 #txtUserIdM, #managerPopup1 #txtLocationM, #managerPopup1 #txtPasswordM').val('');
            
            var path = $.trim(dropped.find('img').attr('src'));
         
             var userid = $.trim(dropped.children('input:eq(1)').val());
             $('#hdnManageUserID').val(userid);
            if (path && path.length > 5) {
                $('#managerPopup1 .userThumb').attr('src', dropped.find('img').attr('src'));
                
            } else {
                $('managerPopup1 .userThumb').attr('src', 'images/defaultUSer.jpg');
            }

            $find('mdlManagerPopup1').show();
            $('#managerPopup1 #txtUserId').focus();
        //}
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
    $('.four span b').text($('#drop3 li').length);



    dragFun();


    $("#drop1").droppable({ accept: "#origin .user, #drop2 .user, #drop3 .user", drop: function(event, ui) {
        			
        var validTime = false;
        var dropped = ui.draggable;
        //console.log(dropped.parent().attr('id'));
        droppedParent2 = dropped.parent().attr('id');
        userLoc1 = dropped.index();
        dropped.attr('currentObj', true)
        var droppedOn = $("#"+droppedParent2+"");
        $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
        
        
        if(droppedParent2 =="origin" || droppedParent2 =="drop3"){        
            $('#subm').val('Sign in');
            $(' .popContent h2').html('<span>Signing in for </span>' + dropped.find('input:eq(0)').val())
        }else{
            $(' .popContent h2').html('<span>Re-signing in for </span>' + dropped.find('input:eq(0)').val());
            $('#subm').val('Re-sign in');
            $('#ScResignIn').val('True');
        }
        $('#lblLIError').text('');
        $('.popContent #txtUserID').val(dropped.find('input:eq(1)').val())
        $('.popContent #txtNpte').text('');
        $('.popContent #userPass').text('');
        var path = $.trim(dropped.find('img').attr('src'));
        if (path && path.length > 5) {
            $('.popContent .userThumb').attr('src', dropped.find('img').attr('src'));
        } else {
            $('.popContent .userThumb').attr('src', 'images/defaultUSer.jpg');
        }

       

        if (dropped.hasClass('lateTime')) {
            validTime = false;
        } else {
            validTime = true;
        }

        if (validTime == false) {
            $('#loginPopup').addClass('error1')
            lateTime = true;
        } else {
            $('#loginPopup').removeClass('error1');
            lateTime = true;
        }



        //$('#loginPopup, #mdlLoginpopup').show();
        $find('mdlLoginpopup').show();
        //$find('mdlLoginpopup').show();

        $('#userPass').val('').focus();
        $('#subm').click(function() {
            if ($.trim($('#userPass').val().length) < 1) {
                alert('Enter passcode');
                $('#subm, #cancel, #userPass, #txtNpte').removeAttr('disabled');
                $('#userPass').focus();
                return false;
            }
            else {
                //$('#cancel, #userPass, #txtNpte').attr('disabled', true);
            }
        })



        $('#cancel').click(function() {
            $('#loginPopup').hide();
            
            var dropped = $("#"+droppedParent2+"").find('li[currentobj=true]');
            //console.log(dropped)
            dropped.removeAttr('currentobj')
            //console.log(dropped)	
            /*
            if(droppedParent2 =="origin"){      	
                var droppedOn = $('#origin');
            }else{
                var droppedOn = $('#drop2');
            }
            */
            var droppedOn = $('#'+droppedParent2);
            
            var obj = $(dropped).detach().css({ top: 0, left: 0 }); //.appendTo(droppedOn);
            //droppedOn.children('li:eq(' + (userLoc1 - 1) + ')').after(obj);
            //console.log('userLoc1: ' + userLoc1)
            if (userLoc1 == 0) {
                droppedOn.prepend(obj);
            } else {
                droppedOn.children('li:eq(' + (userLoc1 - 1) + ')').after(obj);
            }


            $('#txtUserID, #txtNpte, #userPass').val('')
            $('.popContent .userThumb').attr('src', 'images/defaultUSer.jpg');

            $('.dummyPopup').fadeOut('fast');
            $('.boxC1, .boxC2').removeClass('activeDiv');
            //alert(Duplicate);
            if ($('#Duplicate').val() == 'true') {
                pageRefresh();
            }


        })


    }
    });


    $("#drop2").droppable({ accept: "#drop1 .user", drop: function(event, ui) {
        //console.log(dragged, origRevertDuration, origRevertValue)			
        var dropped = ui.draggable;
        userLoc2 = dropped.index();
        //console.log(userLoc2);
        dropped.attr('currentObj', true)
        var droppedOn = $("#drop2");
        $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);

        $('#login').val('Sign out');
        $('.popContent h2').html('<span>Signing out for </span>' + dropped.find('input:eq(0)').val())
        $('#lblLOError').text('');
        $('.popContent #txtUserID2').val(dropped.find('input:eq(1)').val())
        $('.popContent #txtNpte2').text('');
        $('.popContent #userPass2').text('');
        var path = $.trim(dropped.find('img').attr('src'));
        if (path && path.length > 5) {
            $(' .popContent .userThumb').attr('src', dropped.find('img').attr('src'));
        } else {
            $(' .popContent .userThumb').attr('src', 'images/defaultUSer.jpg');
        }


        var tim1 = $.trim(dropped.children('input:eq(3)').val()).split(':')
        var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
        tim1[1] = tim1[1].substring(0, 2);

        var current_tim = $.trim($('.cTime b').text()).split(':')
        var current_timAMPM = current_tim[2].substring(current_tim[2].length - 2, current_tim[2].length);
        
        
        // Assigned time
            if(tim1[0] == 12){
                var Stl =  tim1[1]*60;
            }else{
                var Stl =  tim1[0]*3600 + tim1[1]*60;
            }
            
            // Actual Clock
            if(current_tim[0] == 12){
                var St2 =  current_tim[1]*60;
            }else{
                var St2 =  current_tim[0]*3600 + current_tim[1]*60;
            }          
            
            
            if(tim1AMPM=='PM'){
                Stl += 43200;
            }
            if(current_timAMPM=='PM' ){
                St2 += 43200;
            }
        
        
        
        if (Stl < St2 && tim1AMPM == current_timAMPM) { validTime = true } else { validTime = false }               
       
        if (validTime == false) {
            $('#logoutPopup').addClass('error1')
            lateTime = true;
        } else {
            $('#logoutPopup').removeClass('error1');
            lateTime = true;
        }


        $find('mdlLogoutPopup').show();
        $(' #userPass2').val('').focus();

        $('#hdnLogoutUserID').val(dropped.find('input:eq(5)').val())
        //  $('#logoutPopup').show();



        $('#logout').click(function() {
            if ($.trim($('#userPass2').val().length) < 1) {
                alert('Enter passcode');
                $('#cancel2, #userPass2, #txtNpte2').removeAttr('disabled');
                $('#userPass2').focus();
                return false;
            } else {
                //$('#cancel2, #userPass2, #txtNpte2').attr('disabled', true);
            }

            if ($('#Duplicate').val() == 'true') {
                pageRefresh();
            }


        })

        $(' #cancel2').click(function() {

            var dropped = $("#drop2").find('li[currentobj=true]');
            //console.log(dropped)
            dropped.removeAttr('currentobj');
            var droppedOn = $('#drop1');
            //$(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);

            var obj = $(dropped).detach().css({ top: 0, left: 0 }); //.appendTo(droppedOn);

            if (userLoc2 == 0) {
                droppedOn.prepend(obj);
            } else {
                droppedOn.children('li:eq(' + (userLoc2 - 1) + ')').after(obj);
            }


            $('').hide();
            $('#txtUserID2, #txtNpte2, #userPass2').val('');
            $('.popContent .userThumb').attr('src', 'images/defaultUSer.jpg');

            $('.dummyPopup').fadeOut('fast');
            $('.boxC2, .boxC3').removeClass('activeDiv');

        })


    }
    });
    
    
    
    $("#drop3").droppable({ accept: "#origin .user, #drop1 .user, #drop2 .user", drop: function(event, ui) {
        //console.log(dragged, origRevertDuration, origRevertValue)			
        event.preventDefault();
        var validTime = false;
        var dropped = ui.draggable;
        droppedParent4 = dropped.parent().attr('id');
        //console.log(droppedParent);
        userLoc2 = dropped.index();       
        
        dropped.attr('currentObj', true)
        var droppedOn = $("#drop3");
        $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
        
        
        
        
         $('#dvApplyingLeave h2').html('<span>APPLYING LEAVE FOR </span>' + dropped.find('input:eq(0)').val())
       
        $('#dvApplyingLeave #hdnLeaveUserID').val(dropped.find('input:eq(1)').val())
        
        var path = $.trim(dropped.find('img').attr('src'));
        //console.log(path);
        if (path && path.length > 5) {
            $('#dvApplyingLeave .userThumb').attr('src', path);
        } else {
            $('#dvApplyingLeave .userThumb').attr('src', 'images/defaultUSer.jpg');
        }        
        
         
        if(droppedParent4 == 'origin'){        
            var CurnDate = new Date($.trim($('#lblDate2').text()));
            CurnDate = CurnDate.getMonth()+1+"/"+CurnDate.getDate()+"/"+CurnDate.getFullYear();     
             leaveSe=CurnDate;
        }else{
        
            var CurnDate = new Date($.trim($('#lblDate2').text()));    
            CurnDate.setDate(CurnDate.getDate()+1);
            CurnDate = CurnDate.getMonth()+1+"/"+CurnDate.getDate()+"/"+CurnDate.getFullYear(); 
            leaveSe=CurnDate;
        }
        
        
        
        $('#txtFromDt').val(CurnDate);
         $('#txtToDt').val(CurnDate);
         $('#lblLeaveError').val('')
        $find('mdlApplyingLeave').show();
        
        
        
        
        
            
        
    }
    });
   
   
   
   
    // LeaveCancel Click event 
        $('#LeaveCancel').click(function(event){         
           event.preventDefault();
           event.stopPropagation();
           var dropped = $("#drop3").find('li[currentobj=true]');
            //console.log(dropped)
            dropped.removeAttr('currentobj');          
            
                droppedOn = $('#'+droppedParent4); 
            
            
            //console.log(droppedParent);
            //$(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);

            var obj = $(dropped).detach().css({ top: 0, left: 0 }); //.appendTo(droppedOn);

            if (userLoc2 == 0) {
                droppedOn.prepend(obj);
            } else {
                droppedOn.children('li:eq(' + (userLoc2 - 1) + ')').after(obj);
            }           
            
            $('.popContent .userThumb').attr('src', 'images/defaultUSer.jpg');            
            
            $('#txtFromDt, #txtToDt, #txtLeaveEmpID, #txtLeavePassCode, #txtReason').val('');            
            $('#rdSelf').attr('checked',true).change();            
            $('#rdOther').attr('checked',false);            
            $find('mdlApplyingLeave').hide();              
            
            $('.dummyPopup').fadeOut('fast');
            $('.boxC1, .boxC2, .boxC4').removeClass('activeDiv');
            
            
            
        });  
   


})

var droppedParent1;
var droppedParent2;
var droppedParent3;
var droppedParent4;

function Duplicate() {
    Duplicate = true;
    alert(Duplicate);
}


//function changeSuccess1a(id){ $('#loginID').val(id) }

// function changeSuccess1b(tim){ alert(tim); $('#logintin1').val(tim); }


function changeSuccess1(aa) {
    
    
    
    if (aa == "1") {
        
        /*
        
        var id = $('#loginID').val();
        var tim = $('#logintin1').val();

        $('#loginPopup').hide();
        //alert('Successfully signin')
        var dropped = $("#drop1").find('li[currentobj=true]');

        //console.log(dropped)
        var id1 = '<input type="hidden" value="' + tim + '">';
        id1 += '<input type="hidden" value="' + id + '">';
        dropped.append(id1);
        var title = "<br>Signed in: " + tim + "";

        //$(this).attr('title',($(this).attr('title'))+title);
        // var title= "<b>Signed in </b><br><span>Name: </span>"+dropped.children('input:eq(0)').val()+"<br><span>Sch_in: </span><br><span>Sig_in: </span>"+dropped.children('input:eq(3)').val()+"";

        var title = "<span class='center'>" + dropped.children('input:eq(0)').val() + "</span><br><span>schedule: </span>" + dropped.children('input:eq(2)').val() + " - " + dropped.children('input:eq(3)').val() + "<br><span>Sign In: </span>" + dropped.children('input:eq(4)').val() + "";
        dropped.attr('title', title);
        dropped.removeAttr('currentobj');

        if (lateTime == true) {
            dropped.addClass('lateTime');
        } else {
            dropped.addClass('lateTime');
        }
        lateTime = false;

        $('#txtUserID, #txtNpte, #userPass').val('')
        $('.popContent .userThumb').attr('src', 'images/defaultUSer.jpg');

        $('.dummyPopup').fadeOut('fast');
        $('.boxC1, .boxC2').removeClass('activeDiv');

        $('.one span b').text($('#origin li').length);
        $('.two span b').text($('#drop1 li').length);
        //$('.three span b').text($('#drop2 li').length); 
        $find('mdlLoginpopup').show();
        dragFun()


        var tim1 = $.trim(dropped.children('input:eq(2)').val()).split(':')
        var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
        tim1[1] = tim1[1].substring(0, 2);

        var sp = $.trim(dropped.children('input:eq(4)').val()).split(' ')
        var current_tim = sp[1].split(':')
        var current_timAMPM = sp[2];
        //current_tim[1] = current_tim[1].substring(0, 2);

        //if (parseInt(tim1[0]) >= parseInt(current_tim[0]) && parseInt(tim1[1]) >= parseInt(current_tim[1]) && tim1AMPM == current_timAMPM) { validTime2 = true } else { validTime2 = false }
        var validTime2 = false;
        var Stl = tim1[0] * 3600 + tim1[1] * 60;
        var St2 = current_tim[0] * 3600 + current_tim[1] * 60;
        if (Stl > St2 && tim1AMPM == current_timAMPM) { validTime2 = true } else { validTime2 = false }

        if (validTime2 == false) {
            dropped.addClass('lateTime');
        } else {
            dropped.removeClass('lateTime');
        }

           */

      window.location.reload();

    } else {
        //alert('Something went wrong please try again later');
        //alert('Wrong password ');

        //var dropped = $("#drop1").find('li[currentobj=true]');            
        //dropped.removeAttr('currentobj');           
        //var droppedOn = $('#origin');
        //$(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);



    }
    $('#subm').removeAttr('disabled');
    $(' #userPass').val('').focus();    


}

function changeSuccess2(success) {

    var tim = $('#logintin1').val();
    //console.log(tim)
    if (success == "1") {
        /*
        //$('').hide();	
        //alert('Successfully signout')
        var dropped = $("#drop2").find('li[currentobj=true]');
        //console.log(dropped.find('input:eq(5)'));
        dropped.find('input:eq(5)').remove();

        var id1 = '<input type="hidden" value="' + tim + '">';
        dropped.append(id1);
        //var title= "<br>Signed in: "+tim+"";

        //$(this).attr('title',($(this).attr('title'))+title);
        //var title= "<b>Signed out</b><br><span>Name: </span>"+dropped.children('input:eq(0)').val()+"<br><span>Sch_in: </span><br><span>Sig_in: </span>"+dropped.children('input:eq(3)').val()+"<br><span>Sig_out: </span>"+dropped.children('input:eq(4)').val()+"";
        var title = "<span class='center'>" + dropped.children('input:eq(0)').val() + "</span><br><span>schedule: </span>" + dropped.children('input:eq(2)').val() + " - " + dropped.children('input:eq(3)').val() + "<br><span>Sign In: </span>" + dropped.children('input:eq(4)').val() + "<br><span>Sign Out: </span>" + dropped.children('input:eq(5)').val() + "";

        //var title= "<b>Signed in </b><br><span>Name: </span>"+dropped.children('input:eq(0)').val()+"<br><span>Sch_in: </span><br><span>Sig_in: </span>"+dropped.children('input:eq(3)').val()+"";
        dropped.attr('title', title);

        dropped.removeAttr('class').addClass('user'); //('ui-draggable');
        dropped.removeAttr('currentobj').removeAttr('ui-draggable').removeAttr('style');
        $('#txtUserID2, #txtNpte2, #userPass2').val('')
        $('.popContent .userThumb').attr('src', 'images/defaultUSer.jpg');

        $('.dummyPopup').fadeOut('fast');
        $('.boxC2, .boxC3').removeClass('activeDiv');

        //$('.one span b').text($('#origin li').length);
        $('.two span b').text($('#drop1 li').length);
        $('.three span b').text($('#drop2 li').length);

        $find('mdlLogoutPopup').hide();
        dragFun();


        var tim1 = $.trim(dropped.children('input:eq(3)').val()).split(':')
        var tim1AMPM = tim1[1].substring(tim1[1].length - 2, tim1[1].length);
        tim1[1] = tim1[1].substring(0, 2);

        var sp = $.trim(dropped.children('input:eq(5)').val()).split(' ')
        var current_tim = sp[1].split(':')
        var current_timAMPM = sp[2];
        //current_tim[1] = current_tim[1].substring(0, 2);

        //if (parseInt(tim1[0]) >= parseInt(current_tim[0]) && parseInt(tim1[1]) >= parseInt(current_tim[1]) && tim1AMPM == current_timAMPM) { validTime2 = true } else { validTime2 = false }

        var Stl = tim1[0] * 3600 + tim1[1] * 60;
        var St2 = current_tim[0] * 3600 + current_tim[1] * 60;
        if (Stl > St2 && tim1AMPM == current_timAMPM) { validTime2 = true } else { validTime2 = false }

        if (validTime2 == false) {
            dropped.addClass('lateTime');
        } else {
            dropped.removeClass('lateTime');
        }
        */
      window.location.reload();
        

    } else {
        //alert('Something went wrong please try again later');

        //console.log(dropped)

        //var dropped = $("#drop2").find('li[currentobj=true]');
        //dropped.removeAttr('currentobj');
        //var droppedOn = $('#drop1');
        //$(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);

        //$('#cancel2, #userPass2, #txtNpte2').removeAttr('disabled');
    }
    $('#logout').removeAttr('disabled');    
    $(' #userPass2').val('').focus();


}