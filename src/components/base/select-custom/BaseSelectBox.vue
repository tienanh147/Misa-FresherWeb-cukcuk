<template>
  <div class="select-box" name="Position">
    <div
      class="icon-default icon-x"
      @click="iconXClick"
      v-show="selectedId != null"
    ></div>

    <div
      class="select-selected"
      :class="{ 'select-arrow-active': dropdownShow }"
      @click="dropdownClick"
    >
      {{ selectedName }}
    </div>
    <div class="select-items" v-show="dropdownShow">
      <div
        v-for="item in dropdownData"
        :key="item[idField]"
        @click="itemClick(item[idField], item[nameField])"
        :class="{ selected: item[idField] == selectedId }"
      >
        <i
          class="fal fa-check"
          :style="[
            selectedId == getItemProp(item, 'Id')
              ? { visibility: 'visible' }
              : { visibility: 'hidden' },
          ]"
        ></i
        >{{ item[nameField] }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SelectBox",
  props: {
    dropdownData: {
      type: Array,
      required: true
    },
    initContent: {
      type: String
    },
    idField: {
      type: String
    },
    nameField: {
      type: String
    }
  },

  created() {},
  mounted() {},
  data: function() {
    return {
      dropdownShow: false,
      selectedId: null,
      selectedName: this.initContent
    };
  },

  methods: {
    iconXClick() {
      this.selectedName = this.initContent;
      this.selectedId = null;
    },
    dropdownClick() {
      this.dropdownShow = !this.dropdownShow;
    },
    itemClick(id, name) {
      this.selectedName = name;
      this.selectedId = id;
      this.dropdownShow = false;
      this.sendEmit();
    },
    getItemProp(object, subString) {
      var propValue;
      for (var prop in object) {
        if (prop.includes(subString)) {
          propValue = object[prop];
          break;
        }
      }
      return propValue;
    },
    sendEmit() {
      return this.$emit("selectbox-select", this.selectedId);
    }
  },
  computed: {},
  watch: {}
};
</script>

<style scope>
@import url("../../../css/common/custom-scroll.css");
.select-box {
  position: relative;
  font-size: 12px;
  white-space: nowrap;
  height: 40px;
  width: 100%;
  display: flex;
  align-items: center;
}

.select-box select {
  display: none;
  /*hide original SELECT element:*/
}

/*style the items-container (options), including the selected item:*/

.select-selected {
  position: relative;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.select-selected {
  /* color: #000000; */
  padding-left: 16px;
  /* padding: 10px 16px; */
  border: 1px solid transparent;
  border-color: #bbbbbb;
  cursor: pointer;
  user-select: none;
  border-radius: 4px;
  box-sizing: border-box;
  background-color: var(--object-color);
}

.select-box .icon-x {
  border-radius: 50%;
  position: absolute;
  /* display: none; */
  right: 35px;
  height: 15px;
  width: 15px;
  z-index: 1;
  cursor: pointer;
}

.select-box .icon-x:hover {
  background-color: rgb(252, 140, 140);
}

/*style the arrow inside the select element:*/

.select-selected:after {
  position: absolute;
  right: 0;
  content: "";
  width: 40px;
  height: 100%;
  background-image: url("../../../assets/icon/chevron-down.svg");
  background-size: 18px 18px;
  background-repeat: no-repeat;
  background-position: center;
}

/*point the arrow upwards when the select box is open (active):*/

.select-selected.select-arrow-active:after {
  /* background-image: url('../../Resource/icon/chevron-up-solid.svg'); */
  transform: rotate(180deg);
}

.select-selected.select-arrow-active {
  border-color: #019160;
}

.select-items {
  /* color: #000000; */
  padding: 2px 0px;
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  cursor: pointer;
  user-select: none;
  background-color: #ffffff;
}

/*style items (options):*/

.select-items {
  /* display: none; */
  position: absolute;
  top: 102%;
  left: 0;
  right: 0;
  height: 300%;
  overflow-y: auto;
  z-index: 2;
}

.select-items-bottom {
  top: auto;
  bottom: 102% !important;
}

/* custom list-item */

.select-items div {
  /* color: #000000; */
  height: 33%;
  border-top: 1px solid #bbbbbb;
  cursor: pointer;
  user-select: none;
  display: flex;
  align-items: center;
}

.select-items div i {
  visibility: hidden;
  margin: auto 10px;
}

/* custom selected item */

.same-as-selected {
  color: #ffffff;
  position: relative;
  background-color: #019160;
}

.selected {
  color: #ffffff;
  position: relative;
  background-color: #019160;
}

/* hover option */

.select-items div:hover {
  background-color: #e9ebee;
  color: #000;
}

/*hide the items when the select box is closed:*/

.select-hide {
  display: none;
}
</style>
