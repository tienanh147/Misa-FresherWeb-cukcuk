$(document).ready(function() {

})





/**
 * Hàm ẩn form modal khi ấn ra ngoài
 * @param {Event} event 
 */
window.onclick = function(event) {
    var modal = document.querySelector('.modal');
    if (event.target == modal) {
        modal.style.display = "none";
    } else {

    }
}


/**
 * Set sự kiện cảnh báo người dùng khi không nhập các trường bắt buộc
 */
$("div.modal").find("input[required]").blur(function() {
    let value = $(this).val();
    if (value == "") {
        $(this).addClass("input-required");
    } else {
        $(this).removeClass("input-required");
    }
})

/**
 * Bấm Hủy
 */


/**
 * Set event click cho nút thêm
 */
$(".x-btn").click(function() {
    $(this).parent().parent().parent().css('display', 'none');
});


/**
 * Hàm reset lại các trường thông tin cùa form
 * @param {Element} modal_element 
 */
function resetForm(modal_element) {
    var form = $(modal_element).find('.form-input-info');
    var inputs = $(form).find('input');
    var buttonSave = $(modal_element).find("#buttonSave");
    var mode = $(buttonSave).attr('mode');
    if (mode == '2' || mode == '0') {
        $(inputs).val(null);
        var select_boxes = $('#modal .form-input-info').find('.select-box');
        var select_selected = $(select_boxes).find('.select-selected');
        $(select_boxes).find('.select-items .selected i').css("visibility", "hidden");
        $(select_boxes).find('.select-items .selected').removeClass("selected");
        // select_selected.html("");
        $(select_boxes).find('.icon-x').css('display', 'none');
        $(inputs).val(null);
        $(inputs).removeClass("input-required");

    } else if (mode == '1') {


    }

}