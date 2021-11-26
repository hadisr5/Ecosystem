


function countDownTimer(time) {

	var ts = (new Date(time)).getTime();
	var timerDone = false;
		
	$('#countdown').countdown({
		timestamp	: ts,
		callback: function (days, hours, minutes, seconds) {
			if (timerDone == false && minutes == 0 && seconds == 0) {
				$('#submit').attr("disabled", "disabled");
				swal("پایان آزمون", "زمان آزمون به پایان رسید.", "info");
				timerDone = true;
			}
		}
	});
	
}