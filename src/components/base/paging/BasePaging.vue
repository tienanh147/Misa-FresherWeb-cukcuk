<template>
	<div
		class="paging"
		tabindex="0"
	
		@keydown.right="btnNextPageClick"
		@keydown.left="btnPrevPageClick"
	>
		
		<div class="paging-left">
			Hiển thị <span name="total-record">{{ viewRecordRange }}</span
			>{{ nameRecord }}
		</div>

		<div class="paging-bar" @mouseenter="focusPaging">
			<div
				class="btn-paging-base btn-first-page"
				@click="btnFirstPageClick"
			></div>

			<div
				class="btn-paging-base btn-prev-page"
				@click="btnPrevPageClick"
			></div>

			<div class="number-container">
				<div
					class="btn-paging-base btn-page-number"
					v-for="index in viewPages"
					:key="index"
					:class="{ 'number-active': index == pageNumber }"
					@click="btnNumberClick(index)"
				>
					{{ index }}
				</div>
			</div>

			<div
				class="btn-paging-base btn-next-page"
				@click="btnNextPageClick"
			></div>
			<div
				class="btn-paging-base btn-last-page"
				@click="btnLastPageClick"
			></div>
		</div>
		<SelectBox
			ref="pageSizeSelect"
			style="height: 35px; width: 170px; min-width: 170px"
			:dropup="true"
			:selectBoxData="pageSizeSelect"
			:idField="pageSizeSelect[1]"
			:nameField="pageSizeSelect[2]"
			:initContent="pageSizeSelect[0]"
			v-model="ownPageSize"
		/>
	</div>
</template>

<script>

