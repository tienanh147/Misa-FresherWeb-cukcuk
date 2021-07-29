<template>
  <div class="content">
    <div class="header-content">
      <div class="page-title">Danh sách nhân viên</div>
      <div class="page-feature">
        <div class="button button-red button-icon" id="buttonDel">
          <div class="icon-default icon-trashbin"></div>
          Xóa nhân viên
        </div>
        <div class="button button-icon" id="buttonAdd">
          <div class="icon-default icon-add"></div>
          Thêm nhân viên
        </div>
      </div>
    </div>

    <div class="filter">
      <div class="filter-bar">
        <input
          class="text-box-default icon-search"
          type="text"
          placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
        />
        <SelectBox />
        <div class="select-box" name="Position"></div>
      </div>
      <div class="text-box-default refresh-btn" id="buttonRefresh"></div>
    </div>

    <div class="table-custom" id="content-table">
      <table>
        <thead class="table-header">
          <tr>
            <th fieldName="checkbox">
              <custom-checkbox></custom-checkbox>
            </th>
            <th fieldName="index">#</th>
            <th fieldName="EmployeeCode">Mã nhân viên</th>
            <th fieldName="FullName">Họ và tên</th>
            <th fieldName="GenderName">Giới tính</th>
            <th fieldName="DateOfBirth">Ngày sinh</th>
            <th fieldName="PhoneNumber">Điện thoại</th>
            <th fieldName="Email">Email</th>
            <th fieldName="PositionName">Chức vụ</th>
            <th fieldName="DepartmentName">Phòng ban</th>
            <th fieldName="Salary" class="text-align-right">
              Mức lương cơ bản
            </th>
            <th fieldName="WorkStatus">Tình trạng công việc</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(employee, index) in employees"
            :key="employee.EmployeeId"
            @dblclick="rowOnDblClick(employee.EmployeeId)"
          >
            <td><input type="checkbox" name="" id="" /></td>
            <td>{{ index }}</td>
            <td>{{ employee.EmployeeCode }}</td>
            <td>{{ employee.FullName }}</td>
            <td>{{ employee.GenderName }}</td>
            <td>{{ employee.DateOfBirth }}</td>
            <td>{{ employee.PhoneNumber }}</td>
            <td>{{ employee.Email }}</td>
            <td>{{ employee.PositionName }}</td>
            <td>{{ employee.DepartmentName }}</td>
            <td>{{ employee.Salary }}</td>
            <td>{{ employee.WorkStatus }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="paging">
      <div class="paging-left">
        Hiển thị <span name="employee-number">01-20/123</span>lao động
      </div>

      <div class="paging-bar">
        <div class="btn-paging-base btn-first-page"></div>

        <div class="btn-paging-base btn-prev-page"></div>

        <div class="number-container">
          <div class="btn-paging-base btn-page-number number-active">1</div>
          <div class="btn-paging-base btn-page-number">2</div>
          <div class="btn-paging-base btn-page-number">3</div>
          <div class="btn-paging-base btn-page-number">4</div>
        </div>

        <div class="btn-paging-base btn-next-page"></div>
        <div class="btn-paging-base btn-last-page"></div>
      </div>

      <div class="text-box-default paging-right">
        <span name="employee-per-page">10</span>nhân viên/trang
        <div class="icon-updown-wrapper">
          <div class="icon-up"></div>
          <div class="icon-down"></div>
          <!-- <i class="fas fa-chevron-up"></i>
                    <i class="fas fa-chevron-down"></i> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import SelectBox from "../../components/base/select-custom/BaseSelectBox.vue";
export default {
  name: "Content",
  components: { SelectBox },
  mounted() {
    var me = this;
    axios
      .get("http://cukcuk.manhnv.net/v1/Employees")
      .then((res) => {
        console.log(res);
        me.employees = res.data;
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
      employees: [],
    };
  },
  filter: {
    genderFormat: function (gender) {
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
    
    
  },
};
</script>

<style>
@import url("../../css/layout/content.css");
</style>