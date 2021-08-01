<template>
  <div class="checkbox-custom" :class="{ checked: checked }">
    <i class="far fa-check"></i>
    <!-- @dblclick.stop="" -->
    <!-- <input
      type="checkbox"
      v-bind:checked="checked"
      v-on:change="$emit('change', $event.target.checked)"
    /> -->
  </div>

  <!-- @click="checkBoxClick($event)" @dblclick="preventPopUp($event)" -->
</template>

<script>
export default {
  name: "CheckBox",
  // model: { prop: "checked", event: "change" },
  props: {
    checked: {
      type: Boolean
    }
  },
  data: function() {
    return {
      isChecked: false
    };
  },
  methods: {
    // checkBoxClick(event) {
    //   event.stopPropagation();
    //   this.checked = !this.checked;
    //   this.$emit("check-row", event, this.isChecked);
    // },
    // checkChange($event) {
    //   console.log("change");
    //   this.$emit("checkTest", $event, this.isChecked);
    // }
  },
  watch: {}
};
</script>

<style scoped>
.checkbox-custom {
  color: #fff;
  display: flex;
  justify-content: center;
  align-items: flex-end;
  height: 20px;
  width: 20px;
  font-size: 16px !important;
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  box-sizing: border-box;
  background-color: #fff;
  cursor: pointer;
}

.checkbox-custom input {
  display: none;
  /* opacity: 0; */
}
.checkbox-custom:hover {
  /* border: none; */
  border-color: #70f1c6;
  background-color: #70f1c6;
}

.checked {
  border-color: #01b075;
  background-color: #01b075;
}

.checked:hover {
  border-color: #01b075;
  background-color: #01b075;
}
</style>