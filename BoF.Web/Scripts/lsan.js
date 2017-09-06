$(document).ready(function () {
	PredCal();
});

$('#LoanAmount,#SanctionPercentage,#SanctionTerms,#RepaymentAmount,#FileCharge').keyup(function () {
	PredCal();
});
function PredCal() {
	var $pln = $('#srtplnnam').text();
	var $sAmt = parseFloat($('#LoanAmount').val()) || 0;
	var $trm = parseFloat($('#SanctionTerms').val()) || 0;
	var $per = $('#SanctionPercentage').val() || 0;
	var $frequency = $('#Frequency').text();
	var $repaymnt = parseFloat($('#RepaymentAmount').val()) || 0;
	var $filecharge = parseFloat($('#FileCharge').val()) || 0;
	if ($sAmt > 0 && $repaymnt > 0) {
		switch ($pln) {
			case "OM":
				{
					////Returnable Amount
					//var omrt = ($sAmt + ($sAmt * $per / 100)).toFixed(2);
					//$('#ReturnableAmount').val(omrt);
					////Limit Amount
					//$('.limit').val(omrt);

					//var $mRet = (omrt / $trm).toFixed(2);
					//var $mInt = ((omrt - $sAmt) / $trm).toFixed(2);
					//$mRet = (parseFloat($mRet) + parseFloat($mInt)).toFixed(2);

					////Monthly Interest
					//$('#MonthlyInterest').val($mInt);
					////Monthly Return
					//$('#MonthlyReturn').val($mRet);

					//var $repay = 0;
					//if ($frequency == 'Weekly') {
					//	$repay = ($mRet / 4);
					//}
					//else if ($frequency == 'Fortnightly') {
					//	$repay = ($mRet / 2);
					//}
					//else if ($frequency == 'Monthly') {
					//	$repay = ($mRet / $trm);
					//}
					//$('#RepaymentAmount').val($repay);
				}
				break;
			case "IP":
			    {
                   
					//Returnable Amount
			        var omrt = ($sAmt + (($sAmt * $per) / (100))).toFixed(2);
			        var omrt1 = ($sAmt + $filecharge+(($sAmt * $per) / (100))).toFixed(2);
					$('#ReturnableAmount').val(omrt);
					//Limit Amount
					$('.limit').val(omrt);

					var $repay = 0;
					if ($frequency == 'Weekly') {
					    $repay = ($repaymnt * 4);
					}
					else if ($frequency == 'Fortnightly') {
					    $repay = ($repaymnt * 2);
					}
					else if ($frequency == 'Monthly') {
					    $repay = $repaymnt;
					}
					//$('#RepaymentAmount').val($repay);
			        //var term =( omrt / $repay)+1;
					var $term = Math.ceil((omrt / $repay));
					$('#SanctionTerms').val($term.toFixed(0));
					$('#RequestTerms').val($term.toFixed(0));
					//$('#MonthlyReturn').val($repay).toFixed(2);

                //    //monthly Repayment
				//	var $repay = 0;
				//	if ($frequency == 'Weekly') {
				//	    $repay = ($repaymnt * 4);
				//	}
				//	else if ($frequency == 'Fortnightly') {
				//	    $repay = ($repaymnt * 2);
				//	}
				//	else if ($frequency == 'Monthly') {
				//	    $repay = $repaymnt;

				//	    $('#MonthlyReturn').val($repay).toFixed(2);
				//    //k	var $mRet = (omrt / $trm).toFixed(2);
				
				//	    var term = $sAmt / $repay.toFixed(2);
				////	    $SanctionTerms = term;
					var $mInt = ((omrt - $sAmt) / $term).toFixed(2);
				//	var $mInt = ((omrt - $sAmt) / $trm).toFixed(2);
				//	//$mRet = (parseFloat(omrt) / $trm).toFixed(2);

					//Monthly Interest
					$('#MonthlyInterest').val($mInt);
					//Monthly Return
					$('#MonthlyReturn').val($repay);

					
				//	}
					//$('#RepaymentAmount').val($repay);
				}
				break;
			case "MP":
				{
					////Returnable Amount
					//var omrt = ($sAmt + (($sAmt * $per ) / (100))).toFixed(2);
					//$('#ReturnableAmount').val(omrt);
					////Limit Amount
					//$('.limit').val(omrt).toFixed(2);

					//var $mRet = (omrt / $trm).toFixed(2);

					//var $mInt = ((omrt - $sAmt) / $trm).toFixed(2);

					//$mRet = (parseFloat(omrt) / $trm).toFixed(2);

					
					////Monthly Interest
					//$('#MonthlyInterest').val($mInt);

					////Monthly Return
					//$('#MonthlyReturn').val($mRet);

					//var $repay = 0;
					//if ($frequency == 'Weekly') {
					//	$repay = ($mRet / 4);
					//}
					//else if ($frequency == 'Fortnightly') {
					//	$repay = ($mRet / 2);
					//}
					//else if ($frequency == 'Monthly') {
					//	$repay = $mRet;
					//}
					//$('#RepaymentAmount').val($repay);
				}
				break;
			default:
		}
	}
}

$('.rtrnbleAmt').keyup(function () {
	var $pln = $('#srtplnnam').text();
	var $sAmt = parseFloat($('#LoanAmount').val()) || 0;
	var $trm = parseFloat($('#SanctionTerms').val()) || 0;
	//var $per = $('#SanctionPercentage').val() || 0;
	var $ret = parseFloat($('#ReturnableAmount').val()) || 0;
	//Returnable Amount
	var dif = ($ret - $sAmt) / $trm;

	var per = dif * 100 / $sAmt;
//	$('#SanctionPercentage').val(per);

});
$('.rtrnbleAmt').change(function () {

	PredCal();
});

$('.mnthlyrtrn').keyup(function () {
	var $pln = $('#srtplnnam').text();
	var $sAmt = parseFloat($('#LoanAmount').val()) || 0;
	var $trm = parseFloat($('#SanctionTerms').val()) || 0;
	//var $per = $('#SanctionPercentage').val() || 0;
	//var $ret = parseInt($('#ReturnableAmount').val()) || 0;
	var $mr = parseFloat($('#MonthlyReturn').val()) || 0;
	//Returnable Amount
	var per = ((($mr * $trm) - $sAmt) * 100) / ($sAmt * $trm);
	//var per = dif * 100 / $sAmt;
//	$('#SanctionPercentage').val(per);

});
//$('.mnthlyrtrn').change(function () {

//	PredCal();
//});
