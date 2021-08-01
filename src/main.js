import Vue from "vue";
// import App from "./App.vue";
import EmployeePage from "./page/EmployeePage.vue";
import axios from "axios";
import VueAxios from "vue-axios";


Vue.use(VueAxios, axios);
Vue.config.productionTip = false;

Vue.filter("genderFormat", function (gender) {
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
});

Vue.filter("salaryFormat", function (salary) {
    if (salary != null || salary != undefined) {
        var num = salary.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        return num;
    }
    return "";
});

Vue.filter("dateFormat", function (date) {
    date = new Date(date);
    if (Number.isNaN(date.getTime())) {
        return "";
    } else {
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();
        day = day < 10 ? "0" + day : day;
        month = month < 10 ? "0" + month : month;
        return day + "/" + month + "/" + year;
    }
});
Vue.mixin({
    methods: {
        globalHelper: function () {
            alert("Hello world")
        },
    },
});
new Vue({
    render: (h) => h(EmployeePage),
}).$mount("#employee-page");