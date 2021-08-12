<template>
  <div class="modal" v-show="isShow">
    <div class="form-wrapper animate">
      <div class="form-wrapper-header">
        THÔNG TIN NHÂN VIÊN
        <div class="icon-default icon-x x-btn" @click="btnCancelClick"></div>
      </div>

      <div class="form-content">
        <div class="form-ava">
          <label for="upload-image" class="upload-img"></label>
          <input id="upload-image" type="file" />

          <div class="text">
            (Vui lòng chọn ảnh có định dạng<br />
            .jpg, .jpeg, .png, .gif.)
          </div>
        </div>

        <div class="form-input-info" v-if="gotDetail">
          <div class="header-inputs">
            A. THÔNG TIN CHUNG
            <div class="color-line"></div>
          </div>
          <BaseInput
            ref="EmployeeCode"
            type="text"
            label="Mã nhân viên(*)"
            :required="true"
            v-model="formData.EmployeeCode"
          />

          <BaseInput
            placeholder=""
            ref="FullName"
            label="Họ và tên(*)"
            :required="true"
            warningLabel="Thông tin này là bắt buộc"
            v-model="formData.FullName"
          />
          <BaseInput
            ref="DateOfBirth"
            type="date"
            label="Ngày sinh"
            v-model="formData.DateOfBirth"
          />

          <BaseInput label="Giới tính" v-model="formData.Gender" />
          <BaseInput
            ref="IdentityNumber"
            label="Số CMTND/ Căn cước(*)"
            :required="true"
            v-model="formData.IdentityNumber"
          />

          <BaseInput
            ref="IdentityDate"
            type="date"
            label="Ngày cấp"
            v-model="formData.IdentityDate"
          />

          <BaseInput
            ref="IdentityPlace"
            :style="{ 'flex-basis': '100%' }"
            label="Nơi cấp"
            v-model="formData.IdentityPlace"
          />
          <BaseInput
            ref="Email"
            placeholder="example@domain.com"
            label="Email(*)"
            validate="email"
            :required="true"
            v-model="formData.Email"
          />
          <BaseInput
            ref="PhoneNumber"
            label="Số điện thoại(*)"
            type="number"
            :required="true"
            v-model="formData.PhoneNumber"
          />

          <div class="header-inputs">
            B. THÔNG TIN CÔNG VIỆC
            <div class="color-line"></div>
          </div>
          <div class="input-wrapper" v-if="gotDetail">
            <label for="Position">Vị trí</label>
            <SelectBox
              ref="PositionId"
              style="height: 35px"
              :selectBoxData="selectBoxesData[0]"
              :idField="selectBoxesData[0][1]"
              :nameField="selectBoxesData[0][2]"
              :initContent="selectBoxesData[0][0]"
              v-model="formData.PositionId"
            />
          </div>
          <div class="input-wrapper" v-if="gotDetail">
            <label for="Department">Phòng ban</label>
            <SelectBox
              ref="DepartmentId"
              style="height: 35px"
              :selectBoxData="selectBoxesData[1]"
              :idField="selectBoxesData[1][1]"
              :nameField="selectBoxesData[1][2]"
              :initContent="selectBoxesData[1][0]"
              v-model="formData.DepartmentId"
            />
          </div>
          <BaseInput
            ref="PersonalTaxCode"
            label="Mã số thuế cá nhân"
            v-model="formData.PersonalTaxCode"
          />
          <CurrencyInput v-model="formData.Salary" />
          <BaseInput
            ref="JoinDate"
            type="date"
            label="Ngày gia nhập"
            v-model="formData.JoinDate"
          />

          <div class="input-wrapper" v-if="gotDetail">
            <label for="WorkStatus">Tình trạng công việc</label>
            <SelectBox
              ref="WorkStatus"
              style="height: 35px"
              :dropup="true"
              :selectBoxData="selectBoxesData[2]"
              :idField="selectBoxesData[2][1]"
              :nameField="selectBoxesData[2][2]"
              :initContent="selectBoxesData[2][0]"
              v-model="formData.WorkStatus"
            />
          </div>
        </div>
      </div>

      <div class="form-footer">
        <button
          class="button button-icon"
          id="buttonCancel"
          @click="btnCancelClick"
        >
          Hủy
        </button>
        <button
          objectId
          class="button button-icon"
          id="buttonSave"
          @click="btnSaveClick"
        >
          <i class="far fa-save"></i>
          Lưu
        </button>
      </div>
    </div>
    <Dialog
      v-if="dialog.isShow"
      :header="dialog.header"
      :content="dialog.content"
      :type="dialog.type"
      :confirmBtn="dialog.confirmBtn"
      :cancelBtn="dialog.cancelBtn"
    />
  </div>
</template>

