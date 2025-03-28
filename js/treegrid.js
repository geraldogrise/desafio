function toggleCollapse(el){
	treegrid = findAncestor(el,'treegrid-table');
	row = findAncestor(el,'treegrid-row');
	childrens =  treegrid.getElementsByClassName('treegrid-row');
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

function findAncestor (el, cls) {
    while ((el = el.parentElement) && !el.classList.contains(cls)){
	};
    return el;
}