import SelectBox from "../select-custom/BaseSelectBox.vue";
export default {
	name: "Paging",
	components: { SelectBox },
	props: {
		/**
		 * Số bản ghi
		 */
		totalRecord: {
			type: Number,
			default: function() {
				return 2000;
			},
			required: true
		},

		/**
		 * loại bản ghi
		 */
		nameRecord: {
			type: String,
			default: function() {
				return "nhân viên";
			},
			required: true
		},

		/**
		 * tổng số trang
		 */
		totalPage: {
			type: Number,
			default: function() {
				return 100;
			},
			required: true
		},

		/**
		 * số bản ghi 1 trang
		 */
		pageSize: {
			type: Number,
			default: function() {
				return 20;
			},
			required: true
		},

		/**
		 * trang hiện tại
		 */
		pageNumber: {
			type: Number,
			default: function() {
				return 1;
			},
			required: true
		}
	},
	created(){
		this.defaultPageSize = this.pageSize;
	},
	mounted: function() {
		this.currPage = this.pageNumber;
		this.changeDefaultSelectbox();
	},

	data() {
		return {
			defaultPageSize: this.pageSize,
			ownPageSize: this.pageSize,
			currPage: this.pageNumber,
			pageSizeSelect: [
				`${this.pageSize} ${this.nameRecord}/trang`,
				"pageSize",
				"pageSizeName",
				{ pageSize: 10, pageSizeName: `10 ${this.nameRecord}/trang` },
				{ pageSize: 20, pageSizeName: `20 ${this.nameRecord}/trang` },
				{ pageSize: 50, pageSizeName: `50 ${this.nameRecord}/trang` },
				{ pageSize: 100, pageSizeName: `100 ${this.nameRecord}/trang` }
			]
		};
	},
	methods: {
		
		changeDefaultSelectbox() {
			var vm = this;
			var selectBoxElmt = this.$refs.pageSizeSelect.$el;
			selectBoxElmt.removeChild(selectBoxElmt.querySelector(".icon-x"));
			selectBoxElmt.querySelector(".select-items").onkeydown = function(event) {
				if (event.which == 8 || event.which == 46) {
					event.preventDefault();
				}
				else if(event.which == 9){
					event.preventDefault();
					console.log(vm.$el);
				}
				this.$refs.pageSizeSelect.tabindexItem = "1";
				console.log(event.which);
			};
			// selectBoxElmt.querySelector(".select-items").setAttribute("style","overflow:auto;");
		},
		/**
		 * Set sự kiện click cho nút NextPage.
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		btnNextPageClick() {
			if (this.pageNumber == this.totalPage) return;
			this.currPage = this.pageNumber + 1;
			this.sendEmit();
		},

		/**
		 * Set sự kiện click cho nút PrevPage.
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		btnPrevPageClick() {
			if (this.pageNumber == 1) return;
			this.currPage = this.pageNumber - 1;
			this.sendEmit();
		},

		/**
		 * Set sự kiện click cho nút LastPage.
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		btnLastPageClick() {
			this.currPage = this.totalPage;
			this.sendEmit();
		},

		/**
		 * Set sự kiện click cho nút FirstPage.
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		btnFirstPageClick() {
			this.currPage = 1;
			this.sendEmit();
		},

		/**
		 * Set sự kiện click cho nút Number
		 * @param {Number} index
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		btnNumberClick(index) {
			this.currPage = index;
			this.sendEmit();
		},

		focusPaging() {
			this.$el.focus();
		},
		/**
		 * hàm emit ra ngoài để phân trang
		 */
		sendEmit() {
			return this.$emit("paging-select", this.ownPageSize, this.currPage);
		}
	},

	computed: {
		/**
		 * tính toán các nút Number trang hiện thị
		 * @return {Array}
		 * CreatedBy: TTAnh(10/08/2021)
		 */
		viewPages() {
			/**
			 * số nút Number
			 */
			var num = 4;
			var startNumber = this.pageNumber - ((this.pageNumber - 1) % 4);
			var endNumber;

			endNumber =
				startNumber + num - 1 <= this.totalPage
					? startNumber + num - 1
					: this.totalPage;
			if (endNumber - startNumber + 1 >= 0)
				return Array(endNumber - startNumber + 1)
					.fill()
					.map((_, idx) => startNumber + idx);
			else return null;
		},

		/**
		 * Tính toán số thử tự của trang hiện tại
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		viewRecordRange() {
			var start = (this.pageNumber - 1) * this.pageSize + 1;
			var end =
				this.pageNumber * this.pageSize < this.totalRecord
					? this.pageNumber * this.pageSize
					: this.totalRecord;
			return start + "-" + end + "/" + this.totalRecord;
		}
	},

	watch: {
		ownPageSize: function(newVal) {
			if(newVal == null) this.ownPageSize = this.defaultPageSize;
			this.currPage = 1;
			this.sendEmit();
		}
	}
};
</script>

<style scoped>
.paging {
	height: 56px;
	width: 100%;
	display: flex;
	align-items: center;
	box-sizing: border-box;
	outline: none;
	/* display: grid;
    grid-template-columns: 200px auto 200px;
    align-items: center; */
}

.paging .paging-left {
	width: 200px;
	margin-left: 10px;
	font-size: 13px;
	white-space: nowrap;
}

.paging .paging-right {
	background-color: var(--object-color);
	margin-right: 10px;
	font-size: 13px;
	width: 11em;
	padding-left: 8px;
	padding-right: 10px;
	height: 30px;
	white-space: nowrap;
}

.paging .paging-left span {
	font-weight: bold;
	margin-right: 2px;
}

.paging .paging-right span {
	font-weight: bold;
	margin-right: 2px;
}

.paging .paging-right .icon-updown-wrapper {
	/* height: 30px; */
	margin-left: 0.5em;
	display: flex;
	flex-direction: column;
	font-size: 10px;
	justify-content: flex-start;
}

.icon-updown-wrapper .icon-up {
	height: 14px;
	width: 16px;
	background-image: url(../../../assets/icon/chevron-up.svg);
	background-size: 20px 18px;
	background-position: center top;
	background-repeat: no-repeat;
	cursor: pointer;
}

.icon-updown-wrapper .icon-down {
	height: 14px;
	width: 16px;
	background-image: url(../../../assets/icon/chevron-down.svg);
	background-size: 20px 18px;
	background-position: center bottom;
	background-repeat: no-repeat;
	cursor: pointer;
}

.paging .paging-bar {
	flex: 1;
	display: flex;
	align-items: center;
	justify-content: center;
	min-width: 400px;
	height: 100%;
	width: 100%;
	/* background-color: violet; */
}

.paging-bar .btn-paging-base {
	background-color: transparent;
	height: 30px;
	width: 30px;
	min-width: 30px;
	border-radius: 4px;
	background-position: center;
	background-size: 25px 25px;
	background-repeat: no-repeat;
	cursor: pointer;
}

.paging-bar .btn-paging-base:hover {
	background-color: var(--object-color);
	border: none;
	color: #000;
}

.paging-bar .btn-prev-page {
	margin-left: 15px;
	margin-right: 10px;
	background-image: url(../../../assets/icon/btn-prev-page.svg);
}

.paging-bar .btn-first-page {
	background-image: url(../../../assets/icon/btn-firstpage.svg);
}

.paging-bar .btn-next-page {
	margin-left: 10px;
	margin-right: 15px;
	background-image: url(../../../assets/icon/btn-next-page.svg);
}

.paging-bar .btn-last-page {
	background-image: url(../../../assets/icon/btn-lastpage.svg);
}

.paging-bar .number-container {
	/* flex: 0.3; */
	display: flex;
	flex-direction: row;
	justify-content: center;
}

.paging-bar .btn-page-number {
	margin: 5px;
	font-size: 13px;
	border: 1px solid var(--border-item-color);
	border-radius: 100%;
	display: flex;
	justify-content: center;
	align-items: center;
	box-sizing: border-box;
}

.paging-bar .number-active {
	background-color: #019160;
	color: #ffffff;
}
</style>
