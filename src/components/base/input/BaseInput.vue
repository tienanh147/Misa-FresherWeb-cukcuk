<template>
  <div class="input-wrapper">
    <label>{{ label }}</label>

    <div class="warning" v-if="warning">
      {{ warning }}
    </div>
    <input
      @input="inputEmit"
      @blur="inputBlur($event)"
      class="text-box-default input-item"
      v-bind="$attrs"
      :type="type"
      :value="formatInputValue"
    />
  </div>
</template>

<script>
// import WarningLabel from './WarningLabel.vue';
export default {
  name: "BaseInput",

  /**
   * model đẩy dữ liệu từ trong ra ngoài component thông qua sự kiện $emit('change') từ trong ra ngoài
   * bên ngoài và bên trong trao đổi dữ liệu qua prop inputValue sau đó bên trong binding inputValue vào value của input
   * nội tại component: việc binding inputValue vào value được ưu tiên hơn so với $attrs của input
   * bên ngoài component: v-model được ưu tiên hơn $attrs khi binding từ bên ngoài vào component
   */
  model: { prop: "inputValue", event: "change" },
  props: {
    label: {
      type: String
    },
    inputValue: {
      type: [String, Number]
    },
    required: {
      type: Boolean,
      default() {
        return false;
      }
    },
    validate: {
      type: String,
      default() {
        return null;
      }
    },
    type: {
      type: String
    },
    timer: {
      type: Number,
      default() {
        return 600;
      }
    }
  },
  data() {
    return {
      debounce: null,
      warning: null
    };
  },
  methods: {
    inputBlur(event) {
      var elmt = event.target;
      if (this.required == true) {
        if (elmt.value == "") {
          elmt.classList.add("input-required");
          elmt.setAttribute("title", "thông tin này không được bỏ trống");
          this.warning = "thông tin này là bắt buộc";
        } else if (this.inputValidate(elmt.value)) {
          elmt.classList.remove("input-required");
          this.warning = null;
        }
      }
      this.$emit("change", elmt.value);
    },

    inputEmit(event) {
      var vm = this;
      var elmt = event.target;
      clearTimeout(this.debounce);
      this.warning = null;
      elmt.classList.remove("input-required");
      vm.$emit("change", elmt.value);
      this.debounce = setTimeout(function() {
        var value = elmt.value;
        if (vm.inputValidate(value)) {
          vm.warning = null;
          elmt.classList.remove("input-required");
        } else {
          elmt.classList.add("input-required");
          vm.warning = "vui lòng nhập đúng định dạng";
        }
      }, this.timer);
    },
    inputValidate(value) {
      var re;
      if (this.validate == "email") {
        re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(value).toLowerCase());
      } else {
        return value != null;
      }
    }
  },
  computed: {
    formatInputValue: function() {
      if (this.inputValue == null) {
        return "";
      } else if (this.type == "date") {
        return this.inputValue.slice(0, 10);
      } else return this.inputValue;
    }
  },
  filter: {
    formatDetail: function(value) {
      if (this.type == "date") {
        return value.slice(0, 10);
      } else if (this.type == "date") {
        return this.inputValue.slice(0, 10);
      } else return this.inputValue;
    }
  }
};
</script>

<style scoped>
.input-wrapper {
  position: relative;
  width: 49%;
  /* width: 300px; */
  outline: none;
  box-sizing: border-box;
  padding-bottom: 13px;
  /* margin-right: 4px; */
}

.input-wrapper label {
  font-size: 11px;
  margin-bottom: 2px;
  display: block;
}
.input-wrapper .warning {
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: -6px;
  right: 0px;
  height: 20px;
  width: fit-content;
  padding: 0 3px;
  font-size: 11px;
  border-radius: 4px;
  color: white;
  background-color: rgb(248, 81, 103);
  word-wrap: break-word;
}
.input-wrapper .input-item {
  width: 100%;
  font-size: 12px !important;
  max-width: 224px;
  height: 34px;
  outline: none;
}

.input-wrapper #Salary {
  padding-right: 45px;
  text-align: right;
}

.input-wrapper .money-unit {
  position: absolute;
  right: 10px;
  bottom: 22px;
  font-style: italic;
}

input::placeholder {
  font-size: 12px;
}

input:focus {
  border: 1px solid #019160;
}

.input-required {
  border: 1.7px solid red;
}

/* Chrome, Safari, Edge, Opera */
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

/* Firefox */
input[type="number"] {
  -moz-appearance: textfield;
}

.text-align-center {
  text-align: center;
}

.text-align-right {
  text-align: right;
}

.text-align-left {
  text-align: left;
}
</style>