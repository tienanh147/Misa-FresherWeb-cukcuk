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
          v-for="index in viewPages(4)"
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
    totalRecord: {
      type: Number,
      default: function() {
        return 2000;
      },
      required: true
    },
    nameRecord: {
      type: String,
      default: function() {
        return "nhân viên";
      },
      required: true
    },
    totalPage: {
      type: Number,
      default: function() {
        return 100;
      },
      required: true
    },
    pageSize: {
      type: Number,
      default: function() {
        return 20;
      },
      required: true
    },
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
  methods: {
    // var vm = this;
    btnNextPageClick() {
      if (this.currPage == this.totalPage) return;
      this.currPage++;
      this.sendEmit();
    },
    btnPrevPageClick() {
      if (this.currPage == 1) return;
      this.currPage--;
      this.sendEmit();
    },
    btnLastPageClick() {
      this.currPage = this.totalPage;
      this.sendEmit();
    },
    btnFirstPageClick() {
      this.currPage = 1;
      this.sendEmit();
    },
    btnNumberClick(index) {
      this.currPage = index;
      this.sendEmit();
    },
    sendEmit() {
      return this.$emit("paging-select", this.pageSize, this.currPage);
    }
  },
  computed: {
    viewPages() {
      // var num = 4;
      return num => {
        var startNumber = this.currPage - ((this.currPage - 1) % 4);
        var endNumber =
          startNumber + num - 1 <= this.totalPage
            ? startNumber + num - 1
            : this.totalPage;
        return Array(endNumber - startNumber + 1)
          .fill()
          .map((_, idx) => startNumber + idx);
      };
    },
    viewRecordRange() {
      var start = (this.currPage - 1) * this.pageSize + 1;
      var end =
        this.currPage * this.pageSize < this.totalRecord
          ? this.currPage * this.pageSize
          : this.totalRecord;
      return start + "-" + end + "/" + this.totalRecord;
    }
  }
};
</script>

<style scoped>
@import url("../../../css/base/paging.css");
</style>
