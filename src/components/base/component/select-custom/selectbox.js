$(document).ready(function() {
    //#region extend object
    var objAllDepartment = {
        DepartmentId: null,
        DepartmentCode: " ",
        DepartmentName: "Tất cả phòng ban",
        Description: "All",
        CreatedDate: "1970-01-01T00:07:13",
        CreatedBy: "Tien Anh",
        ModifiedDate: null,
        ModifiedBy: null
    };
    var objAllPosition = {
        PositionId: " ",
        PositionCode: " ",
        PositionName: "Tất cả vị trí",
        Description: "All",
        ParentId: null,
        CreatedDate: "1970-01-01T00:00:55",
        CreatedBy: "Tien Anh",
        ModifiedDate: null,
        ModifiedBy: null
    };
    var nullDepartment = {
        DepartmentId: " ",
        DepartmentCode: null,
        DepartmentName: "Chọn phòng ban",
        Description: "null",
        CreatedDate: "1970-01-01T00:07:13",
        CreatedBy: "Tien Anh",
        ModifiedDate: null,
        ModifiedBy: null
    };
    var nullPosition = {
        PositionId: " ",
        PositionCode: null,
        PositionName: "Chọn vị trí",
        Description: "null",
        ParentId: null,
        CreatedDate: "1970-01-01T00:00:55",
        CreatedBy: "Tien Anh",
        ModifiedDate: null,
        ModifiedBy: null
    };
    var workStatus = [{ WorkStatusId: "", WorkStatusName: "Chọn trạng thái làm việc" },
        { WorkStatusId: 1, WorkStatusName: "Đang làm việc" }, { WorkStatusId: 2, WorkStatusName: "Nghỉ phép" },
        { WorkStatusId: 3, WorkStatusName: "Xin nghỉ việc" }, { WorkStatusId: 4, WorkStatusName: "Bị đuổi việc" }
    ];
    //#endregion
    var selectBoxData = new API();

    var departmentData = selectBoxData.get("http://cukcuk.manhnv.net/api/Department");
    var positionData = selectBoxData.get("http://cukcuk.manhnv.net/v1/Positions");

    departmentData.then(function(response) {
        const res2 = [...response];
        res2.unshift(nullDepartment);
        response.unshift(objAllDepartment);
        renderCustomSelectBox(response, $('.filter').find(`.select-box[name="Department"]`));

        renderCustomSelectBox(res2, $('.modal .form-content').find(`#Department`), );
        // $('.modal .form-input-info').find('.select-selected').html("");
    });

    positionData.then(function(response) {
        const res2 = [...response];
        res2.unshift(nullPosition);
        renderCustomSelectBox(res2, $('.modal .form-content').find(`#Position`));
        // $('.modal .form-input-info').find('.select-selected').html("");

        response.unshift(objAllPosition);
        renderCustomSelectBox(response, $('.filter').find(`.select-box[name="Position"]`));

    });
    renderCustomSelectBox(workStatus, $(`div.select-box[id="WorkStatus"]`));
    $(`div.select-box[id="WorkStatus"]`).find('.select-items').addClass("select-items-bottom");

})

/**
 * Hàm render SelectBox
 * @param {Array} data 
 * @param {String} select_box_name 
 */
function renderCustomSelectBox(data, select_box) {
    // var select_box = $(`div.select-box[name="${select_box_name}"]`);
    $(select_box).empty();
    var select_box_name = $(select_box).attr('name');

    var itemName = select_box_name + "Name";
    var itemId = select_box_name + "Id";
    var selectedIndex = 0;
    var icon_checked = "fal fa-check";
    var select_selected = $(`<div class="select-selected">${data[selectedIndex][itemName]}</div>`);
    var dropdown_container = $('<div class="select-items select-hide"></div>');


    var icon_x = $(`<div class="icon-default icon-x"></div>`);

    $.each(data, function(index, item) {
        var icon = $('<i></i>');
        icon.addClass(icon_checked);
        var option = $(`<div>${item[itemName]}</div>`)
        if (index == selectedIndex) {
            option.css("border", "none");
        }
        $(option).attr(itemId, item[itemId]);
        $(option).prepend(icon);
        dropdown_container.append(option);
        $(option).click(function() {
            var select_selected = $(this).parent().prev();
            var selected_item = $(this).parent().find('.selected');
            $(selected_item).find("i").css("visibility", "hidden");
            $(selected_item).removeClass("selected");
            $(this).addClass("selected");
            $(this).find("i").css("visibility", "visible");
            select_selected.text($(this).text());
            $(icon_x).css('display', 'block');
        });
    });

    $(icon_x).click(function() {
        $(dropdown_container).find("i").css("visibility", "hidden");
        $(dropdown_container).find('.selected').removeClass("selected");
        $(this).next().text(data[selectedIndex][itemName]);
        $(this).css('display', 'none');
    });
    $(select_box).append(icon_x);

    $(select_selected).click(function(e) {
        e.stopPropagation();
        closeAllSelect(this);
        this.nextSibling.classList.toggle("select-hide");
        this.classList.toggle("select-arrow-active");
    });

    $(select_box).append(select_selected);
    $(select_box).append(dropdown_container);
}

function closeAllSelect(elmnt) {
    /*a function that will close all select boxes in the document,
    except the current select box:*/
    var x, y, i, xl, yl, arrNo = [];
    x = document.getElementsByClassName("select-items");
    y = document.getElementsByClassName("select-selected");
    xl = x.length;
    yl = y.length;
    for (i = 0; i < yl; i++) {
        if (elmnt == y[i]) {
            arrNo.push(i)
        } else {
            y[i].classList.remove("select-arrow-active");
        }
    }
    for (i = 0; i < xl; i++) {
        if (arrNo.indexOf(i)) {
            x[i].classList.add("select-hide");
        }
    }

}

/*if the user clicks anywhere outside the select box,
then close all select boxes:*/
document.addEventListener("click", closeAllSelect);