<template>
  <div class="table-custom" id="content-table">
    <table>
      <thead class="table-header">
        <tr>
          <th fieldName="checkbox">
            <custom-checkbox></custom-checkbox>
          </th>
          <th>#</th>
          <th>Mã nhân viên</th>
          <th>Họ và tên</th>
          <th>Giới tính</th>
          <th>Ngày sinh</th>
          <th>Điện thoại</th>
          <th>Email</th>
          <th>Chức vụ</th>
          <th>Phòng ban</th>
          <th>Mức lương cơ bản</th>
          <th>Tình trạng công việc</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(obj, index) in data"
          :key="obj.EmployeeId"
          @dblclick="rowOnDblClick(obj.Id)"
        >
          <td><input type="checkbox" name="" id="" /></td>
          <td>{{ index }}</td>
          <td>{{ obj.Code }}</td>
          <td>{{ obj.FullName }}</td>
          <td>{{ obj.GenderName }}</td>
          <td>{{ obj.DateOfBirth }}</td>
          <td>{{ obj.PhoneNumber }}</td>
          <td>{{ obj.Email }}</td>
          <td>{{ obj.PositionName }}</td>
          <td>{{ obj.DepartmentName }}</td>
          <td>{{ obj.Salary }}</td>
          <td>{{ obj.WorkStatus }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "table",
  props: {
    apiURL: {
      type: String,
      default: " ",
    },
    fieldName: {
      type: Array,
      default: function () {
        return [
          "index",
          "Code",
          "FullName",
          "Gendername",
          "DateOfBirth",
          "PhoneNumber",
          "Email",
          "PositionName",
          "DepartmentName",
          "Salary",
          "WorkStatus",
        ];
      },
    },
  },
  mounted() {
    var me = this;
    axios
      .get("http://cukcuk.manhnv.net/v1/Employees")
      .then((res) => {
        console.log(res);
        me.data = res.data;
      })
      .catch((res) => {
        console.log(res);
      });
  },
  methods: {
    rowOnDblClick(employeeId) {
      alert(employeeId);
    },
  },
  data() {
    return {
      data: [],
    };
  },
};
</script>
