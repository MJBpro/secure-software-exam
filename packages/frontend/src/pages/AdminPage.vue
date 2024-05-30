<template>
    <div>
        <NavBar />
        <div class="mr-auto">
            <button  @click="goBack" class="flex mt-2 ml-2  items-center gap-2 hover:bg-gray-200 text-white rounded p-1 px-2 bg-gray-400">
                <svg data-slot="icon" class="w-4" fill="none" stroke-width="1.5" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M6.75 15.75 3 12m0 0 3.75-3.75M3 12h18"></path>
                </svg>
                Go back
            </button>
        </div>

       <div  class="p-12 pt-4 flex flex-col gap-8"> 

            <div class=" text-left ">
               <h1 class="text-3xl font-bold">Manage users</h1> 
                <p class="text-md ">Here you can view and mange all users</p> 
            </div>

            <div>
                <div class="px-12">
                    <p class="text-left">Find a user</p>
                    <div class="flex gap-1">
                        <input type="text" v-model="query" placeholder="Search query..." class="shadow appearance-none border border-gray-300 rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" >
                        <button @click="search" class="bg-blue-300 rounded text-xs font-semibold w-12 p-2">
                            GO
                        </button>
                    </div>
                </div>

            </div>

            <div v-if="searched && users?.length <= 0" >
                    <h2>No Users Found</h2>
            </div >
            <div v-if="users?.length > 0" class="border rounded mx-12">
                <div v-for="user in users" :key="user" class="rounded m-2 justify-between px-12 flex p-2 gap-6 bg-slate-100">
                        <div class="flex  flex-col">
                            <p class="text-xs font-semibold">First name</p>
                            <p>{{user?.firstName}} </p>
                        </div>
                        <div class="flex flex-col">
                            <p class="text-xs font-semibold">Last name</p>
                            <p>{{user?.lastName}} </p>
                        </div>
                         <div class="flex flex-col">
                            <p class="text-xs font-semibold">Address</p>
                            <p>**********</p>
                        </div>
                        <div class="flex flex-col">
                            <p class="text-xs font-semibold">Birthday</p>
                            <p>**********</p> 
                        </div>
                </div>
            </div>
       </div>
    </div>
</template>

<script setup>
import NavBar from "@/components/NavBar.vue";
import { useUserService } from "@/services/userService";
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter()
const userService = useUserService()

const query = ref("")
const users = ref([])
const searched  = ref(false)

async function search(){
    searched.value = true
    await userService.searchForUser(query.value)
    .then(res=> {
        users.value = res
    })
    .catch((err)=>{
         users.value = []
        console.log("err", err)
    })
}


function goBack(){
    router.push({name: "HomePage"})
}

</script>
