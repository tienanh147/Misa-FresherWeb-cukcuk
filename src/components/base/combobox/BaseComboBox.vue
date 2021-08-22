<template>
	<div
		tabindex="1"
		class="combo-box"
		:class="{
			'combo-box-show': dropdownShow,
			'combo-box-error': valueNotFound,
		}"
	>
		<div
			class="icon-default icon-x"
			@click="iconXClick"
			v-show="selectedId != null"
		></div>
		<input

			:placeholder="initContent"
			@keydown.enter="enterInput"
			@focus="focusInput"
			@blur="blurInput"
			@input="typing"
			
			v-model="inputValue"
			type="text"
			class="combo-box-input"
		/>
		<div class="icon-arrow-wrapper" @click="dropdownClick">
			<div class="icon-chevron"></div>
		</div>

		<div
			class="select-items"
			:class="{
				'select-hide': filterData.length == 0,
				'select-items-bottom': dropup,
			}"
		>
			<div
				v-for="item in displayData"
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
	model: { prop: "selectedId", event: "combobox-select" },
	name: "BaseComboBox",
	props: {
		/** Dữ liệu của comboBox ****
		 * Bao gồm 3 phần:
		 * 1. initContent: nội dung mặc định
		 * 2. idField: tên của trường Id trong object lựa chọn
		 * 3. nameField: tên của trường Name trong object lựa chọn
		 * 4. dropdown data: 1 mảng gồm các lựa chọn,
		 * mỗi lựa chọn là 1 object có 2 trường bắt buộc là Id và Name
		 */
		comboBoxData: {
			type: Array
		},

		/**
		 * trường id trong option của comboBoxData
		 */
		idField: {
			type: String,
			default() {
				return "PositionId";
			}
		},

		/**
		 * trường nội dung trong option của comboBoxData
		 */
		nameField: {
			type: String,
			default() {
				return "PositionName";
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
		},
		initContent:{
			type: String
		},
		dropup: {
			type: Boolean,
			default() {
				return false;
			}
		}
	},
	data: function() {
		return {
			/**
			 * hiển thị dropdown hay không
			 * --true: Hiển thị
			 * --false: Ẩn
			 */
			dropdownShow: false,
			inputValue: null,
			valueNotFound: false,
			showMode: 1
		};
	},
	mounted() {
		/** Giữ nguyên dữ liệu khi chọn chưa đúng*/
		this.inputValue = this.computedInputValue;
	},
	methods: {

		/** Set sự kiện input */
		typing: function() {
			this.dropdownShow = true;
			this.showMode = 2;
			this.valueNotFound = false;
		},
		/**
		 * Sự kiện focus vào ô input
		 * CreatedBy: TTAnh (20/08/2021)
		 * @param {event} event
		 */
		focusInput() {
			this.dropdownShow = true;
			this.valueNotFound = false;
			this.showMode = 1;
		},

		/**
		 * Sự kiện blur ra ngoài ô input
		 * CreatedBy: TTAnh (20/08/2021)
		 * @param {event} event
		 */
		blurInput(event) {
			if (event.relatedTarget !== null) {
				if (event.target.parentElement !== event.relatedTarget) {
					this.dropdownShow = false;
					this.inputValue = this.computedInputValue;
				}
			} else {
				this.dropdownShow = false;
				this.inputValue = this.computedInputValue;
			}
			this.valueNotFound = false;
		},
		/**
		 * Sự kiện nhấn enter ở input combobox
		 * CreatedBy: TTAnh (20/08/2021)
		 */
		enterInput() {
			var valueFound = false;
			this.filterData.forEach(item => {
				var itemValue = item[this.nameField].toLowerCase();
				var inputValue;
				if (this.inputValue == null || this.inputValue == "") {
					this.inputValue = null;
					this.sendEmit(null, "");
				} else {
					inputValue = this.inputValue.toLowerCase();
				}
				if (itemValue.includes(inputValue)) {
					this.sendEmit(item[this.idField], item[this.nameField]);
					valueFound = true;
				}
			});
			if (valueFound == false) this.valueNotFound = true;
		},
		/**
		 * set sự kiện cho nút xóa lựa chọn.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		iconXClick() {
			this.dropdownShow = false;
			this.inputValue = null;
			this.sendEmit(null);
		},
		/**
		 * set sự kiện ẩn hiện dropdown.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		dropdownClick() {
			this.dropdownShow = !this.dropdownShow;
			this.showMode = 1;
		},
		/**
		 * hàm set sự kiện click vào 1 option
		 * @param {String} id id của option được click
		 * @param {String} name nội dung của option được click
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		itemClick(id, name) {
			this.dropdownShow = false;

			this.sendEmit(id, name);
		},


		/**
		 * Hàm emit sự kiện
		 * @param {String} id id của option được chọn
		 */
		sendEmit(id, name) {
			this.inputValue = name;
			return this.$emit("combobox-select", id);
		}
	},
	computed: {
		/** Dữ liệu đã được lọc theo input */
		filterData: {
			get: function() {
				if (this.inputValue == "" || this.inputValue == null) {
					return this.dropdownData;
				} else {
					var filterData = [];

					this.dropdownData.forEach(item => {
						var itemValue = item[this.nameField].toLowerCase();
						var inputValue = this.inputValue.toLowerCase();
						if (itemValue.includes(inputValue)) {
							filterData.push(item);
						}
					});
					return filterData;
				}
			}
		},
		/**
		 * do dữ liệu truyền vào bao gồm cả initContent, idField, nameField
		 * nên cần cắt đi 3 phần tử đầu của mảng.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		dropdownData: function() {
			return this.comboBoxData.slice(3);
		},

		/** Dữ liệu hiển thị lên cho người dùng */
		displayData: function() {
			if (this.showMode == 1) {
				return this.dropdownData;
			} else {
				return this.filterData;
			}
		},
		/**
		 * tìm ra nội dung của option với id là selectedId.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		computedInputValue: function() {
			var name = "";
			this.dropdownData.forEach(item => {
				if (this.selectedId == item[this.idField]) {
					name = item[this.nameField];
				}
			});
			return name;
		}
	}
};
</script>

<style scoped>
.combo-box {
	outline: none;
	font-size: 13px;
	height: 40px;
	width: 100%;
	background-color: #fff;
	/* top: 30%;
	left: 50%;
	transform: translate(-50%, -50%); */
	border-radius: 4px;
	border: 1px solid #bbb;
	display: flex;
	align-items: center;
	justify-content: space-between;
	position: relative;
	z-index: 2;
	box-sizing: border-box;
}
.combo-box.combo-box-error {
	border: 1px solid red !important;
}
.combo-box.combo-box-show .select-items {
	display: block;
}
.combo-box.combo-box-show .icon-chevron {
	transform: rotate(180deg);
	transition: all 0.3s ease;
}

.combo-box.combo-box-show .icon-arrow-wrapper {
	border: none;
	background-color: #e9ebee;
}

.combo-box.combo-box-show {
	border: 1px solid #019160;
}

/* 1. combo-box INPUT */
.combo-box-input {
	outline: none;
	width: calc(100% - 92px);
	border: 0;
	border-radius: 4px;
	outline: none;
	font-size: 13px;
	padding-left: 16px;
	background-color: #fff;
}

::placeholder {
	/* Chrome, Firefox, Opera, Safari 10.1+ */
	font-family: GoogleSans-Regular;
	color: #000;
	opacity: 1; /* Firefox */
}

:-ms-input-placeholder {
	/* Internet Explorer 10-11 */
	font-family: GoogleSans-Regular;
	color: #000;
}

::-ms-input-placeholder {
	/* Microsoft Edge */
	font-family: GoogleSans-Regular;
	color: #000;
}

.combo-box .icon-x {
	background-color: #e5e5e5;
	border-radius: 50%;
	position: absolute;
	right: 50px;
	height: 15px;
	width: 15px;
	z-index: 1;
	cursor: pointer;
}

.combo-box .icon-x:hover {
	background-color: rgb(252, 140, 140);
}
.combo-box .icon-arrow-wrapper {
	border-radius: 0px 4px 4px 0px;
	border-left: 1px solid #bbbbbb;
	position: absolute;
	right: 0;
	width: 40px;
	height: 100%;
	display: flex;
	align-items: center;
	justify-content: center;
}

.icon-arrow-wrapper .icon-chevron {
	transition: all 0.3s ease;

	height: 20px;
	width: 20px;
	background-image: url("../../../assets/icon/chevron-down.svg");
	background-size: 18px 18px;
	background-repeat: no-repeat;
	background-position: center;
}

/*point the arrow upwards when the select box is open (active):*/

.combo-box .icon-chevron-active {
	/* background-image: url('../../Resource/icon/chevron-up-solid.svg'); */
	transform: rotate(180deg);
	border: none;
	background-color: #e9ebee;
	border-radius: 4px 0px 0px 4px;
}

.select-items {
	/* color: #000000; */
	/* padding: 2px 0px; */
	border-radius: 4px;
	cursor: pointer;
	box-shadow: 0 0 5px rgba(0, 0, 0, 0.4);
	height: fit-content;
	background-color: #ffffff;
}

/*style items (options):*/

.select-items {
	display: none;
	position: absolute;
	top: calc(100% + 3px);
	left: 0;
	right: 0;
	max-height: 320%;
	overflow-y: auto;
}

.select-items-bottom {
	top: auto;
	bottom: calc(100% + 1px) !important;
}

/* custom list-item */

.select-items div {
	height: 40px;
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
	display: none !important;
}
</style>