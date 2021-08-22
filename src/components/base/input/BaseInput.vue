<template>
	<div class="input-wrapper">
		<label v-html="computedLabel"></label>

		<div class="warning" v-if="warning">
			{{ warning }}
		</div>
		<input
			:class="{ invalid: warning != null }"
			@blur="inputBlur($event)"
			class="text-box-default input-item"
			v-bind="$attrs"
			:type="type"
			@input="inputEmit"
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
	model: { prop: "inputValue", event: "input" },
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
			type: String,
			default() {
				return "text";
			}
		},
		timer: {
			type: Number,
			default() {
				return 500;
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
		/**
		 * Hàm set sự kiện blur
		 * @param {event} event
		 * CreatedBy: TTAnh(10/08/2021)
		 */
		inputBlur(event) {
			var elmt = event.target;

			if (this.required == true) {
				if (elmt.value == "") {
					elmt.setAttribute("title", "Thông tin này không được bỏ trống");
					this.warning = "Thông tin này là bắt buộc";
				} else if (this.inputValidate(elmt.value)) {
					this.warning = null;
				}
			}
		},

		/**
		 * hàm set sự kiện input
		 * @param {event} event
		 * CreatedBy: TTAnh(10/08/2021)
		 */
		inputEmit(event) {
			var vm = this;
			var elmt = event.target;
			if (elmt.value == "") vm.$emit("input", null);
			else vm.$emit("input", elmt.value);

			clearTimeout(this.debounce);
			this.warning = null;

			this.debounce = setTimeout(function() {
				var value = elmt.value;
				if (value == "") {
					if (this.required) {
						vm.warning = "Thông tin này là bắt buộc";
					}
				} else if (vm.inputValidate(value)) {
					vm.warning = null;
				} else {
					vm.warning = "Vui lòng nhập đúng định dạng";
				}
			}, this.timer);
		},

		/**
		 * hàm validate dữ liệu
		 * CreatedBy: TTAnh(10/08/2021)
		 */
		inputValidate(value) {
			var re;
			if (this.validate == "email") {
				re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
				return re.test(String(value).toLowerCase());
			} else return value != null && value != "";
		}
	},
	computed: {
		formatInputValue: function() {
			if (this.inputValue == null) {
				return "";
			} else if (this.type == "date") {
				return this.inputValue.slice(0, 10);
			} else return this.inputValue;
		},

		computedLabel: function() {
			if (this.required)
				return this.label + " <span style='color: red'>*</span>";
			return this.label;
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
	outline: none;
	box-sizing: border-box;
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
	font-size: 13px;
	max-width: 224px;
	height: 40px;
	outline: none;
}

input::placeholder {
	font-size: 12px;
}

input:focus {
	border: 1px solid #019160;
}

.invalid {
	border: 1.6px solid red;
}

/* Chrome, Safari, Edge, Opera */
/* input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
input[type="date"]::-webkit-calendar-picker-indicator {
  opacity: 1;
  padding: 0;
} */

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