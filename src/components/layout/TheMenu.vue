<template>
    <div class="menu">
        <a class="menu-item" href="#">
            <div class="menu-icon icon-dashboard"></div>
            Tổng quan
        </a>
        <a class="menu-item" href="#">
            <div class="menu-icon icon-report"></div>
            Báo cáo
        </a>
        <a class="menu-item" href="#">
            <div class="menu-icon icon-dashboard"></div>
            Mua hàng
        </a>
        <a class="menu-item menu-item-active" href="#">
            <div class="menu-icon icon-employee "></div>
            Danh mục nhân viên
        </a>
        <a class="menu-item" href="#">
            <div class="menu-icon icon-setting"></div>
            Thiết lập hệ thống
        </a>
    </div>
</template>

<script>
export default {
    name: 'Menu'
}
</script>
<style scoped>
@import '../../css/layout/menu.css';
</style>