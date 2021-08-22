<template>
  <div id="employee-table">
    <table>
      <thead class="table-header">
        <tr>
          <th fieldName="checkbox" style="width: 4em"></th>
          <th fieldName="index" style="width: 3.1em">#</th>
          <th fieldName="EmployeeCode" style="width: 7.5em">Mã nhân viên</th>
          <th fieldName="FullName" style="width:12em">Họ và tên</th>
          <th fieldName="GenderName" style="width:5.5em">Giới tính</th>
          <th fieldName="DateOfBirth" style="width:7em">Ngày sinh</th>
          <th fieldName="PhoneNumber" style="width:8em">Điện thoại</th>
          <th fieldName="Email" style="width:18em">Email</th>
          <th fieldName="PositionName" style="width:8em">Chức vụ</th>
          <th fieldName="DepartmentName" style="width:10em">Phòng ban</th>
          <th fieldName="Salary" class="text-align-right" style="width:10em; padding-right: 1.5em!important;">Mức lương cơ bản</th>
          <th fieldName="WorkStatus" style="width:11em;">Tình trạng công việc</th>
        </tr>
      </thead>

      <tbody>
        <tr
          v-for="(employee, index) in employees"
          :id="employee.EmployeeId"
          :key="index"
          :class="{ checked: employee.IsChecked }"
          @click="rowOnClick(employee)"
          @dblclick="rowOnDblClick(employee.EmployeeId)"
        >
          <td @dblclick.stop><CheckBox :checked="employee.IsChecked" /></td>
          <td :title="(pageNumber - 1) * pageSize + index + 1">{{ (pageNumber - 1) * pageSize + index + 1 }}</td>
          <td :title="employee.EmployeeCode">{{ employee.EmployeeCode }}</td>
          <td :title="employee.FullName">{{ employee.FullName }}</td>
          <td :title="employee.GenderName">{{ employee.GenderName}}</td>
          <td :title="employee.DateOfBirth | dateFormat">{{ employee.DateOfBirth | dateFormat }}</td>
          <td :title="employee.PhoneNumber">{{ employee.PhoneNumber }}</td>
          <td :title="employee.Email">{{ employee.Email }}</td>
          <td :title="employee.PositionName">{{ employee.PositionName }}</td>
          <td :title="employee.DepartmentName">{{ employee.DepartmentName }}</td>
          <td :title=" employee.Salary | salaryFormat" class="text-align-right" style="padding-right: 1.5em !important;">{{ employee.Salary | salaryFormat }}</td>
          <td :title="employee.WorkStatusName">{{ employee.WorkStatusName }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
// import Loader from "../loader/BaseLoader.vue";
import CheckBox from "../checkbox-custom/BaseCheckBox.vue";
export default {
  name: "EmployeeTable",
  components: { CheckBox },

  props: {
    /** danh sách employees được truyền vào
     */
    employees: {
      type: Array,
      required: true
    },

    /** Dùng để đánh số thứ tự
     */
    pageSize: {
      type: Number,
      default: function() {
        return 20;
      },
      required: true
    },

    /** Dùng để đánh số thứ tự
     */
    pageNumber: {
      type: Number,
      default: function() {
        return 1;
      },
      required: true
    },

    loading: {
      type: Boolean,
      default() {
        return false;
      }
    }
  },

  methods: {
    /** Hàm set sự kiên Click vào row
     * CreatedBy: TTAnh(08/08/2021)
     */
    rowOnClick(employee) {
      employee.IsChecked = !employee.IsChecked;
      var obj = {
        id: employee.EmployeeId,
        code: employee.EmployeeCode
      };

      if (employee.IsChecked) {
        this.selectedList.push(obj);
      } else {
        this.selectedList.forEach((item, index) => {
          if (obj.id == item.id) {
            this.selectedList.splice(index, 1);
          }
        });
      }
      this.$emit("changeSelectedList", this.selectedList);
    },

    /** Hàm set sự kiện dblClick vào row
     * CreatedBy: TTAnh(08/08/2021)
     */
    rowOnDblClick(employeeId) {
      this.$emit("showDetail", employeeId);
    },

    /** Hàm lấy dữ liệu table dự phòng
     * CreatedBy: TTAnh(08/08/2021)
     */
    getTableData() {
      // var vm = this;
      this.axios
        .get(
          "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageNumber=1&employeeFilter=n&pageSize=10&departmentId=&positionId"
        )
        .then(res => {
          this.data = res.data.Data;
          this.data = this.data.map(e => ({ ...e, IsChecked: false }));
          console.log(this.data);
        });
    }
  },

  data() {
    return {
      /** danh sách cac hàng được chọn
       */
      selectedList: []
    };
  },

  watch: {
    /** Hàm quan sát sự thay đổi của prop employees,
     * khi danh sách employees thay đổi thì cần check xem có phần tử nào
     * trong mảng này đã được chọn trước đó hay không
     * CreatedBy: TTAnh(08/08/2021)
     */
    employees: function(list) {
      for (var checked of this.selectedList) {
        for (var e of list) {
          if (checked.id == e.EmployeeId) {
            e.IsChecked = true;
            break;
          }
        }
      }
    }
  }
};
</script>

<style scoped>
@import url("../../../css/common/custom-scroll.css");
.table-custom {
  /* width: 100%; */
  position: relative;
  font-size: 13px;
  overflow: auto;
}

.table-header {
  position: sticky;
  top: 0;
  background-color: var(--object-color) !important;
  height: 40px;
  white-space: nowrap;
}

.table-custom table {
  width: calc(100vw - var(--menu-width) - 2 * var(--content-padding));
  /* display: table; */
  table-layout: fixed;
  border-collapse: collapse;
}

.table-custom table th,
td {
  box-sizing: border-box;
  padding-right: 0.5em;
  overflow-x: hidden;
  text-overflow: ellipsis;
  text-align: left;
  white-space: nowrap;
}

.table-custom table tr {
  height: 48px;
  border-bottom: 1px solid #f1f1f1;
}

.table-custom table tr:hover {
  background-color: #e9ebee;
}

.table-custom table tr.checked {
  background-color: #e3f3ee;
}

.table-custom table tr > :first-child {
  padding-left: 16px;
}

.table-custom table .row-selected {
  background: #e3f3ee;
}

/* .table-custom table tbody tr:nth-child(odd) {
    background-color: #f2f2f2;
} */

.table-custom .text-align-center {
  text-align: center;
}

.table-custom .text-align-right {
  text-align: right;
  padding-left: 0px !important;
  /* padding-right: 30px; */
}

.table-custom .text-align-left {
  text-align: left;
}
.table-custom .modal-loader {
  z-index: 50;
  position: absolute;
  top: 0;
  height: 100%;
  width: 100%;
}
</style>
