/* 
 * User Icon Toggle Menu
 */

 // toggle show/hide
function toggle() {
    document.getElementById("myDropdown").classList.toggle("show");
}

var edit = "/images/edit.png";
var modify_img = document.getElementsByClassName("modify_image");

for (var i = 0; i < modify_img.length; i++) {
    modify_img[i].src = edit;
}