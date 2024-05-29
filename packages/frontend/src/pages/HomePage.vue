<template>
  <div class="home">
    <NavBar />

    <h1>Welcome to the Home Page</h1>
    <p>You are now logged in!</p>
    <button @click="logout">Logout</button>
    {{auth.isAuthenticated}}
  </div>
</template>

<script setup>
import { useAuth0 } from "@auth0/auth0-vue";
import NavBar from "@/components/NavBar.vue";

  var auth = useAuth0()
  async function logout(){
    await auth.logout({openUrl: false}).then(()=>{
      window.location.reload()
    })
  }
</script>

<style scoped>
.home {
  text-align: center;

}
</style>
