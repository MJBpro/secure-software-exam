<template>
  <div class="h-full " style="padding: auto">


  
      <router-view class="h-full" />
  </div>
</template>

<script setup >

</script>

<style>
@tailwind base;
@tailwind components;
@tailwind utilities;
body{
  padding: 0;
  margin: 0;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  padding: 0px;
}

.login-button {
  background-color: #42b983; /* Green background */
  color: white; /* White text */
  border: none; /* No borders */
  padding: 10px 20px; /* Padding around the text */
  border-radius: 5px; /* Rounded corners */
  font-size: 16px; /* Larger font size */
  cursor: pointer; /* Pointer cursor on hover */
}

.login-button:hover {
  background-color: #333; /* Darker background on hover */
}
</style>
