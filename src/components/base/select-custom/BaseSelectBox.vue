<template>
  <div class="select-box" name="Position">
    <div class="icon-default icon-x" @click="iconXClick"></div>
    <div
      class="select-selected"
      :class="{ 'select-arrow-active': dropdownShow }"
      @click="dropdownShow = !dropdownShow"
    >
      Tất cả vị trí
    </div>
    <div class="select-items" v-show="dropdownShow">
      <div positionid=" " style="border: none">
        <i class="fal fa-check" @click="itemClick"></i>Tất cả vị trí
      </div>
      <div v-for="item in selectItem" :key="item.id" @click="itemClick">
        <i class="fal fa-check"></i>{{ item.content }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SelectBox",
  props: {
    apiURL: {
      type: String,
    },
    extendObj: {
      type: Object,
      default: function () {
        return {
          Id: " ",
          Code: null,
          Name: "Chọn vị trí",
          Description: "null",
          ParentId: null,
          CreatedDate: "1970-01-01T00:00:55",
          CreatedBy: "Tien Anh",
          ModifiedDate: null,
          ModifiedBy: null,
        };
      },
    },
  },
  data() {
    return {
      dropdownShow: false,
      selectItem: [
        { checked: false, id: 1, content: "Giám đốc" },
        { checked: false, id: 2, content: "Trưởng phòng" },
        { checked: false, id: 3, content: "Nhân viên" },
      ],
    };
  },
  methods: {
    iconXClick() {},
    dropdownClick() {},
    itemClick() {},
  },
};
</script>

<style scope>
@import url("../../../css/base/custom-scroll.css");
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
  display: none;
  right: 35px;
  /* top: calc(50% - var(--icon-x-size) / 2); */
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
