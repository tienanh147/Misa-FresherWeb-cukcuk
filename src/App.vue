<template>
  <!-- <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    
  </div> -->
  <div id ="app">
    <Header/>
    <Menu/>
    <Content/>
  </div>
  
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue';
import Header from './components/layout/TheHeader.vue';
import Menu from './components/layout/TheMenu.vue';
import Content from './components/layout/TheContent.vue'
export default {
  name: 'App',
  components: {
    Header, Menu, Content
    
  }
}
</script>

<style>

@import './css/main.css';
</style>
