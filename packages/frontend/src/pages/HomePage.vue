<template>
  <div class="home h-full">
    <NavBar />
  <div class="flex h-full mt-24 justify-center">

    <UserInformationPage v-if="!user" />
</div>

    <button @click="logout">Logout</button>
    {{auth.isAuthenticated}}
  </div>
</template>

<script setup>
import { useAuth0 } from "@auth0/auth0-vue";
import NavBar from "@/components/NavBar.vue";
import UserInformationPage from "@/pages/UserInformationPage.vue";

import { onMounted, ref } from "vue";



var user = ref()

async function getUser(){
    var userId = auth.user.value?.sub;
    
    await setTimeout(() => {
      console.log(userId)
        return userId
    }, 1000);
}
  var auth = useAuth0()

  async function logout(){
    await auth.logout({openUrl: false}).then(()=>{
      window.location.reload()
    })
  }

  onMounted(async ()=>{
   user.value = await getUser()

  })
</script>

<style scoped>
.home {
  text-align: center;

}
</style>
