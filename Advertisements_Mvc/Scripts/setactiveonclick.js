var menu = document.querySelector('.navbar-brand');
var menu1 = document.getElementsByTagName('ul');
var a = menu1[0].children;
var a1 = a[0];
var a2 = a[1];

a1.addEventListener('click', function () { clickHandler(a1) }, false);
a2.addEventListener('click', function () { clickHandler(a2) }, false);
a1['onclick'] = function () { clickHandler(a1), false }
a2['onclick'] = function () { clickHandler(a2), false }


function clickHandler(anchor) {
	var hasClass = anchor.getAttribute('class');
	if (hasClass !== 'active') {
		anchor.setAttribute('class', 'active');

	}
}