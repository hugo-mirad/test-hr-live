var se = new Date($.trim($('#lblDate2').text()));

var month = new Array();
month[0] = "Jan";
month[1] = "Feb";
month[2] = "Mar";
month[3] = "Apr";
month[4] = "May";
month[5] = "Jun";
month[6] = "Jul";
month[7] = "Aug";
month[8] = "Sep";
month[9] = "Oct";
month[10] = "Nov";
month[11] = "Dec";
var mon = month[se.getMonth()];

var dayname = new Array();
dayname[0] = "Sun";
dayname[1] = "Mon";
dayname[2] = "Tue";
dayname[3] = "Wed";
dayname[4] = "Thu";
dayname[5] = "Fri";
dayname[6] = "Sat";


var day = se.getDate();
var dayee = dayname[se.getDay()];
var year = se.getFullYear();
bindDate();

function bindDate() {
    $('.cDate').text('Date: ' + day + ' ' + mon + ', ' + year + ' ' + dayee);
}


//var serverTime2 = <%= DateTime.Now.TimeOfDay.TotalSeconds %>;

//console.log(serverTime2);
// var serverTime = <% %>$.trim($('#lblDate').text());

var serverTime = $.trim($('#lblDate').text());

var serverOffset = serverTime - getClientTime();

function getClientTime() {
    var time = new Date($.trim($('#lblDate2').text()));
    // console.log(time.getMinutes());
    return (time.getHours() * 60 * 60) + (time.getMinutes() * 60) + (time.getSeconds());
}


var first = true;

//console.log(serverTime)
//console.log(serverOffset)

var increment = 0;

//  ---------------------------

function updateClock() {
    // lblTime lblDate

    var serverTime = getClientTime() + increment;
    increment++;

    var currentHours = Math.floor(serverTime / 60 / 60);
    var currentMinutes = Math.floor((serverTime / 60) % (currentHours * 60));   
    var currentSeconds = Math.floor(serverTime % 60);

   
    // Pad the minutes and seconds with leading zeros, if required
    currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
    currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;

    // Choose either "AM" or "PM" as appropriate
    var timeOfDay = (currentHours < 12) ? "AM" : "PM";

    // Convert the hours component to 12-hour format if needed
    currentHours = (currentHours > 12) ? currentHours - 12 : currentHours;

    // Convert an hours component of "0" to "12"
    currentHours = (currentHours == 0) ? 12 : currentHours;

    // Compose the string for display
    var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;


    $(".cTime b").html(currentTimeString);
}

$(function () {
    setInterval('updateClock()', 1000);
});

// Loader 

function showSpinner() {
    $('#spinner').show();
}
function hideSpinner() {
    $('#spinner').hide();
}
