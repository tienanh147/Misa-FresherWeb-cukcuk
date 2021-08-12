<template>
  <div class="select-box" v-bind="$attrs">
    <div
      class="icon-default icon-x"
      @click="iconXClick"
      v-show='selectedId != null && selectedId!=""'
    ></div>

    <div class="select-selected" @click="dropdownClick($event)">
      {{ selectedName }}
    </div>
    <div
      class="select-items select-hide"
      :class="{ 'select-items-bottom': dropup }"
    >
      <div
        v-for="item in dropdownData"
        :key="item[idField]"
        @click="itemClick(item[idField], item[nameField])"
        :class="{ selected: item[idField] == selectedId }"
      >
        <i
          class="fal fa-check"
          :style="[
            selectedId == item[idField]
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
  model: {
    prop: "selectedId",
    event: "selectbox-select"
  },
  props: {
    /**
     * dữ liệu truyền vào. là mảng các option dưới dạng Object
     * 3 phần tử đầu là initContent, idField, nameField
     */
    selectBoxData: {
      type: Array,
      required: true
    },

    /**
     * hiển thị nội dung khi không chọn
     */
    initContent: {
      type: String,
      default() {
        return "Chọn vị trí";
      }
    },

    /**
     * trường id trong option của selectBoxData
     */
    idField: {
      type: String,
      default() {
        return "PositionId";
      }
    },

    /**
     * trường nội dung trong option của selectBoxData
     */
    
    nameField: {
      type: String,
      default() {
        return "PositionName";
      }
    },

    /**
     * đảo ngược chiều dropdown lên bên trên, cần dùng để không bị tràn trang
     */
    dropup: {
      type: Boolean,
      default() {
        return false;
      }
    },

    /**
     * id đã được lựa chọn
     */
    selectedId: {
      type: [String, Number],
      default() {
        return "";
      }
    }
  },

  created() {},
  mounted() {
    this.selectedName = this.computedSelectedName;
  },
  data: function() {
    return {
      /**
       * hiển thị dropdown hay không
       * --true: Hiển thị
       * --false: Ẩn
       */
      dropdownShow: false,

      /**
       * nội dung của option đẫ chọn
       */
      selectedName: this.computedSelectedName
    };
  },

  methods: {
    /**
     * set sự kiện cho nút xóa lựa chọn.
     * CreatedBy: TTAnh(08/08/2021)
     */
    iconXClick() {
      this.selectedName = this.initContent;
      this.sendEmit("");
    },

    /**
     * @param {event} e
     * set sự kiện ẩn hiện dropdown.
     * CreatedBy: TTAnh(08/08/2021)
     */
    dropdownClick(e) {
      this.dropdownShow = !this.dropdownShow;
      e.stopPropagation();
      this.closeAllSelect(e.target);
      // console.log(e.target);
      e.target.nextSibling.classList.toggle("select-hide");
      e.target.classList.toggle("select-arrow-active");
    },

    /**
     * hàm set sự kiện click vào 1 option
     * @param {String} id id của option được click
     * @param {String} name nội dung của option được click
     * CreatedBy: TTAnh(08/08/2021)
     */
    itemClick(id, name) {
      // this.selectedId = id;
      this.selectedName = name;
      this.dropdownShow = false;
      this.sendEmit(id);
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

    /**
     * Hàm emit sự kiện
     * @param {String} id id của option được chọn
     */
    sendEmit(id) {
      return this.$emit("selectbox-select", id);
    }
  },
  computed: {
    /**
     * do dữ liệu truyền vào bao gồm cả initContent, idField, nameField
     * nên cần cắt đi 3 phần tử đầu của mảng.
     * CreatedBy: TTAnh(08/08/2021)
     */
    dropdownData: function() {
      return this.selectBoxData.slice(3);
    },

    /**
     * tìm ra nội dung của option với id là selectedId.
     * CreatedBy: TTAnh(08/08/2021)
     */
    computedSelectedName: function() {
      var data = this.dropdownData;
      var name = this.initContent;
      data.forEach(item => {
        if (this.selectedId == item[this.idField]) {
          name = item[this.nameField];
        }
      });
      return name;
    }
  },
  watch: {}
};
</script>

<style scope>
@import url("../../../css/common/custom-scroll.css");
.select-box {
  position: relative;
  font-size: 13px;
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
  top: calc(100% + 1px);
  left: 0;
  right: 0;
  height: 300%;
  overflow-y: auto;
  z-index: 2;
}

.select-items-bottom {
  top: auto;
  bottom: calc(100% + 1px) !important;
}

/* custom list-item */

.select-items div {
  /* color: #000000; */
  height: 32.5%;
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
