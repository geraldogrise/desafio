function tree(){
	var toggler = document.getElementsByClassName("caret");
	var i;
	for (i = 0; i < toggler.length; i++) {
	  toggler[i].addEventListener("click", function() {
		this.parentElement.parentElement.querySelector(".nested").classList.toggle("active");
		this.classList.toggle("caret-down");
	  });
	}
}
window.onload = function() {
  tree();
};