<script>
import Dialog from "../dialog/BaseDialog.vue";
import SelectBox from "../select-custom/BaseSelectBox.vue";
import BaseInput from "../input/BaseInput.vue";
import CurrencyInput from "../input/CurrencyInput.vue";
export default {
  name: "ModalForm",
  components: { SelectBox, BaseInput, CurrencyInput, Dialog },
  props: {
    isShow: {
      type: Boolean,
      default() {
        return false;
      }
    },
    formDataId: {
      type: String
    },
    selectBoxesData: {
      type: Array
    },
    formMode: {
      type: Number
    }
  },

  created() {},
  mounted() {},
  data() {
    return {
      gotSelectBoxes: true,
      // selectBoxesData: [],
      formData: {},
      gotDetail: false,

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
      },

      focused: document.activeElement
    };
  },
  methods: {
    btnCancelClick() {
      var vm = this;
      var code = this.formData.EmployeeCode;
      var dialogSetting = {
        type: "warning-yellow",
        header:
          vm.formMode == 2
            ? "Đóng form thông tin chi tiết"
            : "Đóng form thêm mới nhân viên",
        content:
          vm.formMode == 2
            ? `Bạn có muốn đóng form thông tin của nhân viên "<b>${code}</b>" không?`
            : `Bạn có muốn đóng form thêm mới nhân viên "<b>${code}</b>" không?`,
        cancelBtn: {
          content: "Tiếp tục nhập",
          function: function() {
            vm.dialog.isShow = false;
          }
        },
        confirmBtn: {
          content: "Đóng",
          function: vm.hideModalForm
        },
        isShow: true
      };
      this.dialog = dialogSetting;
    },
    hideModalForm() {
      this.dialog.isShow = false;
      this.$emit("hideForm");
    },
    validateOnSave() {
      var refs = this.$refs;
      for (var prop in refs) {
        var ref = refs[prop];
        if (ref.required) {
          if (ref.inputValue == null || ref.inputValue == "") {
            ref.$el.querySelector("input").focus();
            ref.warning = "thông tin này là bắt buộc";
            return false;
          }
        }
        if (ref.validate != null && ref.warning != null) {
          ref.$el.querySelector("input").focus();
          return false;
        }
      }
      // this.focused = document.activeElement;
      return true;
    },
    btnSaveClick() {
      if (this.validateOnSave()) {
        this.saveModalForm();
      }
    },
    saveModalForm() {
      this.formData["Gender"] = parseInt(this.formData["Gender"]);
      this.formData["Salary"] = parseInt(this.formData["Salary"]);
      var dataPost = JSON.stringify(this.formData);
      if (this.formMode == 0) {
        this.axios
          .post("http://cukcuk.manhnv.net/v1/Employees", dataPost, {
            headers: {
              // Overwrite Axios's automatically set Content-Type
              "Content-Type": "application/json"
            }
          })
          .then(res => {
            if (res) {
              console.log("Lưu thành công");
              this.$emit("refreshData");
              this.hideModalForm();
            }
          })
          .catch(error => {
            console.log(error);
          });
      } else if (this.formMode == 2) {
        this.axios
          .put(
            `http://cukcuk.manhnv.net/v1/Employees/${this.formDataId}`,
            dataPost,
            {
              headers: {
                // Overwrite Axios's automatically set Content-Type
                "Content-Type": "application/json"
              }
            }
          )
          .then(res => {
            if (res) {
              console.log("Lưu thành công");
              this.$emit("refreshData");
              this.$refs['EmployeeCode'].$el.querySelector('input').focus();
            }
          })
          .catch(error => {
            console.log(error);
          });
      }
    },

    getDetail: async function() {
      await this.axios
        .get(`http://cukcuk.manhnv.net/v1/Employees/${this.formDataId}`)
        .then(res => {
          if (res.status == 200) this.formData = res.data;
        })
        .catch(error => {
          console.log(error.response.status);
        });
      this.gotDetail = true;
    },

    getSelectBoxesData() {
      this.selectBoxesData = [];
      this.axios.get("http://cukcuk.manhnv.net/v1/Positions").then(res => {
        const arrayData = res.data;
        arrayData.unshift("PositionName");
        arrayData.unshift("PositionId");
        arrayData.unshift("Chọn vị trí");
        this.selectBoxesData.unshift(arrayData);
        this.axios.get("http://cukcuk.manhnv.net/api/Department").then(res => {
          const arrayData = res.data;
          arrayData.unshift("DepartmentName");
          arrayData.unshift("DepartmentId");
          arrayData.unshift("Chọn phòng ban");
          this.selectBoxesData.push(arrayData);
          this.selectBoxesData.push([
            "Chọn trạng thái làm việc",
            "WorkStatus",
            "WorkStatusName",
            { WorkStatus: "1", WorkStatusName: "Đang làm việc" },
            { WorkStatus: "2", WorkStatusName: "Nghỉ phép" },
            { WorkStatus: "3", WorkStatusName: "Xin nghỉ việc" },
            { WorkStatus: "4", WorkStatusName: "Bị đuổi việc" }
          ]);

          this.gotSelectBoxes = true;
        });
      });
    },

    getNewCode: async function(codeField) {
      await this.axios
        .get("http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode")
        .then(res => {
          if (res.status == 200) this.$set(this.formData, codeField, res.data);
        })
        .catch(error => {
          console.log(error.response.status);
        });
      this.gotDetail = true;
    }
  },
  computed: {
    activeInput() {
      var refs = this.$refs;
      for (var prop in refs) {
        var ref = refs[prop];
        if (ref.$el.querySelector("input").hasFocus())
          return ref.$el.querySelector("input");
      }
      return null;
    }
  },
  watch: {
    /**
     * nếu hiển thị form(isShow=true) thì lấy code mới hoặc lấy thông tin chi tiết
     * nếu ẩn form thì reset các trường
     */
    isShow: async function(val) {
      if (val) {
        // this.gotSelectBoxes = true;
        if (this.formMode == 0) {
          await this.getNewCode("EmployeeCode");
        } else if (this.formMode == 2) {
          await this.getDetail();
        }
        this.$nextTick(function() {
          this.$refs.EmployeeCode.$el.querySelector("input").focus();
        });
      } else {
        // this.gotSelectBoxes = false;
        this.gotDetail = false;
        this.formData = {};
      }
    },
  }
};
</script>

<style scoped>
@import url("./modal-form-custom.css");
</style>