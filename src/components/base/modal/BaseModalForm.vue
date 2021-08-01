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

        <div class="form-input-info">
          <div class="header-inputs">
            A. THÔNG TIN CHUNG
            <div class="color-line"></div>
          </div>
          <div class="input-wrapper">
            <label for="EmployeeCode">Mã nhân viên(*)</label>
            <input
              type="text"
              class="text-box-default input-item"
              id="EmployeeCode"
              required
            />
          </div>
          <div class="input-wrapper">
            <label for="FullName">Họ và tên(*)</label>
            <input
              type="text "
              class="text-box-default input-item"
              id="FullName"
              required
            />
          </div>
          <div class="input-wrapper">
            <label for="DateOfBirth ">Ngày sinh</label>
            <input
              type="date"
              class="text-box-default input-item"
              id="DateOfBirth"
            />
          </div>
          <div class="input-wrapper">
            <label for="GenderName">Giới tính</label>
            <input
              type="text"
              class="text-box-default input-item"
              id="GenderName"
            />
          </div>
          <div class="input-wrapper">
            <label for="IdentityNumber">Số CMTND/ Căn cước(*)</label>
            <input
              type="text"
              class="text-box-default input-item"
              id="IdentityNumber"
              required
            />
          </div>
          <div class="input-wrapper">
            <label for="IdentityDate">Ngày cấp</label>
            <input
              type="date"
              class="text-box-default input-item"
              id="IdentityDate"
            />
          </div>

          <div class="input-wrapper" style="flex-basis: 100%">
            <label for="IdentityPlace">Nơi cấp</label>
            <input
              type="text "
              class="text-box-default input-item"
              id="IdentityPlace"
            />
          </div>
          <div class="input-wrapper">
            <label for="Email">Email(*)</label>
            <input
              type="text"
              class="text-box-default input-item"
              placeholder="example@domain.com "
              id="Email"
              required
            />
          </div>
          <div class="input-wrapper">
            <label for="PhoneNumber">Số điện thoại(*)</label>
            <input
              type="text"
              class="text-box-default input-item"
              id="PhoneNumber"
              required
            />
          </div>
          <div class="header-inputs">
            B. THÔNG TIN CÔNG VIỆC
            <div class="color-line"></div>
          </div>
          <div class="input-wrapper">
            <label for="Position">Vị trí</label>
          </div>
          <div class="input-wrapper">
            <label for="Department">Phòng ban</label>
          </div>
          <div class="input-wrapper">
            <label for="PersonalTaxCode">Mã số thuế cá nhân</label>
            <input
              type="text"
              class="text-box-default input-item"
              id="PersonalTaxCode"
            />
          </div>
          <div class="input-wrapper" style="position: relative">
            <label for="Salary">Mức lương cơ bản</label>
            <input
              type="text"
              class="text-box-default input-item"
              id="Salary"
            />
            <div class="money-unit">(VND)</div>
          </div>
          <div class="input-wrapper">
            <label for="JoinDate">Ngày gia nhập</label>
            <input
              type="date"
              class="text-box-default input-item"
              id="JoinDate"
            />
          </div>
          <div class="input-wrapper">
            <label for="WorkStatus">Tình trạng công việc</label>
            <div
              class="select-box input-item"
              id="WorkStatus"
              name="WorkStatus"
            >
              <select>
                <option value="0">Đang làm việc</option>
                <option value="1">Xin tạm nghỉ</option>
                <option value="2">Đã nghỉ việc</option>
                <option value="3">Bị đuổi việc</option>
              </select>
            </div>
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
        <button mode objectId class="button button-icon" id="buttonSave">
          <i class="far fa-save"></i>
          Lưu
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ModalForm",
  
  props: {
    isShow: {
      type: Boolean,
      default() {
        return false;
      }
    },
    employeeId:{
      type: String,
    }
  },
  methods: {
    hideModalForm() {
      this.$emit('hideForm');
    }
  }
};
</script>

<style scoped>
@import url("./modal-form-custom.css");
</style>