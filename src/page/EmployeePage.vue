<template>
  <div id="employee-page">
    <Header />
    <Menu />
    <EmployeeContent />
  </div>
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue';
import Header from "../components/layout/TheHeader.vue";
import Menu from "../components/layout/TheMenu.vue";
import EmployeeContent from "../components/layout/TheEmployeeContent.vue";
export default {
  name: "EmployeePage",
  components: {
    Header,
    Menu,
    EmployeeContent
  }
};
</script>

<style>
@import "../css/main.css";
</style>
