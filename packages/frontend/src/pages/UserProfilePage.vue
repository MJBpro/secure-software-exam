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
                        </div>
                    </div>
            </div>


        </div>
    </div>
</template>

<script setup>
import NavBar from "@/components/NavBar.vue";
import { useAuth0 } from "@auth0/auth0-vue";
import { onMounted, ref } from "vue";
import { useUserService } from "../services/userService";


var userService = useUserService()
const auth = useAuth0()

const user = ref(undefined)

onMounted( async ()=>{

        var uId = auth.user.value?.sub

        user.value =  await userService.ensureUser(uId)

})


</script>
