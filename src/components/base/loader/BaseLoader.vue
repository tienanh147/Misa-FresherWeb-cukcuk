<template>
  <div class="modal-loader">
    <div class="loader" :style="loaderStyle"></div>
  </div>
</template>

<script>
export default {
  name: "Loader",
  props: {
    size: {
      type: String,
      default() {
        return "60px";
      }
    },
    color: {
      type: String,
      default() {
        return "#01b075";
      }
    }
  },
  created() {
  },
  computed: {
    loaderStyle() {
      return {
        width: this.size,
        height: this.size,
        "border-width": this.calcBorderSize,
        "border-top-color": this.color
      };
    },
    calcBorderSize() {
      var size = parseFloat(this.size);
      var unit = this.size.replace(/[\d.]/g, "");
      return (size / 6).toFixed(1).toString() + unit;
    }
  }
};
</script>

<style scoped>
.modal-loader {
  background-color: rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: space-around;
  align-items: center;
}
.loader {
  border-style: solid;
  border-color: #f3f3f3;
  border-radius: 50%;
  border-top-color: #01b075;
  -webkit-animation: spin 1s linear infinite; /* Safari */
  animation: spin 0.5s linear infinite;
}

/* Safari */
@-webkit-keyframes spin {
  0% {
    -webkit-transform: rotate(0deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
  }
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>