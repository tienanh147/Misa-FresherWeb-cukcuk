<template>
	<!--   @focus="focusDropDown"-->
	<div class="select-box" v-bind="$attrs">
		<div
  
			class="icon-default icon-x"
			@click="iconXClick"
			v-show="selectedId != null"
		></div>

		<div
			tabindex="0"
			@blur="blurSelectBox"
			@focus="showDropdown"
      @keydown.delete="iconXClick"
			@keydown.down="focusOnItem"
			class="select-selected"
			:class="{ 'select-arrow-active': dropdownShow }"
			
		>
			{{ selectedName }}
		</div>

		<div
			class="select-items"
			v-show="dropdownShow"
			:class="{ 'select-items-bottom': dropup }"
		>
    
			<div
				v-for="item in dropdownData"
				:key="item[idField]"
				:tabindex="tabindexItem"
        @keydown.delete="iconXClick"
				@keydown.up="prevItemFocus"
				@keydown.down="nextItemFocus"
				@keydown.enter="itemClick(item[idField], item[nameField])"
				@blur="blurSelectBox"
        @keydown.tab="dropdownHide"
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
		/** Dữ liệu của selectbox ****
		 * Bao gồm 3 phần:
		 * 1. initContent: nội dung mặc định
		 * 2. idField: tên của trường Id trong object lựa chọn
		 * 3. nameField: tên của trường Name trong object lựa chọn
		 * 4. dropdown data: 1 mảng gồm các lựa chọn,
		 * mỗi lựa chọn là 1 object có 2 trường bắt buộc là Id và Name
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
		 * tên của trường Id trong option của dropdown data
		 */
		idField: {
			type: String,
			default() {
				return "PositionId";
			}
		},

		/**
		 * tên của trường Name trong option của dropdown data
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
				return null;
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
			selectedName: this.computedSelectedName,

			/** */
			itemFocusIndex: 0,
      tabindexItem: "1"
		};
	},

	methods: {
		/**
		 * set sự kiện cho nút xóa lựa chọn.
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		iconXClick() {
			this.dropdownShow = false;
			this.selectedName = this.initContent;
			this.sendEmit(null);
		},

		/**
		 * ẩn dropdown
		 */
		blurSelectBox(event) {
			if (event.relatedTarget !== null) {
				if (!this.$el.contains(event.relatedTarget)) {
					this.dropdownShow = false;
				}
				
			}else{
        this.dropdownShow = false;
      }
      // console.log(event.relatedTarget);
		},

		/**
		 * set sự kiện ẩn hiện dropdown.
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		showDropdown() {

			this.itemFocusIndex = 0;
			this.dropdownShow = true;
      this.tabindexItem = "1";
		},

    dropdownHide(event){
      event.preventDefault();
      this.dropdownShow = false;
      this.tabindexItem = "0";
    },

		focusOnItem() {
			// this.$el.querySelector('.select-items').childNodes[0].focus();
			this.$el
				.querySelector(".select-items")
				.childNodes[0].focus();
      this.tabindexItem = "0";
		},

		nextItemFocus() {
			if (this.itemFocusIndex == this.dropdownData.length - 1) {
				this.itemFocusIndex = 0;
				return;
			}
			this.itemFocusIndex++;
		},
		prevItemFocus() {
			if (this.itemFocusIndex == 0) {
				this.dropdownShow = false;
				return;
			}
			this.itemFocusIndex--;
		},
		/**
		 * hàm set sự kiện click vào 1 option
		 * @param {String} id id của option được click
		 * @param {String} name nội dung của option được click
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		itemClick(id, name) {
			// this.selectedId = id;
			this.dropdownShow = false;
			this.selectedName = name;
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
	watch: {
		itemFocusIndex: function(val) {
      var focused = this.$el.querySelector(".select-items").childNodes[val];
			focused.focus();
      focused.scrollIntoView({ behavior: 'smooth' });

		}
	}
};
</script>

<style scope>
.select-box {
	outline: none;
	position: relative;
	font-size: 13px;
	white-space: nowrap;
	height: 40px;
	width: 100%;
	display: flex;
	align-items: center;
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
	outline: none;
	padding-left: 16px;
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
	transition: all 0.3s ease;
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
	transition: all 0.3s ease;
}

.select-selected.select-arrow-active {
	border-color: #019160;
}

.select-items {
	box-shadow: 0 0 5px rgba(0, 0, 0, 0.4);
	padding: 2px 0px;
	border-radius: 4px;
	cursor: pointer;
	background-color: #ffffff;
}

/*style items (options):*/

.select-items {
	/* display: none; */
	outline: none;
	position: absolute;
	top: calc(100% + 3px);
	left: 0;
	right: 0;
	height: 305%;
	/* max-height: 305%; */
	overflow-y: hidden;
	/* overscroll-behavior: contain; */
	z-index: 2;
}

.select-items-bottom {
	top: auto;
	bottom: calc(100% + 1px) !important;
}

/* custom list-item */

.select-items div {
	outline: none;
	height: 33%;
	cursor: pointer;
	user-select: none;
	display: flex;
	align-items: center;
}

.select-items div i {
	visibility: hidden;
	margin: auto 10px;
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

.select-items div:focus {
	background-color: #e9ebee;
	color: #000;
}

/*hide the items when the select box is closed:*/

.select-hide {
	display: none;
}
</style>
