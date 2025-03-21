/**
 * 
 */

function parseSSN(thisObj, birthDate, isSpouse)
{
	var ssn = $(thisObj).val() + "";
	var parsedSSN = $(thisObj).val().split("");
	var year = 17;
	if(isSpouse == true){
	    if ($('#SPOUSE_EGYPTIAN').val() == '1')
	    {
	        if ($(thisObj).val() == '______________')
	        {
				$("[id$='" + birthDate + "']").val("");
				return;
			}

			if (parsedSSN[0] == 1 || parsedSSN[0] == 2 || parsedSSN[0] == 3)
			{
				$("[id$='" + birthDate + "']").attr('onfocus','this.blur();');
				$('.spouseDOB').datepicker("destroy");
				year = year + Number(parsedSSN[0]);
				$("[id$='" + birthDate + "']").val(parsedSSN[3] + parsedSSN[4] + "/" + parsedSSN[5] + parsedSSN[6] + "/" + year + parsedSSN[1] + parsedSSN[2]);
			}
			else
            {
				$("[id$='" + birthDate + "']").val("");
				$("[id$='" + birthDate + "']").attr('onfocus','');
			}
	    }
	    else
	    {
			$("[id$='" + birthDate + "']").val("");
			$("[id$='" + birthDate + "']").attr('onfocus','');
			$('.spouseDOB').datepicker({
				dateFormat : 'dd/mm/yy',
				altFormat : 'dd/mm/yy',
				showOn : "button",
				yearRange : '1912:2120',
				buttonImageOnly : true,
				buttonImage : '../theme/img/calendar.png',
				buttonText : 'Calendar',
				changeMonth : true,
				changeYear : true,
				maxDate : '01/01/2120',
				onClose : function() {
					this.focus();
				}
			});
		}
	}
	else
	{
	    if ($(thisObj).val() == '______________')
	    {
			$("[id$='" + birthDate + "']").val("");
			$(".maleRadioBtn").attr('checked', false);
			$(".femaleRadioBtn").attr('checked', false);
			return;
		}
		if (parsedSSN[0] == 1 || parsedSSN[0] == 2 || parsedSSN[0] == 3)
		{
			$("[id$='" + birthDate + "']").attr('onfocus','this.blur();');
			$('.investorDate').datepicker("destroy");
			year = year + Number(parsedSSN[0]);
			$("[id$='" + birthDate + "']").val(
					parsedSSN[5] + parsedSSN[6] + "/" + parsedSSN[3] + parsedSSN[4]
							+ "/" + year + parsedSSN[1] + parsedSSN[2]);
		}
		else
		{
			$("[id$='" + birthDate + "']").val("");
			$("[id$='" + birthDate + "']").attr('onfocus','');
		}
	}

	if (isSpouse == false)
	{
	    if (parsedSSN[12] % 2 == 1)
	    {
			$(".maleRadioBtn").attr('readOnly','readonly');
			$(".femaleRadioBtn").attr('readOnly','readonly');
			$(".maleRadioBtn").attr('checked', true);
		}
		else if (parsedSSN[12] % 2 == 0)
		{
			$(".maleRadioBtn").attr('readOnly','readonly');
			$(".femaleRadioBtn").attr('readOnly','readonly');
			$(".femaleRadioBtn").attr('checked', true);
		}
	}
}