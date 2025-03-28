function combo(el) {
  div = findAncestor(el,'dropdown');
  element = div.getElementsByClassName('dropdown-content');
  if(!element[0].classList.contains('show'))
	  element[0].classList.toggle("show");
  
}

function filterFunction(el) {
  var input, filter, ul, li, a, i;
  div = findAncestor(el,'dropdown');
  input = div.getElementsByClassName('form-search');
  filter = input[0].value.toUpperCase();
  div = el.parentElement.parentElement;
  reloadComboSearchData(div,filter);
}

function setComboSearchValue(el){
  div = findAncestor(el,'dropdown');
  comboText = div.getElementsByClassName('combo-text');
  comboText[0].value = el.innerText;
  comboValue = div.getElementsByClassName('combo-value');
  comboValue[0].value = el.rel;
  el.parentElement.classList.toggle("show");
  inputSearch = div.getElementsByClassName('form-search');
  inputSearch[0].value = "";
  reloadComboSearchData(el.parentElement,"");
  
}

function setComboBoxValue(el){
	div = findAncestor(el,'dropdown');
	comboText = div.getElementsByClassName('combo-text');
	comboText[0].value = el.innerText;
	comboValue = div.getElementsByClassName('combo-value');
	comboValue[0].value = el.innerText;
	content = findAncestor(el,'dropdown-content');
    content.classList.toggle("show");
}

function findAncestor (el, cls) {
    while ((el = el.parentElement) && !el.classList.contains(cls)){
	};
    return el;
}

function setComboTreeValue(el){
	div = findAncestor(el,'dropdown');
	content = findAncestor(el,'dropdown-content');
	comboText =  div.getElementsByClassName('combo-text');
	comboText[0].value = el.innerText
	content.classList.toggle("show");
}

function setComboTreeGridValue(el){
	div = findAncestor(el,'dropdown');
	content = findAncestor(el,'dropdown-content');
	comboText =  div.getElementsByClassName('combo-text');
	comboText[0].value = el.getAttribute('text');
	comboValue =  div.getElementsByClassName('combo-value');
	comboValue[0].value = el.getAttribute('value');
	content.classList.toggle("show");
}


function setComboGridValue(el){
	div = findAncestor(el,'dropdown');
	content = findAncestor(el,'dropdown-content');
	comboText =  div.getElementsByClassName('combo-text');
	comboText[0].value = el.getAttribute('text');
	comboValue =  div.getElementsByClassName('combo-value');
	comboValue[0].value = el.getAttribute('value');
	content.classList.toggle("show");
}




function reloadComboSearchData(div,filter)
{
  var a = div.getElementsByTagName("a");
  var txtValue = "";
  for (i = 0; i < a.length; i++) {
    txtValue = a[i].textContent || a[i].innerText;
    if (txtValue.toUpperCase().indexOf(filter) > -1) {
      a[i].style.display = "";
    } else {
      a[i].style.display = "none";
    }
  }
}

function toggleCollapse(el){
	div = findAncestor(el,'dropdown');
	treegrid = findAncestor(el,'treegrid-table');
	row = findAncestor(el,'treegrid-row');
	childrens =  div.getElementsByClassName('treegrid-row');
	var closed = el.classList.contains('caret');
	if(closed)
	{
		el.classList.remove("caret");
		el.classList.add("caret-down");
	}
	else{
		el.classList.add("caret");
		el.classList.remove("caret-down");
	}
	for (i = 0; i < childrens.length; i++) {
	     if(childrens[i].getAttribute('rel') == row.getAttribute('value')){
			if(closed){
			   childrens[i].style.display = "table-row";
			}else{
				childrens[i].style.display = "none";
			}
		 }
		 
	}
}

function leaveCombo(el){
	//el.classList.remove("show");
}