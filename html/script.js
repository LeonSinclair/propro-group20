/* 
 * User Icon Toggle Menu
 */

 // toggle show/hide
function toggle() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// close dropdown if user clicks outside bounds
wondow.onclick = function(event) {
    if (!event.target.matches('.droptn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
              }
        }
    }
}