<template>
  <div class="input-wrapper" style="position: relative">
    <label for="Salary">Mức lương cơ bản</label>
    <input
      type="text"
      class="text-box-default input-item text-align-right"
      v-model="displayValue"
    />
    <div class="money-unit">({{ moneyUnit }})</div>
  </div>
</template>

<script>
export default {
  name: "CurrencyInput",
  props: {
    value: {
      type: [String, Number]
    },
    moneyUnit: {
      type: String,
      default() {
        return "VND";
      }
    }
  },
  computed: {
    displayValue: {
      get: function() {
        if (this.value != null)
          return this.value
            .toFixed(0)
            .replace(/[^\d,]/m, "")
            .replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        else {
          return "";
        }
      },
      set: function(modifiedValue) {
        // Recalculate value after ignoring "$" and "," in user input
        let newValue = parseFloat(modifiedValue.replace(/[^\d.]/g, ""));
        // Ensure that it is not NaN
        if (isNaN(newValue)) {
          newValue = null;
        }
        // Note: we cannot set this.value as it is a "prop". It needs to be passed to parent component
        // $emit the event so that parent component gets it
        this.$emit("input", newValue);
      }
    }
  }
};
</script>

<style scoped>
.input-wrapper {
  position: relative;
  outline: none;
  box-sizing: border-box;
  padding-bottom: 13px;
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
  padding-right: 45px;
  width: 100%;
  font-size: 13px;
  max-width: 224px;
  height: 35px;
  outline: none;
}

.input-wrapper .money-unit {
  height: 1em;
  position: absolute;
  right: 10px;
  bottom: calc(50% - 0.5em);
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