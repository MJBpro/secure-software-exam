<template>
  <div v-if="!loading" class="home h-full">
    <NavBar />
    <div class="flex h-full mt-24 justify-center">

      <CreateUserPage   />
   </div>


  </div>

  <div v-else>
        loading...
  </div>
</template>

<script setup>
import { useAuth0 } from "@auth0/auth0-vue";
import NavBar from "@/components/NavBar.vue";
import CreateUserPage from "@/pages/CreateUserPage.vue";
import { onMounted, ref } from "vue";
import { useUserService} from '../services/userService';
import { useRouter } from "vue-router";


var userService = useUserService()
var router = useRouter()
var user = ref()


const loading = ref(false)




  var auth = useAuth0()

 

  onMounted(async ()=>{
      loading.value = true
      await userService.getUser(auth.user.value?.sub)
      .then((res)=>{
        user.value = res
        router.push({name:"Profile"})
      })
      .catch((err)=>{
        console.log("err",err)
      })
        
   
  loading.value = false
  })
</script>

<style scoped>
.home {
  text-align: center;

}
</style>
