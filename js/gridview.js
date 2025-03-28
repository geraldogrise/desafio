function InsertInline(icon)
{
	var tableId = $(icon).closest('table').attr('id');
	var row = $(icon).closest('tr');
	$('#'+tableId).find('tr.aux').show();
	$('#'+tableId).find('tr.aux').addClass('adding');
	DisabledButtons(tableId);
}

function EditInline(icon)
{
	var tableId = $(icon).closest('table').attr('id');
	var row = $(icon).closest('tr');
	$(row).addClass('hide');
	var rowHtml = $("#"+tableId).find('.aux').html();
	DisabledButtons(tableId);
	$(row).after('<tr class="editting">'+rowHtml+"</tr>");
	LoadInlineData(icon);
	
}

function DeleteInline(icon)
{
	$(icon).closest('tr').remove();
}

function CancelInline(icon)
{
	var tableId = $(icon).closest('table').attr('id');
	var row = $(icon).closest('tr');
	if($(row).hasClass('adding'))
	{
		$(row).hide();
	}
	else
	{
		$(icon).closest('tr').remove();
		$('#'+tableId).find('tr.hide').removeClass('hide');
	}
	EnabledButtons(tableId);
}


function LoadInlineData(icon){
	var tableId = $(icon).closest('table').attr('id');
	var row = $(icon).closest('tr');
	var values = [];
	$(row).find('td:not(.action)').each(function(){
		value = $(this).html();
		if($(this).attr('value') != undefined)
		{
			value = $(this).attr('value');
		}
		values.push(value);
	});
	
	var count = 0;
	$('#'+tableId).find('tr.editting').find('td:not(.action)').each(function(){
	    $(this).find('input').val(values[count]);
		if($(this).find('select option').length > 0){
			$(this).find('select').val(values[count]);
			$(this).find('select option').each(function() {
				if($(this).val() == values[count]) {
				   $(this).prop("selected", true);
				}
			});
		}
		count++;
	});
}

function DisabledButtons(tableId){
	$('#'+tableId).find('tr:not(.aux)').find('.ibtn').addClass('disabled');
}

function EnabledButtons(tableId){
	$('#'+tableId).find('tr:not(.aux)').find('.ibtn').removeClass('disabled');
}