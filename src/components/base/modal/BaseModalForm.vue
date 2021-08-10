<template>
  <div class="modal" v-show="isShow">
    <div class="form-wrapper animate">
      <div class="form-wrapper-header">
        THÔNG TIN NHÂN VIÊN
        <div class="icon-default icon-x x-btn" @click="hideModalForm"></div>
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
            label="Họ và tên(*)"
            :required="true"
            warningLabel="Thông tin này là bắt buộc"
            v-model="formData.FullName"
          />
          <BaseInput
            type="date"
            label="Ngày sinh"
            v-model="formData.DateOfBirth"
          />

          <BaseInput label="Giới tính" v-model="formData.Gender" />
          <BaseInput
            label="Số CMTND/ Căn cước(*)"
            :required="true"
            v-model="formData.IdentityNumber"
          />

          <BaseInput
            type="date"
            label="Ngày cấp"
            v-model="formData.IdentityDate"
          />

          <BaseInput
            :style="{ 'flex-basis': '100%' }"
            label="Nơi cấp"
            v-model="formData.IdentityPlace"
          />
          <BaseInput
            placeholder="example@domain.com"
            label="Email(*)"
            validate="email"
            :required="true"
            v-model="formData.Email"
          />
          <BaseInput
            label="Số điện thoại(*)"
            type="number"
            :required="true"
            v-model="formData.PhoneNumber"
          />

          <div class="header-inputs">
            B. THÔNG TIN CÔNG VIỆC
            <div class="color-line"></div>
          </div>
          <div class="input-wrapper" v-if="gotSelectBoxes">
            <label for="Position">Vị trí</label>
            <SelectBox
              style="height: 35px"
              :selectBoxData="selectBoxesData[0]"
              :idField="selectBoxesData[0][1]"
              :nameField="selectBoxesData[0][2]"
              :initContent="selectBoxesData[0][0]"
              v-model="formData.PositionId"
            />
          </div>
          <div class="input-wrapper" v-if="gotSelectBoxes">
            <label for="Department">Phòng ban</label>
            <SelectBox
              style="height: 35px"
              :selectBoxData="selectBoxesData[1]"
              :idField="selectBoxesData[1][1]"
              :nameField="selectBoxesData[1][2]"
              :initContent="selectBoxesData[1][0]"
              v-model="formData.DepartmentId"
            />
          </div>
          <BaseInput
            label="Mã số thuế cá nhân"
            v-model="formData.PersonalTaxCode"
          />
          <div class="input-wrapper" style="position: relative">
            <label for="Salary">Mức lương cơ bản</label>
            <input
              type="number"
              class="text-box-default input-item"
              id="Salary"
              v-model="formData.Salary"
            />
            <div class="money-unit">(VND)</div>
          </div>
          <BaseInput
            type="date"
            label="Ngày gia nhập"
            v-model="formData.JoinDate"
          />

          <div class="input-wrapper" v-if="gotSelectBoxes">
            <label for="WorkStatus">Tình trạng công việc</label>
            <SelectBox
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
          @click="hideModalForm"
        >
          Hủy
        </button>
        <button
          objectId
          class="button button-icon"
          id="buttonSave"
          @click="saveModalForm"
        >
          <i class="far fa-save"></i>
          Lưu
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import SelectBox from "../select-custom/BaseSelectBox.vue";
import BaseInput from "../input/BaseInput.vue";
export default {
  name: "ModalForm",
  components: { SelectBox, BaseInput },
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

  created() {
  },
  mounted() {
  
  },
  data() {
    return {
      gotSelectBoxes: true,
      // selectBoxesData: [],
      formData: {},
      testInput: "outsideData",
      gotDetail: false
    };
  },
  methods: {
    hideModalForm() {
      this.$emit("hideForm");
    },
    getDetail() {
      this.axios
        .get(`http://cukcuk.manhnv.net/v1/Employees/${this.formDataId}`)
        .then(res => {
          this.formData = res.data;
          console.log(this.formData);
          this.gotDetail = true;
        })
        .catch(res => {
          console.log(res);
        });
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
          // console.log(res.data);
          this.$set(this.formData, codeField, res.data);
          // this.formData[codeField] = res.data;
          this.gotDetail = true;
        })
        .catch(error => {
          console.log(error);
        });
    },
    saveModalForm() {
      this.formData["Gender"] = parseInt(this.formData["Gender"]);
      this.formData["Salary"] = parseInt(this.formData["Salary"]);
      var dataPost = JSON.stringify(this.formData);
      // console.log(JSON.stringify(this.formData));
      var request;
      if (this.formMode == 0) {
        request = this.axios.post(
          "http://cukcuk.manhnv.net/v1/Employees",
          dataPost,
          {
            headers: {
              // Overwrite Axios's automatically set Content-Type
              "Content-Type": "application/json"
            }
          }
        );
      } else if (this.formMode == 2) {
        request = this.axios.put(
          `http://cukcuk.manhnv.net/v1/Employees/${this.formDataId}`,
          dataPost,
          {
            headers: {
              // Overwrite Axios's automatically set Content-Type
              "Content-Type": "application/json"
            }
          }
        );
      }

      request
        .then(res => {
          if (res) {
            console.log("Lưu thành công");
          }
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  computed: {},
  watch: {
    /**
     * nếu hiển thị form(isShow=true) thì lấy code mới hoặc lấy thông tin chi tiết
     * nếu ẩn form thì reset các trường
     */
    isShow: function() {
      if (this.isShow == true) {
        if (this.formMode == 0) {
          this.getNewCode("EmployeeCode");
        } else if (this.formMode == 2) {
          this.getDetail();
        }
      } else {
        // this.gotSelectBoxes=false;
        this.gotDetail = false;
        this.formData = {};
      }
    }
  }
};
</script>

<style scoped>
@import url("./modal-form-custom.css");
</style>