<template>
    <div>
        <NavBar />
        <div v-if="user" class="p-12 flex flex-col gap-8"> 

            <div class=" text-left ">
               <h1 class="text-3xl font-bold">Welcome to your profile {{`${user?.firstName} ${user?.lastName}`}}</h1> 
            </div>
            

            <div class="w-full flex ">
                    <div class="text-left p-6  w-[22rem] rounded-md min-h-5 bg-slate-200 ">
                        <h3 class="font-bold">Your information</h3>
                        <div class="flex flex-col gap-4 mt-2">
                                <div class="w-full flex flex-col gap-">
                                    <p>Full name</p>
                                    <span class="bg-blue-200 w-full p-2 rounded">
                                        {{user?.firstName}}  {{user?.firstName}}
                                    </span>
                                </div>

                                 <div class="w-full flex flex-col gap-">
                                    <p>Address</p>
                                    <span class="bg-blue-200 w-full p-2 rounded">
                                        {{user?.address}} 
                                    </span>
                                </div>
                                 <div class="w-full flex flex-col gap-">
                                    <p>Birthday</p>
                                    <span class="bg-blue-200 w-full p-2 rounded">
                                        {{user?.birthDate}} 
                                    </span>
                                </div>
                                <div class="w-full flex flex-col gap-">
                                    <p>Role</p>
                                    <span class="bg-blue-200 w-full p-2 rounded">
                                        {{isAdmin ? 'Admin' : 'Member'}} 
                                    </span>
                                </div>
                        </div>
                    </div>

                    <div class="ml-auto">
                        <button v-if="isAdmin" @click="goToAdminPage" class="flex items-center gap-2 hover:bg-blue-400 text-white rounded p-2 bg-blue-500">
                            <svg data-slot="icon" class="w-6" fill="none" stroke-width="1.5" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M15 19.128a9.38 9.38 0 0 0 2.625.372 9.337 9.337 0 0 0 4.121-.952 4.125 4.125 0 0 0-7.533-2.493M15 19.128v-.003c0-1.113-.285-2.16-.786-3.07M15 19.128v.106A12.318 12.318 0 0 1 8.624 21c-2.331 0-4.512-.645-6.374-1.766l-.001-.109a6.375 6.375 0 0 1 11.964-3.07M12 6.375a3.375 3.375 0 1 1-6.75 0 3.375 3.375 0 0 1 6.75 0Zm8.25 2.25a2.625 2.625 0 1 1-5.25 0 2.625 2.625 0 0 1 5.25 0Z"></path>
                            </svg>
                            Manage users
                        </button>
                    </div>

            </div>


        </div>
    </div>
</template>

<script setup>
import NavBar from "@/components/NavBar.vue";
import { useAuth0 } from "@auth0/auth0-vue";
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useUserService } from "../services/userService";


var userService = useUserService()
const auth = useAuth0()
const router = useRouter()

const user = ref(undefined)
const isAdmin = computed(()=> userService.userIsAdmin(auth))

function goToAdminPage(){
    if(isAdmin.value)
        router.push({name:"Admin"})
}

onMounted( async ()=>{

        var uId = auth.user.value?.sub

        user.value =  await userService.ensureUser(uId)

})


</script>
