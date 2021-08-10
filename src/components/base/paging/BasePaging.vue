<template>
  <div class="paging">
    <div class="paging-left">
      Hiển thị <span name="total-record">{{ viewRecordRange }}</span
      >{{ nameRecord }}
    </div>

    <div class="paging-bar">
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

    <div class="text-box-default paging-right">
      <span name="record-per-page">{{ pageSize }}</span
      >nhân viên/trang
      <div class="icon-updown-wrapper">
        <div class="icon-up"></div>
        <div class="icon-down"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Paging",
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

  data() {
    return {
      currPage: this.pageNumber
    };
  },
  mounted() {
    // this.pageNumber = this.pageNumber;
  },
  methods: {
    // var vm = this;
    /**
     * Set sự kiện click cho nút NextPage.
     * CreatedBy: TTAnh(08/08/2021)
     */
    btnNextPageClick() {
      if (this.pageNumber == this.totalPage) return;
      this.currPage++;
      this.sendEmit();
    },

    /**
     * Set sự kiện click cho nút PrevPage.
     * CreatedBy: TTAnh(08/08/2021)
     */
    btnPrevPageClick() {
      if (this.pageNumber == 1) return;
      this.currPage--;
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

    /**
     * hàm emit ra ngoài để phân trang
     */
    sendEmit() {
      return this.$emit("paging-select", this.pageSize, this.currPage);
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
  }
};
</script>

<style scoped>
@import url("../../../css/base/paging.css");
</style>
