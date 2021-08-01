<template>
  <div id="employee-table">
    <table>
      <thead class="table-header">
        <tr>
          <th fieldName="checkbox"><CheckBox /></th>
          <th fieldName="index">#</th>
          <th fieldName="EmployeeCode">Mã nhân viên</th>
          <th fieldName="FullName">Họ và tên</th>
          <th fieldName="GenderName">Giới tính</th>
          <th fieldName="DateOfBirth">Ngày sinh</th>
          <th fieldName="PhoneNumber">Điện thoại</th>
          <th fieldName="Email">Email</th>
          <th fieldName="PositionName">Chức vụ</th>
          <th fieldName="DepartmentName">Phòng ban</th>
          <th fieldName="Salary" class="text-align-right">Mức lương cơ bản</th>
          <th fieldName="WorkStatus">Tình trạng công việc</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(employee, index) in employees"
          :id="employee.EmployeeId"
          :key="employee.EmployeeId"
          :class="{ checked: employee.IsChecked }"
          @click="rowOnClick(employee)"
          @dblclick="rowOnDblClick(employee.EmployeeId)"
        >
          <td @dblclick.stop=""><CheckBox :checked="employee.IsChecked" /></td>
          <td>{{ (pageNumber - 1) * pageSize + index + 1 }}</td>
          <td>{{ employee.EmployeeCode }}</td>
          <td>{{ employee.FullName }}</td>
          <td>{{ employee.GenderName | genderFormat }}</td>
          <td>{{ employee.DateOfBirth | dateFormat }}</td>
          <td>{{ employee.PhoneNumber }}</td>
          <td>{{ employee.Email }}</td>
          <td>{{ employee.PositionName }}</td>
          <td>{{ employee.DepartmentName }}</td>
          <td>{{ employee.Salary | salaryFormat }}</td>
          <td>{{ employee.WorkStatus }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>

import CheckBox from "../checkbox-custom/BaseCheckBox.vue";
export default {
  name: "EmployeeTable",
  components: { CheckBox },
  props: {
    employees: {
      type: Array,
      required: true
    },
    pageSize: {
      type: Number,
      default: function() {
        return 20;
      },
      required: true
    },
    pageNumber: {
      type: Number,
      default: function() {
        return 1;
      },
      required: true
    }
  },
  methods: {
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
    rowOnDblClick(employeeId) {
      alert(employeeId);
    },
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
      selectedList: []
    };
  },
  watch: {
    employees: function() {
      for (var checked of this.selectedList) {
        for (var e of this.employees) {
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
  width: 100%;
  font-size: 13px;
  overflow: auto;
}

.table-header {
  position: sticky;
  top: 0;
  background-color: var(--object-color) !important;
  width: 100%;
  height: 40px;
  white-space: nowrap;
}

.table-custom table {
  width: 100%;
  border-collapse: collapse;
}

.table-custom table th,
td {
  text-align: left;
  padding-right: 20px;
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

.table-custom table tbody {
  white-space: nowrap;
}

.table-custom .text-align-center {
  text-align: center;
}

.table-custom .text-align-right {
  text-align: right;
  padding-left: 0px !important;
  padding-right: 30px;
}

.table-custom .text-align-left {
  text-align: left;
}
</style>
