import Vue from "vue";
// import App from "./App.vue";
import EmployeePage from "./page/EmployeePage.vue";
import axios from "axios";
import VueAxios from "vue-axios";
Vue.use(VueAxios, axios);
Vue.config.productionTip = false;


/**
 * Hàm set sự kiện đóng tất cả các SelectBox ngoại trừ elmt khi click ra ngoài elmt
 * @param {Element} elmnt 
 * CreatedBy: TTAnh(05/08/2021)
 */
var closeAllSelect = function(elmnt) {
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

};

Vue.mixin({
    methods: {
        closeAllSelect
    },
    filters: {
        dateFormat: function(date) {
            date = new Date(date);

            if (date.getTime() == 0) {
                return "";
            } else {
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                day = day < 10 ? "0" + day : day;
                month = month < 10 ? "0" + month : month;
                return day + "/" + month + "/" + year;
            }
        },
        salaryFormat: function(salary) {
            if (salary != null || salary != undefined) {
                var num = salary.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
                return num;
            }
            return "";
        },
        genderFormat: function(gender) {
            if (
                gender == "Nam" ||
                gender == "Nữ" ||
                gender == "nam" ||
                gender == "nữ"
            ) {
                return gender;
            } else if (gender == "Không xác định") {
                return "Khác";
            } else {
                return " ";
            }
        },
        workStatusFormat: function(workStatus) {
            if (workStatus == null) return "";
            else if (workStatus == 1) return "Đang làm việc";
            else if (workStatus == 2) return "Nghỉ phép";
            else if (workStatus == 3) return "Xin nghỉ việc";
            else if (workStatus == 4) return "Bị đuổi việc";

        },

    }
});

new Vue({
    render: (h) => h(EmployeePage),
}).$mount("#employee-page");


/*if the user clicks anywhere outside the select box,
then close all select boxes:*/
document.addEventListener("click", closeAllSelect);