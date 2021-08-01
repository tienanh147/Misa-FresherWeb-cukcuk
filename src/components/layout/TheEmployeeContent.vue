<template>
  <div class="content">
    <div class="header-content">
      <div class="page-title">Danh sách nhân viên</div>
      <div class="page-feature">
        <div
          class="button button-red button-icon"
          id="buttonDel"
          @click="btnDelClick"
          :style="[
            selectedList.length > 0
              ? { visibility: 'visible' }
              : { visibility: 'hidden' },
          ]"
        >
          <div class="icon-default icon-trashbin"></div>
          Xóa nhân viên
        </div>
        <div class="button button-icon" id="buttonAdd" @click="btnAddClick">
          <div class="icon-default icon-add"></div>
          Thêm nhân viên
        </div>
      </div>
    </div>

    <FilterTable
      class="filter"
      :filterObj="filter"
      @filter-choose="filterTable"
      :selectBoxesData="selectBoxesFilter"
    />

    <EmployeeTable
      class="table-custom"
      :employees="employees"
      :pageSize="paging.pageSize"
      :pageNumber="paging.pageNumber"
      @changeSelectedList="setBtnDelete"
    >
    </EmployeeTable>

    <Paging
      class="paging"
      nameRecord="nhân viên"
      :totalRecord="paging.totalRecord"
      :totalPage="paging.totalPage"
      :pageSize="paging.pageSize"
      :pageNumber="paging.pageNumber"
      @paging-select="pagingTable"
    ></Paging>

    <ModalForm @hideForm="showModalForm=false" :isShow="showModalForm"/>
  </div>
</template>

<script>
import axios from "axios";
import FilterTable from "../base/filter/BaseFilter.vue";
import EmployeeTable from "../base/table/EmployeeTable.vue";
import Paging from "../base/paging/BasePaging.vue";
import ModalForm from "../base/modal/BaseModalForm.vue";
export default {
  name: "EmployeeContent",
  components: { FilterTable, EmployeeTable, Paging, ModalForm },
  created() {
    this.getTableData(this.paging.pageSize, this.paging.pageNumber, this.filter);
    this.getSelectBoxesData();
  },

  methods: {
    getSelectBoxesData() {
      this.selectBoxesForm = [];
      this.selectBoxesFilter = [];
      axios.get("http://cukcuk.manhnv.net/api/Department").then(res => {
        // console.log(res);
        const arrayData = res.data;
        arrayData.unshift("DepartmentName");
        arrayData.unshift("DepartmentId");
        const arrayData2 = [...arrayData];
        arrayData.unshift("Tất cả phòng ban");
        arrayData2.unshift("Chọn phòng ban");
        this.selectBoxesForm.push(arrayData2);
        this.selectBoxesFilter.push(arrayData);
      });
      axios.get("http://cukcuk.manhnv.net/v1/Positions").then(res => {
        const arrayData = res.data;
        arrayData.unshift("PositionName");
        arrayData.unshift("PositionId");
        const arrayData2 = [...arrayData];
        arrayData.unshift("Tất cả vị trí");
        arrayData2.unshift("Chọn vị trí");
        this.selectBoxesForm.unshift(arrayData2);
        this.selectBoxesFilter.push(arrayData);
      });
    },
    getTableData: function(pageSize, pageNumber, filter) {
      var vm = this;

      axios
        .get(
          "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=" +
            pageSize +
            "&pageNumber=" +
            (pageNumber - 1) * pageSize +
            "&employeeFilter=" +
            filter.employeeFilter +
            "&departmentId=" +
            filter.departmentId +
            "&positionId=" +
            filter.positionId
        )
        .then(res => {
          vm.employees = res.data.Data;
          vm.employees = vm.employees.map(e => ({ ...e, IsChecked: false }));
          vm.paging.totalRecord = res.data.TotalRecord;
          vm.paging.totalPage = res.data.TotalPage;

          vm.paging.pageSize = pageSize;
          vm.paging.pageNumber = pageNumber;
          vm.filter = filter;
          vm.loaded = true;
          vm.loaded = true;
        })
        .catch(res => {
          console.log(res);
        });
    },
    pagingTable: function(pageSize, pageNumber) {
      this.getTableData(pageSize, pageNumber, this.filter);
    },
    filterTable: function(filter) {
      this.getTableData(this.pageSize, this.pageNumber, filter);
    },
    getEmit: function(pageSize, pageNumber, filter) {
      console.log(pageSize + " " + pageNumber + " " + filter);
    },
    setBtnDelete(selectedList) {
      this.selectedList = selectedList;
    },
    btnDelClick: function() {
      var vm = this;
      vm.selectedList.forEach((item, index) => {
        axios
          .delete("http://cukcuk.manhnv.net/v1/Employees/" + item.id)
          .then(response => {
            if (response.status == 200) {
              console.log(`delete ${item.code} sucessfully`);
            } else if (response.status == 204) {
              console.log(`not found ${item.code}, maybe it is deleted`);
            } else if (response.status == 400) {
              console.log(`bad request for ${item.code}`);
            } else if (response.status == 500) {
              console.log(
                `server fail when delete ${item.code}, try again or contact admin`
              );
            }
            vm.selectedList.splice(index, 1);
            this.getTableData(this.pageSize, this.pageNumber, this.filter);
          });
      });
    },
    btnAddClick() {
      this.showModalForm = true;
    }
  },

  data() {
    return {
      paging: {
        totalRecord: 4000,
        totalPage: 125,
        pageSize: 10,
        pageNumber: 1
      },
      employees: [],
      filter: {
        employeeFilter: "n",
        departmentId: "",
        positionId: ""
      },
      selectBoxesFilter: [],
      selectBoxesForm: [],
      selectedList: [],
      showModalForm: false
    };
  }
};
</script>

<style scoped>
/* @import url("../../css/layout/content.css"); */

.content {
  min-width: 500px;
  position: absolute;
  top: 48px;
  left: 226px;
  padding: 16px 16px 0 16px;
  height: calc(100vh - 48px);
  width: calc(100vw - 226px);
  box-sizing: border-box;
  background-color: var(--bg-color);
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

/* header-content */

.content .header-content {
  /* padding: 10px 0px; */
  display: flex;
  align-items: center;
  height: 40px;
  justify-content: space-between;
  margin-bottom: 10px;
  background-color: var(--bg-color);
}

.content .header-content .page-title {
  font-family: GoogleSans-Bold;
  font-size: 20px;
  white-space: nowrap;
  margin-right: 16px;
}

.content .header-content .page-feature {
  width: 300px;
  display: flex;
  justify-content: space-between;
}

/* filer */

.content .filter {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 10px;
  height: 40px;
  background-color: var(--bg-color);
}

/* Grid */

.content .table-custom {
  flex: 1;
  background-color: var(--object-color);
}

/* Paging */

.content .paging {
  /* position: sticky; */
  bottom: 0px;
  width: 100%;
  height: 56px;
  /* background-color: red; */
}
</style>
