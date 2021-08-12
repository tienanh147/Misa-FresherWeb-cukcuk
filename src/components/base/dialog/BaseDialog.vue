<template>
  <div class="modal-dialog">
    <div class="dialog">
      <div class="dialog-header">{{ header }}</div>

      <div class="dialog-content">
        <div class="icon-warning" :class="iconClass"></div>
        <div class="dialog-content-text" v-html="content"></div>
      </div>
      <div class="dialog-footer">
        <button class="button btnCancel" @click="cancelBtn.function">
          {{ cancelBtn.content }}
        </button>
        <button class="button" :class="btnClass" @click="confirmBtn.function">
          {{ confirmBtn.content }}
        </button>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  model: { prop: "mode", event: "confirm" },
  name: "BaseDialog",
  props: {
    type: {
      type: String,
      default() {
        return "warning-yellow";
      }
    },
    header: {
      type: String,
      default() {
        return "Đóng Form thông tin chung";
      }
    },
    content: {
      type: String,
      default() {
        return 'Bạn có chắc muốn đóng form nhập "Thông tin chung của thủ tục 603" hay không?';
      }
    },
    confirmBtn: {
      type: Object,
      default() {
        return {
          content: "Đóng",
          function: null,
          bgColor: "#019160",
          hover: "#01B075",
          color: "#fff"
        };
      }
    },
    cancelBtn: {
      type: Object,
      default() {
        return {
          function: null,
          content: "Tiếp tục nhập",
          bgColor: "transparent",
          hover: "#E5E5E5",
          color: "#000"
        };
      }
    },
    mode: {
      type: Number,
      default() {
        return 0;
      }
    }
  },
  computed: {
    iconClass() {
      if (this.type == "warning-yellow") return { "warning-yellow": true };
      else if (this.type == "warning-red") return { "warning-red": true };
      else return { "warning-yellow": true };
    },

    btnClass() {
      if (this.type == "warning-yellow") return { btnGreen: true };
      else if (this.type == "warning-red") return { btnRed: true };
      else return { btnGreen: true };
    },
    cancelBtnStyle() {
      return {
        backgroundColor: this.cancelBtn.bgColor,
        color: this.cancelBtn.color
      };
    },
    confirmBtnStyle() {
      return {
        backgroundColor: this.confirmBtn.bgColor,
        color: this.confirmBtn.color
      };
    }
  }
};
</script>
<style scoped>
@import url("./dialog-variable.css");

.modal-dialog {
  /* display: none; */
  position: fixed;
  z-index: 200;
  left: 0;
  top: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.4);
}

.dialog {
  position: absolute;
  display: flex;
  flex-direction: column;
  width: var(--dialog-width);
  height: var(--dialog-height);
  left: calc(50% - var(--dialog-width) / 2);
  top: calc(50% - var(--dialog-height) / 2);
  border-radius: var(--border-radius);
  box-sizing: border-box;
  background-color: #fff;
}

.dialog .dialog-header {
  font-family: GoogleSans-Bold;
  font-size: 15px;
  /* font-weight: bold; */
  padding: 24px 24px 0 24px;
  height: var(--dialog-header-height);
  box-sizing: border-box;
  /* background-color: khaki; */
}

.dialog .dialog-content {
  width: 100%;
  height: calc(
    100% - var(--dialog-header-height) - var(--dialog-footer-height)
  );
  /* background-color: lemonchiffon; */
  padding: 24px;
  box-sizing: border-box;
  display: flex;
}

.dialog-content .icon-warning {
  background-size: 25px;
  background-repeat: no-repeat;
  background-position: center 7px;
  margin-right: 10px;
  height: var(--icon-warning-size);
  width: var(--icon-warning-size);
  border-radius: 50%;
  background-color: #f4f4f4;
}

.icon-warning.warning-yellow {
  background-image: url("../../../assets/icon/warning-yellow2.png");
}
.icon-warning.warning-red {
  background-image: url("../../../assets/icon/warning-red.png");
}
.dialog-content .dialog-content-text {
  line-height: 1.6;
  font-size: 13px;
  height: 100%;
  width: 100%;
  flex: 1;
  overflow: auto;
}

.dialog .dialog-footer {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  border-radius: 0 0 var(--border-radius) var(--border-radius);
  height: var(--dialog-footer-height);
  background-color: #f4f4f4;
}

.dialog-footer .button {
  border: none;
  width: 100px;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: unset !important;
  margin-right: 16px;
}

.dialog-footer .button.btnRed {
  background-color: #f65454;
}

.dialog-footer .button.btnRed:hover {
  background-color: #fd7b7b;
}
.dialog-footer .button.btnCancel {
  background-color: transparent;
  color: #000;
}

.dialog-footer .btnCancel:hover {
  background-color: #e5e5e5;
}
</style>