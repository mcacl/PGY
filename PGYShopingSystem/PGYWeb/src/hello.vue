<template>
  <div >
    <p>hellow vue</p>
    <p>{{msg}}</p>
  </div>
</template>

<script>
  export default {
    name: "hello",
    data() {
      return {msg: '组件666'};
    }
  }
</script>
