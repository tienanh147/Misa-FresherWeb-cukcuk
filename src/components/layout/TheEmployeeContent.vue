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
      placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
      :filterObj="filter"
      @filter-choose="filterTable"
      @refreshBtn="refreshData"
      :selectBoxesData="selectBoxesFilter"
    />

    <EmployeeTable
      class="table-custom"
      :employees="employees"
      :pageSize="paging.pageSize"
      :pageNumber="paging.pageNumber"
      @changeSelectedList="setBtnDelete"
      @showDetail="showEmployeeDetail"
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

    <ModalForm
      @hideForm="hideForm"
      :isShow="modalForm.showModalForm"
      :selectBoxesData="selectBoxesForm"
      :formMode="modalForm.formMode"
      :formDataId="modalForm.employeeId"
      @refreshData="refreshData"
    />
    <Dialog
      v-if="dialog.isShow"
      :header="dialog.header"
      :content="dialog.content"
      :type="dialog.type"
      :confirmBtn="dialog.confirmBtn"
      :cancelBtn="dialog.cancelBtn"
    />

    <div class="loading" v-if="loading">
      <i class="fas fa-spinner fa-pulse"></i>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import FilterTable from "../base/filter/BaseFilter.vue";
import EmployeeTable from "../base/table/EmployeeTable.vue";
import Paging from "../base/paging/BasePaging.vue";
import ModalForm from "../base/modal/BaseModalForm.vue";
import Dialog from "../base/dialog/BaseDialog.vue";
export default {
  name: "EmployeeContent",
  components: { FilterTable, EmployeeTable, Paging, ModalForm, Dialog },
  created() {
    this.getTableData(
      this.paging.pageSize,
      this.paging.pageNumber,
      this.filter
    );
    this.getSelectBoxesData();
  },
  mounted() {},

  methods: {
    /** Hàm lấy dữ liệu cho các Select Box và thêm các trường init content, idField, nameField vào đầu mảng.
     * --initContent: Nội dung hiển thị khi không chọn.
     * --idField: trường id của object.
     * --nameField: trường nội dung của object.
     * CreatedBy: TTAnh(05/08/2021)
     */
    getSelectBoxesData: async function() {
      this.selectBoxesFilter = [];
      this.selectBoxesForm = [];
      this.doneSelectBoxesData = 0;

      await this.axios
        .get("http://cukcuk.manhnv.net/v1/Positions")
        .then(res => {
          /**
           * dữ liệu lấy được
           */
          const arrayData = res.data;

          //#region Thêm trường nameField và idField
          arrayData.unshift("PositionName");
          arrayData.unshift("PositionId");
          //#endregion
          const arrayData2 = arrayData.map(x => x);
          //#region Thêm trường initContent
          arrayData.unshift("Tất cả vị trí");
          arrayData2.unshift("Chọn vị trí");
          //#endregion
          //#region thêm dữ liệu của selectbox WorkStatus vào selectBoxesForm

          //#endregion

          this.selectBoxesForm.unshift(arrayData2);
          this.selectBoxesFilter.push(arrayData);

          //khi hoàn thành thì tăng biến doneSelectBoxesData lên 1
          this.doneSelectBoxesData++;
        })
        .catch(error => {
          console.log(error);
        });

      await this.axios
        .get("http://cukcuk.manhnv.net/api/Department")
        .then(res => {
          // console.log(res);
          const arrayData = res.data;
          //#region Thêm trường nameField và idField
          arrayData.unshift("DepartmentName");
          arrayData.unshift("DepartmentId");
          //#endregion
          const arrayData2 = arrayData.map(x => x);
          //#region Thêm trường initContent
          arrayData.unshift("Tất cả phòng ban");
          arrayData2.unshift("Chọn phòng ban");
          //#endregion

          this.selectBoxesForm.push(arrayData2);
          this.selectBoxesFilter.unshift(arrayData);

          //khi hoàn thành thì tăng biến doneSelectBoxesData lên 1
          this.doneSelectBoxesData++;
        })
        .catch(error => {
          console.log(error);
        });
      this.selectBoxesForm.push([
        "Chọn trạng thái làm việc",
        "WorkStatus",
        "WorkStatusName",
        { WorkStatus: 1, WorkStatusName: "Đang làm việc" },
        { WorkStatus: 2, WorkStatusName: "Nghỉ phép" },
        { WorkStatus: 3, WorkStatusName: "Xin nghỉ việc" },
        { WorkStatus: 4, WorkStatusName: "Bị đuổi việc" }
      ]);
    },

    /** Hàm lấy dữ liệu của table table.
     * @param {Number} pageSize
     * @param {Number} pageNumber
     * @param {Object} filter
     * CreatedBy: TTAnh(05/08/2021)
     */
    getTableData: function(pageSize, pageNumber, filter) {
      var vm = this;
      var pageNumFormat = (pageNumber - 1) * pageSize;
      this.loading = true;
      axios
        .get(
          "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=" +
            pageSize +
            "&pageNumber=" +
            pageNumFormat +
            "&employeeFilter=" +
            filter.EmployeeFilter +
            "&departmentId=" +
            filter.DepartmentId +
            "&positionId=" +
            filter.PositionId
        )
        .then(res => {
          if (res.status == 200) {
            vm.employees = res.data.Data;
            vm.employees = vm.employees.map(e => ({
              ...e,
              IsChecked: false
            }));
            //#region Lưu lại các trường nhằm mục đích phân trang
            vm.paging.totalRecord = res.data.TotalRecord; //sửa theo api cũ là TotalRecord
            vm.paging.totalPage = vm.totalPageComputed; //sửa theo api, cũ là TotalPage
            vm.paging.pageSize = pageSize;
            vm.paging.pageNumber = pageNumber;
            //#endregion
            //Lưu lại object filter
            vm.filter = filter;
          } else if (res.status == 204) {
            vm.employees = [];
          }
          this.loading = false;
        })
        .catch(res => {
          console.log(res);
        });
    },

    /** Hàm phân trang được emit từ paging.
     * 
     * @param {Number} pageSize
     * @param {Number} pageNumber
     * CreatedBy: TTAnh(05/08/2021)
     */
    pagingTable(pageSize, pageNumber) {
      this.getTableData(pageSize, pageNumber, this.filter);
    },

    /** Hàm lọc dữ liệu được emit từ filter.
     * @param {Object} filter
     * CreatedBy: TTAnh(05/08/2021)
     */
    filterTable(filter) {
      try {
        this.paging.pageNumber = 1;
        this.getTableData(this.paging.pageSize, this.paging.pageNumber, filter);
      } catch (error) {
        console.log(error);
      }
    },

    /** Hàm refresh lại dữ liệu
     * @param {Object} filter
     */
    refreshData(filter) {
      if (filter) {
        this.getSelectBoxesData();
        this.getTableData(this.paging.pageSize, this.paging.pageNumber, filter);
      } else {
        this.getTableData(
          this.paging.pageSize,
          this.paging.pageNumber,
          this.filter
        );
      }
    },

    /** lưu danh sách các nhân viên cần xóa.
     * @param {Array} selectedList
     * CreatedBy: TTAnh(05/08/2021)
     */
    setBtnDelete(selectedList) {
      this.selectedList = selectedList;
    },

    /** set sự kiện cho nút xóa.
     * CreatedBy: TTAnh(12/08/2021)
     */
    btnDelClick() {
      var vm = this;
      var list = "";
      for (var item of this.selectedList) {
        list += item["code"] + ", ";
      }
      list = list.slice(0, -2);
      var dialogSetting = {
        type: "warning-red",
        header: "Xóa các bản ghi",
        content: `Bạn có muốn xóa các bản ghi sau không?: "<b>${list}</b>"`,
        cancelBtn: {
          content: "Hủy",
          function: function() {
            vm.dialog.isShow = false;
          }
        },
        confirmBtn: {
          content: "Xóa",
          function: vm.deleteEmployees
        },
        isShow: true
      };
      this.dialog = dialogSetting;
    },

    /** Hàm xóa danh sách nhân viên
     * CreatedBy: TTAnh(10/08/2021)
     */
    deleteEmployees: async function() {
      this.dialog.isShow = false;
      this.loading = true;
      while (this.selectedList.length != 0) {
        var item = this.selectedList[0];
        await this.axios
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
          });
        this.selectedList.shift();
      }
      await this.getTableData(
        this.paging.pageSize,
        this.paging.pageNumber,
        this.filter
      );
      this.loading = false;
    },

    /** set sự kiện cho nút thêm mới.
     * CreatedBy: TTAnh(05/08/2021)
     */
    btnAddClick() {
      this.modalForm.formMode = 0;
      this.modalForm.employeeId = null;
      this.modalForm.showModalForm = true;
    },

    /** Hiển thị modal formMode
     * @param {String} employeeId
     * CreatedBy: TTAnh(05/08/2021)
     */
    showEmployeeDetail(employeeId) {
      this.modalForm.employeeId = employeeId;
      this.modalForm.formMode = 2;
      this.modalForm.showModalForm = true;
    },

    /** set cho sự kiện hideForm được emit từ modalform.
     * CreatedBy: TTAnh(05/08/2021)
     */
    hideForm() {
      this.modalForm.formMode = null;
      this.modalForm.showModalForm = false;
    }
  },

  data() {
    return {
      /** các thông số của paging
       */
      paging: {
        /** số bản ghi
         */
        totalRecord: 1000,

        /** số trang
         */
        totalPage: 50,

        /** kích thước 1 trang
         */
        pageSize: 20,

        /** trang hiện tại
         */
        pageNumber: 1
      },

      /** danh sách nhân viên
       */
      employees: [],

      /** hiển thị màn hình loading
       * true: Hiển thị. false: không hiển thị
       */
      loading: false,

      /** các thông số cho filter
       */
      filter: {
        /** từ cần lọc của filter
         */
        EmployeeFilter: "n",
        /** id của phòng ban cần loc
         */
        DepartmentId: "",
        /** id của vị trí cần lọc
         */
        PositionId: "" //vị trí cần tìm
      },

      /** dữ liệu của selectbox trong filter
       */
      selectBoxesFilter: [],

      /** dữ liệu của các selectbox trong form
       */
      selectBoxesForm: [],

      /** đã lấy thành công mấy API selectbox
       * , trường này nhằm mục đích xử lý bất đồng bộ
       */
      doneSelectBoxesData: 0,

      /** cac bản ghi đã được chọn, nhằm mục đích xóa nhiều
       */
      selectedList: [],

      /** các thông số của modal form
      */
      modalForm: {
        /** hiển thị Form hay không
         */
        showModalForm: false,

        /** mode của form:
         * 0: Thêm mới.
         * 2: Sủa.
         */
        formMode: null,

        /** Id của employee cần xem chi tiết */
        employeeId: null,

        doneData: false
      },

      /**
       * các thông số của dialog popup
       */
      dialog: {
        isShow: false,
        header: "Xóa bản ghi",
        content: "Bạn có chắc muốn xóa các nhân viên hay không?",
        type: "warning-red",
        confirmBtn: { content: "Xóa", function: null },
        cancelBtn: { content: "Tiếp tục nhập", function: null }
      }
    };
  },

  computed: {
    /**
     * Do phân trang từ API bị sai nên phải tính toán lại tổng số trang.
     * CreatedBy: TTAnh(05/08/2021)
     */
    totalPageComputed: function() {
      return (
        (this.paging.totalRecord -
          (this.paging.totalRecord % this.paging.pageSize)) /
          this.paging.pageSize +
        1
      );
    }
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
.content .loading {
  z-index: 100;
  position: absolute;
  top: 0;
  left: 0;
  display: flex;
  align-items: center;
  justify-content: space-around;
  height: 100%;
  width: 100%;
  background-color: rgba(0, 0, 0, 0.15);
}

.content .loading i {
  font-size: 64px;
  color: black;
}
</style>
