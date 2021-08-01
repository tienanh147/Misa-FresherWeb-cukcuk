<template>
  <div class="header">
    <div class="header-left">
      <div class="toggle"></div>
      <div class="logo"></div>
    </div>
    <div class="header-right">
      <div class="list-dropdown">
        <div class="dropdown-text">Nhà hàng Biển Đông</div>
        <i class="dropdown-arrow fas fa-solid fa-chevron-down"></i>
      </div>
      <div class="account">
        <div class="avatar"></div>
        <div class="name">Trần Tiến Anh</div>
        <div class="more"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Header"
};
</script>

<style scoped>
@import "../../css/layout/header.css";
</style>
