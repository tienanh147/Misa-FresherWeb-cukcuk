// var select_boxes, selElmnt, select_selected, select_dropdown_container, option;
var class_icon = "fal fa-check";
/*look for any elements with the class "custom-select":*/
var select_boxes = document.querySelectorAll(".select-box");

select_boxes.forEach(select_box => {

    var selElmnt = select_box.querySelector("select");
    /*for each element, create a new DIV that will act as the selected item:*/
    var select_selected = document.createElement("DIV");
    select_selected.setAttribute("class", "select-selected");
    select_selected.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
    select_box.appendChild(select_selected);
    /*for each element, create a new DIV that will contain the option list:*/
    var select_dropdown_container = document.createElement("DIV");
    select_dropdown_container.setAttribute("class", "select-items select-hide");


    var options = selElmnt.querySelectorAll("option");
    options.forEach((child_selElemnt, index) => {
        var option = document.createElement("DIV");
        if (index == 0) {
            option.setAttribute("style", "border:none;");
        }
        option.innerHTML = child_selElemnt.innerHTML;
        select_dropdown_container.appendChild(option);

        /*you can put this code anywhere in this js file to refactor code but slower
        , just find all .select-selected and set to select_selected*/
        //start
        option.addEventListener("click", function(e) {
            /*when an item is clicked, update the original select box,
                        and the selected item:*/
            var same_as_selected, selElmnt, select_selected;
            selElmnt = this.parentNode.parentNode.querySelector("select");
            select_selected = this.parentNode.previousSibling; //get select-selected
            select_length = selElmnt.length;
            var options = selElmnt.querySelectorAll("option");
            Array.from(options).every((opt, i) => {
                if (opt.innerHTML == this.innerHTML) {
                    selElmnt.selectedIndex = i;
                    select_selected.innerHTML = this.innerHTML;
                    same_as_selected = this.parentNode.querySelectorAll(".same-as-selected");
                    same_as_selected.forEach(item_same => {
                        item_same.removeChild(item_same.querySelector("i")); // remove checked icon
                        item_same.removeAttribute("class"); //remove class show checked row
                    });
                    this.setAttribute("class", "same-as-selected");

                    //add check icon
                    var icon = document.createElement("I");
                    icon.setAttribute("class", class_icon);
                    this.insertBefore(icon, this.childNodes[0]);
                    return false;
                }
                return true;

            });

            // for (var i = 0; i < select_length; i++) {
            //     if (selElmnt.options[i].innerHTML == this.innerHTML) {
            //         selElmnt.selectedIndex = i;
            //         select_selected.innerHTML = this.innerHTML;
            //         same_as_selected = this.parentNode.querySelectorAll(".same-as-selected");
            //         same_as_selected.forEach(item_same => {
            //             item_same.removeChild(item_same.querySelector("i")); // remove checked icon
            //             item_same.removeAttribute("class"); //remove class show checked row
            //         });
            //         this.setAttribute("class", "same-as-selected");
            //         //add check icon
            //         var icon = document.createElement("I");
            //         icon.setAttribute("class", class_icon);
            //         this.insertBefore(icon, this.childNodes[0]);
            //         break;
            //     }
            //     console.log(option);
            // }

            select_selected.click();
        });
        //end
    });


    select_box.appendChild(select_dropdown_container);

    /*you can put this code anywhere in this js file to refactor code but slower
    , just find all .select-selected and set to select_selected*/
    //start
    select_selected.addEventListener("click", function(e) {
        /*when the select box is clicked, close any other select boxes,
        and open/close the current select box:*/
        e.stopPropagation();
        closeAllSelect(this);
        this.nextSibling.classList.toggle("select-hide");
        this.classList.toggle("select-arrow-active");
    });
    //end

});

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