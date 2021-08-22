<template>
	<div class="input-wrapper" style="position: relative">
		<label for="Salary">Mức lương cơ bản</label>
		<div class="warning" v-if="warning">
			{{ warning }}
		</div>
		<input
			:class="{ invalid: warning != null }"
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
	data: function() {
		return {
			warning: null
		};
	},
	computed: {
		displayValue: {
			get: function() {
				if (this.value != null && this.value != "")
					return this.value
						.toFixed(0)
						.replace(/[^\d,+]/m, "")
						.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
				else {
					return null;
				}
			},
			set: function(modifiedValue) {
				// var newValue = modifiedValue;
				// Recalculate value after ignoring "$" and "," in user input
				let newValue = parseFloat(modifiedValue.replace(/[^\d.]/g, ""));
				// Ensure that it is not NaN
				if (isNaN(newValue)) {
					newValue = null;
				}
				this.$emit("input", newValue);
			}
		}
	},
	watch: {
		displayValue: function() {
			if (this.displayValue == null) {
					this.warning = null;
				} else if (this.value != parseInt(this.displayValue.replace(/[,]/g, ""))) {
					console.log("oke");
					this.warning = "Chưa đúng định dạng tiền";
				} else {
					this.warning = null;
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
	height: 40px;
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

.invalid {
	border: 1.6px solid red;
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