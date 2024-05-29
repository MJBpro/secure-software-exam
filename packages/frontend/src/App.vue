<template>
  <img alt="Vue logo" src="./assets/logo.png">
  <HelloWorld msg="Welcome to Your Vue.js App"/>

  <button class="login-button" @click="login">Log in</button>

</template>

<script>
import {useAuth0} from '@auth0/auth0-vue';
import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'App',
  components: {
    HelloWorld,
  },

  setup() {
    const {loginWithRedirect} = useAuth0();

    return {
      login: () => {
        loginWithRedirect();
      }
    };
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
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
